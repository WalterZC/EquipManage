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
    /// OperationalPlanEquipEntryApp
    /// </summary>    
    public class OperationalPlanEquipEntryApp
    {
        private IOperationalPlanEquipEntryRepository service=new OperationalPlanEquipEntryRepository();

        public List<OperationalPlanEquipEntryEntity> GetList()
        {
            return service.IQueryable().ToList();
        }

        public OperationalPlanEquipEntryEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(OperationalPlanEquipEntryEntity entity)
        {
            service.Delete(entity);
        }

        public void SubmitForm(OperationalPlanEquipEntryEntity entity, string keyValue)
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
