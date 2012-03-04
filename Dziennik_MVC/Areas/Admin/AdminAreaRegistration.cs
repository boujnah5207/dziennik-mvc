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
                "Grops",
                "Admin/Groups/{action}",
                new { controller = "Groups", action = "List" }
            );

            context.MapRoute(
                "Semesters",
                "Admin/Semesters/{action}",
                new { controller = "Semesters", action = "List" }
            );
           
            context.MapRoute(
                "Admin_default",
                "Admin/Profile/{action}",
                new { controller = "Admin", action = "Profile" }
            );
        }
           
    }
}
