using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;
using Store.Domain.Abstract;
using Moq;
using Store.Controllers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Store.Models;

namespace Store.Tests
{
    [TestClass]
    public class CommonTests
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Prepare test data
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = "P1"},
            new Product {ProductID = 2, Name = "P2"},
            new Product {ProductID = 3, Name = "P3"},
            new Product {ProductID = 4, Name = "P4"},
            new Product {ProductID = 5, Name = "P5"}
            });
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Perform verifications
            ProductsListViewModel result =
                (ProductsListViewModel)controller.List(null, 2).Model;
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

        [TestMethod]
        public void Can_Use_PaginationViewModel()
        {
            // Prepare test data
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = "P1"},
            new Product {ProductID = 2, Name = "P2"},
            new Product {ProductID = 3, Name = "P3"},
            new Product {ProductID = 4, Name = "P4"},
            new Product {ProductID = 5, Name = "P5"}
            });
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 4;

            // Perform verifications
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;

            PagingInfo pageInfo = result.PAgingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 4);
            Assert.AreEqual(pageInfo.TotalPages, 2);
            Assert.AreEqual(pageInfo.TotalItems, 5);
        }

        [TestMethod]
        public void Can_Filter()
        {
            // Prepare test data
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
            new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
            new Product {ProductID = 3, Name = "P3", Category = "Cat2"},
            new Product {ProductID = 4, Name = "P4", Category = "Cat3"},
            new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
            });

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 4;

            // Perform verifications
            Product[] result = ((ProductsListViewModel)controller.List("Cat2", 1).Model).Products.ToArray();
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2");
            Assert.IsTrue(result[1].Name == "P3");
        }

        [TestMethod]
        public void Can_Categorize()
        {
            // Prepare test data
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = "P1", Category = "Orange"},
            new Product {ProductID = 2, Name = "P2", Category = "Orange"},
            new Product {ProductID = 3, Name = "P3", Category = "Pink"},
            new Product {ProductID = 4, Name = "P4", Category = "Blue"},
            new Product {ProductID = 5, Name = "P5", Category = "Green"}
            });

            NavigationController controller = new NavigationController(mock.Object);

            // Perform verifications
            string[] results = ((IEnumerable<string>)controller.Menu().Model).ToArray();

            Assert.AreEqual(results.Length, 4);
            Assert.AreEqual(results[0], "Blue");
            Assert.AreEqual(results[1], "Green");
            Assert.AreEqual(results[2], "Orange");
            Assert.AreEqual(results[3], "Pink");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            // Prepare test data
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = "P1", Category = "Orange"},
            new Product {ProductID = 2, Name = "P2", Category = "Blue"},
            });

            NavigationController controller = new NavigationController(mock.Object);

            string categoryToSelect = "Orange";
            string result = controller.Menu(categoryToSelect).ViewBag.SelectedCategory;

            // Perform verifications
            Assert.AreEqual(categoryToSelect, result);
        }

        [TestMethod]
        public void Arranges_Products_To_Categories()
        {
            // Prepare test data
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = "P1", Category = "Orange"},
            new Product {ProductID = 2, Name = "P2", Category = "Orange"},
            new Product {ProductID = 3, Name = "P3", Category = "Pink"},
            new Product {ProductID = 4, Name = "P4", Category = "Blue"},
            new Product {ProductID = 5, Name = "P5", Category = "Green"}
            });

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 4;

            // Get results
            int res1 = ((ProductsListViewModel)controller.List("Pink").Model).PAgingInfo.TotalItems;
            int res2 = ((ProductsListViewModel)controller.List("Orange").Model).PAgingInfo.TotalItems;
            int res3 = ((ProductsListViewModel)controller.List("Blue").Model).PAgingInfo.TotalItems;
            int res4 = ((ProductsListViewModel)controller.List("Green").Model).PAgingInfo.TotalItems;
            int resAll = ((ProductsListViewModel)controller.List(null).Model).PAgingInfo.TotalItems;

            // Verifications
            Assert.AreEqual(res1, 1);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(res4, 1);
            Assert.AreEqual(resAll, 5);
        }

    }
}
