using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;

        public DbInitializer(StoreDbContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            try
            {
                // database found or not
                //if(_context.Database.GetPendingMigrations().Any())
                //    _context.Database.Migrate();

                if (!_context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText(@"..\Persistence\Data\Seeding\types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    if (types is not null && types.Any())
                    {
                        _context.ProductTypes.AddRange(types);
                        _context.SaveChanges();
                    }
                }

                if (!_context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText(@"..\Persistence\Data\Seeding\brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    if (brands is not null && brands.Any())
                    {
                        _context.ProductBrands.AddRange(brands);
                        _context.SaveChanges();
                    }
                }

                if (!_context.Products.Any())
                {
                    var ProductsData = File.ReadAllText(@"..\Persistence\Data\Seeding\products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                    if (Products is not null && Products.Any())
                    {
                        _context.Products.AddRange(Products);
                        _context.SaveChanges();
                    }
                }

            }
            catch (Exception)
            {

            }
        }
    }
}
