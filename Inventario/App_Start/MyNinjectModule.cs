using MediatR;
using Ninject.Modules;
using System;
using Ninject;
using Inventario.Controllers;
using System.Linq;
using System.Reflection;
using System.Data.Entity;
using Inventario.Models;
using Inventario.UnitOfWork;
using Inventario.CQRS.Queries;
using System.Collections.Generic;
using Inventario.CQRS.Handlers;
using Inventario.CQRS.Commands;
using MediatR.Pipeline;

namespace Inventario.App_Start
{
    public class MyNinjectModule : NinjectModule
    {
        public override void Load()
        {
            //Bind<DbContext>().To<ApplicationDbContext>().InSingletonScope();
            //Bind<IUnitOfWork>()
            //    .To<UnitOfWork.UnitOfWork>()
            //    .InSingletonScope()
            //    .WithConstructorArgument("dbContext", ctx => ctx.Kernel.Get<ApplicationDbContext>());

            //Bind<Mediator>().ToSelf().InSingletonScope()
            //    .WithConstructorArgument("serviceFactory", ctx => ctx.Kernel.Get<ServiceFactory>());

            //Bind<ServiceFactory>().ToMethod(ctx => t => ctx.Kernel.Get(t));



            //Bind<Controllers.ItemsController>().ToSelf()
            //    .WithConstructorArgument("mediator", context => context.Kernel.Get<IMediator>());
            //Bind<HomeController>().ToSelf()
            //    .WithConstructorArgument("mediator", context => context.Kernel.Get<IMediator>());
            //Bind<Controllers.API.ItemsController>().ToSelf()
            //    .WithConstructorArgument("mediator", context => context.Kernel.Get<IMediator>());

            //Bind<IRequestHandler<GetItemListQuery, List<Item>>>()
            //    .To<GetItemListHandler>()
            //    .InSingletonScope();
            //Bind<IRequestHandler<GetItemByIdQuery, Item>>()
            //    .To<GetItemByIdHandler>()
            //    .InSingletonScope();
            //Bind<IRequestHandler<InsertItemCommand, Item>>()
            //    .To<InsertItemHandler>()
            //    .InSingletonScope();
            //Bind<IRequestHandler<GetItemsModelQuery, List<ItemModel>>>()
            //    .To<GetItemsModelListHandler>()
            //    .InSingletonScope();
            //Bind<IRequestHandler<GetItemsCategoryQuery, List<ItemCategory>>>()
            //    .To<GetItemsCategoryListHandler>()
            //    .InSingletonScope();
            //Bind<IRequestHandler<UpdateItemCommand, Item>>()
            //    .To<UpdateItemHandler>()
            //    .InSingletonScope();
            //Bind<IRequestHandler<DeleteItemCommand, Unit>>()
            //    .To<DeleteItemHandler>()
            //    .InSingletonScope();
        }
    }
}