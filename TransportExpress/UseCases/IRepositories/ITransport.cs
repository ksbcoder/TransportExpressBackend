using TransportExpress.Domain.Entities;

namespace TransportExpress.UseCases.IRepositories
{
    public interface ITransport
    {
        Task<List<Transport>> GetTransportsAsync();
    }
}