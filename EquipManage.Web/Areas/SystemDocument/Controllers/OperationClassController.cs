using EquipManage.Application.SystemDocument;
using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EquipManage.Web.Areas.SystemDocument.Controllers
{
    public class OperationClassController : ControllerBase
    {
        private OperationClassApp operationClassApp = new OperationClassApp();

        //[HttpGet]
        //[HandlerAjaxOnly]
        //public ActionResult GetGridJson()
        //{
        //    return Content(operationClassApp.GetList().ToJson());
        //}
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = operationClassApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeJson()
        {
            //var data = operationClassApp.GetList();
            var data = operationClassApp.GetPermissionGridList("","");

            var treeList = new List<TreeViewModel>();
            foreach (OperationClassEntity item in data)
            {
                TreeViewModel tree = new TreeViewModel();
                tree.id = item.FId;
                tree.text = item.FShortName;
                tree.value = item.FNumber;
                tree.parentId = "0";
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(OperationClassEntity userEntity, string keyValue)
        {
            operationClassApp.SubmitForm(userEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            operationClassApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DisabledAccount(string keyValue)
        {
            OperationClassEntity operationClassEntity = new OperationClassEntity();
            operationClassEntity.FId = keyValue;
            operationClassEntity.FEnabledMark = false;
            operationClassApp.UpdateForm(operationClassEntity);
            return Success("禁用成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult EnabledAccount(string keyValue)
        {
            OperationClassEntity operationClassEntity = new OperationClassEntity();
            operationClassEntity.FId = keyValue;
            operationClassEntity.FEnabledMark = false;
            operationClassApp.UpdateForm(operationClassEntity);
            return Success("启用成功。");
        }
        public ActionResult GetSelectJson(string itemId,string keyword)
        {
            var data = itemId == null ? new List<OperationClassEntity>() : operationClassApp.GetPermissionGridList(itemId, keyword);
            return Content(data.ToJson());
        }

        [HttpGet]
        public ActionResult Info()
        {
            return View();
        }
    }
}
