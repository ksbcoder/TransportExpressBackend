using Dapper;
using TransportExpress.Domain.Common;
using TransportExpress.Infrastructure.SQLAdapter.Gateway;
using TransportExpress.UseCases.IRepositories;

namespace TransportExpress.Infrastructure.SQLAdapter.Repositories
{
    public class Storage : IStorage
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string _tableNameStorage = "Storage";

        public Storage(IDbConnectionBuilder dbConnectionBuilder)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
        }

        public async Task<List<Domain.Entities.Storage>> GetStoragesAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string query = $"SELECT * FROM {_tableNameStorage}";
            var storagesFound = (from storage in await connection.QueryAsync<Domain.Entities.Storage>(query)
                                 where storage.StateStorage == Enums.StateEntity.Active
                                 select storage).ToList();
            connection.Close();
            return storagesFound;
        }
    }
}