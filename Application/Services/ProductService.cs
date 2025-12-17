using SampleCrud.Application.DTOs;
using SampleCrud.Domain.Entities;
using SampleCrud.Infrastructure.Repositories;

namespace SampleCrud.Application.Services;

public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _repository.GetAllAsync();
        return products.Select(product => new ProductDto
        {
            ProductId = product.ProductId,
            Title = product.Title,
            Description = product.Description,
            ProviderId = product.ProviderId,
            Price = product.Price,
            ImageSrc = product.ImageSrc,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
        });
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null) return null;

        return new ProductDto
        {
            ProductId = product.ProductId,
            Title = product.Title,
            Description = product.Description,
            ProviderId = product.ProviderId,
            Price = product.Price,
            ImageSrc = product.ImageSrc,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
        };
    }

    public async Task<int> CreateAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Title = dto.Title,
            Description = dto.Description,
            ProviderId = dto.ProviderId,
            Price = dto.Price,
            ImageSrc = dto.ImageSrc,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };

        return await _repository.CreateAsync(product);
    }

    public async Task<bool> UpdateAsync(int productId, UpdateProductDto dto)
    {
        var existing = await _repository.GetByIdAsync(productId);
        if (existing is null) return false;

        existing.Title = dto.Title;
        existing.Description = dto.Description;
        existing.ProviderId = dto.ProviderId;
        existing.Price = dto.Price;
        existing.ImageSrc = dto.ImageSrc;
        existing.UpdatedAt = DateTime.UtcNow;

        return await _repository.UpdateAsync(existing);
    }

    public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
}