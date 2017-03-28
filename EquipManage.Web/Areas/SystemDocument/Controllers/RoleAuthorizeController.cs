/*******************************************************************************
 * Copyright © 2017 EquipManage.Framework 版权所有
 * Author: 王成元
 * Description: 设备管理系统-匠盟科技
 * Date:2017-02-17
*********************************************************************************/
using EquipManage.Application.SystemDocument;
using EquipManage.Application.SystemManage;
using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EquipManage.Web.Areas.SystemDocument.Controllers
{
    public class RoleAuthorizeController : ControllerBase
    {
        private RoleAuthorizeApp roleAuthorizeApp = new RoleAuthorizeApp();
        private ModuleApp moduleApp = new ModuleApp();
        private ModuleButtonApp moduleButtonApp = new ModuleButtonApp();

        public ActionResult GetPermissionTree(string roleId)
        {
            string[] FRoleArray = roleId.Split(',');
            var moduledata = moduleApp.GetList();
            var buttondata = moduleButtonApp.GetList();
            List<RoleAuthorizeEntity> authorizedata = new List<RoleAuthorizeEntity>();
            if (FRoleArray.Length > 0)
            {
                foreach (string FRoleId in FRoleArray)
                {
                    if (!string.IsNullOrEmpty(FRoleId))
                    {
                        authorizedata.AddRange(roleAuthorizeApp.GetList(FRoleId));
                    }
                }
            }
            var treeList = new List<TreeViewModel>();
            foreach (ModuleEntity item in moduledata)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = moduledata.Count(t => t.FParentId == item.FId) == 0 ? false : true;
                tree.id = item.FId;
                tree.text = item.FFullName;
                tree.value = item.FEnCode;
                tree.parentId = item.FParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = authorizedata.Count(t => t.FItemId == item.FId);
                tree.hasChildren = true;
                tree.img = item.FIcon == "" ? "" : item.FIcon;
                treeList.Add(tree);
            }
            foreach (ModuleButtonEntity item in buttondata)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = buttondata.Count(t => t.FParentId == item.FId) == 0 ? false : true;
                tree.id = item.FId;
                tree.text = item.FFullName;
                tree.value = item.FEnCode;
                tree.parentId = item.FParentId == "0" ? item.FModuleId : item.FParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = authorizedata.Count(t => t.FItemId == item.FId);
                tree.hasChildren = hasChildren;
                tree.img = item.FIcon == "" ? "" : item.FIcon;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
    }
}
