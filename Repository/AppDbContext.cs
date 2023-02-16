using Core;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature()
            {
                Id = 1,
                Color = "kırmızı",
                Height = 100,
                Width = 10,
                ProductId = 1
            },
            new ProductFeature()
            {
                Id = 2,
                Color = "mavi",
                Height = 400,
                Width = 40,
                ProductId = 2

            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
