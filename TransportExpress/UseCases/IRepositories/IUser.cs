using TransportExpress.Domain.Entities;

namespace TransportExpress.UseCases.IRepositories
{
    public interface IUser
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserByUidUserAsync(string uidUser);
        Task<User> CreateUser(User user);
    }
}