using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Domain.Models;
using NotesApp.DTOs.NotesDtos;
using NotesApp.Mappers;
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

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<NoteDto>> GetById(int id)
        {
            User user = GetCurrentUser();
            try
            {
                Note note = await _noteService.GetNoteById(id);
                if (note == null || note.UserId != user.Id)
                    return BadRequest("No note with that id");
                return Ok(note.ToNoteDto());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteById(int id)
        {
            User user = GetCurrentUser();
            try
            {
                Note note = await _noteService.GetNoteById(id);
                if (note == null || note.UserId != user.Id)
                    return BadRequest("No note with that id");
                await _noteService.DeleteNoteByIdAsync(note.Id);
                return Ok("Note deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] NoteDto dto)
        {
            User user = GetCurrentUser();
            try
            {
                Note note = await _noteService.GetNoteById(dto.Id);
                if (note.UserId != user.Id) 
                    return BadRequest("No note with that id");
                _noteService.UpdateNoteAsync(dto);
                return Ok("Note updated");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] NoteCreateDto dto)
        {
            if (dto == null)
            {
                return BadRequest("No note provided");
            }
            User user = GetCurrentUser();
            if (user == null)
            {
                return BadRequest("Not logged in");
            }
            await _noteService.CreateNoteAsync(new NoteDto { UserId = user.Id, CreateDate = DateTime.UtcNow, Title = dto.Title, Text = dto.Text });
            return StatusCode(StatusCodes.Status201Created, "Note created");
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
