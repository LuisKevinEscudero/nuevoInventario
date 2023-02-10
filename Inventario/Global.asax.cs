


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
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            var defaultResolver = new DefaultDependencyResolver(serviceProvider);
            DependencyResolver.SetResolver(defaultResolver);

        }
        private void ConfigureServices(ServiceCollection services)
        {

            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));

            services.AddTransient(typeof(UnitOfWork.UnitOfWork), typeof(ApplicationDbContext));
            //services.AddTransient<IUnitOfWork, IUnitOfWork>();

            services.AddMvcControllers("*");

            /*      services.AddControllersAsServices(typeof(Global).Assembly.GetExportedTypes()
            .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
            .Where(t => typeof(IController).IsAssignableFrom(t)
            || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)));
            */
        }

        
    }
}
