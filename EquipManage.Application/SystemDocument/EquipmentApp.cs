using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using EquipManage.Repository.SystemDocument;
using System.Collections.Generic;
using System.Linq;

namespace EquipManage.Application.SystemDocument
{
    public class EquipmentApp
    {
        private IEquipmentRepository service = new EquipmentRepository();

        public List<EquipmentEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<EquipmentEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.FFullName.Contains(keyword));
                expression = expression.Or(t => t.FNumber.Contains(keyword));
            }
            return service.IQueryable().OrderBy(t => t.FSortCode).ToList();
        }
        public List<EquipmentEntity> GetPermissionGridList(string FObjectType= "Organize", string itemId = "", string keyword = "")
        {
            List<EquipmentEntity> datalist = new List<EquipmentEntity>();
            if (OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                if (FObjectType == "Organize")
                {
                    datalist = service.GetSelectOrgItemList(itemId);
                }
                else if (FObjectType == "EquipmentType")
                {
                    datalist = service.GetSelectTypeItemList(itemId);
                }
                
            }
            else
            {
                if (FObjectType == "Organize")
                {
                    datalist = this.GetPermissionOrgItemList(OperatorProvider.Provider.GetCurrent().DepartmentId, OperatorProvider.Provider.GetCurrent().UserId);
                }
                else if (FObjectType == "EquipmentType")
                {
                    datalist = this.GetPermissionTypeItemList(itemId, OperatorProvider.Provider.GetCurrent().UserId);
                }
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                datalist = datalist.Where(t => t.FShortName.Contains(keyword) || t.FNumber.Contains(keyword)).ToList();
            }
            return datalist;
        }
        /// <summary>
        /// 获取所有该用户有权限部门内的设备列表
        /// </summary>
        /// <param name="OrgId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<EquipmentEntity> GetPermissionOrgItemList(string OrgId,string UserId)
        {
            return service.GetPermissionOrgItemList(OrgId, UserId);
        }
        /// <summary>
        /// 获取所有该用户有权限部门内的设备列表
        /// </summary>
        /// <param name="TypeId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<EquipmentEntity> GetPermissionTypeItemList(string TypeId, string UserId)
        {
            return service.GetPermissionTypeItemList(TypeId, UserId);
        }
        public EquipmentEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.FId == keyValue);
        }
        public void SubmitForm(EquipmentEntity EquipmentEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                EquipmentEntity.Modify(keyValue);
                service.Update(EquipmentEntity);
            }
            else
            {
                EquipmentEntity.Create();
                service.Insert(EquipmentEntity);
            }
        }
    }
}
