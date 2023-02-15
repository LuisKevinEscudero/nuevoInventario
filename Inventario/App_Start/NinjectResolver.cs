using Ninject.Extensions.ChildKernel;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Inventario.UnitOfWork;
using Inventario.Controllers.API;
using MediatR;
using Inventario.Models;
using System.Data.Entity;

namespace Inventario.App_Start
{
    public class NinjectResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectResolver() : this(new StandardKernel())
        {
        }

        public NinjectResolver(IKernel ninjectKernel, bool scope = false)
        {
            kernel = ninjectKernel;
            if (!scope)
            {
                AddBindings(kernel);
            }
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectResolver(AddRequestBindings(new ChildKernel(kernel)), true);
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public void Dispose()
        {

        }

        private void AddBindings(IKernel kernel)
        {
            kernel.Bind<DbContext>().To<ApplicationDbContext>().InSingletonScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork.UnitOfWork>()
                .InSingletonScope()
                .WithConstructorArgument("dbContext", kernel.Get<DbContext>());
            kernel.Bind<IMediator>().To<Mediator>().InSingletonScope();
            kernel.Bind<ItemsController>().ToSelf()
                .WithConstructorArgument("mediator", kernel.Get<IMediator>());
        }


        private IKernel AddRequestBindings(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork.UnitOfWork>().InSingletonScope();
            
            return kernel;
        }
    }
}