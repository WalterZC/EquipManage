/*******************************************************************************
 * Copyright © 2017 EquipManage.Framework 版权所有
 * Author: 王成元
 * Description: 设备管理系统-匠盟科技
 * Date:2017-03-02
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
    public class OperationClassApp
    {
        private IOperationClassRepository service = new OperationClassRepository();
        private IOperationClassMemberRepository detailService = new OperationClassMemberRepository();
        private OrganizeApp organizeApp = new OrganizeApp();

        public List<OperationClassEntity> GetList()
        {
            return service.IQueryable().OrderBy(t => t.FCreatorTime).ToList();
        }
        //public List<OperationClassEntity> GetSelectEntitys(string itemId, string keyword)
        //{
        //    return service.GetItemList(itemId).Where(t => t.FEnCode.Contains(keyword) || t.FShortName.Contains(keyword)).ToList();
        //}
        public List<OperationClassEntity> GetPermissionGridList(string itemId,string keyword)
        {
            List<OperationClassEntity> datalist = new List<OperationClassEntity>();
            List<OperationClassEntity> classlist = new List<OperationClassEntity>();
            List<OrganizeEntity> orgList = new List<OrganizeEntity>();
            orgList = organizeApp.GetPermissionGridList(itemId);
            classlist = string.IsNullOrEmpty(itemId) == true ? this.GetList() : service.GetItemList(itemId);

            datalist = (from c in classlist
                        join o in orgList on c.FBelongOrgID equals o.FId
                        select c).ToList();

            if (!string.IsNullOrEmpty(keyword))
            {
                datalist = datalist.Where(t => t.FShortName.Contains(keyword) || t.FNumber.Contains(keyword)).OrderBy(t => t.FCreatorTime).ToList();
            }
            return datalist.OrderBy(t => t.FCreatorTime).ToList();
            //if (string.IsNullOrEmpty(keyword))
            //{
            //    return service.GetItemList(itemId).OrderBy(t => t.FCreatorTime).ToList();
            //}
            //else
            //{
            //    return service.GetItemList(itemId).Where(t => t.FNumber.Contains(keyword) || t.FShortName.Contains(keyword)).OrderBy(t => t.FCreatorTime).ToList();

            //}
        }
        public OperationClassEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void SubmitForm(OperationClassEntity operationClassEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                operationClassEntity.Modify(keyValue);
                service.Update(operationClassEntity);
            }
            else
            {
                operationClassEntity.Create();
                service.Insert(operationClassEntity);
            }
        }
        public void DeleteForm(string keyValue)
        {
            if (detailService.IQueryable().Count(t => t.FOperationClassID.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了班组成员。");
            }
            else
            {
                service.Delete(t => t.FId == keyValue);
            }
        }
        public void UpdateForm(OperationClassEntity operationClassEntity)
        {
            service.Update(operationClassEntity);
        }
    }
}
