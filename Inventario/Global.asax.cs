using Inventario.App_Start;
using Inventario.Controllers.API;
using Inventario.Models;
using Inventario.UnitOfWork;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ninject;

using Ninject.Modules;
using Ninject.Web.Mvc;
using Ninject.Web.WebApi;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;
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

            var kernel = NinjectWebCommon.CreateKernel();
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new NinjectHttpControllerActivator(kernel));
        }
    }
}
