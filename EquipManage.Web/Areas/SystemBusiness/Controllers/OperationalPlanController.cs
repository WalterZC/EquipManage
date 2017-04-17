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

            headEntity = EquipManage.Code.Json.ToObject<OperationalPlanEntity>(dataHead);
            EquipEntryList = EquipManage.Code.Json.ToList<OperationalPlanEquipEntryEntity>(dataEquipEntry);
            PartsEntryList = EquipManage.Code.Json.ToList<OperationalPlanPartsEntryEntity>(dataPartsEntry);

            operationalPlanApp.Delete(operationalPlanApp.GetForm(headEntity.FId));
            operationalPlanApp.SubmitForm(headEntity, headEntity == null ? "" : headEntity.FId);
            operationalPlanEquipEntryApp.Delete(headEntity.FId);
            operationalPlanEquipEntryApp.SubmitForm(headEntity,EquipEntryList);
            operationalPlanPartsEntryApp.Delete(headEntity.FId);
            operationalPlanPartsEntryApp.SubmitForm(headEntity,PartsEntryList);

            return Success("操作成功。");
        }
    }
}
