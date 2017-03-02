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

        public List<PositionEntity> GetList(string keyword)
        {
            var expression = ExtLinq.True<PositionEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.FOrganizeId.Contains(keyword));
            }
            return service.IQueryable(expression).OrderBy(t => t.FSortCode).ToList();
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
