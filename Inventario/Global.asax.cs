using Inventario.App_Start;
using Inventario.Controllers.API;
using Inventario.Models;
using Inventario.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Inventario
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var services = new ServiceCollection();
            ServiceConfig.ConfigureServices(services);

            var defaultResolver = new DefaultDependencyResolver(services.BuildServiceProvider());
            DependencyResolver.SetResolver(defaultResolver);
        }
    }
}
