using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Store.Domain.Abstract;
using Store.Controllers;
using Store.Domain.Entities;
using Store.Infrastructure.Abstract;
using System.Collections.Generic;
using System.Linq;
using Store.Models;

namespace Store.Tests
{
    [TestClass]
    public class AdminModuleTests
    {
        [TestMethod]
        public void Can_Get_Product_Image()
        {
            // Prepare data - create product with image
            Product product = new Product { ProductID = 0, Name = "P0", ImageData = new byte[] { }, ImageMimeType = "image/jpg" };
            
            // Prepare data - create mock repo
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                product,
                new Product { ProductID = 1, Name = "P1" },
                new Product { ProductID = 2, Name = "P2" }
            }.AsQueryable());

            // Prepare data - create controller
            AdminController controller = new AdminController(mock.Object);
            ActionResult result = controller.GetImage();

            // Assert
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(product.ImageMimeType, ((FileResult)result).ContentType);
        }

        [TestMethod]
        public void Login_Valid_Credentials()
        {
            // Prepare data - create mock auth
            Mock<IAuthenticated> mock = new Mock<IAuthenticated>();
            mock.Setup(m => m.Authenticate("adminUser", "adminPass")).Returns(true);

            // Prepare data - create model
            LoginModel model = new LoginModel
            {
                UserName = "adminUser",
                Password = "adminPass"
            };

            AccountController controller = new AccountController(mock.Object);
            ActionResult result = controller.Login(model, "/TestUrl");

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/TestUrl", ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void Login_Invalid_Credentials()
        {
            // Prepare data - create mock auth
            Mock<IAuthenticated> mock = new Mock<IAuthenticated>();
            mock.Setup(m => m.Authenticate("adminUser", "wrongPass")).Returns(false);

            // Prepare data - create model
            LoginModel model = new LoginModel
            {
                UserName = "adminUser",
                Password = "wrongPass"
            };

            AccountController controller = new AccountController(mock.Object);
            ActionResult result = controller.Login(model, "/TestUrl");

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }

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
