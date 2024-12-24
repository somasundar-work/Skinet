using System.Text.Json;
using Skinet.Data.Context;
using Skinet.Entities.Product;

namespace Skinet.Data.Seed;

public class StoreContextSeed
{
    /// <summary>
    /// Seeds the store context with initial data if the Products table is empty.
    /// </summary>
    /// <param name="storeContext">The store context to seed.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task SeedAsync(StoreContext storeContext)
    {
        if (!storeContext.Products.Any())
        {
            var productsData = File.ReadAllText(Constants.ContextConstants.ProductsJsonPath);
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            if (products != null)
            {
                foreach (var product in products)
                {
                    storeContext.Products.Add(product);
                }

                await storeContext.SaveChangesAsync();
            }
        }
    }
}
