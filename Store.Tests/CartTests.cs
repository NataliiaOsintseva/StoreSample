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
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lists()
        {
            Product p1 = new Product { ProductID = 1, Name = "P1" };            Product p2 = new Product { ProductID = 2, Name = "P2" };
            
            // Arrange - create a new cart
            Cart target = new Cart();
            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartList[] results = target.Lists.ToArray();
            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Product, p1);
            Assert.AreEqual(results[1].Product, p2);
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Arrange - create test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            
            // Act - create and fill a new cart
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 4);
            target.AddItem(p1, 10);
            CartList[] results = target.Lists.OrderBy(c =>
            c.Product.ProductID).ToArray();

            // Verify
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Quantity, 11);
            Assert.AreEqual(results[1].Quantity, 4);
        }

        [TestMethod]
        public void Can_Remove_From_List()
        {
            // Arrange - create test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };

            // Act - create and fill a new cart
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 4);
            target.AddItem(p3, 10);
            target.AddItem(p2, 5);

            target.RemoveFromList(p2);

            // Verify
            Assert.AreEqual(target.Lists.Where(c => c.Product == p2).Count(), 0);
            Assert.AreEqual(target.Lists.Count(), 2);

        }

        [TestMethod]
        public void Calculate_Totals()
        {
            // Arrange - create test products
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 30M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 12M };
            Product p3 = new Product { ProductID = 3, Name = "P3", Price = 10M };

            // Act - create and fill a new cart
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 4);
            target.AddItem(p3, 10);

            decimal res = target.CalculateTotalValue();

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
            Cart target = new Cart();
            target.AddItem(p1, 5);
            target.AddItem(p2, 5);
            target.AddItem(p3, 6);

            target.ClearCart();
            // TODO: ask about anonymous objects
            // Console.WriteLine(new { ProductID = 3, Name = "P3", Price = 10M });

            Assert.AreEqual(target.Lists.Count(), 0);
            Assert.AreEqual(target.CalculateTotalValue(), 0);
        }


    }
}
