using NotesApp.Domain.Models;
using NotesApp.DTOs.NotesDtos;

namespace NotesApp.Services.Interfaces
{
    public interface INoteService
    {
        Task<List<NoteListDto>> GetUserNotesListAsync(int userId);
        Task<Note> GetNoteById(int id);
        Task CreateNoteAsync(NoteDto model);
        Task UpdateNoteAsync(NoteDto note);
        Task DeleteNoteByIdAsync(int id);
    }
}
