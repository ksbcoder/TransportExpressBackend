using Ardalis.GuardClauses;
using Dapper;
using TransportExpress.Domain.Common;
using TransportExpress.Domain.Entities;
using TransportExpress.Infrastructure.SQLAdapter.Gateway;
using TransportExpress.UseCases.IRepositories;
using TransportExpress.Wrappers;

namespace TransportExpress.Infrastructure.SQLAdapter.Repositories
{
    public class UserImplementation : IUser
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string _tableNameUser = "Users";

        public UserImplementation(IDbConnectionBuilder dbConnectionBuilder)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
        }

        public async Task<User> CreateUser(User user)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            User.SetDetailsToUser(user);

            Guard.Against.Null(user, nameof(user));
            Guard.Against.NullOrEmpty(user.UidUser, nameof(user.UidUser));
            Guard.Against.NullOrEmpty(user.NameUser, nameof(user.NameUser));
            Guard.Against.NullOrEmpty(user.Identification, nameof(user.Identification));
            Guard.Against.NullOrEmpty(user.Phone, nameof(user.Phone));
            Guard.Against.NullOrEmpty(user.Email, nameof(user.Email));
            Guard.Against.NullOrEmpty(user.Address, nameof(user.Address));
            Guard.Against.EnumOutOfRange(user.TypeUser, nameof(user.TypeUser));
            Guard.Against.EnumOutOfRange(user.StateUser, nameof(user.StateUser));

            string query = $"INSERT INTO {_tableNameUser} (UidUser, NameUser, Identification, Phone, Email, " +
                            $"Address, TypeUser, StateUser) " +
                            $"VALUES (@UidUser, @NameUser, @Identification, @Phone, @Email, @Address, " +
                            $"@TypeUser, @StateUser)";

            var userCreated = await connection.ExecuteAsync(query, user);
            connection.Close();

            return userCreated == 0 ?
                throw new ApiException("User not created.", 400) :
                user;
        }

        public async Task<User> GetUserByUidUserAsync(string uidUser)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string query = $"SELECT * FROM {_tableNameUser}";
            var userFound = (from user in await connection.QueryAsync<User>(query)
                             where user.UidUser == uidUser && user.StateUser == Enums.StateEntity.Active
                             select user).SingleOrDefault();
            connection.Close();
            return userFound ?? throw new ApiException("User not found.", 404);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string query = $"SELECT * FROM {_tableNameUser}";
            var usersFound = (from user in await connection.QueryAsync<User>(query)
                              where user.StateUser == Enums.StateEntity.Active
                              select user).ToList();
            connection.Close();
            return usersFound.Count == 0 ?
                throw new ApiException("There are no users available.", 204) :
                usersFound;
        }
    }
}