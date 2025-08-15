using Microsoft.Extensions.Configuration;

using Npgsql;

using System.Data;

namespace ProductWarehouse.Infrastructure.Abstractions;

public class DbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")!;

    public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
}