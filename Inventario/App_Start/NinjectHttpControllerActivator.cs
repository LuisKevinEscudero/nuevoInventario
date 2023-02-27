using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Inventario.App_Start
{
    public class NinjectHttpControllerActivator : IHttpControllerActivator
    {
        private readonly IKernel _kernel;

        public NinjectHttpControllerActivator(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IHttpController Create(System.Net.Http.HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            return _kernel.Get(controllerType) as IHttpController;
        }
    }

}