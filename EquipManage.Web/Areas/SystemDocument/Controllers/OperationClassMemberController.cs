using EquipManage.Application.SystemDocument;
using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EquipManage.Web.Areas.SystemDocument.Controllers
{
    public class OperationClassMemberController : ControllerBase
    {
        private OperationClassMemberApp operationClassMemberApp = new OperationClassMemberApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string itemId, string keyword)
        {
            var data = operationClassMemberApp.GetList(itemId, keyword);
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectJson(string enCode)
        {
            var data = operationClassMemberApp.GetItemList(enCode);
            List<object> list = new List<object>();
            foreach (OperationClassMemberEntity item in data)
            {
                list.Add(new { id = item.FMemberID});
            }
            return Content(list.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = operationClassMemberApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(OperationClassMemberEntity operationClassMemberEntity, string keyValue)
        {
            operationClassMemberApp.SubmitForm(operationClassMemberEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            operationClassMemberApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
