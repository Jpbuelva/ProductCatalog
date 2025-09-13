using ProductCatalog.Application.DTOs;

namespace ProductCatalog.Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto?> GetByIdAsync(Guid id);
    Task<string> CreateAsync(AddProductDto dto);
    Task<ProductDto?> UpdateAsync(Guid id, ProductDto dto);
    Task<bool> DeleteAsync(Guid id);
}
