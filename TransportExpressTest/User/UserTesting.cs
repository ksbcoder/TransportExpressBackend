using Moq;
using TransportExpress.Domain.Common;
using TransportExpress.UseCases.IRepositories;

namespace TransportExpressTest.User
{
    public class UserTesting
    {
        private readonly Mock<IUser> _mockImplementation;

        public UserTesting()
        {
            _mockImplementation = new();
        }

        [Fact]
        public async void GetUsers_Ok()
        {
            //arrange
            var user = BuildEntity();
            var users = new List<TransportExpress.Domain.Entities.User> { user };
            _mockImplementation.Setup(x => x.GetClientsAsync()).ReturnsAsync(users);
            //act
            var result = await _mockImplementation.Object.GetClientsAsync();
            //assert
            Assert.NotNull(result);
            Assert.IsType<List<TransportExpress.Domain.Entities.User>>(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async void GetUserByID_Ok()
        {
            //arrange
            var user = BuildEntity();
            _mockImplementation.Setup(x => x.GetUserByIDAsync(user.UserID.ToString())).ReturnsAsync(user);
            //act
            var result = await _mockImplementation.Object.GetUserByIDAsync(user.UserID.ToString());
            //assert
            Assert.NotNull(result);
            Assert.IsType<TransportExpress.Domain.Entities.User>(result);
            Assert.Equal(user.Identification, result.Identification);
        }

        [Fact]
        public async void GetUserByUidUser_Ok()
        {
            //arrange
            var user = BuildEntity();
            _mockImplementation.Setup(x => x.GetUserByUidUserAsync(user.UidUser)).ReturnsAsync(user);
            //act
            var result = await _mockImplementation.Object.GetUserByUidUserAsync(user.UidUser);
            //assert
            Assert.NotNull(result);
            Assert.IsType<TransportExpress.Domain.Entities.User>(result);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async void CreateUser_Ok()
        {
            //arrange
            var user = BuildEntity();
            _mockImplementation.Setup(x => x.CreateUser(user)).ReturnsAsync(user);
            //act
            var result = await _mockImplementation.Object.CreateUser(user);
            //assert
            Assert.NotNull(result);
            Assert.IsType<TransportExpress.Domain.Entities.User>(result);
            Assert.Equal(user.NameUser, result.NameUser);
        }

        [Fact]
        public async void GetUserByUidUser_Bad()
        {
            //arrange
            var uidUser = Guid.NewGuid();
            var user = BuildEntity();
            _mockImplementation.Setup(x => x.GetUserByUidUserAsync(uidUser.ToString())).ReturnsAsync(user);
            //act
            var result = await _mockImplementation.Object.GetUserByUidUserAsync(uidUser.ToString());
            //assert
            Assert.NotEqual(uidUser.ToString(), result.UidUser);
            Assert.NotNull(result);
        }
        public static TransportExpress.Domain.Entities.User BuildEntity()
        {
            var user = new TransportExpress.Domain.Entities.User();
            user.SetUserID(Guid.NewGuid());
            user.SetUidUser("uidUser");
            user.SetNameUser("nameUser");
            user.SetIdentification("identification");
            user.SetPhone("phone");
            user.SetEmail("email");
            user.SetAddress("address");
            user.SetTypeUser(Enums.TypeUser.Client);
            user.SetStateUser(Enums.StateEntity.Active);
            return user;
        }
    }
}