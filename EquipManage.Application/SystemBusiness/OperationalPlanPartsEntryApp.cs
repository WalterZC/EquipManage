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

        public List<OperationalPlanPartsEntryEntity> GetList(string FId)
        {
            var expression = ExtLinq.True<OperationalPlanPartsEntryEntity>();
            expression = expression.And(t => t.FItemId == FId);
            return service.IQueryable(expression).ToList();
        }

        public OperationalPlanPartsEntryEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(string FId)
        {
            var expression = ExtLinq.True<OperationalPlanPartsEntryEntity>();
            expression = expression.And(t => t.FItemId == FId);
            service.Delete(expression);
        }

        public void SubmitForm(OperationalPlanEntity headEntity,List<OperationalPlanPartsEntryEntity> entitylist)
        {
            if((!(entitylist==null)) && entitylist.Count>0)
            { 
                foreach (OperationalPlanPartsEntryEntity Entity in entitylist)
                {
                    Entity.FItemId = headEntity.FId;
                    Entity.Create();
                }

                service.Insert(entitylist);
            }
        }

    }
}
