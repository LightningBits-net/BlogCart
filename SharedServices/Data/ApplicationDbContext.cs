// LightningBits
using System;
using Microsoft.EntityFrameworkCore;

namespace SharedServices.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Category> ECommerceCategories { get; set; }

        public DbSet<Product> ECommmerceProducts { get; set; }
    }
}

