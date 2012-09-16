using System.Data.Entity;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Dziennik_MVC.Infrastructure.Logging;
using Dziennik_MVC.Models.Data.Abstract;
using Dziennik_MVC.Models.Data.Concrete;
using Ninject;
using Ninject.Web.Mvc;

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
                "default", // Route name
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
            DefaultModelBinder.ResourceClassKey = "datavalidation";
            

            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<EFContext>().InRequestScope();
            kernel.Bind<IUsersRepository>().To<UsersRepository>().InRequestScope();
            kernel.Bind<IGrupyRepository>().To<GrupyRepository>().InRequestScope();
            kernel.Bind<IPrzedmiotyRepository>().To<PrzedmiotyRepository>().InRequestScope();
            kernel.Bind<ILogger>().To<NLogLogger>().InSingletonScope();
        }
    }
}