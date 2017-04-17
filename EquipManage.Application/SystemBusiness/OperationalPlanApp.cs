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
            if (!(entity == null))
            {
                service.Delete(entity);
            }
        }

        public void SubmitForm(OperationalPlanEntity entity, string keyValue)
        {
            var Entity = this.GetForm(keyValue);
            if (!(Entity == null))
            {
                entity.Modify(keyValue);
                service.Update(entity);
            }
            else
            {
                entity.BillHeadCreate();
                entity.UnCheck();
                entity.UnCancel();
                service.Insert(entity);
            }
        }

    }
}
