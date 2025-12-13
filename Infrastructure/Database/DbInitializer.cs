using Dapper;

namespace SampleCrud.Infrastructure.Database;

public static class DbInitializer
{
    public static async Task InitializeAsync(DapperContext context)
    {
        const string sql = @"
                CREATE TABLE IF NOT EXISTS Products (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Price REAL NOT NULL,
                    Stock INTEGER NOT NULL,
                    CreatedAt TEXT NOT NULL
                );";

        using var connection = context.CreateConnection();
        await connection.ExecuteAsync(sql);
    }
}