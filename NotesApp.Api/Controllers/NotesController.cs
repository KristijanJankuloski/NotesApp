using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Domain.Models;
using NotesApp.DTOs;
using NotesApp.Services.Interfaces;
using System.Security.Claims;

namespace NotesApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;
        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<NoteListDto>>> Get()
        {
            User currentUser = GetCurrentUser();
            List<NoteListDto> notes = await _noteService.GetUserNotesListAsync(currentUser.Id);
            return Ok(notes);
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create(NoteCreateDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            User user = GetCurrentUser();
            if (user == null)
            {
                return BadRequest();
            }
            await _noteService.CreateNoteAsync(new NoteDto { UserId = user.Id, CreateDate = DateTime.UtcNow, Title = dto.Title, Text = dto.Text });
            return Ok();
        }

        private User GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new User
                {
                    Id = int.Parse(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value),
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                };
            }
            return null;
        }
    }
}
