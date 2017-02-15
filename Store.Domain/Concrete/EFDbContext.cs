using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Store.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Colour> Colours { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Colours)
                .WithMany(c => c.Products)
                .Map(pc => 
                {
                    pc.MapLeftKey("ProductID");
                    pc.MapRightKey("ColorId");
                    pc.ToTable("products_colours_junction");
                });
                
        }
    }
}