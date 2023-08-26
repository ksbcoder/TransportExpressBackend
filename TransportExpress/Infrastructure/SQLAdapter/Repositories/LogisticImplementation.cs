using AutoMapper;
using Dapper;
using TransportExpress.Domain.Common;
using TransportExpress.Domain.Entities;
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

        public Task<Logistic> CreateLogisticAsync(Logistic logistic)
        {
            throw new NotImplementedException();
        }

        public Task<Logistic> DeleteLogisticAsync(string logisticID)
        {
            throw new NotImplementedException();
        }

        public Task<Logistic> GetLogisticByIDAsync(string logisticID)
        {
            throw new NotImplementedException();
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

        public Task<List<Logistic>> GetLogisticsByEntityIDAsync(string entityID)
        {
            throw new NotImplementedException();
        }

        public Task<Logistic> UpdateLogisticAsync(string logisticID, Logistic logistic)
        {
            throw new NotImplementedException();
        }
    }
}