using System.Data;

namespace TransportExpress.Infrastructure.SQLAdapter.Gateway
{
    public interface IDbConnectionBuilder
    {
        Task<IDbConnection> CreateConnectionAsync();
    }
}