using EquipManage.Application.SystemBusiness;
using EquipManage.Domain.Entity.SystemBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            //operationalPlanApp.SubmitForm(dataHead, dataHead.FId);
            //operationalPlanEquipEntryApp.Delete(dataHead);
            //operationalPlanEquipEntryApp.SubmitForm(dataEquipEntry);
            //operationalPlanPartsEntryApp.Delete(dataHead);
            //operationalPlanPartsEntryApp.SubmitForm(dataPartsEntry);
            headEntity = EquipManage.Code.Json.ToObject<OperationalPlanEntity>(dataHead);
            EquipEntryList = EquipManage.Code.Json.ToList<OperationalPlanEquipEntryEntity>(dataEquipEntry);
            PartsEntryList = EquipManage.Code.Json.ToList<OperationalPlanPartsEntryEntity>(dataPartsEntry);
            return Success("操作成功。");
        }
    }
}
