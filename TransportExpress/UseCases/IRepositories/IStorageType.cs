using TransportExpress.Domain.Entities;

namespace TransportExpress.UseCases.IRepositories
{
    public interface IStorageType
    {
        Task<List<StorageType>> GetStorageTypesAsync();
    }
}