using Microsoft.AspNetCore.Mvc;
using TransportExpress.Domain.Entities;
using TransportExpress.UseCases.IRepositories;

namespace TransportExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClient _clientUseCase;

        public ClientController(IClient clientUseCase)
        {
            _clientUseCase = clientUseCase;
        }

        [HttpGet]
        public async Task<List<Client>> GetClients()
        {
            return await _clientUseCase.GetClientsAsync();
        }
    }
}