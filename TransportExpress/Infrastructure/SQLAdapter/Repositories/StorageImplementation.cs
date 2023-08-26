using Dapper;
using TransportExpress.Domain.Common;
using TransportExpress.Domain.Entities;
using TransportExpress.Infrastructure.SQLAdapter.Gateway;
using TransportExpress.UseCases.IRepositories;
using TransportExpress.Wrappers;

namespace TransportExpress.Infrastructure.SQLAdapter.Repositories
{
    public class StorageImplementation : IStorage
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string _tableNameStorage = "Storage";

        public StorageImplementation(IDbConnectionBuilder dbConnectionBuilder)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
        }

        public async Task<List<Storage>> GetStoragesAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string query = $"SELECT * FROM {_tableNameStorage}";
            var storagesFound = (from storage in await connection.QueryAsync<Storage>(query)
                                 where storage.StateStorage == Enums.StateEntity.Active
                                 select storage).ToList();
            connection.Close();
            return storagesFound.Count == 0 ?
                throw new ApiException("There are no storages available.", 204) :
                storagesFound;
        }
    }
}