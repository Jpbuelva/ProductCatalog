using System.Text.Json;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Infrastructure.Persistence;

public class FileContext
{
    private readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "products.json");

    public async Task<List<Product>> LoadAsync()
    {
        if (!File.Exists(_filePath)) return new List<Product>();

        var json = await File.ReadAllTextAsync(_filePath);
        return string.IsNullOrWhiteSpace(json) ? new List<Product>() :
            JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
    }

    public async Task SaveAsync(List<Product> products)
    {
        var json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_filePath, json);
    }
}
