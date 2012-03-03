using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject.Web.Mvc;
using Ninject;
using System.Web.Security;
using Dziennik_MVC.Models.Data.Abstract;
using Dziennik_MVC.Models.Data.Concrete;
using Dziennik_MVC.Helpers;
using System.Reflection;
using System.Data.Entity;
using Ninject;

namespace Dziennik_MVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : NinjectHttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Account", action = "LogOn", id = UrlParameter.Optional},
                new[] { "Dziennik_MVC.Controllers"}
            );
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());            
            RegisterServices(kernel);
            return kernel;
        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            Database.SetInitializer(new Dziennik_MVC.Models.Data.Concrete.EFContext.EFContextInitializer());
            

            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUsersRepository>().To<UsersRepository>().InRequestScope();
            //kernel.Inject(Membership.Provider);
           // kernel.Inject(Roles.Provider);     
        }
    }
}