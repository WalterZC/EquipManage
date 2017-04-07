using System.Web.Mvc;

namespace EquipManage.Web.Areas.ReportManage
{
    public class SystemBusinessAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SystemBusiness";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              this.AreaName + "_Default",
              this.AreaName + "/{controller}/{action}/{id}",
              new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
              new string[] { "EquipManage.Web.Areas." + this.AreaName + ".Controllers" }
            );
        }
    }
}
