using Inventario.UnitOfWork;
using Microsoft.Owin;
using Owin;
using Microsoft.Extensions.DependencyInjection;
using Inventario.Models;
using Inventario.CQRS.Queries;
using MediatR;
using System.Collections.Generic;
using Autofac;
using System.Web.Mvc;
using Inventario.CQRS.Handlers;
using Autofac.Integration.Mvc;

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
            services.AddScoped<ApplicationDbContext>(sp => new ApplicationDbContext());
            services.AddScoped<IUnitOfWork>(sp => new UnitOfWork.UnitOfWork(new ApplicationDbContext()));

            var container = new ContainerBuilder();

            container.RegisterType<UnitOfWork.UnitOfWork>().As<IUnitOfWork>();
            container.RegisterType<GetItemListHandler>().As<IRequestHandler<GetItemListQuery, List<Item>>>();

            var resolver = new AutofacDependencyResolver(container.Build());
            DependencyResolver.SetResolver(resolver);

            services.AddMediatR(typeof(Startup).Assembly);
            var serviceProvider = new ServiceCollection().AddMediatR(typeof(Startup)).BuildServiceProvider();

            IMediator mediator = serviceProvider.GetService<IMediator>();
            services.AddMediatR(typeof(Startup));
        }

    }


}
