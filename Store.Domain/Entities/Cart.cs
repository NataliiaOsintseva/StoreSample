using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Domain.Entities
{
    public class Cart
    {
        private List<CartList> listCollection = new List<CartList>();

        public void AddItem(Product product, int quantity)
        {
            CartList line = listCollection
                .Where(p => p.Product.ProductID == product.ProductID)
                .FirstOrDefault();

            if (line == null)
            {
                listCollection.Add(new CartList { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveFromList(Product product)
        {
            listCollection.RemoveAll(i => i.Product.ProductID == product.ProductID);
        }

        public decimal CalculateTotalValue()
        {
            return listCollection.Sum(p => p.Product.Price * p.Quantity);
        }

        public void ClearCart()
        {
            listCollection.Clear();
        }

        public IEnumerable<CartList> Lists
        {
            get { return listCollection; }
        }

    }

    public class CartList
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}