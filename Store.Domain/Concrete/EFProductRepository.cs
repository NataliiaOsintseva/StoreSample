using Store.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace Store.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public DbSet<Product> Products
        {
            get
            {
                return context.Products;
            }
        }

        public Product Delete(int productId)
        {
            Product dbEntity = context.Products.Find(productId);
            if(dbEntity != null)
            {
                context.Products.Remove(dbEntity);
                context.SaveChanges();
            }
            return dbEntity;
        }

        public Product GetProductById(int productID)
        {
            if (productID == 0)
            {
                return context.Products.Add(new Product());                
            }
            else
            {
                return context.Products.Find(productID);
            }
        }

        public void Update(Product prod)
        {
            context.Entry(prod).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public void Save(int productId)
        {
            Product dbEntity = context.Products.Find(productId);
            try { context.SaveChanges(); }
            catch (DbUpdateException ex)
            {
                var errorMessages = ex.Data;

                var fullErrorMessage = string.Join("; ", errorMessages);

                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                throw new DbUpdateException(exceptionMessage, ex.InnerException);
            }
        }
    }
}