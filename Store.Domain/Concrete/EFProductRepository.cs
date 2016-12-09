﻿using Store.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Domain.Entities;
using System.Data.Entity;

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

        private Product GetProduct(int productID)
        {
            if (productID == 0)
            {
                return new Product();
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


        public void Save(Product product)
        {
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntity = context.Products.Find(product.ProductID);
                if(dbEntity != null)
                {
                    dbEntity.Name = product.Name;
                    dbEntity.Description = product.Description;
                    dbEntity.Category = product.Category;
                    //dbEntity.Color = product.Color;
                    dbEntity.Price = product.Price;
                    //dbEntity.Image = product.Image;
                   // dbEntity.ImageMimeType = product.ImageMimeType;
                }
            }
            context.SaveChanges();
        }
    }
}