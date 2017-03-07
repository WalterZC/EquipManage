using EquipManage.Application.SystemDocument;
using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using System.Collections;
using System.Web.Mvc;

namespace EquipManage.Web.Areas.SystemDocument.Controllers
{
    public class ChangeTypeController : ControllerBase
    {
        private ChangeTypeApp changeType = new ChangeTypeApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson()
        {
            var data = changeType.GetList();
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = changeType.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(FormCollection collection, string keyValue)
        {
            ArrayList ChangeTypeArray = new ArrayList();
            ChangeTypeEntity Entity = new ChangeTypeEntity();
            Entity.FNumber = collection.GetValue("FNumber").AttemptedValue;
            Entity.FFullName = collection.GetValue("FFullName").AttemptedValue;
            Entity.FSortCode =Ext.ToInt(collection.GetValue("FSortCode").AttemptedValue);
            Entity.FEnabledMark =Ext.ToBool(collection.GetValue("FEnabledMark").AttemptedValue);
            Entity.FDescription = collection.GetValue("FDescription").AttemptedValue;

            foreach (string key in collection.AllKeys)
            {
                if (key.Contains("ChangeType")&& collection.GetValue(key).AttemptedValue=="true")
                {
                    ChangeTypeArray.Add(key.Replace("ChangeType", ""));
                }

            }
            Entity.FContent= string.Join(",", (string[])ChangeTypeArray.ToArray(typeof(string)));
            changeType.SubmitForm(Entity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            changeType.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
