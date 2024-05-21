using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost("LoginAdmin")]
        public IActionResult LoginAdmin([FromBody] LoginModel loginModel)
        {
            var token = GenerateToken("Admin");
            return Ok(new { Token = token });
        }

        [HttpPost("LoginUser")]
        public IActionResult LoginUser([FromBody] LoginModel loginModel)
        {
            var token = GenerateToken("User");
            return Ok(new { Token = token });
        }

        private string GenerateToken(string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("qazwsx][pasdedcrfvtgbyhn][';/.zx./,cjqp");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, role) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
