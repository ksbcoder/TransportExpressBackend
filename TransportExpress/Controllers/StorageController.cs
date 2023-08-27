using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TransportExpress.Domain.Commands.Storage;
using TransportExpress.Domain.Entities;
using TransportExpress.UseCases.IRepositories;

namespace TransportExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IStorage _storageUseCase;
        private readonly IMapper _mapper;

        public StorageController(IStorage storageUseCase, IMapper mapper)
        {
            _storageUseCase = storageUseCase;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<Storage>> GetStorages()
        {
            return await _storageUseCase.GetStoragesAsync();
        }
        [HttpGet("ID")]
        public async Task<Storage> GetStorageByID(string storageID)
        {
            return await _storageUseCase.GetStorageByIDAsync(storageID);
        }

        [HttpPost]
        public async Task<Storage> CreateStorage([FromBody] CreateStorageCommand storage)
        {
            return await _storageUseCase.CreateStorageAsync(_mapper.Map<Storage>(storage));
        }
    }
}