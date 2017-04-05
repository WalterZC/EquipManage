/*******************************************************************************
 * Copyright © 2017 EquipManage.Framework 版权所有
 * Author: 王成元
 * Description: 设备管理系统-匠盟科技
 * Date:2017-02-17
*********************************************************************************/
using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using EquipManage.Repository.SystemDocument;
using System.Collections.Generic;
using System.Linq;

namespace EquipManage.Application.SystemDocument
{
    public class DutyApp
    {
        private IRoleRepository service = new RoleRepository();
        private OrganizeApp organizeApp = new OrganizeApp();

        public List<RoleEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<RoleEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.FFullName.Contains(keyword));
                expression = expression.Or(t => t.FEnCode.Contains(keyword));
            }
            expression = expression.And(t => t.FCategory == 2);
            return service.IQueryable(expression).OrderBy(t => t.FSortCode).ToList();
        }

        public List<RoleEntity> GetPermissionGridList(string OrgId = "", string keyword = "")
        {

            List<RoleEntity> datalist = new List<RoleEntity>();
            List<RoleEntity> rolelist = new List<RoleEntity>();
            List<OrganizeEntity> orgList = new List<OrganizeEntity>();
            orgList = organizeApp.GetPermissionGridList(OrgId);
            rolelist = this.GetList().Where(t => t.FCategory == 2).ToList();

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
            service.Delete(t => t.FId == keyValue);
        }
        public void SubmitForm(RoleEntity roleEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                roleEntity.Modify(keyValue);
                service.Update(roleEntity);
            }
            else
            {
                roleEntity.Create();
                roleEntity.FCategory = 2;
                service.Insert(roleEntity);
            }
        }
    }
}
