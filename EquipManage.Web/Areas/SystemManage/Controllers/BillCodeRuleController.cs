using EquipManage.Application.SystemManage;
using EquipManage.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EquipManage.Web.Areas.SystemManage.Controllers
{
    public class BillCodeRuleController : ControllerBase
    {
        private BillCodeRuleApp billCodeRuleApp = new BillCodeRuleApp();
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetNewBillInfoJson(string FBillName,bool FROB)
        {
            string FBillNo = billCodeRuleApp.GetBillNo(FBillName, FROB);
            string FId = Common.GuId();
            return Success("OK", FId + "," + FBillNo);
        }
    }
}
