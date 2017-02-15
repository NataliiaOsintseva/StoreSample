using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Abstract
{
    public interface IProductRepository
    {
        DbSet<Product> Products { get; }
        void Save(int productId);
        Product Delete(int productId);
        Product GetProductById(int productId);
        void SaveChanges();
    }
}
