// LightningBits
using System;
using Microsoft.EntityFrameworkCore;

namespace BlazorWeb.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Category> ECommerceCategories { get; set; }
    }
}

