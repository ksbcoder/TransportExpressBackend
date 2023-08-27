using Moq;
using TransportExpress.Domain.Common;
using TransportExpress.UseCases.IRepositories;

namespace TransportExpressTest.Transport
{
    public class TransportTesting
    {
        private readonly Mock<ITransport> _mockImplementation;

        public TransportTesting()
        {
            _mockImplementation = new();
        }

        [Fact]
        public async void GetTransports_Ok()
        {
            //arrange
            var transport = BuildEntity();
            var transports = new List<TransportExpress.Domain.Entities.Transport> { transport };
            _mockImplementation.Setup(x => x.GetTransportsAsync()).ReturnsAsync(transports);
            //act
            var result = await _mockImplementation.Object.GetTransportsAsync();
            //assert
            Assert.NotNull(result);
            Assert.IsType<List<TransportExpress.Domain.Entities.Transport>>(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async void GetTransportByID_Ok()
        {
            //arrange
            var transport = BuildEntity();
            _mockImplementation.Setup(x => x.GetTransportByIDAsync(transport.TransportID.ToString())).ReturnsAsync(transport);
            //act
            var result = await _mockImplementation.Object.GetTransportByIDAsync(transport.TransportID.ToString());
            //assert
            Assert.NotNull(result);
            Assert.IsType<TransportExpress.Domain.Entities.Transport>(result);
            Assert.Equal(transport.TransportID, result.TransportID);
        }

        [Fact]
        public async void CreateTransport_Ok()
        {
            //arrange
            var transport = BuildEntity();
            _mockImplementation.Setup(x => x.CreateTransportAsync(transport)).ReturnsAsync(transport);
            //act
            var result = await _mockImplementation.Object.CreateTransportAsync(transport);
            //assert
            Assert.NotNull(result);
            Assert.IsType<TransportExpress.Domain.Entities.Transport>(result);
            Assert.Equal(transport.DescriptionTransport, result.DescriptionTransport);
        }

        private static TransportExpress.Domain.Entities.Transport BuildEntity()
        {
            var transport = new TransportExpress.Domain.Entities.Transport();
            transport.SetTransportID(Guid.NewGuid());
            transport.SetDescription("DescriptionTransport");
            transport.SetCapacityTransport(100);
            transport.SetStateTransport(Enums.StateEntity.Active);
            return transport;
        }
    }
}
