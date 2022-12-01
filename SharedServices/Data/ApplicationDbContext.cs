// LightningBits
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SharedServices.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
        //public class ApplicationDbContext : DbContext
    //public class ECommerce_ServerIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        //     public ECommerce_ServerIdentityDbContext(DbContextOptions<ECommerce_ServerIdentityDbContext> options)
        //: base(options)
        {

        }

        public DbSet<Category> ECommerceCategories { get; set; }

        public DbSet<Product> ECommerceProducts { get; set; } 

        public DbSet<ProductPrice> ECommerceProductPrices { get; set; }

        public DbSet<OrderHeader> OrderHeaders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}

