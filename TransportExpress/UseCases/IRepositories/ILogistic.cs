using TransportExpress.Domain.Entities;

namespace TransportExpress.UseCases.IRepositories
{
    public interface ILogistic
    {
        Task<Logistic> CreateLogisticAsync(Logistic logistic);
        Task<Logistic> UpdateLogisticAsync(string logisticID, Logistic logistic);
        Task<Logistic> DeleteLogisticAsync(string logisticID);
        Task<Logistic> GetLogisticByIDAsync(string logisticID);
        Task<List<Logistic>> GetLogisticsAsync();
        Task<List<Logistic>> GetLogisticsByEntityIDAsync(string entityID);
    }
}
