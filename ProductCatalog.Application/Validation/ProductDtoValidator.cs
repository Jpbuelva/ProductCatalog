using FluentValidation;
using ProductCatalog.Application.DTOs;

public class ProductDtoValidator : AbstractValidator<ProductDto>
{
    public ProductDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(2, 100);

        RuleFor(x => x.Description)
            .NotEmpty()
            .Length(5, 500);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("El precio debe ser mayor que cero.");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo.");

        RuleFor(x => x.Category)
            .NotEmpty()
            .Length(1, 50);
    }
}