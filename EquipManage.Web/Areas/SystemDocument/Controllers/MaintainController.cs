using EquipManage.Application.SystemDocument;
using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EquipManage.Web.Areas.SystemDocument.Controllers
{
    public class MaintainController : ControllerBase
    {
        private MaintainApp maintainApp = new MaintainApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson()
        {
            var data = maintainApp.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach (MaintainEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.FId;
                treeModel.text = item.FFullName;
                treeModel.parentId = item.FParentId;
                treeModel.data = item;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeJson()
        {
            var data = maintainApp.GetList();
            var treeList = new List<TreeViewModel>();
            foreach (MaintainEntity item in data)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = data.Count(t => t.FParentId == item.FId) == 0 ? false : true;
                tree.id = item.FId;
                tree.text = item.FFullName;
                tree.value = item.FNumber;
                tree.parentId = item.FParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeGridJson(string keyword)
        {
            var data = maintainApp.GetList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.FFullName.Contains(keyword));
            }
            var treeList = new List<TreeGridModel>();
            foreach (MaintainEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.FParentId == item.FId) == 0 ? false : true;
                treeModel.id = item.FId;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.FParentId;
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
            var data = maintainApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetPermissionGridJson(string itemId, string keyword)
        {
            var data = maintainApp.GetPermissionGridList(itemId, keyword);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(MaintainEntity maintainEntity, string keyValue)
        {
            maintainApp.SubmitForm(maintainEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            maintainApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
