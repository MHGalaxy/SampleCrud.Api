using SampleCrud.Domain.Entities;

namespace SampleCrud.Infrastructure.Repositories;

public interface IProviderRepository
{
    Task<IEnumerable<Provider>> GetAllAsync();
    Task<Provider?> GetByIdAsync(int providerId);
    Task<int> CreateAsync(Provider provider);
    Task<bool> UpdateAsync(Provider provider);
    Task<bool> DeleteAsync(int providerId);
    Task<bool> ExistsAsync(int providerId);
}