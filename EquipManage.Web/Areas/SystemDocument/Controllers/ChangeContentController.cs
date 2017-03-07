using EquipManage.Application.SystemDocument;
using EquipManage.Code;
using System.Web.Mvc;

namespace EquipManage.Web.Areas.SystemDocument.Controllers
{
    public class ChangeContentController : ControllerBase
    {
        private ChangeContentApp changeContentApp = new ChangeContentApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson()
        {
            var data = changeContentApp.GetList();
            return Content(data.ToJson());
        }
    }
}
