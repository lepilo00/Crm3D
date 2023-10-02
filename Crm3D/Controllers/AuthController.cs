using Crm3D.Models;
using Crm3D.Models.DTOs;
using Crm3D.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Crm3D.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenService _refreshTokenService;


        private static Employee? _employee;

        public AuthController(IEmployeeService employeeService, ITokenService tokenService, IRefreshTokenService refreshTokenService)
        {
            _employeeService = employeeService;
            _tokenService = tokenService;
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Employee>> Register(EmployeeDto request)
        {
            var result = await _employeeService.Register(request);
            if (result == null)
                return NotFound();

            _employee = result;
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Employee>> Login(EmployeeDto request)
        {
            var result = await _employeeService.Login(request, Response);
            if (result == null)
            {
                return BadRequest("Employee not found!");
            }

            _employee = result;

            string token = await _tokenService.CreateToken(result);

            return Ok(token);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (!_employee.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid refresh token!");
            }
            else if (_employee.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expires!");
            }

            string token = await _tokenService.CreateToken(_employee);
            _ = await _refreshTokenService.AddRefreshToken(_employee, Response);

            return Ok(token);
        }
    }
}
