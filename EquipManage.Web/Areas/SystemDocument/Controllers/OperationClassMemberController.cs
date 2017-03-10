using EquipManage.Application.SystemDocument;
using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace EquipManage.Web.Areas.SystemDocument.Controllers
{
    public class OperationClassMemberController : ControllerBase
    {
        private OperationClassMemberApp operationClassMemberApp = new OperationClassMemberApp();
        private UserApp userApp = new UserApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string itemId, string keyword)
        {
            var data = operationClassMemberApp.GetList(itemId, keyword);
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectJson(string keyValue)
        {
            var data = userApp.GetClassUserList(keyValue);
            DataTable list = new DataTable();
            list.Columns.Add("FId", Type.GetType("System.String"));
            list.Columns.Add("FRealName", Type.GetType("System.String"));
            foreach (UserEntity item in data)
            {
                list.Rows.Add(new object[] { item.FId, item.FRealName });
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
