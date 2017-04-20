using EquipManage.Application.SystemBusiness;
using EquipManage.Code;
using EquipManage.Domain.Entity.SystemBusiness;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EquipManage.Web.Areas.SystemBusiness.Controllers
{
    public class OperationalPlanController : ControllerBase
    {
        private OperationalPlanApp operationalPlanApp = new OperationalPlanApp();
        private OperationalPlanEquipEntryApp operationalPlanEquipEntryApp = new OperationalPlanEquipEntryApp();
        private OperationalPlanPartsEntryApp operationalPlanPartsEntryApp = new OperationalPlanPartsEntryApp();

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(string dataHead,string dataEquipEntry,string dataPartsEntry)
        {
            OperationalPlanEntity headEntity = new OperationalPlanEntity();
            List<OperationalPlanEquipEntryEntity> EquipEntryList = new List<OperationalPlanEquipEntryEntity>();
            List<OperationalPlanPartsEntryEntity> PartsEntryList = new List<OperationalPlanPartsEntryEntity>();

            headEntity = EquipManage.Code.Json.ToObject<OperationalPlanEntity>(dataHead);
            EquipEntryList = EquipManage.Code.Json.ToList<OperationalPlanEquipEntryEntity>(dataEquipEntry);
            PartsEntryList = EquipManage.Code.Json.ToList<OperationalPlanPartsEntryEntity>(dataPartsEntry);

            operationalPlanApp.Delete(operationalPlanApp.GetForm(headEntity.FId));
            operationalPlanApp.SubmitForm(headEntity, headEntity == null ? "" : headEntity.FId);
            //operationalPlanEquipEntryApp.Delete(headEntity.FId);
            operationalPlanEquipEntryApp.SubmitForm(headEntity,EquipEntryList);
            //operationalPlanPartsEntryApp.Delete(headEntity.FId);
            operationalPlanPartsEntryApp.SubmitForm(headEntity,PartsEntryList);

            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(FormCollection formdata)
        {
            Pagination pagination = new Pagination();
            pagination.rows = Ext.ToInt(formdata["length"]);
            pagination.page = Ext.ToInt(formdata["page"]);
            pagination.sord = formdata["sord"];
            pagination.sidx= formdata["sidx"];
            string keyword = formdata["search[value]"];
            var data = new
            {
                rows = operationalPlanApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson()); 
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string FId)
        {
            var data = operationalPlanApp.GetForm(FId);
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetEquipFormJson(string FId)
        {
            var data = operationalPlanEquipEntryApp.GetList(FId);
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetPartFormJson(string FId)
        {
            var data = operationalPlanPartsEntryApp.GetList(FId);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult Delete(string FId)
        {
            if (!string.IsNullOrEmpty(FId))
            {
                operationalPlanApp.Delete(operationalPlanApp.GetForm(FId));
            }
            return Success("删除成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult Check(string FId,string checkType)
        {
            bool result = false;
            if ((!string.IsNullOrEmpty(FId))&&(!string.IsNullOrEmpty(checkType)))
            {
                result=operationalPlanApp.CheckForm(operationalPlanApp.GetForm(FId),Ext.ToBool(checkType));
            }
            if (result)
            {
                return Success("操作成功。");
            }
            else
            {
                return Error("操作失败。");
            }
        }
    }
}
