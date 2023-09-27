using Crm3D.Models;
using Crm3D.Services;
using Microsoft.AspNetCore.Mvc;

namespace Crm3D.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISendEmailService _emailHandle;

        public UserController(IUserService userService, ISendEmailService emailHandle)
        {
            _userService = userService;
            _emailHandle = emailHandle;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();
            if (result == null)
                return NotFound("Users not found!");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<User>>> AddUser(User user)
        {
            var result = await _userService.AddUser(user);
            if (result == null)
                return NotFound("User not found!");

            await _emailHandle.SendEmailAsync(user);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (result == null)
                return NotFound("Hero not found!");
            return Ok(result);
        }
    }
}
