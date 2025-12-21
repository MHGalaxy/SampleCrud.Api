using Dapper;
using SampleCrud.Domain.Entities;
using SampleCrud.Infrastructure.Database;

namespace SampleCrud.Infrastructure.Repositories;

public class ProviderRepository : IProviderRepository
{
    private readonly DapperContext _context;

    public ProviderRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Provider>> GetAllAsync()
    {
        const string sql = @"SELECT ProviderId, Name, CreatedAt, UpdatedAt FROM Providers;";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Provider>(sql);
    }

    public async Task<Provider?> GetByIdAsync(int providerId)
    {
        const string sql = @"SELECT ProviderId, Name, CreatedAt, UpdatedAt
                                 FROM Providers WHERE ProviderId = @ProviderId;";
        using var connection = _context.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Provider>(sql, new { ProviderId = providerId });
    }

    public async Task<bool> ExistsAsync(int providerId)
    {
        const string sql = @"SELECT COUNT(1) FROM Providers WHERE ProviderId = @ProviderId;";
        using var connection = _context.CreateConnection();
        var count = await connection.ExecuteScalarAsync<long>(sql, new { ProviderId = providerId });
        return count > 0;
    }

    public async Task<int> CreateAsync(Provider provider)
    {
        const string sql = @"
                INSERT INTO Providers (Name, CreatedAt, UpdatedAt)
                VALUES (@Name, @Email, @Phone, @CreatedAt, @UpdatedAt);
                SELECT last_insert_rowid();";

        using var connection = _context.CreateConnection();
        var id = await connection.ExecuteScalarAsync<long>(sql, new
        {
            provider.Name,
            CreatedAt = provider.CreatedAt.ToString("O"),
            UpdatedAt = provider.UpdatedAt?.ToString("O")
        });

        return (int)id;
    }

    public async Task<bool> UpdateAsync(Provider provider)
    {
        const string sql = @"
                UPDATE Providers
                SET Name = @Name,
                    UpdatedAt = @UpdatedAt
                WHERE ProviderId = @ProviderId;";

        using var connection = _context.CreateConnection();
        var affected = await connection.ExecuteAsync(sql, new
        {
            provider.ProviderId,
            provider.Name,
            UpdatedAt = provider.UpdatedAt?.ToString("O")
        });

        return affected > 0;
    }

    public async Task<bool> DeleteAsync(int providerId)
    {
        const string sql = @"DELETE FROM Providers WHERE ProviderId = @ProviderId;";
        using var connection = _context.CreateConnection();
        var affected = await connection.ExecuteAsync(sql, new { ProviderId = providerId });
        return affected > 0;
    }
}