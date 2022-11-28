// LightningBits
using System;
using Microsoft.EntityFrameworkCore;

namespace SharedServices.Data
{
    public class ApplicationDbContext : DbContext
    {
        //internal object OrderHeaders;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Category> ECommerceCategories { get; set; }

        public DbSet<Product> ECommerceProducts { get; set; } 

        public DbSet<ProductPrice> ECommerceProductPrices { get; set; }

        public DbSet<OrderHeader> OrderHeaders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}

