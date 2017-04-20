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

        public List<OperationalPlanEquipEntryEntity> GetList(string FId)
        {
            var expression = ExtLinq.True<OperationalPlanEquipEntryEntity>();
            expression = expression.And(t => t.FItemId == FId);
            return service.IQueryable(expression).OrderBy(t => t.FEntryId).ToList();
        }

        public OperationalPlanEquipEntryEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(string FId)
        {
            var expression = ExtLinq.True<OperationalPlanEquipEntryEntity>();
            expression = expression.And(t => t.FItemId==FId);
            service.Delete(expression);
        }

        public void SubmitForm(OperationalPlanEntity headEntity,List<OperationalPlanEquipEntryEntity> entitylist)
        {
            if (entitylist.Count > 0)
            { 
                foreach (OperationalPlanEquipEntryEntity Entity in entitylist)
                {
                    Entity.FItemId = headEntity.FId;
                    Entity.Create();
                }

                service.Insert(entitylist);
            }
        }

    }
}
