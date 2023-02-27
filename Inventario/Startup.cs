using Microsoft.Owin;
using Owin;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Ninject;
using Ninject.Extensions.Conventions;
using System.Collections.Generic;
using System;
using System.Linq;

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
            // Configurar MediatR con Ninject
            services.AddMediatR(typeof(Startup).Assembly);
            var kernel = new StandardKernel();
            kernel.Bind(x => x.FromThisAssembly()
                .SelectAllClasses()
                .InheritedFrom(typeof(IRequestHandler<,>))
                .BindAllInterfaces());

            kernel.Bind<Func<Type, object>>().ToMethod(ctx =>
            {
                var type = ctx.Kernel.GetBindings(typeof(IEnumerable<>)
                    .MakeGenericType(typeof(IRequestHandler<,>)
                    .MakeGenericType(ctx.Request.ParentRequest.Service.GetGenericArguments())))
                    .Select(x => x.BindingConfiguration.ProviderCallback(null))
                    .Cast<object>()
                    .ToArray();

                return t => type.FirstOrDefault(x => x.GetType().GetInterfaces()
                    .Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)
                    && y.GenericTypeArguments[0] == t));
            });

            kernel.Bind<IMediator>().To<Mediator>();

            // Registra el contenedor Ninject en el contenedor de servicios de ASP.NET
            services.AddSingleton<IKernel>(kernel);
        }

    }


}
