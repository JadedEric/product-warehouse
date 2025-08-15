using Npgsql;

using ProductWarehouse.Core.Interfaces;

using System.Data;

namespace ProductWarehouse.Infrastructure.Tests;

public class TestDbConnectionFactory(string connectionString) : IDbConnectionFactory
{
    private readonly string _connectionString = connectionString;

    public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
}
