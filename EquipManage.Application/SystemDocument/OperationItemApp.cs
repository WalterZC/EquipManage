using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using EquipManage.Repository.SystemDocument;
using System.Collections.Generic;
using System.Linq;

namespace EquipManage.Application.SystemDocument
{
    public class OperationItemApp
    {
        private IOperationItemRepository service = new OperationItemRepository();
        public List<OperationItemEntity> GetList(string itemId = "", string keyword = "")
        {
            var expression = ExtLinq.True<OperationItemEntity>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.FItemId == itemId);
        }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.FFullName.Contains(keyword));
                expression = expression.Or(t => t.FNumber.Contains(keyword));
            }
            return service.IQueryable(expression).OrderBy(t => t.FSortCode).ToList();
        }
        public List<OperationItemEntity> GetItemList(string enCode)
        {
            return service.GetItemList(enCode);
        }
        public OperationItemEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.FId == keyValue);
        }
        public void SubmitForm(OperationItemEntity OperationItemEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                OperationItemEntity.Modify(keyValue);
                service.Update(OperationItemEntity);
            }
            else
            {
                OperationItemEntity.Create();
                service.Insert(OperationItemEntity);
            }
        }
        public void SubmitCloneProjectItem(string FOperationProjectId, string FIds)
        {
            string[] ArrayId = FIds.Split(',');
            var data = this.GetList();
            List<OperationItemEntity> entitys = new List<OperationItemEntity>();
            foreach (string item in ArrayId)
            {
                OperationItemEntity moduleButtonEntity = data.Find(t => t.FId == item);
                moduleButtonEntity.FId = Common.GuId();
                moduleButtonEntity.FItemId = FOperationProjectId;
                entitys.Add(moduleButtonEntity);
            }
            service.SubmitCloneProjectItem(entitys);
        }
    }
}
