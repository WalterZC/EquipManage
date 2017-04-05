/*******************************************************************************
 * Copyright © 2017 EquipManage.Framework 版权所有
 * Author: 王成元
 * Description: 设备管理系统-匠盟科技
 * Date:2017-02-17
*********************************************************************************/
using EquipManage.Application.SystemManage;
using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using EquipManage.Repository.SystemDocument;
using System.Collections.Generic;
using System.Linq;

namespace EquipManage.Application.SystemDocument
{
    public class RoleApp
    {
        private IRoleRepository service = new RoleRepository();
        private ModuleApp moduleApp = new ModuleApp();
        private ModuleButtonApp moduleButtonApp = new ModuleButtonApp();
        private OrganizeApp organizeApp = new OrganizeApp();

        public List<RoleEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<RoleEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.FFullName.Contains(keyword));
                expression = expression.Or(t => t.FEnCode.Contains(keyword));
            }
            expression = expression.And(t => t.FCategory == 1);
            return service.IQueryable(expression).OrderBy(t => t.FSortCode).ToList();
        }

        public List<RoleEntity> GetPermissionGridList(string OrgId="",string keyword="")
        {

            List<RoleEntity> datalist = new List<RoleEntity>();
            List<RoleEntity> rolelist = new List<RoleEntity>();
            List<OrganizeEntity> orgList = new List<OrganizeEntity>();
            orgList = organizeApp.GetPermissionGridList(OrgId);
            rolelist = this.GetList().Where(t=>t.FCategory==1).ToList();

            datalist = (from c in rolelist
                        join o in orgList on c.FOrganizeId equals o.FId
                            select c).ToList();

            if (!string.IsNullOrEmpty(keyword))
            {
                datalist = datalist.Where(t => t.FFullName.Contains(keyword) || t.FEnCode.Contains(keyword)).ToList();
            }
            return datalist;
        }

        public RoleEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }
        public void SubmitForm(RoleEntity roleEntity, string[] permissionIds, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                roleEntity.FId = keyValue;
            }
            else
            {
                roleEntity.FId = Common.GuId();
            }
            var moduledata = moduleApp.GetList();
            var buttondata = moduleButtonApp.GetList();
            List<RoleAuthorizeEntity> roleAuthorizeEntitys = new List<RoleAuthorizeEntity>();
            foreach (var itemId in permissionIds)
            {
                RoleAuthorizeEntity roleAuthorizeEntity = new RoleAuthorizeEntity();
                roleAuthorizeEntity.FId = Common.GuId();
                roleAuthorizeEntity.FObjectType = 1;
                roleAuthorizeEntity.FObjectId = roleEntity.FId;
                roleAuthorizeEntity.FItemId = itemId;
                if (moduledata.Find(t => t.FId == itemId) != null)
                {
                    roleAuthorizeEntity.FItemType = 1;
                }
                if (buttondata.Find(t => t.FId == itemId) != null)
                {
                    roleAuthorizeEntity.FItemType = 2;
                }
                roleAuthorizeEntitys.Add(roleAuthorizeEntity);
            }
            service.SubmitForm(roleEntity, roleAuthorizeEntitys, keyValue);
        }
    }
}
