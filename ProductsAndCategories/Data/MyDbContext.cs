using Microsoft.EntityFrameworkCore;
using ProductsAndCategories.Data.Entities;

namespace ProductsAndCategories.Data
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options):base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Category { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p=>p.ProdId);
            modelBuilder.Entity<Category>().HasKey(c=>c.CategId);
        }
    }
}
