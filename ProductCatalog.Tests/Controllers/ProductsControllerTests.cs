using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductCatalog.Api.Controllers;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Interfaces;

namespace ProductCatalog.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _serviceMock;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _serviceMock = new Mock<IProductService>();
            _controller = new ProductsController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfProducts()
        {
            // Arrange
            var products = new List<ProductDto> { new ProductDto { Id = Guid.NewGuid(), Name = "Test", Description = "Desc", Price = 10, Stock = 1, Category = "Cat", CreatedAt = DateTime.UtcNow } };
            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(products, okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((ProductDto?)null);

            // Act
            var result = await _controller.GetById(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction_WithCreatedProduct()
        {
            // Arrange
            var dto = new ProductDto { Id = Guid.NewGuid(), Name = "Test", Description = "Desc", Price = 10, Stock = 1, Category = "Cat", CreatedAt = DateTime.UtcNow };
            _serviceMock.Setup(s => s.CreateAsync(dto)).ReturnsAsync(dto);

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(dto, createdResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenProductExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var product = new ProductDto { Id = id, Name = "Test", Description = "Desc", Price = 10, Stock = 1, Category = "Cat", CreatedAt = DateTime.UtcNow };
            _serviceMock.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(product);

            // Act
            var result = await _controller.GetById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(product, okResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsOk_WhenProductIsUpdated()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = new ProductDto { Id = id, Name = "Updated", Description = "Desc", Price = 20, Stock = 2, Category = "Cat", CreatedAt = DateTime.UtcNow };
            _serviceMock.Setup(s => s.UpdateAsync(id, dto)).ReturnsAsync(dto);

            // Act
            var result = await _controller.Update(id, dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(dto, okResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = new ProductDto { Id = id, Name = "Updated", Description = "Desc", Price = 20, Stock = 2, Category = "Cat", CreatedAt = DateTime.UtcNow };
            _serviceMock.Setup(s => s.UpdateAsync(id, dto)).ReturnsAsync((ProductDto?)null);

            // Act
            var result = await _controller.Update(id, dto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenProductIsDeleted()
        {
            // Arrange
            var id = Guid.NewGuid();
            _serviceMock.Setup(s => s.DeleteAsync(id)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            _serviceMock.Setup(s => s.DeleteAsync(id)).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}