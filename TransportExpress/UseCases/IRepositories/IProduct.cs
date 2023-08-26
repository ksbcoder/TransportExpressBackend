using TransportExpress.Domain.Entities;

namespace TransportExpress.UseCases.IRepositories
{
    public interface IProduct
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> CreateProductAsync(Product product);
    }
}