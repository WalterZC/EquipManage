using EquipManage.Application.SystemDocument;
using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EquipManage.Web.Areas.SystemDocument.Controllers
{
    public class UserItemAuthorizeController : ControllerBase
    {
        private ItemRightApp itemRightApp = new ItemRightApp();
        private OrganizeApp OrganizeApp = new OrganizeApp();

        public ActionResult GetUseItemPermissionTree(string FUserID, string FObjectType)
        {
            var Organizedata = OrganizeApp.GetList();
            var Rightdata = itemRightApp.GetList(FUserID, FObjectType);
            List<ItemRightEntity> itemRightdata = new List<ItemRightEntity>();

            var treeList = new List<TreeViewModel>();
            foreach (OrganizeEntity item in Organizedata)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = Organizedata.Count(t => t.FParentId == item.FId) == 0 ? false : true;
                tree.id = item.FId;
                tree.text = item.FFullName;
                tree.value = item.FEnCode;
                tree.parentId = item.FParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = Rightdata.Count(t => t.FObjectId == item.FId);
                tree.hasChildren = hasChildren;
                //tree.img = item.FIcon == "" ? "" : item.FIcon;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
    }
}
