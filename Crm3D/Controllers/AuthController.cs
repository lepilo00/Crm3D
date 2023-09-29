using Crm3D.Models;
using Crm3D.Models.DTOs;
using Crm3D.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Crm3D.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ITokenService _tokenService;

        private readonly Employee _employee;

        public AuthController(IEmployeeService employeeService, ITokenService tokenService)
        {
            _employeeService = employeeService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Employee>> Register(EmployeeDto request)
        {
            var result = await _employeeService.Register(request);
            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // preveri ce lahko refreshToken v bazo zapisemo preko servica _employeeService.Login(request);
        // preveri ce lahko prvo ustvarimo refresh tokken in se le na to Token

        //https://www.youtube.com/watch?v=_F2hB4cWg-M - 11:12

        [HttpPost("login")]
        public async Task<ActionResult<Employee>> Login(EmployeeDto request)
        {
            var result = await _employeeService.Login(request);
            if (result == null)
            {
                return BadRequest("Employee not found!");
            }

            string token = await _tokenService.CreateToken(result);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken);

            return Ok(token);
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(7)
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires,
            };

            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            _employee.RefreshToken = newRefreshToken.Token;
            _employee.TokenCreated = newRefreshToken.Created;
            _employee.TokenExpires = newRefreshToken.Expires;
        }
    }
}
