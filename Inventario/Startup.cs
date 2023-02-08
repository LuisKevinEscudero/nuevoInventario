using Inventario.UnitOfWork;
using Microsoft.Owin;
using Owin;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using System;
using Inventario.Models;
using Inventario.Repository;
using Inventario.Controllers;

[assembly: OwinStartupAttribute(typeof(Inventario.Startup))]
namespace Inventario
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ApplicationDbContext>().AsSelf();
            builder.RegisterType<UnitOfWork.UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<ItemsController>().AsSelf();
            var container = builder.Build();

            var controller = container.Resolve<ItemsController>();
        }

    }
}
