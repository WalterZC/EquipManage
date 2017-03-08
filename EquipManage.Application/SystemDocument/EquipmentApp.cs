using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using EquipManage.Repository.SystemDocument;
using System.Collections.Generic;
using System.Linq;

namespace EquipManage.Application.SystemDocument
{
    public class EquipmentApp
    {
        private IEquipmentRepository service = new EquipmentRepository();

        public List<EquipmentEntity> GetList(string typeId = "", string keyword = "")
        {
            var expression = ExtLinq.True<EquipmentEntity>();
            if (!string.IsNullOrEmpty(typeId))
            {
                expression = expression.And(t => t.FEquipmentTypeId == typeId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.FFullName.Contains(keyword));
                expression = expression.Or(t => t.FNumber.Contains(keyword));
            }
            return service.IQueryable(expression).OrderBy(t => t.FSortCode).ToList();
        }
        public List<EquipmentEntity> GetItemList(string enCode)
        {
            return service.GetEquipmentList(enCode);
        }
        public EquipmentEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.FId == keyValue);
        }
        public void SubmitForm(EquipmentEntity EquipmentEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                EquipmentEntity.Modify(keyValue);
                service.Update(EquipmentEntity);
            }
            else
            {
                EquipmentEntity.Create();
                service.Insert(EquipmentEntity);
            }
        }
    }
}
