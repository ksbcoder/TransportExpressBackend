using Ardalis.GuardClauses;
using AutoMapper;
using Dapper;
using TransportExpress.Domain.Common;
using TransportExpress.Domain.Common.Handlers;
using TransportExpress.Domain.DTO.Logistic;
using TransportExpress.Domain.Entities;
using TransportExpress.Domain.Queries.Logistic;
using TransportExpress.Infrastructure.SQLAdapter.Gateway;
using TransportExpress.UseCases.IRepositories;
using TransportExpress.Wrappers;

namespace TransportExpress.Infrastructure.SQLAdapter.Repositories
{
    public class LogisticImplementation : ILogistic
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string _tableNameLogistic = "Logistic";
        private readonly IMapper _mapper;

        public LogisticImplementation(IDbConnectionBuilder dbConnectionBuilder, IMapper mapper)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
            _mapper = mapper;
        }

        public async Task<Logistic> CreateLogisticAsync(Logistic logistic)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            Logistic.SetDetailsLogistic(logistic);

            var productFound = new ProductImplementation(_dbConnectionBuilder, _mapper).
                GetProductByIDAsync(logistic.ProductID.ToString()).Result;
            var transportFound = new TransportImplementation(_dbConnectionBuilder, _mapper).
                GetTransportByIDAsync(productFound.TransportID.ToString()).Result;

            var discount = LogisticHandler.CalculateDiscount(logistic, transportFound);
            logistic.SetDiscount(discount);

            string query = $"INSERT INTO {_tableNameLogistic} (ProductID, UserID, StorageID, RegisteredAt, DeliveredAt, " +
                                $"QuantityProduct, ShippingPrice, Discount, NumberPlate, FleetNumber, GuideNumber, StateLogistic) " +
                            $"VALUES (@ProductID, @UserID, @StorageID, @RegisteredAt, @DeliveredAt, " +
                                $"@QuantityProduct, @ShippingPrice, @Discount, @NumberPlate, @FleetNumber, @GuideNumber, @StateLogistic)";

            var logisticCreated = await connection.ExecuteAsync(query, logistic);
            connection.Close();
            return logisticCreated == 0 ?
                throw new ApiException("Logistic not created.", 400) :
                logistic;
        }

        public async Task<int> DeleteLogisticAsync(string logisticID)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var logisticFound = (await GetLogisticByIDAsync(logisticID));

            string query = $"UPDATE {_tableNameLogistic} SET StateLogistic = @StateLogistic WHERE LogisticID = @LogisticID";
            var logisticDeleted = await connection.ExecuteAsync(query, new
            {
                logisticFound.LogisticID,
                StateLogistic = Enums.StateEntity.Inactive
            });
            connection.Close();
            return logisticDeleted == 0 ?
                throw new ApiException("Logistic not deleted.", 400) :
                StatusCodes.Status200OK;
        }

        public async Task<LogisticDTO> GetLogisticByIDAsync(string logisticID)
        {

            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string query = $"SELECT * FROM {_tableNameLogistic}";
            #region Queries
            var logisticFound = (from logistic in await connection.QueryAsync<Logistic>(query)
                                 where logistic.StateLogistic == Enums.StateEntity.Active
                                    && logistic.LogisticID == Guid.Parse(logisticID)
                                 select logistic).SingleOrDefault() ?? throw new ApiException("Logistic not found.", 204);

            var productFound = new ProductImplementation(_dbConnectionBuilder, _mapper).
                GetProductByIDAsync(logisticFound.ProductID.ToString()).Result;

            var userFound = new UserImplementation(_dbConnectionBuilder, _mapper).
                GetUserByIDAsync(logisticFound.UserID.ToString()).Result;

            var transportFound = new TransportImplementation(_dbConnectionBuilder, _mapper).
                GetTransportByIDAsync(productFound.TransportID.ToString()).Result;

            var storageFound = new StorageImplementation(_dbConnectionBuilder, _mapper).
                GetStorageByIDAsync(logisticFound.StorageID.ToString()).Result;

            var storageTypeFound = new StorageTypeImplementation(_dbConnectionBuilder, _mapper).
                GetStorageTypeByIDAsync(storageFound.StorageTypeID.ToString()).Result;
            #endregion

            connection.Close();

            Guard.Against.Null(productFound, nameof(productFound));
            Guard.Against.Null(userFound, nameof(userFound));
            Guard.Against.Null(transportFound, nameof(transportFound));
            Guard.Against.Null(storageFound, nameof(storageFound));
            Guard.Against.Null(storageTypeFound, nameof(storageTypeFound));

            var logisticDTO = new LogisticDTO(logisticFound, productFound, userFound, storageFound, storageTypeFound, transportFound);
            return logisticDTO ?? throw new ApiException("Logistic not found.", 424);
        }

        public async Task<List<Logistic>> GetLogisticsAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string query = $"SELECT * FROM {_tableNameLogistic}";
            var logisticsFound = (from logistic in await connection.QueryAsync<Logistic>(query)
                                  where logistic.StateLogistic == Enums.StateEntity.Active
                                  select logistic).ToList();
            connection.Close();
            return logisticsFound.Count == 0 ?
                throw new ApiException("There are no logistics available.", 204) :
                logisticsFound;
        }

        public async Task<List<Logistic>> GetLogisticsByFilterAsync(LogisticFilters filterOption)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string query = $"SELECT * FROM {_tableNameLogistic} ";

            var filterConditions = new List<string>();

            void AddConditionIfNotEmpty(string value, string columnName)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    filterConditions.Add($"{columnName} = '{value}'");
                }
            }

            AddConditionIfNotEmpty(filterOption.Logistic, "LogisticID");
            AddConditionIfNotEmpty(filterOption.Product, "ProductID");
            AddConditionIfNotEmpty(filterOption.Client, "UserID");
            AddConditionIfNotEmpty(filterOption.Storage, "StorageID");

            if (filterConditions.Count > 0)
            {
                string conditions = string.Join(" AND ", filterConditions);
                query += $"WHERE {conditions}";
            }

            Console.WriteLine(query);
            var logisticsFound = (from logistic in await connection.QueryAsync<Logistic>(query)
                                  where logistic.StateLogistic == Enums.StateEntity.Active
                                  select logistic).ToList();

            connection.Close();
            return logisticsFound.Count == 0 ?
                throw new ApiException("There are no logistics available.", 204) :
                logisticsFound;
        }

        public async Task<Logistic> UpdateLogisticAsync(string logisticID, Logistic logistic)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var logisticFound = (await GetLogisticByIDAsync(logisticID));

            var logisticToUpdate = LogisticHandler.SetNewValuesLogistic(_mapper.Map<Logistic>(logisticFound), logistic);
            var productFound = new ProductImplementation(_dbConnectionBuilder, _mapper).
                    GetProductByIDAsync(logisticToUpdate.ProductID.ToString()).Result;

            if (logistic.QuantityProduct != logisticFound.QuantityProduct || logistic.ProductID != logisticFound.ProductID)
            {
                var transportFound = new TransportImplementation(_dbConnectionBuilder, _mapper).
                    GetTransportByIDAsync(productFound.TransportID.ToString()).Result;

                var discount = LogisticHandler.CalculateDiscount(logisticToUpdate, transportFound);
                logisticToUpdate.SetDiscount(discount);
            }

            string query = $"UPDATE {_tableNameLogistic} SET ProductID = @ProductID, UserID = @UserID, StorageID = @StorageID, " +
                                $"RegisteredAt = @RegisteredAt, DeliveredAt = @DeliveredAt, QuantityProduct = @QuantityProduct, " +
                                $"ShippingPrice = @ShippingPrice, Discount = @Discount, NumberPlate = @NumberPlate, " +
                                $"FleetNumber = @FleetNumber, GuideNumber = @GuideNumber, StateLogistic = @StateLogistic " +
                            $"WHERE LogisticID = @LogisticID";

            var logisticUpdated = await connection.ExecuteAsync(query, logisticToUpdate);
            connection.Close();
            return logisticUpdated == 0 ?
                throw new ApiException("Logistic not updated.", 400) :
                logisticToUpdate;
        }
    }
}