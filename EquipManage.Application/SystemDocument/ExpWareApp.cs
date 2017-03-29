using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using EquipManage.Repository.SystemDocument;
using System.Collections.Generic;
using System.Linq;

namespace EquipManage.Application.SystemDocument
{
    public class ExpWareApp
    {
        private IExpWareRepository service = new ExpWareRepository();

        public List<ExpWareEntity> GetList()
        {
            return service.IQueryable().ToList();
        }
        public ExpWareEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.FId == keyValue);
        }
        public void SubmitForm(ExpWareEntity expWareEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                expWareEntity.Modify(keyValue);
                service.Update(expWareEntity);
            }
            else
            {
                expWareEntity.Create();
                service.Insert(expWareEntity);
            }
        }
    }
}
