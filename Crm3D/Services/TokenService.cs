using Crm3D.Models;
using Crm3D.Models.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Crm3D.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> CreateToken(Employee employee)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, employee.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                    ) ;

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Task.FromResult(jwt);
        }
    }
}
