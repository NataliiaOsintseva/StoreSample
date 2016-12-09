using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        void Save(Product product);
        Product Delete(int productId);
        Product GetProductById(int productId);
        void SaveChanges();
    }
}
