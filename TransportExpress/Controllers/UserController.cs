using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransportExpress.Domain.Commands.User;
using TransportExpress.Domain.Entities;
using TransportExpress.UseCases.IRepositories;

namespace TransportExpress.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
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

        [HttpGet]
        public async Task<List<User>> GetClients()
        {
            return await _userUseCase.GetClientsAsync();
        }

        [HttpGet("uid")]
        public async Task<User> GetUserByUidUser(string uidUser)
        {
            return await _userUseCase.GetUserByUidUserAsync(uidUser);
        }

        [HttpGet("ID")]
        public async Task<User> GetUserByID(string userID)
        {
            return await _userUseCase.GetUserByIDAsync(userID);
        }

        [HttpPost]
        public async Task<User> CreateUser([FromBody] CreateUserCommand user)
        {
            return await _userUseCase.CreateUser(_mapper.Map<User>(user));
        }
    }
}