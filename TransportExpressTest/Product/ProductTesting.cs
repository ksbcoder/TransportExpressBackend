using Moq;
using TransportExpress.Domain.Common;
using TransportExpress.UseCases.IRepositories;

namespace TransportExpressTest.Product
{
    public class ProductTesting
    {
        private readonly Mock<IProduct> _mockImplementation;

        public ProductTesting()
        {
            _mockImplementation = new();
        }

        [Fact]
        public async void GetProducts_Ok()
        {
            //arrange
            var product = BuildEntity();
            var products = new List<TransportExpress.Domain.Entities.Product> { product };
            _mockImplementation.Setup(x => x.GetProductsAsync()).ReturnsAsync(products);
            //act
            var result = await _mockImplementation.Object.GetProductsAsync();
            //assert
            Assert.NotNull(result);
            Assert.IsType<List<TransportExpress.Domain.Entities.Product>>(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async void GetProductByID_Ok()
        {
            //arrange
            var product = BuildEntity();
            _mockImplementation.Setup(x => x.GetProductByIDAsync(product.ProductID.ToString())).ReturnsAsync(product);
            //act
            var result = await _mockImplementation.Object.GetProductByIDAsync(product.ProductID.ToString());
            //assert
            Assert.NotNull(result);
            Assert.IsType<TransportExpress.Domain.Entities.Product>(result);
            Assert.Equal(product.NameProduct, result.NameProduct);
        }

        [Fact]
        public async void CreateProduct_Ok()
        {
            //arrange
            var product = BuildEntity();
            _mockImplementation.Setup(x => x.CreateProductAsync(product)).ReturnsAsync(product);
            //act
            var result = await _mockImplementation.Object.CreateProductAsync(product);
            //assert
            Assert.NotNull(result);
            Assert.IsType<TransportExpress.Domain.Entities.Product>(result);
            Assert.Equal(product.NameProduct, result.NameProduct);
        }

        private static TransportExpress.Domain.Entities.Product BuildEntity()
        {
            var product = new TransportExpress.Domain.Entities.Product();
            product.SetProductID(Guid.NewGuid());
            product.SetTransportID(Guid.NewGuid());
            product.SetNameProduct("Product");
            product.SetDescription("Description");
            product.SetStateProduct(Enums.StateEntity.Active);
            return product;
        }
    }
}