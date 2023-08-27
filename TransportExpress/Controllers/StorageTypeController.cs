using Microsoft.AspNetCore.Mvc;
using TransportExpress.Domain.Entities;
using TransportExpress.UseCases.IRepositories;

namespace TransportExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageTypeController : ControllerBase
    {
        private readonly IStorageType _storageTypeUseCase;

        public StorageTypeController(IStorageType storageTypeUseCase)
        {
            _storageTypeUseCase = storageTypeUseCase;
        }

        [HttpGet]
        public async Task<List<StorageType>> GetStorageTypes()
        {
            return await _storageTypeUseCase.GetStorageTypesAsync();
        }
        [HttpGet("ID")]
        public async Task<StorageType> GetStorageTypeByID(string storageTypeID)
        {
            return await _storageTypeUseCase.GetStorageTypeByIDAsync(storageTypeID);
        }
    }
}