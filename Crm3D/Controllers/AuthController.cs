using Crm3D.Models;
using Crm3D.Models.DTOs;
using Crm3D.Services;
using Microsoft.AspNetCore.Mvc;

namespace Crm3D.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public AuthController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Employee>> Register(EmployeeDto request)
        {
            var result = await _employeeService.Register(request);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Employee>> Login(EmployeeDto request)
        {
            var result = await _employeeService.Login(request);
            if (result == null)
            {
                return BadRequest("Employee not found!");
            }

            return Ok(result);
        }

    }
}
