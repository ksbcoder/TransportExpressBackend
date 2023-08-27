using TransportExpress.Domain.Entities;

namespace TransportExpress.UseCases.IRepositories
{
    public interface IUser
    {
        Task<List<User>> GetClientsAsync();
        Task<User> GetUserByUidUserAsync(string uidUser);
        Task<User> GetUserByIDAsync(string userID);
        Task<User> CreateUser(User user);
    }
}