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
using Inventario.CQRS.Queries;
using Inventario.CQRS.Handlers;
using Inventario.DTOs;
using Inventario.CQRS.Commands;
using Inventario.Controllers;

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
            /*kernel.Bind<DbContext>().To<ApplicationDbContext>().InSingletonScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork.UnitOfWork>()
                .InSingletonScope()
                .WithConstructorArgument("dbContext", kernel.Get<DbContext>());
            kernel.Bind<IMediator>().To<Mediator>().InSingletonScope();
            kernel.Bind<ServiceFactory>().ToMethod(ctx => t => ctx.Kernel.Get(t));
            kernel.Bind<Mediator>().ToSelf().InSingletonScope()
                .WithConstructorArgument("serviceFactory", kernel.Get<ServiceFactory>());*/

            //kernel.Bind<IDependencyResolver>().To<NinjectResolver>();

            //add queries to dependency injection
            /*kernel.Bind<IRequestHandler<GetItemListQuery, List<Item>>>()
                .To<GetItemListHandler>()
                .InSingletonScope();
            kernel.Bind<IRequestHandler<GetItemByIdQuery, Item>>()
                .To<GetItemByIdHandler>()
                .InSingletonScope();
            kernel.Bind<IRequestHandler<InsertItemCommand, Item>>()
                .To<InsertItemHandler>()
                .InSingletonScope();
            kernel.Bind<IRequestHandler<GetItemsModelQuery, List<ItemModel>>>()
                .To<GetItemsModelListHandler>()
                .InSingletonScope();
            kernel.Bind<IRequestHandler<GetItemsCategoryQuery, List<ItemCategory>>>()
                .To<GetItemsCategoryListHandler>()
                .InSingletonScope();
            kernel.Bind<IRequestHandler<UpdateItemCommand, Item>>()
                .To<UpdateItemHandler>()
                .InSingletonScope();
            kernel.Bind<IRequestHandler<DeleteItemCommand, Unit>>()
                .To<DeleteItemHandler>()
                .InSingletonScope();*/
        }

        private IKernel AddRequestBindings(IKernel kernel)
        {
            return kernel;
        }
    }
}