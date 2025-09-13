using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Interfaces;

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
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _service.GetByIdAsync(id);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProductDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(id, dto);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        return !deleted ? NotFound() : NoContent();
    }
}
