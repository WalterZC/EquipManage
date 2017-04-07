using System.Collections.Generic;
using System.Linq;
using EquipManage.Domain.Entity.SystemManage;
using EquipManage.Domain.IRepository.SystemManage;
using EquipManage.Repository.SystemManage;
using EquipManage.Code;

namespace EquipManage.Application.SystemManage
{
    /// <summary>
    /// BillCodeRuleApp
    /// </summary>    
    public class BillCodeRuleApp
    {
        private IBillCodeRuleRepository service = new BillCodeRuleRepository();

        public List<BillCodeRuleEntity> GetList()
        {
            return service.IQueryable().ToList();
        }

        public BillCodeRuleEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(BillCodeRuleEntity entity)
        {
            service.Delete(entity);
        }

        public void SubmitForm(BillCodeRuleEntity entity, string keyValue)
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
        public BillCodeRuleEntity FindEntity(string FTableName,bool FROB)
        {
            var expression = ExtLinq.True<BillCodeRuleEntity>();

            expression = expression.And(t => t.FTableName.Equals(FTableName));

            if (!FROB)
            {
                expression = expression.Or(t => t.FROB.Equals(false));
            }
            else
            {
                expression = expression.Or(t => t.FROB.Equals(true));
            }
            BillCodeRuleEntity entity = new BillCodeRuleEntity();
            entity = service.FindEntity(expression);

            return entity;
        }

        public string GetBillNo(string FTableName, bool FROB)
        {
            string FBillNo;
            BillCodeRuleEntity entity = new BillCodeRuleEntity();
            entity = this.FindEntity(FTableName, FROB);
            FBillNo = entity.FPreLetter.ToString() + Ext.ToString(entity.FMaxInterId).ToString().PadLeft(entity.FLength, '0');
            if(!string.IsNullOrEmpty(FBillNo))
            { 
                entity.FMaxInterId += 1;
                this.SubmitForm(entity, entity.FId);
            }
            return FBillNo;
        }
            
    }
}
