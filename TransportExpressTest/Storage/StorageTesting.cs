using Moq;
using TransportExpress.Domain.Common;
using TransportExpress.UseCases.IRepositories;

namespace TransportExpressTest.Storage
{
    public class StorageTesting
    {
        private readonly Mock<IStorage> _mockImplementation;

        public StorageTesting()
        {
            _mockImplementation = new();
        }

        [Fact]
        public async void GetStorages_Ok()
        {
            //arrange
            var storage = BuildEntity();
            var storages = new List<TransportExpress.Domain.Entities.Storage> { storage };
            _mockImplementation.Setup(x => x.GetStoragesAsync()).ReturnsAsync(storages);
            //act
            var result = await _mockImplementation.Object.GetStoragesAsync();
            //assert
            Assert.NotNull(result);
            Assert.IsType<List<TransportExpress.Domain.Entities.Storage>>(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async void GetStorageByID_Ok()
        {
            //arrange
            var storage = BuildEntity();
            _mockImplementation.Setup(x => x.GetStorageByIDAsync(storage.StorageID.ToString())).ReturnsAsync(storage);
            //act
            var result = await _mockImplementation.Object.GetStorageByIDAsync(storage.StorageID.ToString());
            //assert
            Assert.NotNull(result);
            Assert.IsType<TransportExpress.Domain.Entities.Storage>(result);
            Assert.Equal(storage.NameStorage, result.NameStorage);
        }

        [Fact]
        public async void CreateStorage_Ok()
        {
            //arrange
            var storage = BuildEntity();
            _mockImplementation.Setup(x => x.CreateStorageAsync(storage)).ReturnsAsync(storage);
            //act
            var result = await _mockImplementation.Object.CreateStorageAsync(storage);
            //assert
            Assert.NotNull(result);
            Assert.IsType<TransportExpress.Domain.Entities.Storage>(result);
            Assert.Equal(storage.NameStorage, result.NameStorage);
        }

        private static TransportExpress.Domain.Entities.Storage BuildEntity()
        {
            var storage = new TransportExpress.Domain.Entities.Storage();
            storage.SetStorageID(Guid.NewGuid());
            storage.SetStorageTypeID(Guid.NewGuid());
            storage.SetNameStorage("Storage");
            storage.SetLocation("Address");
            storage.SetCapacityStorage(100);
            storage.SetStateStorage(Enums.StateEntity.Active);
            return storage;
        }
    }
}