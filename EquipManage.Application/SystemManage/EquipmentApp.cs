using EquipManage.Code;
using EquipManage.Domain.Entity.SystemManage;
using EquipManage.Domain.IRepository.SystemManage;
using EquipManage.Repository.SystemManage;
using System.Collections.Generic;
using System.Linq;

namespace EquipManage.Application.SystemManage
{
    public class EquipmentApp
    {
        private IEquipmentRepository service = new EquipmentRepository();

        public List<EquipmentEntity> GetList(string keyword)
        {
            var expression = ExtLinq.True<EquipmentEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.FFullName.Contains(keyword));
                expression = expression.Or(t => t.FNumber.Contains(keyword));
            }
            return service.IQueryable(expression).OrderBy(t => t.FCreatorTime).ToList();

            //return service.IQueryable().OrderBy(t => t.FCreatorTime).ToList();
        }
        public EquipmentEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.FId == keyValue);
        }
        public void SubmitForm(EquipmentEntity equipmentEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                equipmentEntity.Modify(keyValue);
                service.Update(equipmentEntity);
            }
            else
            {
                equipmentEntity.Create();
                service.Insert(equipmentEntity);
            }
        }
    }
}
