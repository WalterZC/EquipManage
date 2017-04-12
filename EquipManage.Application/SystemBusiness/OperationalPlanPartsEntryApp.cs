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

        public void Delete(OperationalPlanEntity entity)
        {
            var expression = ExtLinq.True<OperationalPlanPartsEntryEntity>();
            expression = expression.And(t => t.FItemId.Equals(entity.FId));
            service.Delete(expression);
        }

        public void SubmitForm(List<OperationalPlanPartsEntryEntity> entitylist)
        {
            foreach (OperationalPlanPartsEntryEntity Entity in entitylist)
            {
                Entity.Create();
            }

            service.Insert(entitylist);
        }

    }
}
