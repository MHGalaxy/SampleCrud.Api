using SampleCrud.Application.DTOs;
using SampleCrud.Domain.Entities;
using SampleCrud.Infrastructure.Repositories;

namespace SampleCrud.Application.Services;

public class ProviderService
{
    private readonly IProviderRepository _providerRepository;

    public ProviderService(IProviderRepository repo)
    {
        _providerRepository = repo;
    }

    public async Task<IEnumerable<ProviderDto>> GetAllAsync()
    {
        var providers = await _providerRepository.GetAllAsync();
        return providers.Select(p => new ProviderDto
        {
            ProviderId = p.ProviderId,
            Name = p.Name,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt
        });
    }

    public async Task<ProviderDto?> GetByIdAsync(int providerId)
    {
        var p = await _providerRepository.GetByIdAsync(providerId);
        if (p is null) return null;

        return new ProviderDto
        {
            ProviderId = p.ProviderId,
            Name = p.Name,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt
        };
    }

    public async Task<int> CreateAsync(CreateProviderDto dto)
    {
        var provider = new Provider
        {
            Name = dto.Name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };

        return await _providerRepository.CreateAsync(provider);
    }

    public async Task<bool> UpdateAsync(int providerId, UpdateProviderDto dto)
    {
        var existing = await _providerRepository.GetByIdAsync(providerId);
        if (existing is null) return false;

        existing.Name = dto.Name;
        existing.UpdatedAt = DateTime.UtcNow;

        return await _providerRepository.UpdateAsync(existing);
    }

    public Task<bool> DeleteAsync(int providerId) => _providerRepository.DeleteAsync(providerId);
}