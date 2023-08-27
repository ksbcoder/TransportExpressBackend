using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransportExpress.Domain.Commands.Transport;
using TransportExpress.Domain.Entities;
using TransportExpress.UseCases.IRepositories;

namespace TransportExpress.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
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
        [HttpGet("ID")]
        public async Task<Transport> GetTransportByID(string transportID)
        {
            return await _transportUseCase.GetTransportByIDAsync(transportID);
        }
        [HttpPost]
        public async Task<ActionResult<Transport>> CreateTransport([FromBody] CreateTransportCommand transport)
        {
            return await _transportUseCase.CreateTransportAsync(_mapper.Map<Transport>(transport));
        }
    }
}