using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TransportExpress.Domain.Commands.User;
using TransportExpress.Infrastructure.SQLAdapter.Gateway;
using TransportExpress.Infrastructure.SQLAdapter.Repositories;

namespace TransportExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly string secretKey;
        private readonly IDbConnectionBuilder _dbConnectionBuilder;

        public AuthenticationController(IConfiguration configuration, IDbConnectionBuilder dbConnectionBuilder)
        {
            secretKey = configuration.GetSection("Settings").GetSection("secretKey").ToString();
            _dbConnectionBuilder = dbConnectionBuilder;
        }

        [HttpPost]
        [Route("validate")]
        public IActionResult Validate([FromBody] AuthenticateUserCommand userToValidate)
        {
            if (userToValidate != null)
            {
                // Consult user in database
                var user = new UserImplementation(_dbConnectionBuilder).GetUserByUidUserAsync(userToValidate.UidUSer).Result;
                if (user == null)
                {
                    return StatusCode(401, new { token = "" });
                }

                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, userToValidate.Email));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string createdToken = tokenHandler.WriteToken(tokenConfig);

                return StatusCode(200, new { token = createdToken });
            }
            else
            {
                return StatusCode(401, new { token = "" });
            }
        }
    }
}
