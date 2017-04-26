
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipManage.Domain.Entity.SystemBusiness;
using EquipManage.Domain.IRepository.SystemBusiness;
using EquipManage.Repository.SystemBusiness;
namespace EquipManage.Application.SystemBusiness
{

    /// <summary>
    /// OperationalTaskEquipEntryApp
    /// </summary>    
    public class OperationalTaskEquipEntryApp
    {
        private IOperationalTaskEquipEntryRepository service = new OperationalTaskEquipEntryRepository();

        public List<OperationalTaskEquipEntryEntity> GetList()
        {
            return service.IQueryable().ToList();
        }

        public OperationalTaskEquipEntryEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(OperationalTaskEquipEntryEntity entity)
        {
            service.Delete(entity);
        }

        public void SubmitForm(OperationalTaskEquipEntryEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                service.Update(entity);
            }
            else
            {
                entity.Create();
                service.Insert(entity);
            }
        }

    }

}
