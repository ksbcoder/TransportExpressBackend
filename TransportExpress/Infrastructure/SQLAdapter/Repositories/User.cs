using Dapper;
using TransportExpress.Domain.Common;
using TransportExpress.Infrastructure.SQLAdapter.Gateway;
using TransportExpress.UseCases.IRepositories;
using TransportExpress.Wrappers;

namespace TransportExpress.Infrastructure.SQLAdapter.Repositories
{
    public class User : IUser
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string _tableNameUser = "Users";

        public User(IDbConnectionBuilder dbConnectionBuilder)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
        }

        public async Task<List<Domain.Entities.User>> GetUsersAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string query = $"SELECT * FROM {_tableNameUser}";
            var usersFound = (from client in await connection.QueryAsync<Domain.Entities.User>(query)
                                where client.StateUser == Enums.StateEntity.Active
                                select client).ToList();
            connection.Close();
            return usersFound.Count == 0 ?
                throw new ApiException("There are no users available.", 204) :
                usersFound;
        }
    }
}
