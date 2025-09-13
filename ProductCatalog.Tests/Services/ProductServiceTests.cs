using AutoMapper;
using Moq;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.Application.Services;
using ProductCatalog.Domain.Entities;
using Xunit;

namespace ProductCatalog.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _repoMock = new Mock<IProductRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new ProductService(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedDtos()
        {
            // Arrange
            var products = new List<Product> { new Product("Test", "Desc", 10, 1, "Cat") };
            var dtos = new List<ProductDto> { new ProductDto { Name = "Test" } };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(products);
            _mapperMock.Setup(m => m.Map<IEnumerable<ProductDto>>(products)).Returns(dtos);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(dtos, result);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Product?)null);

            // Act
            var result = await _service.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMappedDto_WhenFound()
        {
            // Arrange
            var product = new Product("Test", "Desc", 10, 1, "Cat");
            var dto = new ProductDto { Name = "Test" };
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);
            _mapperMock.Setup(m => m.Map<ProductDto>(product)).Returns(dto);

            // Act
            var result = await _service.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Equal(dto, result);
        }

        [Fact]
        public async Task CreateAsync_AddsProduct_AndReturnsMappedDto()
        {
            // Arrange
            var dto = new ProductDto { Name = "Test", Description = "Desc", Price = 10, Stock = 1, Category = "Cat" };
            var product = new Product("Test", "Desc", 10, 1, "Cat");
            _mapperMock.Setup(m => m.Map<Product>(dto)).Returns(product);
            _mapperMock.Setup(m => m.Map<ProductDto>(product)).Returns(dto);

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            _repoMock.Verify(r => r.AddAsync(product), Times.Once);
            Assert.Equal(dto, result);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsNull_WhenNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Product?)null);

            // Act
            var result = await _service.UpdateAsync(Guid.NewGuid(), new ProductDto());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesProduct_AndReturnsMappedDto()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = new ProductDto { Name = "Updated", Description = "Desc", Price = 20, Stock = 2, Category = "Cat" };
            var product = new Product("Old", "OldDesc", 5, 1, "OldCat");
            _repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(product);
            _mapperMock.Setup(m => m.Map<ProductDto>(product)).Returns(dto);

            // Act
            var result = await _service.UpdateAsync(id, dto);

            // Assert
            _repoMock.Verify(r => r.UpdateAsync(product), Times.Once);
            Assert.Equal(dto, result);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalse_WhenNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Product?)null);

            // Act
            var result = await _service.DeleteAsync(Guid.NewGuid());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_DeletesProduct_AndReturnsTrue()
        {
            // Arrange
            var id = Guid.NewGuid();
            var product = new Product("Test", "Desc", 10, 1, "Cat");
            _repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(product);

            // Act
            var result = await _service.DeleteAsync(id);

            // Assert
            _repoMock.Verify(r => r.DeleteAsync(id), Times.Once);
            Assert.True(result);
        }
    }
}