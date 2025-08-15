using System.Data;

namespace ProductWarehouse.Core.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}