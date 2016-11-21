using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Domain.Entities
{
    public class Cart
    {
        private List<CartListItem> listCollection = new List<CartListItem>();

        public void AddItem(Product product, int quantity)
        {
            CartListItem list = listCollection
                .Where(p => p.Product.ProductID == product.ProductID)
                .FirstOrDefault();

            if (list == null)
            {
                listCollection.Add(new CartListItem { Product = product, Quantity = quantity });
            }
            else
            {
                list.Quantity += quantity;
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

        public IEnumerable<CartListItem> Lists
        {
            get { return listCollection; }
        }

    }

    public class CartListItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}