﻿using Ardalis.GuardClauses;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductImplementation(IDbConnectionBuilder dbConnectionBuilder, IMapper mapper)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
            _mapper = mapper;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            Product.SetDetailsProduct(product);

            Guard.Against.Null(product, nameof(product));
            Guard.Against.NullOrEmpty(product.TransportID, nameof(product.TransportID));
            Guard.Against.NullOrEmpty(product.NameProduct, nameof(product.NameProduct));
            Guard.Against.NullOrEmpty(product.DescriptionProduct, nameof(product.DescriptionProduct));
            Guard.Against.EnumOutOfRange(product.StateProduct, nameof(product.StateProduct));

            string query = $"INSERT INTO {_tableNameProduct} (TransportID, NameProduct, DescriptionProduct, StateProduct) " +
                $"VALUES (@TransportID, @NameProduct, @DescriptionProduct, @StateProduct)";

            var productCreated = await connection.ExecuteAsync(query, product);
            connection.Close();
            return productCreated == 0 ?
                throw new ApiException("Product not created.", 400) :
                product;
        }

        public async Task<Product> GetProductByIDAsync(string productID)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string query = $"SELECT * FROM {_tableNameProduct}";
            var productFound = (from product in connection.Query<Product>(query)
                                where product.StateProduct == Enums.StateEntity.Active
                                    && product.ProductID == Guid.Parse(productID)
                                select product).FirstOrDefault();
            connection.Close();
            return productFound ?? throw new ApiException("Product not found.", 404);
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
