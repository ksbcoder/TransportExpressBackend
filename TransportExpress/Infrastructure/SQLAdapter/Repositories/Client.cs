using Dapper;
using TransportExpress.Domain.Common;
using TransportExpress.Infrastructure.SQLAdapter.Gateway;
using TransportExpress.UseCases.IRepositories;

namespace TransportExpress.Infrastructure.SQLAdapter.Repositories
{
    public class Client : IClient
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string _tableNameClient = "Client";

        public Client(IDbConnectionBuilder dbConnectionBuilder)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
        }

        public async Task<List<Domain.Entities.Client>> GetClientsAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string query = $"SELECT * FROM {_tableNameClient}";
            var clientsFound = (from client in await connection.QueryAsync<Domain.Entities.Client>(query)
                               where client.StateClient == Enums.StateEntity.Active
                               select client).ToList();
            connection.Close();
            return clientsFound;
        }
    }
}
