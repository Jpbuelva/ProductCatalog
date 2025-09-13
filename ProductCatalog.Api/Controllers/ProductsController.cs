using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.Api.Models;

namespace ProductCatalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _service.GetAllAsync();
        return Ok(new ApiResponse<IEnumerable<ProductDto>>(products));
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _service.GetByIdAsync(id);
        if (product is null)
            return NotFound(new ApiResponse<ProductDto>(null, false, "Product not found"));
        return Ok(new ApiResponse<ProductDto>(product));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddProductDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<AddProductDto>(null, false, "Invalid model"));

        var filePath = await _service.CreateAsync(dto);
        return Created(string.Empty, new ApiResponse<string>(filePath, true, "Product created", filePath));
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProductDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<ProductDto>(null, false, "Invalid model"));

        var updated = await _service.UpdateAsync(id, dto);
        if (updated is null)
            return NotFound(new ApiResponse<ProductDto>(null, false, "Product not found"));
        return Ok(new ApiResponse<ProductDto>(updated, true, "Product updated"));
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound(new ApiResponse<bool>(false, false, "Product not found"));
        return Ok(new ApiResponse<bool>(true, true, "Product deleted"));
    }
}
