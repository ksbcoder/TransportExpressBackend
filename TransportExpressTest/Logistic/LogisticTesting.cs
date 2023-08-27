using Moq;
using TransportExpress.Domain.Common;
using TransportExpress.Domain.DTO.Logistic;
using TransportExpress.Domain.Queries.Logistic;
using TransportExpress.UseCases.IRepositories;

namespace TransportExpressTest.Logistic
{
    public class LogisticTesting
    {
        private readonly Mock<ILogistic> _mockImplementation;

        public LogisticTesting()
        {
            _mockImplementation = new();
        }
        [Fact]
        public async void GetLogisticsAsync_Ok()
        {
            //arrange
            var logistic = BuildEntity();

            var logistics = new List<TransportExpress.Domain.Entities.Logistic> { logistic };
            _mockImplementation.Setup(x => x.GetLogisticsAsync()).ReturnsAsync(logistics);
            //act
            var result = await _mockImplementation.Object.GetLogisticsAsync();
            //assert
            Assert.NotNull(result);
            Assert.IsType<List<TransportExpress.Domain.Entities.Logistic>>(result);
        }

        [Fact]
        public async void GetLogisticByIDAsync_Ok()
        {
            //arrange
            var logisticDTO = BuildEntityDTO();
            _mockImplementation.Setup(x => x.GetLogisticByIDAsync(logisticDTO.LogisticID.ToString())).ReturnsAsync(logisticDTO);
            //act
            var result = await _mockImplementation.Object.GetLogisticByIDAsync(logisticDTO.LogisticID.ToString());
            //assert
            Assert.NotNull(result);
            Assert.IsType<LogisticDTO>(result);
        }

        [Fact]
        public async void GetLogisticsByFilterAsync_Ok()
        {
            //arrange
            var logistic = BuildEntity();
            var filterOption = new LogisticFilters()
            {
                Logistic = logistic.LogisticID.ToString(),
                Product = logistic.ProductID.ToString(),
                Client = logistic.UserID.ToString(),
                Storage = logistic.StorageID.ToString(),
            };
            var logistics = new List<TransportExpress.Domain.Entities.Logistic> { logistic };
            _mockImplementation.Setup(x => x.GetLogisticsByFilterAsync(filterOption)).ReturnsAsync(logistics);
            //act
            var result = await _mockImplementation.Object.GetLogisticsByFilterAsync(filterOption);
            //assert
            Assert.NotNull(result);
            Assert.IsType<List<TransportExpress.Domain.Entities.Logistic>>(result);
        }

        [Fact]
        public async void CreateLogisticAsync_Ok()
        {
            //arrange
            var logistic = BuildEntity();
            var logisticDTO = new LogisticDTO()
            {
                LogisticID = logistic.LogisticID,
                ProductID = logistic.ProductID,
                UserID = logistic.UserID,
                StorageID = logistic.StorageID,
                RegisteredAt = logistic.RegisteredAt,
                DeliveredAt = logistic.DeliveredAt,
                QuantityProduct = logistic.QuantityProduct,
                ShippingPrice = logistic.ShippingPrice,
                Discount = logistic.Discount,
                NumberPlate = logistic.NumberPlate,
                FleetNumber = logistic.FleetNumber,
                GuideNumber = logistic.GuideNumber
            };
            _mockImplementation.Setup(x => x.CreateLogisticAsync(logistic)).ReturnsAsync(logistic);
            //act
            var result = await _mockImplementation.Object.CreateLogisticAsync(logistic);
            //assert
            Assert.NotNull(result);
            Assert.IsType<TransportExpress.Domain.Entities.Logistic>(result);
        }

        [Fact]
        public async void UpdateLogisticAsync_Ok()
        {
            //arrange
            var logistic = BuildEntity();
            var logisticDTO = new LogisticDTO()
            {
                LogisticID = logistic.LogisticID,
                ProductID = logistic.ProductID,
                UserID = logistic.UserID,
                StorageID = logistic.StorageID,
                RegisteredAt = logistic.RegisteredAt,
                DeliveredAt = logistic.DeliveredAt,
                QuantityProduct = logistic.QuantityProduct,
                ShippingPrice = logistic.ShippingPrice,
                Discount = logistic.Discount,
                NumberPlate = logistic.NumberPlate,
                FleetNumber = logistic.FleetNumber,
                GuideNumber = logistic.GuideNumber
            };
            _mockImplementation.Setup(x => x.UpdateLogisticAsync(logistic.LogisticID.ToString(), logistic)).ReturnsAsync(logistic);
            //act
            var result = await _mockImplementation.Object.UpdateLogisticAsync(logistic.LogisticID.ToString(), logistic);
            //assert
            Assert.NotNull(result);
            Assert.IsType<TransportExpress.Domain.Entities.Logistic>(result);
        }

        [Fact]
        public async void DeleteLogisticAsync_Ok()
        {
            //arrange
            var logistic = BuildEntity();
            _mockImplementation.Setup(x => x.DeleteLogisticAsync(logistic.LogisticID.ToString())).ReturnsAsync(200);
            //act
            var result = await _mockImplementation.Object.DeleteLogisticAsync(logistic.LogisticID.ToString());
            //assert
            Assert.IsType<int>(result);
            Assert.Equal(200, result);
        }

        [Fact]
        public async void DeleteLogisticAsync_Bad()
        {
            //arrange
            var logistic = BuildEntity();
            logistic.SetStateLogistic(Enums.StateEntity.Inactive);
            _mockImplementation.Setup(x => x.DeleteLogisticAsync(logistic.LogisticID.ToString())).ReturnsAsync(400);
            //act
            var result = await _mockImplementation.Object.DeleteLogisticAsync(logistic.LogisticID.ToString());
            //assert
            Assert.IsType<int>(result);
            Assert.Equal(400, result);
        }

        public static TransportExpress.Domain.Entities.Logistic BuildEntity()
        {
            var logistic = new TransportExpress.Domain.Entities.Logistic();
            logistic.SetLogisticID(Guid.NewGuid());
            logistic.SetProductID(Guid.NewGuid());
            logistic.SetUserID(Guid.NewGuid());
            logistic.SetStorageID(Guid.NewGuid());
            logistic.SetRegisteredAt(DateTime.Now);
            logistic.SetDeliveredAt(DateTime.Now);
            logistic.SetQuantityProduct(100);
            logistic.SetShippingPrice(1000);
            logistic.SetDiscount(50.00m);
            logistic.SetNumberPlate("ABC123");
            logistic.SetFleetNumber("");
            logistic.SetGuideNumber("XASsd3442A");
            logistic.SetStateLogistic(Enums.StateEntity.Active);
            return logistic;
        }

        public static LogisticDTO BuildEntityDTO()
        {
            var logisticDTO = new LogisticDTO()
            {
                LogisticID = Guid.NewGuid(),
                ProductID = Guid.NewGuid(),
                UserID = Guid.NewGuid(),
                StorageID = Guid.NewGuid(),
                RegisteredAt = DateTime.Now,
                DeliveredAt = DateTime.Now,
                QuantityProduct = 100,
                ShippingPrice = 1000,
                Discount = 50.00m,
                NumberPlate = "ABC123",
                FleetNumber = "",
                GuideNumber = "XASsd3442A"
            };
            return logisticDTO;
        }
    }
}