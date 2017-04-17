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
    /// OperationalPlanApp
    /// </summary>    
    public class OperationalPlanApp
    {
        private IOperationalPlanRepository service = new OperationalPlanRepository();

        //public List<OperationalPlanEntity> GetList()
        //{
        //    return service.IQueryable().ToList();
        //}

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

        public List<OperationalPlanEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<OperationalPlanEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.FNumber.Contains(keyword));
                expression = expression.Or(t => t.FName.Contains(keyword));
            }
            //expression = expression.And(t => t.FAccount != "admin");
            return service.FindList(expression, pagination);
        }

    }
}
