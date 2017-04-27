/*******************************************************************************
 * Copyright © 2017 EquipManage.Framework 版权所有
 * Author: 王成元
 * Description: 设备管理系统-匠盟科技
 * Date:2017-02-17
*********************************************************************************/
using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using EquipManage.Repository.SystemDocument;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EquipManage.Application.SystemDocument
{
    public class OrganizeApp
    {
        private IOrganizeRepository service = new OrganizeRepository();
        private ItemRightApp itemRightApp = new ItemRightApp();

        /// <summary>
        /// 获取所有部门
        /// </summary>
        /// <returns></returns>
        public List<OrganizeEntity> GetList()
        {
            return service.IQueryable().OrderBy(t => t.FCreatorTime).ToList();
        }
        /// <summary>
        /// 获取当前选中部门及其子部门
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<OrganizeEntity> GetSelectEntitys(string itemId, string keyword)
        {
            return service.GetItemList(itemId).Where(t => t.FEnCode.Contains(keyword) || t.FShortName.Contains(keyword)).ToList();
        }
        /// <summary>
        /// 获取当前用户拥有权限的所有部门
        /// </summary>
        /// <returns></returns>
        public List<OrganizeEntity> GetPermissionGridList(string ItemId = "")
        {
            List<OrganizeEntity> organizeList = new List<OrganizeEntity>();

            List<OrganizeEntity> itemList = new List<OrganizeEntity>();

            if (!string.IsNullOrEmpty(ItemId))
            {
                itemList = this.GetSelectEntitys(ItemId, "");
            }
            else
            {
                itemList = this.GetList();
            }

            var Rightdata = itemRightApp.GetList(OperatorProvider.Provider.GetCurrent().UserId, "Organize");

            if (OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                organizeList = itemList;
            }
            else
            {
                organizeList = (from c in itemList
                                join o in Rightdata on c.FId equals o.FObjectId
                                select c).ToList();
            }
            return organizeList;
        }

        /// <summary>
        /// 获取有作业权限的部门列表
        /// </summary>
        /// <returns></returns>
        public List<OrganizeEntity> GetOperationList()
        {
            List<OrganizeEntity> organizeList = new List<OrganizeEntity>();
            if (OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                organizeList = this.GetList().Where(t => (t.FIsOperation.Equals(true))).ToList();
            }
            else
            {
                organizeList = this.GetSelectEntitys(OperatorProvider.Provider.GetCurrent().CompanyId, "").Where(t => (t.FIsOperation.Equals(true))).ToList();
            }
            return organizeList;
        }

        public OrganizeEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            if (service.IQueryable().Count(t => t.FParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                service.Delete(t => t.FId == keyValue);
            }
        }
        public void SubmitForm(OrganizeEntity organizeEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                List<OrganizeEntity> SelectEntitys = this.GetSelectEntitys(keyValue, "");
                organizeEntity.FIsLeaf = SelectEntitys.Count > 1 ? false : true;

                organizeEntity.Modify(keyValue);
                service.Update(organizeEntity);

                if (organizeEntity.FIsOperation)
                {
                    if (SelectEntitys.Count > 0)
                    {
                        for (int i = 0; i < SelectEntitys.Count; i++)
                        {
                            if (SelectEntitys[i].FId != keyValue)
                            {
                                OrganizeEntity Entity = new OrganizeEntity();
                                Entity = GetForm(SelectEntitys[i].FId);

                                Entity.FIsOperation = organizeEntity.FIsOperation;

                                Entity.Modify(Entity.FId);
                                service.Update(Entity);
                            }
                        }
                    }
                }
            }
            else
            {
                organizeEntity.Create();
                service.Insert(organizeEntity);
            }
        }
    }
}
