using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Domain.Models;
using NotesApp.DTOs.UserDtos;
using NotesApp.Helpers;
using NotesApp.Services.Interfaces;

namespace NotesApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<string>> Get()
        {
            User user = UserHelpers.GetCurrentUser(HttpContext);
            if (user == null)
            {
                return BadRequest();
            }
            return  Ok(user.Email);
        }

        [HttpPost("pwreset")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] UserChangePasswordDto dto)
        {
            User user = UserHelpers.GetCurrentUser(HttpContext);
            if (user == null)
            {
                return BadRequest();
            }
            await _userService.ChangePasswordAsync(dto, user.Id);
            return Ok("Password changed");
        }
    }
}
