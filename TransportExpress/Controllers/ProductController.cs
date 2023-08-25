using Microsoft.AspNetCore.Mvc;
using TransportExpress.UseCases.IRepositories;

namespace TransportExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productUseCase;

        public ProductController(IProduct productUseCase)
        {
            _productUseCase = productUseCase;
        }

        [HttpGet]
        public async Task<List<Domain.Entities.Product>> GetProducts()
        {
            return await _productUseCase.GetProductsAsync();
        }
    }
}