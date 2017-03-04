using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using EquipManage.Repository.SystemDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipManage.Application.SystemDocument
{
    public class OperationClassMemberApp
    {
        private IOperationClassMemberRepository service = new OperationClassMemberRepository();

        public List<OperationClassMemberEntity> GetList(string itemId = "", string keyword = "")
        {
            var expression = ExtLinq.True<OperationClassMemberEntity>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.FOperationClassID == itemId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.FMemberID.Contains(keyword));
            }
            return service.IQueryable(expression).OrderBy(t => t.FSortCode).ToList();
        }
        public List<OperationClassMemberEntity> GetItemList(string FNumber)
        {
            return service.GetItemList(FNumber);
        }
        public OperationClassMemberEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.FId == keyValue);
        }
        public void SubmitForm(OperationClassMemberEntity OperationClassMemberEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                OperationClassMemberEntity.Modify(keyValue);
                service.Update(OperationClassMemberEntity);
            }
            else
            {
                OperationClassMemberEntity.Create();
                service.Insert(OperationClassMemberEntity);
            }
        }
    }
}
