using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TransportExpress.Domain.Entities;
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
    }
}