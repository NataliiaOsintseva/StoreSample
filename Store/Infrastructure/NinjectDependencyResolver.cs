﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using Moq;
using Store.Domain.Abstract;
using Store.Domain;

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
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { Name = "Cup", Price = 15 },
                new Product { Name = "Plate", Price = 23 },
                new Product { Name = "Pan", Price = 40 }
            });

            kernel.Bind<IProductRepository>().ToConstant(mock.Object);
        }
    }
}