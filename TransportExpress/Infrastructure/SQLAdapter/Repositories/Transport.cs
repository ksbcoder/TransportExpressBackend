using Dapper;
using TransportExpress.Domain.Common;
using TransportExpress.Infrastructure.SQLAdapter.Gateway;
using TransportExpress.UseCases.IRepositories;
using TransportExpress.Wrappers;

namespace TransportExpress.Infrastructure.SQLAdapter.Repositories
{
    public class Transport : ITransport
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string _tableNameTransport = "Transport";

        public Transport(IDbConnectionBuilder dbConnectionBuilder)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
        }

        public async Task<List<Domain.Entities.Transport>> GetTransportsAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            string query = $"SELECT * FROM {_tableNameTransport}";
            var transportsFound = (from transport in await connection.QueryAsync<Domain.Entities.Transport>(query)
                                   where transport.StateTransport == Enums.StateEntity.Active
                                   select transport).ToList();
            connection.Close();
            return transportsFound.Count == 0 ?
                throw new ApiException("There are no transports available.", 204) :
                transportsFound;
        }
    }
}