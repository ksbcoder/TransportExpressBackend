using TransportExpress.Domain.Entities;

namespace TransportExpress.UseCases.IRepositories
{
    public interface IStorage
    {
        Task<List<Storage>> GetStoragesAsync();
        Task<Storage> CreateStorageAsync(Storage storage);
    }
}