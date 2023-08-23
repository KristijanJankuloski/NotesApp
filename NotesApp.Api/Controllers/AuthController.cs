using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NotesApp.Domain.Models;
using NotesApp.DTOs;
using NotesApp.DTOs.UserDtos;
using NotesApp.Helpers;
using NotesApp.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NotesApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserRegisterDto request)
        {
            try
            {
                User user = await _authService.RegisterUserAsync(request);
                if (user == null)
                {
                    return BadRequest(new ErrorDto("Someting went wrong"));
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LogInResposeDto>> Login(UserLoginDto request)
        {
            try
            {
                User user = await _authService.LoginUserAsync(request);
                if (user == null)
                {
                    return BadRequest(new ErrorDto("Bad credentials"));
                }
                string token = GenerateToken(user, 1);
                string refreshToken = GenerateToken(user, 30, true);
                await _authService.UpdateLastToken(user.Id, token);
                LogInResposeDto response = new LogInResposeDto()
                {
                    Username = user.Username,
                    Email = user.Email,
                    Token = token,
                    RefreshToken = refreshToken
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("token-refresh")]
        public async Task<ActionResult<LogInResposeDto>> RefreshToken(TokenRefreshDto dto)
        {
            try
            {
                User user = UserHelpers.GetCurrentUser(HttpContext);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
                if (!await _authService.CheckToken(user.Id, dto.LastToken))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
                string token = GenerateToken(user, 15);
                string refreshToken = GenerateToken(user, 30, true);
                LogInResposeDto response = new LogInResposeDto()
                {
                    Username = user.Username,
                    Email = user.Email,
                    Token = token,
                    RefreshToken = refreshToken
                };
                await _authService.UpdateLastToken(user.Id, token);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private string GenerateToken(User user, int timeout, bool refresh = false)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: refresh ? DateTime.Now.AddDays(timeout) : DateTime.Now.AddMinutes(timeout),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
