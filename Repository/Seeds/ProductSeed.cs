using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new Product
            {
                Id = 1,
                CategoryId = 1,
                Name = "kalem1",
                Price = 100,
                Stock = 20,
                CreatedDate = DateTime.Now,

            },
            new Product
            {
                Id = 2,
                CategoryId = 1,
                Name = "kalem2",
                Price = 5400,
                Stock = 40,
                CreatedDate = DateTime.Now,

            },
            new Product
            {
                Id = 3,
                CategoryId = 2,
                Name = "kitap1",
                Price = 5400,
                Stock = 500,
                CreatedDate = DateTime.Now
            },
            new Product
            {
                Id = 4,
                CategoryId = 2,
                Name = "kitap2",
                Price = 300,
                Stock = 50,
                CreatedDate = DateTime.Now
            });
        }
    }
}
