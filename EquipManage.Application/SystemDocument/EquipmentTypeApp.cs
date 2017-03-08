using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using EquipManage.Repository.SystemDocument;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EquipManage.Application.SystemDocument
{
    public class EquipmentTypeApp
    {
        private IEquipmentTypeRepository service = new EquipmentTypeRepository();

        public List<EquipmentTypeEntity> GetList()
        {
            return service.IQueryable().ToList();
        }
        public EquipmentTypeEntity GetForm(string keyValue)
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
        public void SubmitForm(EquipmentTypeEntity equipmentTypeEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                equipmentTypeEntity.Modify(keyValue);
                service.Update(equipmentTypeEntity);
            }
            else
            {
                equipmentTypeEntity.Create();
                service.Insert(equipmentTypeEntity);
            }
        }
    }
}
