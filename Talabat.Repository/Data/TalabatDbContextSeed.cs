using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Eintites;

namespace Talabat.Repository.Data
{
    public static class TalabatDbContextSeed
    {
        public static async Task SeedAsync(TalabatDbContext dbContext)
        {

            #region ProductBrand
            if (!dbContext.ProductBrands.Any())
            {
                var BrandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
                if (Brands?.Count > 0)
                {
                    foreach (var brand in Brands)
                    {
                        await dbContext.Set<ProductBrand>().AddAsync(brand);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
            #endregion
            #region ProductType
            if (!dbContext.ProductTypes.Any())//دي بنعملها عشان ميعملش للداتا سيييد اكتر من مرة 
            {
                var TypeData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);
                if (Types?.Count > 0)
                {
                    foreach (var type in Types)
                    {
                        await dbContext.Set<ProductType>().AddAsync(type);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
            #endregion
            #region Products
            if (!dbContext.Products.Any())
            {
                var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                if (Products?.Count > 0)
                {
                    foreach (var product in Products)
                    {
                        await dbContext.Set<Product>().AddAsync(product);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
            #endregion

        }
    }
}
