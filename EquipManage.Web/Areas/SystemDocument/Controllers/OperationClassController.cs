using EquipManage.Application.SystemDocument;
using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using System.Web.Mvc;

namespace EquipManage.Web.Areas.SystemDocument.Controllers
{
    public class OperationClassController : ControllerBase
    {
        private OperationClassApp operationClassApp = new OperationClassApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson()
        {
            return Content(operationClassApp.GetList().ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = operationClassApp.GetForm(keyValue);
            return Content(data.ToJson());
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
        public ActionResult GetSelectJson(string keyValue)
        {
            var data = operationClassApp.GetList(keyValue);
            return Content(data.ToJson());
        }

        [HttpGet]
        public ActionResult Info()
        {
            return View();
        }
    }
}
