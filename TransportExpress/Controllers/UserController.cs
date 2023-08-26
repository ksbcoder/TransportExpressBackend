using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransportExpress.Domain.Commands.User;
using TransportExpress.Domain.Entities;
using TransportExpress.UseCases.IRepositories;

namespace TransportExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userUseCase;
        private readonly IMapper _mapper;

        public UserController(IUser userUseCase, IMapper mapper)
        {
            _userUseCase = userUseCase;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<List<User>> GetUsers()
        {
            return await _userUseCase.GetUsersAsync();
        }

        [HttpGet("uidUser")]
        public async Task<User> GetUserByUidUser(string uidUser)
        {
            return await _userUseCase.GetUserByUidUserAsync(uidUser);
        }

        [HttpPost]
        public async Task<User> CreateUser([FromBody] CreateUserCommand user)
        {
            return await _userUseCase.CreateUser(_mapper.Map<User>(user));
        }
    }
}