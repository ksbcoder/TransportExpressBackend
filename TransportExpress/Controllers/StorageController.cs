using Microsoft.AspNetCore.Mvc;
using TransportExpress.Domain.Entities;
using TransportExpress.UseCases.IRepositories;

namespace TransportExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IStorage _storageUseCase;

        public StorageController(IStorage storageUseCase)
        {
            _storageUseCase = storageUseCase;
        }

        [HttpGet]
        public async Task<List<Storage>> GetStorages()
        {
            return await _storageUseCase.GetStoragesAsync();
        }
    }
}