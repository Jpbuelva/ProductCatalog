using ProductCatalog.Application.Interfaces;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Infrastructure.Persistence;

public class ProductRepository : IProductRepository
{
    private readonly FileContext _context;

    public ProductRepository(FileContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync() => await _context.LoadAsync();

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        var products = await _context.LoadAsync();
        return products.FirstOrDefault(p => p.Id == id);
    }

    public async Task AddAsync(Product product)
    {
        var products = await _context.LoadAsync();
        products.Add(product);
        await _context.SaveAsync(products);
    }

    public async Task UpdateAsync(Product product)
    {
        var products = await _context.LoadAsync();
        var index = products.FindIndex(p => p.Id == product.Id);
        if (index != -1)
        {
            products[index] = product;
            await _context.SaveAsync(products);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var products = await _context.LoadAsync();
        var existing = products.FirstOrDefault(p => p.Id == id);
        if (existing != null)
        {
            products.Remove(existing);
            await _context.SaveAsync(products);
        }
    }
}
