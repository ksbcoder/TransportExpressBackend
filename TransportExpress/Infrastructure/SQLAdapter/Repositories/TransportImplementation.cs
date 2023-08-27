using Ardalis.GuardClauses;
using AutoMapper;
using Dapper;
using TransportExpress.Domain.Common;
using TransportExpress.Domain.Entities;
using TransportExpress.Infrastructure.SQLAdapter.Gateway;
using TransportExpress.UseCases.IRepositories;
using TransportExpress.Wrappers;

namespace TransportExpress.Infrastructure.SQLAdapter.Repositories
{
    public class TransportImplementation : ITransport
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string _tableNameTransport = "Transport";
        private readonly IMapper _mapper;

        public TransportImplementation(IDbConnectionBuilder dbConnectionBuilder, IMapper mapper)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
            _mapper = mapper;
        }

        public async Task<Transport> CreateTransportAsync(Transport transport)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            Transport.SetDetailsTransport(transport);

            Guard.Against.Null(transport, nameof(transport));
            Guard.Against.NullOrEmpty(transport.DescriptionTransport, nameof(transport.DescriptionTransport));
            Guard.Against.Null(transport.CapacityTransport, nameof(transport.CapacityTransport));
            Guard.Against.NegativeOrZero(transport.CapacityTransport, nameof(transport.CapacityTransport));
            Guard.Against.EnumOutOfRange(transport.StateTransport, nameof(transport.StateTransport));

            string query = $"INSERT INTO {_tableNameTransport} (DescriptionTransport, CapacityTransport, StateTransport) " +
                $"VALUES (@DescriptionTransport, @CapacityTransport, @StateTransport)";

            var transportCreated = await connection.ExecuteAsync(query, transport);
            connection.Close();
            return transportCreated == 0 ?
                throw new ApiException("Transport not created.", 400) :
                transport;
        }

        public async Task<Transport> GetTransportByIDAsync(string transportID)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string query = $"SELECT * FROM {_tableNameTransport}";
            var transportFound = (from transport in await connection.QueryAsync<Transport>(query)
                                  where transport.TransportID == Guid.Parse(transportID) 
                                  && transport.StateTransport == Enums.StateEntity.Active
                                  select transport).SingleOrDefault();
            connection.Close();
            return transportFound ?? throw new ApiException("Transport not found.", 404);
        }

        public async Task<List<Transport>> GetTransportsAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            string query = $"SELECT * FROM {_tableNameTransport}";
            var transportsFound = (from transport in await connection.QueryAsync<Transport>(query)
                                   where transport.StateTransport == Enums.StateEntity.Active
                                   select transport).ToList();
            connection.Close();
            return transportsFound.Count == 0 ?
                throw new ApiException("There are no transports available.", 204) :
                transportsFound;
        }
    }
}