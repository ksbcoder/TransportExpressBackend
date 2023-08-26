using Dapper;
using TransportExpress.Domain.Common;
using TransportExpress.Domain.Entities;
using TransportExpress.Infrastructure.SQLAdapter.Gateway;
using TransportExpress.UseCases.IRepositories;
using TransportExpress.Wrappers;

namespace TransportExpress.Infrastructure.SQLAdapter.Repositories
{
    public class ProductImplementation : IProduct
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string _tableNameProduct = "Product";

        public ProductImplementation(IDbConnectionBuilder dbConnectionBuilder)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string query = $"SELECT * FROM {_tableNameProduct}";
            var productsFound = (from product in await connection.QueryAsync<Product>(query)
                                 where product.StateProduct == Enums.StateEntity.Active
                                 select product).ToList();
            connection.Close();
            return productsFound.Count == 0 ?
                throw new ApiException("There are no products available.", 204) :
                productsFound;
        }
    }
}
