using AutoMapper;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>(); 
            CreateMap<Product, AddProductDto>().ReverseMap( )
           
        .ConstructUsing(dto => Product.Create(
            dto.Name,
            dto.Description,
            dto.Price,
            dto.Stock,
            dto.Category
        ));
        }
    }
}
