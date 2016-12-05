using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using Moq;
using Store.Domain.Abstract;
using Store.Infrastructure.Abstract;
using Store.Infrastructure.Concrete;
using Store.Domain.Entities;
using Store.Domain.Concrete;
using System.Configuration;

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
            //Uncomment to try using app without database
            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product>
            //{
            //    new Product { ProductID = 0, Name = "Cup", Price = 15, Category = "Kitchen" },
            //    new Product { ProductID = 1, Name = "Plate", Price = 23, Category = "Kitchen" },
            //    new Product { ProductID = 2, Name = "Fork", Price = 9, Category = "Kitchen" },
            //    new Product { ProductID = 3, Name = "Knife", Price = 9, Category = "Kitchen" },
            //    new Product { ProductID = 4, Name = "Spoon", Price = 10, Category = "Kitchen" },
            //    new Product { ProductID = 5, Name = "Soup bowl", Price = 15, Category = "Kitchen" },
            //    new Product { ProductID = 6, Name = "Wine glass", Price = 30, Category = "Kitchen" },
            //    new Product { ProductID = 7, Name = "Pan", Price = 68, Category = "Kitchen" },
            //    new Product { ProductID = 8, Name = "Pillow", Price = 60, Category = "Bedroom" },
            //    new Product { ProductID = 9, Name = "Cover", Price = 120, Category = "Bedroom" },
            //    new Product { ProductID = 10, Name = "Blanket", Price = 80, Category = "Bedroom" }
            //});

            //kernel.Bind<IProductRepository>().ToConstant(mock.Object);

            EmailSettings settings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IProductRepository>().To<EFProductRepository>();
            kernel.Bind<IOrder>().To<OrderProcessor>().WithConstructorArgument("settings", settings);
            kernel.Bind<IAuthenticated>().To<Authenticator>();
        }
    }
}