using EquipManage.Application.SystemManage;
using EquipManage.Code;
using EquipManage.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EquipManage.Web.Areas.SystemManage.Controllers
{
    public class EquipmentController : ControllerBase
    {
        private EquipmentApp equipmentApp = new EquipmentApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = equipmentApp.GetList(keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = equipmentApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(EquipmentEntity userEntity, string keyValue)
        {
            equipmentApp.SubmitForm(userEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            equipmentApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
