using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using EquipManage.Repository.SystemDocument;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EquipManage.Application.SystemDocument
{
    public class OperationProjectApp
    {
        private IOperationProjectRepository service = new OperationProjectRepository();
        private OrganizeApp organizeApp = new OrganizeApp();

        public List<OperationProjectEntity> GetList(string itemId)
        {
            var expression = ExtLinq.True<OperationProjectEntity>();
            expression = expression.And(t => t.FEquipmentTypeId.Equals(itemId));
            List<OperationProjectEntity> datalist = new List<OperationProjectEntity>();
            List<OperationProjectEntity> projectlist = new List<OperationProjectEntity>();
            List<OrganizeEntity> orgList = new List<OrganizeEntity>();
            orgList = organizeApp.GetPermissionGridList();
            projectlist = service.IQueryable(expression).ToList();

            datalist = (from c in projectlist
                        join o in orgList on c.FOrganizeId equals o.FId
                        select c).ToList();

            return datalist;
        }
        public OperationProjectEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public List<OperationProjectEntity> GetConditionsEntitys(string FOperationTypeId, string FOperationLevelId, string FObjectTypeId, string FObjectId)
        {
            List<OperationProjectEntity> list = new List<OperationProjectEntity>();
            var expression = ExtLinq.True<OperationProjectEntity>();

            if (("Equipment").Equals(FObjectTypeId))
            {
                list = this.GetEntitysByEquipmentID(FObjectId);
            }
            else if (("EquipmentType").Equals(FObjectTypeId))
            {
                list = this.GetList(FObjectId);
            }

            return list.Where(t=>t.FOperationTypeId.Equals(FOperationTypeId)).Where(t => t.FOperationLevelId.Equals(FOperationLevelId)).OrderBy(t => t.FCreatorTime).ToList();
        }
        public List<OperationProjectEntity> GetEntitysByEquipmentID(string keyword)
        {
            var expression = ExtLinq.True<OperationProjectEntity>();
            expression = expression.And(t => t.FItemIds.Contains(keyword));

            return service.IQueryable(expression).OrderBy(t => t.FCreatorTime).ToList();
        }
        public void DeleteForm(string keyValue)
        {
            if (service.IQueryable().Count(t => t.FItemIds.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                service.Delete(t => t.FId == keyValue);
            }
        }
        public string SubmitForm(OperationProjectEntity operationProjectEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                operationProjectEntity.Modify(keyValue);
                service.Update(operationProjectEntity);
            }
            else
            {
                operationProjectEntity.Create();
                service.Insert(operationProjectEntity);
            }
            return operationProjectEntity.FId;
        }
    }
}
