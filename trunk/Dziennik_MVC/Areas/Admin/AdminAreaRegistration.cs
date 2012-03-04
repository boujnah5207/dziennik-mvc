﻿using System.Web.Mvc;

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
                "Admin_default",
                "Admin/{controller}/{action}",
                new { controller = "Profile", action = "Profile" }
            );
        }
           
    }
}
