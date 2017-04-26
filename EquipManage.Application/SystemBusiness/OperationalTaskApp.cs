
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
    /// OperationalTaskApp
    /// </summary>    
    public class OperationalTaskApp
    {
        private IOperationalTaskRepository service = new OperationalTaskRepository();

        public List<OperationalTaskEntity> GetList()
        {
            return service.IQueryable().ToList();
        }

        public OperationalTaskEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(OperationalTaskEntity entity)
        {
            service.Delete(entity);
        }

        public void SubmitForm(OperationalTaskEntity entity, string keyValue)
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
