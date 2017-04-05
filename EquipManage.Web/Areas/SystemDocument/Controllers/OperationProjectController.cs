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
    public class OperationProjectController : ControllerBase
    {
        private OperationProjectApp operationProjectApp = new OperationProjectApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string itemId)
        {
            var data = operationProjectApp.GetList(itemId);
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = operationProjectApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(OperationProjectEntity Entity, string keyValue)
        {
            string data = operationProjectApp.SubmitForm(Entity, Entity.FId);
            return Content(data);
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            operationProjectApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
        [HttpGet]
        [HandlerAuthorize]
        public ActionResult PartForm()
        {
            return View();
        }
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitEquipRelateForm(FormCollection collection, string keyValue)
        {
            OperationProjectEntity entity = operationProjectApp.GetForm(keyValue);
            entity.FItemIds = collection["FItemIds"]==null?"": collection["FItemIds"].ToString();
            operationProjectApp.SubmitForm(entity, entity.FId);
            return Success("操作成功。");
        }
    }
}
