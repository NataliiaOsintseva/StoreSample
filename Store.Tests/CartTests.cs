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
using System.Web.Mvc;

namespace Store.Tests
{
    [TestClass]
    public class CartTests
    {

        [TestMethod]
        public void Validates_If_Cart_Is_Empty()
        {
            // Prepare data - create mock order
            Mock<IOrder> mock = new Mock<IOrder>();
            // Prepare data - create a new cart and fill it
            Cart cart = new Cart();
            Shipment shipment = new Shipment();

            CartController controller = new CartController(null, mock.Object);
            ViewResult result = controller.Checkout(cart, shipment);

            // Assert
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<Shipment>()),
                Times.Never());
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Can_Add_To_Cart()
        {
            // Prepare data - create mock repo
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product { ProductID = 1, Name = "P1", Category = "Bathroom" },
                new Product { ProductID = 2, Name = "P2", Category = "Living Room" }
            }.AsQueryable());

            // Prepare data - create a new cart and fill it
            Cart cart = new Cart();
            CartController controller = new CartController(mock.Object, null);
            controller.AddToCart(cart, 1, null);
            controller.AddToCart(cart, 2, null);

            // Assert
            Assert.AreEqual(2, cart.Lists.Count());
            Assert.AreEqual("P1", cart.Lists.ToArray()[0].Product.Name);
            Assert.AreEqual("P2", cart.Lists.ToArray()[1].Product.Name);
        }

        [TestMethod]
        public void Redirects_After_Adding_To_Cart()
        {
            // Prepare data - create mock repo
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product { ProductID = 1, Name = "P1", Category = "Bathroom" },
                new Product { ProductID = 2, Name = "P2", Category = "Living Room" }
            });

            // Prepare data - create a new cart and fill it
            Cart cart = new Cart();
            CartController controller = new CartController(mock.Object, null);
            RedirectToRouteResult result = controller.AddToCart(cart, 1, "mockingUrl");

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("mockingUrl", result.RouteValues["returnUrl"]);
        }

        [TestMethod]
        public void Can_Add_New_List_Items()
        {
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            
            // Prepare data - create a new cart and fill it
            Cart cart = new Cart();
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);

            CartListItem[] results = cart.Lists.ToArray();

            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Product, p1);
            Assert.AreEqual(results[1].Product, p2);

        }

        [TestMethod]
        public void Can_Modify_Quantity_For_Existing_Items()
        {
            // Arrange - create test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            
            // Act - create and fill a new cart
            Cart cart = new Cart();
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 4);
            cart.AddItem(p1, 10);
            CartListItem[] results = cart.Lists.OrderBy(c =>
            c.Product.ProductID).ToArray();

            // Verify
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Quantity, 11);
            Assert.AreEqual(results[1].Quantity, 4);
        }

        [TestMethod]
        public void Can_Remove_Item_From_List()
        {
            // Arrange - create test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };

            // Act - create and fill a new cart
            Cart cart = new Cart();
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 4);
            cart.AddItem(p3, 10);
            cart.AddItem(p2, 5);

            cart.RemoveFromList(p2);

            // Verify
            Assert.AreEqual(cart.Lists.Where(c => c.Product == p2).Count(), 0);
            Assert.AreEqual(cart.Lists.Count(), 2);

        }

        [TestMethod]
        public void Calculate_Totals()
        {
            // Arrange - create test products
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 30M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 12M };
            Product p3 = new Product { ProductID = 3, Name = "P3", Price = 10M };

            // Act - create and fill a new cart
            Cart cart = new Cart();
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 4);
            cart.AddItem(p3, 10);

            decimal res = cart.CalculateTotalValue();

            // Verify
            Assert.AreEqual(178, res);

        }

        [TestMethod]
        public void Can_Clear_Cart()
        {
            // Arrange - create test products
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 30M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 12M };
            Product p3 = new Product { ProductID = 3, Name = "P3", Price = 10M };

            // Act - create and fill a new cart
            Cart cart = new Cart();
            cart.AddItem(p1, 5);
            cart.AddItem(p2, 5);
            cart.AddItem(p3, 6);

            cart.ClearCart();
            // TODO: ask about anonymous objects
            // Console.WriteLine(new { ProductID = 3, Name = "P3", Price = 10M });

            Assert.AreEqual(cart.Lists.Count(), 0);
            Assert.AreEqual(cart.CalculateTotalValue(), 0);
        }



    }
}
