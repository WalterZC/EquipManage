using EquipManage.Domain.Entity.SystemBusiness;
using EquipManage.Domain.IRepository.SystemBusiness;
using EquipManage.Repository.SystemBusiness;
using System.Collections.Generic;
using System.Linq;
namespace EquipManage.Application.SystemBusiness
{

    /// <summary>
    /// OperationalTaskPartsEntryApp
    /// </summary>    
    public class OperationalTaskPartsEntryApp
    {
        private IOperationalTaskPartsEntryRepository service = new OperationalTaskPartsEntryRepository();

        public List<OperationalTaskPartsEntryEntity> GetList()
        {
            return service.IQueryable().ToList();
        }

        public OperationalTaskPartsEntryEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(OperationalTaskPartsEntryEntity entity)
        {
            service.Delete(entity);
        }

        public void SubmitForm(OperationalTaskPartsEntryEntity entity, string keyValue)
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
