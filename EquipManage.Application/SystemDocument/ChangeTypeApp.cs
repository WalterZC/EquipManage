using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using EquipManage.Repository.SystemDocument;
using System.Collections.Generic;
using System.Linq;

namespace EquipManage.Application.SystemDocument
{
    public class ChangeTypeApp
    {
        private IChangeTypeRepository service = new ChangeTypeRepository();

        public List<ChangeTypeEntity> GetList()
        {
            return service.IQueryable().OrderBy(t => t.FSortCode).ToList();
        }
        public ChangeTypeEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.FId == keyValue);
        }

        public void SubmitForm(ChangeTypeEntity changeTypeEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                changeTypeEntity.Modify(keyValue);
                service.Update(changeTypeEntity);
            }
            else
            {
                changeTypeEntity.Create();
                service.Insert(changeTypeEntity);
            }
        }
    }
}
