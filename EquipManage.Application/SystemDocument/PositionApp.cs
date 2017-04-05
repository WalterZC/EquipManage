using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using EquipManage.Repository.SystemDocument;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EquipManage.Application.SystemDocument
{
    public class PositionApp
    {
        private IPositionRepository service = new PositionRepository();
        private OrganizeApp organizeApp = new OrganizeApp();

        public List<PositionEntity> GetList()
        {
            return service.IQueryable().OrderBy(t => t.FSortCode).ToList();
        }
        public List<PositionEntity> GetEntitys(string itemId, string keyword="")
        {
            var expression = ExtLinq.True<PositionEntity>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.FBelongOrgID == itemId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.FFullName.Contains(keyword));
                expression = expression.Or(t => t.FNumber.Contains(keyword));
            }
            return service.IQueryable(expression).OrderBy(t => t.FSortCode).ToList();
        }
        public List<PositionEntity> GetItemList(string itemId)
        {
            return service.GetItemList(itemId).OrderBy(t => t.FCreatorTime).ToList();
        }
        public List<PositionEntity> GetPermissionGridList(string OrgId = "", string keyword = "")
        {

            List<PositionEntity> datalist = new List<PositionEntity>();
            List<PositionEntity> positionlist = new List<PositionEntity>();
            List<OrganizeEntity> orgList = new List<OrganizeEntity>();
            orgList = organizeApp.GetPermissionGridList(OrgId);
            positionlist = this.GetItemList(OrgId);

            datalist = (from c in positionlist
                        join o in orgList on c.FBelongOrgID equals o.FId
                        select c).ToList();

            if (!string.IsNullOrEmpty(keyword))
            {
                datalist = datalist.Where(t => t.FFullName.Contains(keyword) || t.FNumber.Contains(keyword)).ToList();
            }
            return datalist;
        }


        public PositionEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            if (service.IQueryable().Count(t => t.FParentID.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                service.Delete(t => t.FId == keyValue);
            }
        }

        public void SubmitForm(PositionEntity positionEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                positionEntity.Modify(keyValue);
                service.Update(positionEntity);
            }
            else
            {
                positionEntity.Create();
                service.Insert(positionEntity);
            }
        }
    }
}
