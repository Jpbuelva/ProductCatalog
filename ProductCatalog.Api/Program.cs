using FluentValidation;
using ProductCatalog.Api.Middleware;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.Application.Mapping;
using ProductCatalog.Application.Services;
using ProductCatalog.Infrastructure.Persistence; 
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Inyección de dependencias
builder.Services.AddSingleton<FileContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new ProductMappingProfile());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Nueva integración FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<ProductDtoValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.MapControllers();
app.Run();
