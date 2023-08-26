using Ardalis.GuardClauses;
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

        public async Task<Storage> CreateStorageAsync(Storage storage)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            Storage.SetDetailsStorage(storage);

            Guard.Against.Null(storage, nameof(storage));
            Guard.Against.NullOrEmpty(storage.StorageTypeID, nameof(storage.StorageTypeID));
            Guard.Against.NullOrEmpty(storage.NameStorage, nameof(storage.NameStorage));
            Guard.Against.Null(storage.CapacityStorage, nameof(storage.CapacityStorage));
            Guard.Against.NegativeOrZero(storage.CapacityStorage, nameof(storage.CapacityStorage));
            Guard.Against.NullOrEmpty(storage.Location, nameof(storage.Location));
            Guard.Against.EnumOutOfRange(storage.StateStorage, nameof(storage.StateStorage));

            string query = $"INSERT INTO {_tableNameStorage} (StorageTypeID, NameStorage, CapacityStorage, " +
                            $"Location, StateStorage) " +
                            $"VALUES (@StorageTypeID, @NameStorage, @CapacityStorage, @Location, @StateStorage)";

            var storageCreated = await connection.ExecuteAsync(query, storage);
            connection.Close();

            return storageCreated == 0 ?
                throw new ApiException("Storage not created.", 400) :
                storage;
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