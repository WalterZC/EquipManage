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
        private OrganizeApp organizeApp = new OrganizeApp();
        private EquipmentTypeApp equipmentTypeApp = new EquipmentTypeApp();

        public ActionResult GetUseItemPermissionSelectTree(string FUserId,string FObjectType)
        {
            var itemList = organizeApp.GetList();
            var Rightdata = itemRightApp.GetList(string.IsNullOrEmpty(FUserId) == true? OperatorProvider.Provider.GetCurrent().UserId: FUserId, FObjectType);
            List<ItemRightEntity> itemRightdata = new List<ItemRightEntity>();

            var treeList = new List<TreeViewModel>();
            foreach (OrganizeEntity item in itemList)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = itemList.Count(t => t.FParentId == item.FId) == 0 ? false : true;
                tree.id = item.FId;
                tree.text = item.FShortName;
                tree.value = item.FEnCode;
                tree.parentId = item.FParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = Rightdata.Count(t => t.FObjectId == item.FId);
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }

        public ActionResult GetUseItemPermissionTree(string FUserID="", string FObjectType="")
        {
            if (string.IsNullOrEmpty(FUserID))
            {
                FUserID = OperatorProvider.Provider.GetCurrent().UserId;
            }

            var treeList = new List<TreeViewModel>();
            var Rightdata = itemRightApp.GetList(FUserID, FObjectType);

            switch (FObjectType)
            {
                default:
                    var organizeList = organizeApp.GetPermissionGridList();
                    var itemList = organizeApp.GetList();

                    foreach (var item in organizeList)
                    {
                        TreeViewModel tree = new TreeViewModel();
                        bool hasChildren = itemList.Count(t => t.FParentId == item.FId) == 0 ? false : true;
                        tree.id = item.FId;
                        tree.text = item.FShortName;
                        tree.value = item.FEnCode;
                        tree.parentId = item.FParentId;
                        tree.isexpand = true;
                        tree.complete = true;
                        tree.hasChildren = hasChildren;
                        treeList.Add(tree);
                    }
                    break;
                case "EquipmentType":
                    List<EquipmentTypeEntity> equipmentTypeList = new List<EquipmentTypeEntity>();
                    equipmentTypeList = equipmentTypeApp.GetList();
                    foreach (var item in equipmentTypeList)
                    {
                        TreeViewModel tree = new TreeViewModel();
                        bool hasChildren = equipmentTypeList.Count(t => t.FParentId == item.FId) == 0 ? false : true;
                        tree.id = item.FId;
                        tree.text = item.FShortName;
                        tree.value = item.FNumber;
                        tree.parentId = item.FParentId;
                        tree.isexpand = true;
                        tree.complete = true;
                        tree.hasChildren = hasChildren;
                        treeList.Add(tree);
                    }
                    break;
            }

            return Content(treeList.TreeViewJson());
        }
    }
}
