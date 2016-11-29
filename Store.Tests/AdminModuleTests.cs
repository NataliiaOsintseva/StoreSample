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
        public void Admin_Verify_All_Products_Present()
        {
            // Prepare data - create mock repo
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product { ProductID = 1, Name = "P1", Category = "Bathroom" },
                new Product { ProductID = 2, Name = "P2", Category = "Living Room" },
                new Product { ProductID = 3, Name = "P3", Category = "Living Room" }
            });

            // Prepare data - create admin controller
            AdminController controller = new AdminController(mock.Object);
            Product[] result = ((IEnumerable<Product>)controller.Index().ViewData.Model).ToArray();

            // Assert
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);
        }

        [TestMethod]
        public void Admin_Verify_Nonexisting_Products()
        {
            // Prepare data - create mock repo
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product { ProductID = 1, Name = "P1", Category = "Bathroom" },
                new Product { ProductID = 2, Name = "P2", Category = "Living Room" },
                new Product { ProductID = 3, Name = "P3", Category = "Living Room" }
            });

            // Prepare data - create admin controller
            AdminController controller = new AdminController(mock.Object);
            Product res = (Product)controller.Edit(4).ViewData.Model;
            Product [] result = ((IEnumerable<Product>)controller.Index().ViewData.Model).ToArray();

            // Assert
            Assert.IsNull(res);
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);
        }

        [TestMethod]
        public void Admin_Verify_Product_Editable()
        {
            // Prepare data - create mock repo
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product { ProductID = 1, Name = "P1", Category = "Bathroom" },
                new Product { ProductID = 2, Name = "P2", Category = "Living Room" },
                new Product { ProductID = 3, Name = "P3", Category = "Living Room" }
            });

            // Prepare data - create admin controller
            AdminController controller = new AdminController(mock.Object);
            Product res1 = (Product)controller.Edit(1).ViewData.Model;
            Product res2 = (Product)controller.Edit(2).ViewData.Model;
            Product res3 = (Product)controller.Edit(3).ViewData.Model;

            // Assert
            Assert.AreEqual("P1", res1.Name);
            Assert.AreEqual("P2", res2.Name);
            Assert.AreEqual("P3", res3.Name);
        }
    }
}
