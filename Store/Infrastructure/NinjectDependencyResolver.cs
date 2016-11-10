using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;

namespace Store.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelArg)
        {
            kernel = kernelArg;
            AddBindings();            
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public void AddBindings()
        {

        }
    }
}