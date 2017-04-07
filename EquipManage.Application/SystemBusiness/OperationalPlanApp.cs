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
    /// OperationalPlanApp
    /// </summary>    
    public class OperationalPlanApp
    {
        private IOperationalPlanRepository service = new OperationalPlanRepository();

        public List<OperationalPlanEntity> GetList()
        {
            return service.IQueryable().ToList();
        }

        public OperationalPlanEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(OperationalPlanEntity entity)
        {
            service.Delete(entity);
        }

        public void SubmitForm(OperationalPlanEntity entity, string keyValue)
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
