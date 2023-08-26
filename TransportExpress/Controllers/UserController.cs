using Microsoft.AspNetCore.Mvc;
using TransportExpress.Domain.Entities;
using TransportExpress.UseCases.IRepositories;

namespace TransportExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userUseCase;

        public UserController(IUser userUseCase)
        {
            _userUseCase = userUseCase;
        }

        [HttpGet]
        public async Task<List<User>> GetUsers()
        {
            return await _userUseCase.GetUsersAsync();
        }
    }
}