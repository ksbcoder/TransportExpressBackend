using AutoMapper;
using Dapper;
using TransportExpress.Domain.Common;
using TransportExpress.Domain.Entities;
using TransportExpress.Infrastructure.SQLAdapter.Gateway;
using TransportExpress.UseCases.IRepositories;
using TransportExpress.Wrappers;

namespace TransportExpress.Infrastructure.SQLAdapter.Repositories
{
    public class StorageTypeImplementation : IStorageType
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string _tableNameStorageType = "StorageType";
        private readonly IMapper _mapper;

        public StorageTypeImplementation(IDbConnectionBuilder dbConnectionBuilder, IMapper mapper)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
            _mapper = mapper;
        }

        public async Task<StorageType> GetStorageTypeByIDAsync(string storageTypeID)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string query = $"SELECT * FROM {_tableNameStorageType}";
            var storageTypeFound = (from storage in await connection.QueryAsync<StorageType>(query)
                                    where storage.StateStorageType == Enums.StateEntity.Active
                                        && storage.StorageTypeID == Guid.Parse(storageTypeID)
                                    select storage).FirstOrDefault();
            connection.Close();
            return storageTypeFound ?? throw new ApiException("Storage type not found.", 404);
        }

        public async Task<List<StorageType>> GetStorageTypesAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string query = $"SELECT * FROM {_tableNameStorageType}";
            var storageTypesFound = (from storageType in await connection.QueryAsync<StorageType>(query)
                                     where storageType.StateStorageType == Enums.StateEntity.Active
                                     select storageType).ToList();
            connection.Close();
            return storageTypesFound.Count == 0 ?
                throw new ApiException("There are no storage types available.", 204) :
                storageTypesFound;
        }
    }
}