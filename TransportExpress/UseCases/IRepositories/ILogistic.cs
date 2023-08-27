using TransportExpress.Domain.DTO.Logistic;
using TransportExpress.Domain.Entities;
using TransportExpress.Domain.Queries.Logistic;

namespace TransportExpress.UseCases.IRepositories
{
    public interface ILogistic
    {
        Task<Logistic> CreateLogisticAsync(Logistic logistic);
        Task<Logistic> UpdateLogisticAsync(string logisticID, Logistic logistic);
        Task<int> DeleteLogisticAsync(string logisticID);
        Task<LogisticDTO> GetLogisticByIDAsync(string logisticID);
        Task<List<Logistic>> GetLogisticsAsync();
        Task<List<Logistic>> GetLogisticsByFilterAsync(LogisticFilters filterOption);
    }
}