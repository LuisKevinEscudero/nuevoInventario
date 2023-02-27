using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using Inventario.Controllers;
using Inventario.Models;
using Inventario.UnitOfWork;
using MediatR;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;


[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Inventario.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Inventario.App_Start.NinjectWebCommon), "Stop")]

namespace Inventario.App_Start
{
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<DbContext>().To<ApplicationDbContext>().InSingletonScope();

            kernel.Bind<IUnitOfWork>()
                .To<UnitOfWork.UnitOfWork>()
                .InSingletonScope()
                .WithConstructorArgument("dbContext", ctx => ctx.Kernel.Get<ApplicationDbContext>());

            kernel.Bind<IMediator>().To<Mediator>()
                .WithConstructorArgument("serviceFactory", new ServiceFactory(t => kernel.Get(t)));

            kernel.Bind<ServiceFactory>().ToMethod(ctx => t => ctx.Kernel.TryGet(t));

            kernel.Bind<System.Web.Http.Dispatcher.IHttpControllerActivator>().To<NinjectHttpControllerActivator>().InSingletonScope();

            kernel.Bind<Controllers.API.ItemsController>().ToSelf().InRequestScope();

            kernel.Bind<Controllers.ItemsController>().ToSelf()
                .WithConstructorArgument("mediator", ctx => ctx.Kernel.Get<IMediator>());
           
            kernel.Bind<HomeController>()
                .ToSelf().WithConstructorArgument("mediator", ctx => ctx.Kernel.Get<IMediator>());

            var assembly = Assembly.GetExecutingAssembly();
            var handlerInterface = typeof(IRequestHandler<,>);

            foreach (var type in assembly.GetTypes())
            {
                var interfaces = type.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface);

                foreach (var @interface in interfaces)
                {
                    kernel.Bind(@interface).To(type);
                }
            }
        }
    }
}