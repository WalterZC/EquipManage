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
        private IOrganizeRepository organizeservice = new OrganizeRepository();

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
