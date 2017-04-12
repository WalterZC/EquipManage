using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipManage.Domain.Entity.SystemBusiness;
using EquipManage.Domain.IRepository.SystemBusiness;
using EquipManage.Repository.SystemBusiness;
using EquipManage.Code;

namespace EquipManage.Application.SystemBusiness
{
    /// <summary>
    /// OperationalPlanEquipEntryApp
    /// </summary>    
    public class OperationalPlanEquipEntryApp
    {
        private IOperationalPlanEquipEntryRepository service = new OperationalPlanEquipEntryRepository();

        public List<OperationalPlanEquipEntryEntity> GetList()
        {
            return service.IQueryable().ToList();
        }

        public OperationalPlanEquipEntryEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(OperationalPlanEntity entity)
        {
            var expression = ExtLinq.True<OperationalPlanEquipEntryEntity>();
            expression = expression.And(t => t.FItemId.Equals(entity.FId));
            service.Delete(expression);
        }

        public void SubmitForm(List<OperationalPlanEquipEntryEntity> entitylist)
        {
            foreach (OperationalPlanEquipEntryEntity Entity in entitylist)
            {
                Entity.Create();
            }

            service.Insert(entitylist);
        }

    }
}
