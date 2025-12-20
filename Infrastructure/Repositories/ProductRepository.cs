using SampleCrud.Domain.Entities;
using SampleCrud.Infrastructure.Database;
using Dapper;

namespace SampleCrud.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DapperContext _context;

    public ProductRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        const string sql = @"
            SELECT ProductId, Title, Description, ProviderId,  CAST(Price AS REAL) AS Price, ImageSrc, CreatedAt, UpdatedAt 
            FROM Products;";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Product>(sql);
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        const string sql = @"
            SELECT ProductId, Title, Description, ProviderId, Price, ImageSrc, CreatedAt, UpdatedAt
            FROM Products
            WHERE ProductId = @ProductId;";
        using var connection = _context.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });
    }

    public async Task<int> CreateAsync(Product product)
    {
        const string sql = @"
            INSERT INTO Products (Title, Description, ProviderId, Price, ImageSrc, CreatedAt, UpdatedAt)
            VALUES (@Title, @Description, @ProviderId, @Price, @ImageSrc, @CreatedAt, @UpdatedAt);
            SELECT last_insert_rowid();";

        using var connection = _context.CreateConnection();
        var id = await connection.ExecuteScalarAsync<long>(sql, product);
        return (int)id;
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        const string sql = @"
            UPDATE Products
            SET
                Title = @Title,
                Description = @Description,
                ProviderId = @ProviderId,
                Price = @Price,
                ImageSrc = @ImageSrc,
                UpdatedAt = @UpdatedAt
            WHERE ProductId = @ProductId;";
        using var connection = _context.CreateConnection();
        var affected = await connection.ExecuteAsync(sql, product);
        return affected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        const string sql = "DELETE FROM Products WHERE ProductId = @ProductId;";
        using var connection = _context.CreateConnection();
        var affected = await connection.ExecuteAsync(sql, new { Id = id });
        return affected > 0;
    }
}