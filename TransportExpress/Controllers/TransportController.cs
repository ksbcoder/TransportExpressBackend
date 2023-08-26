using Microsoft.AspNetCore.Mvc;
using TransportExpress.UseCases.IRepositories;

namespace TransportExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        private readonly ITransport _transportUseCase;

        public TransportController(ITransport transportUseCase)
        {
            _transportUseCase = transportUseCase;
        }

        [HttpGet]
        public async Task<List<Domain.Entities.Transport>> GetTransports()
        {
            return await _transportUseCase.GetTransportsAsync();
        }
    }
}