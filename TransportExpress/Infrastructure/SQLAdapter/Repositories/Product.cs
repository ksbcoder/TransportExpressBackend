using Dapper;
using TransportExpress.Domain.Common;
using TransportExpress.Infrastructure.SQLAdapter.Gateway;
using TransportExpress.UseCases.IRepositories;

namespace TransportExpress.Infrastructure.SQLAdapter.Repositories
{
    public class Product : IProduct
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string _tableNameProduct = "Product";

        public Product(IDbConnectionBuilder dbConnectionBuilder)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
        }

        public async Task<List<Domain.Entities.Product>> GetProductsAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string query = $"SELECT * FROM {_tableNameProduct}";
            var productsFound = (from product in await connection.QueryAsync<Domain.Entities.Product>(query)
                                 where product.StateProduct == Enums.StateEntity.Active
                                 select product).ToList();
            connection.Close();
            return productsFound;
        }
    }
}
