using TransportExpress.Domain.Entities;

namespace TransportExpress.UseCases.IRepositories
{
    public interface IClient
    {
        Task<List<Client>> GetClientsAsync();
    }
}