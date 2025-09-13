using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductCatalog.Api.Controllers;
using ProductCatalog.Api.Models;
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
        public async Task GetAll_ReturnsOk_WithApiResponse()
        {
            // Arrange
            var products = new List<ProductDto>
            {
                new ProductDto { Id = Guid.NewGuid(), Name = "Test", Description = "Desc", Price = 10, Stock = 1, Category = "Cat", CreatedAt = DateTime.UtcNow }
            };
            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<IEnumerable<ProductDto>>>(okResult.Value);
            Assert.True(response.Success);
            Assert.Equal(products, response.Data);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                        .ReturnsAsync((ProductDto?)null);

            // Act
            var result = await _controller.GetById(Guid.NewGuid());

            // Assert     
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);      
            var response = Assert.IsType<ApiResponse<ProductDto>>(notFoundResult.Value); 
            Assert.False(response.Success);
            Assert.Equal("Product not found", response.Message);
            Assert.Null(response.Data);
        }
        [Fact]
        public async Task GetById_ReturnsOk_WhenProductExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var product = new ProductDto
            {
                Id = id,
                Name = "Test",
                Description = "Desc",
                Price = 10,
                Stock = 1,
                Category = "Cat",
                CreatedAt = DateTime.UtcNow
            };
            _serviceMock.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(product);

            // Act
            var result = await _controller.GetById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);  
            var response = Assert.IsType<ApiResponse<ProductDto>>(okResult.Value);
            Assert.True(response.Success);  
            Assert.Equal(product, response.Data);  
            Assert.Null(response.Message);  
        }

        [Fact]
        public async Task Create_ReturnsCreated_WithApiResponse()
        {
            // Arrange
            var dto = new AddProductDto { Name = "Test", Description = "Desc", Price = 10, Stock = 1, Category = "Cat", CreatedAt = DateTime.UtcNow };
            var expectedPath = "products/123";
            _serviceMock.Setup(s => s.CreateAsync(dto)).ReturnsAsync(expectedPath);

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            var response = Assert.IsType<ApiResponse<string>>(createdResult.Value);
            Assert.True(response.Success);
            Assert.Equal("Product created", response.Message);
            Assert.Equal(expectedPath, response.Data);
        }

        [Fact]
        public async Task Update_ReturnsOk_WhenProductUpdated()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = new ProductDto { Id = id, Name = "Updated", Description = "Desc", Price = 20, Stock = 2, Category = "Cat", CreatedAt = DateTime.UtcNow };
            _serviceMock.Setup(s => s.UpdateAsync(id, dto)).ReturnsAsync(dto);

            // Act
            var result = await _controller.Update(id, dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<ProductDto>>(okResult.Value);
            Assert.True(response.Success);
            Assert.Equal(dto, response.Data);
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
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var response = Assert.IsType<ApiResponse<ProductDto>>(notFoundResult.Value);
            Assert.False(response.Success);
            Assert.Equal("Product not found", response.Message);
        }

        [Fact]
        public async Task Delete_ReturnsOk_WhenProductDeleted()
        {
            // Arrange
            var id = Guid.NewGuid();
            _serviceMock.Setup(s => s.DeleteAsync(id)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<bool>>(okResult.Value);
            Assert.True(response.Success);
            Assert.True(response.Data);
            Assert.Equal("Product deleted", response.Message);
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
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var response = Assert.IsType<ApiResponse<bool>>(notFoundResult.Value);
            Assert.False(response.Success);
            Assert.False(response.Data);
            Assert.Equal("Product not found", response.Message);
        }
    }
}
