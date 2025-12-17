using Dapper;

namespace SampleCrud.Infrastructure.Database;

public static class DbInitializer
{
    public static async Task InitializeAsync(DapperContext context)
    {
        const string sql = @"
            CREATE TABLE IF NOT EXISTS Products (
                ProductId INTEGER PRIMARY KEY AUTOINCREMENT,
                Title TEXT NOT NULL,
                Description TEXT NULL,
                ProviderId INTEGER NOT NULL,
                Price NUMERIC NOT NULL,
                ImageSrc TEXT NULL,
                CreatedAt TEXT NOT NULL,
                UpdatedAt TEXT NULL
            );";

        using var connection = context.CreateConnection();
        await connection.ExecuteAsync(sql);
    }
}