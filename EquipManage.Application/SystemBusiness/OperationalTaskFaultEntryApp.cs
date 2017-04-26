
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
    /// OperationalTaskFaultEntryApp
    /// </summary>    
    public class OperationalTaskFaultEntryApp
    {
        private IOperationalTaskFaultEntryRepository service = new OperationalTaskFaultEntryRepository();

        public List<OperationalTaskFaultEntryEntity> GetList()
        {
            return service.IQueryable().ToList();
        }

        public OperationalTaskFaultEntryEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(OperationalTaskFaultEntryEntity entity)
        {
            service.Delete(entity);
        }

        public void SubmitForm(OperationalTaskFaultEntryEntity entity, string keyValue)
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
