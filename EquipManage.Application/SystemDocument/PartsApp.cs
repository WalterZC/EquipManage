using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using EquipManage.Repository.SystemDocument;
using System.Collections.Generic;
using System.Linq;

namespace EquipManage.Application.SystemDocument
{
    public class PartsApp
    {
        private IPartsRepository service = new PartsRepository();

        public List<PartsEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<PartsEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.FFullName.Contains(keyword));
                expression = expression.Or(t => t.FNumber.Contains(keyword));
            }
            return service.IQueryable(expression).OrderBy(t => t.FSortCode).ToList();
        }
        public PartsEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.FId == keyValue);
        }
        public void SubmitForm(PartsEntity partsEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                partsEntity.Modify(keyValue);
                service.Update(partsEntity);
            }
            else
            {
                partsEntity.Create();
                service.Insert(partsEntity);
            }
        }
    }
}
