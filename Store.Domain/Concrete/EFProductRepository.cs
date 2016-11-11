using Store.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Domain.Entities;

namespace Store.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Product> Products
        {
            get
            {
                return context.Products;
            }
        }
    }
}