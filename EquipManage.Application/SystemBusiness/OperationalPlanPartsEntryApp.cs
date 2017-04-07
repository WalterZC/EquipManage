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
    /// OperationalPlanPartsEntryApp
    /// </summary>    
    public class OperationalPlanPartsEntryApp
    {
        private IOperationalPlanPartsEntryRepository service = new OperationalPlanPartsEntryRepository();

        public List<OperationalPlanPartsEntryEntity> GetList()
        {
            return service.IQueryable().ToList();
        }

        public OperationalPlanPartsEntryEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(OperationalPlanPartsEntryEntity entity)
        {
            service.Delete(entity);
        }

        public void SubmitForm(OperationalPlanPartsEntryEntity entity, string keyValue)
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
