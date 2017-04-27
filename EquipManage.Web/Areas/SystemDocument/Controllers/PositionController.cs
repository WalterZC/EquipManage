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
    public class PositionController : ControllerBase
    {
        private PositionApp positionApp = new PositionApp();
        private OrganizeApp organizeApp = new OrganizeApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson(string FOrganize)
        {
            var data = positionApp.GetItemList(FOrganize);
            var treeList = new List<TreeSelectModel>();
            foreach (PositionEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.FId;
                treeModel.text = item.FShortName;
                treeModel.parentId = data.Count(t => t.FId == item.FParentID) == 0 ? "0" : item.FParentID;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeJson(string FOrganize)
        {
            var data = positionApp.GetEntitys(FOrganize);
            var treeList = new List<TreeViewModel>();
            foreach (PositionEntity item in data)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = data.Count(t => t.FParentID == item.FId) == 0 ? false : true;
                tree.id = item.FId;
                tree.text = item.FShortName;
                tree.value = item.FNumber;
                tree.parentId = item.FParentID;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }

        public ActionResult GetTreeGridJson(string itemId, string keyword)
        {
            List<PositionEntity> data = new List<PositionEntity>();
            if (!string.IsNullOrEmpty(itemId))
            {
                data = positionApp.GetPermissionGridList(itemId);
            }
            
            var treeList = new List<TreeGridModel>();
            foreach (PositionEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.FParentID == item.FId) == 0 ? false : true;
                treeModel.id = item.FId;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = data.Count(t => t.FId == item.FParentID) == 0 ? "0" : item.FParentID;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = positionApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(PositionEntity positionEntity, string keyValue)
        {
            positionApp.SubmitForm(positionEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            positionApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
        public ActionResult GetSelectJson(string keyValue)
        {
            var data = positionApp.GetEntitys(keyValue);
            return Content(data.ToJson());
        }
    }
}
