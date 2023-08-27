using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TransportExpress.Domain.Commands.Logistic;
using TransportExpress.Domain.DTO.Logistic;
using TransportExpress.Domain.Entities;
using TransportExpress.Domain.Queries.Logistic;
using TransportExpress.UseCases.IRepositories;

namespace TransportExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogisticController : ControllerBase
    {
        private readonly ILogistic _logisticUseCase;
        private readonly IMapper _mapper;

        public LogisticController(ILogistic logisticUseCase, IMapper mapper)
        {
            _logisticUseCase = logisticUseCase;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<Logistic>> GetLogistics()
        {
            return await _logisticUseCase.GetLogisticsAsync();
        }

        [HttpGet("ID")]
        public async Task<LogisticDTO> GetLogisticByID(string logisticID)
        {
            return await _logisticUseCase.GetLogisticByIDAsync(logisticID);
        }

        [HttpGet("Filter")]
        public async Task<List<Logistic>> GetLogisticsByFilter([FromQuery] LogisticFilters filterOption)
        {
            return await _logisticUseCase.GetLogisticsByFilterAsync(filterOption);
        }

        [HttpPost]
        public async Task<Logistic> CreateLogistic([FromBody] CreateLogisticCommand logistic)
        {
            return await _logisticUseCase.CreateLogisticAsync(_mapper.Map<Logistic>(logistic));
        }

        [HttpDelete("ID")]
        public async Task<int> DeleteLogistic(string logisticID)
        {
            return await _logisticUseCase.DeleteLogisticAsync(logisticID);
        }

        [HttpPut("ID")]
        public async Task<Logistic> UpdateLogistic(string logisticID, [FromBody] UpdateLogisticCommand logistic)
        {
            return await _logisticUseCase.UpdateLogisticAsync(logisticID, _mapper.Map<Logistic>(logistic));
        }
    }
}