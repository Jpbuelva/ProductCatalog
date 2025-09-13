using System.Text.Json.Serialization;

namespace ProductCatalog.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public string Category { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    public Product() { }

    [JsonConstructor]
    public Product(Guid id, string name, string description, decimal price, int stock, string category, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        Category = category;
        CreatedAt = createdAt;
    }

    public static Product Create(string name, string description, decimal price, int stock, string category)
    {
        return new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Price = price,
            Stock = stock,
            Category = category,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void UpdateDetails(string name, string description, decimal price, string category)
    {
        Name = name;
        Description = description;
        Price = price;
        Category = category;
    }

    public void UpdateStock(int stock)
    {
        if (stock < 0) throw new InvalidOperationException("Stock no puede ser negativo.");
        Stock = stock;
    }
}
