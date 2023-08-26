using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TransportExpress.Domain.Commands.Transport;
using TransportExpress.Domain.Entities;
using TransportExpress.UseCases.IRepositories;

namespace TransportExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        private readonly ITransport _transportUseCase;
        private readonly IMapper _mapper;

        public TransportController(ITransport transportUseCase, IMapper mapper)
        {
            _transportUseCase = transportUseCase;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<Transport>> GetTransports()
        {
            return await _transportUseCase.GetTransportsAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Transport>> CreateTransport([FromBody] CreateTransportCommand transport)
        {
            return await _transportUseCase.CreateTransportAsync(_mapper.Map<Transport>(transport));
        }
    }
}