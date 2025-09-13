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

    public async Task<string> SaveAsync(List<Product> products)
    {
        try
        {
            // Asegura que el directorio existe
            var directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
            return _filePath;
        }
        catch (Exception ex)
        {
            throw new IOException($"Error saving products to file: {_filePath}", ex);
        }
    }
}
