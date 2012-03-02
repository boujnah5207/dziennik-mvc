using System.Web.Mvc;

namespace Dziennik_MVC.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Profile",
                "Profile/{login}",
                new { controller = "Admin", action = "Profile" }
            );
        }
           
    }
}
