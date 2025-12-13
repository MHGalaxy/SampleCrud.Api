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
        const string sql = "SELECT Id, Name, Price, Stock, CreatedAt FROM Products;";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Product>(sql);
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        const string sql = "SELECT Id, Name, Price, Stock, CreatedAt FROM Products WHERE Id = @Id;";
        using var connection = _context.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });
    }

    public async Task<int> CreateAsync(Product product)
    {
        const string sql = @"
                INSERT INTO Products (Name, Price, Stock, CreatedAt)
                VALUES (@Name, @Price, @Stock, @CreatedAt);
                SELECT last_insert_rowid();";

        using var connection = _context.CreateConnection();
        var id = await connection.ExecuteScalarAsync<long>(sql, product);
        return (int)id;
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        const string sql = @"
                UPDATE Products
                SET Name = @Name,
                    Price = @Price,
                    Stock = @Stock
                WHERE Id = @Id;";

        using var connection = _context.CreateConnection();
        var affected = await connection.ExecuteAsync(sql, product);
        return affected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        const string sql = "DELETE FROM Products WHERE Id = @Id;";
        using var connection = _context.CreateConnection();
        var affected = await connection.ExecuteAsync(sql, new { Id = id });
        return affected > 0;
    }
}