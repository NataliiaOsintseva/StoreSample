using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Store.Domain.Abstract;
using Store.Controllers;
using Store.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Store.Tests
{
    [TestClass]
    public class AdminModuleTests
    {
        [TestMethod]
        public void Verify_All_Products_Present()
        {
            // Prepare data - create mock repo
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product { ProductID = 1, Name = "P1", Category = "Bathroom" },
                new Product { ProductID = 2, Name = "P2", Category = "Living Room" },
                new Product { ProductID = 3, Name = "P3", Category = "Living Room" }
            });

            // Prepare data - create admin controller
            AdminController target = new AdminController(mock.Object);
            Product[] result = ((IEnumerable<Product>)target.Index().ViewData.Model).ToArray();

            // Assert
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);
        }
    }
}
