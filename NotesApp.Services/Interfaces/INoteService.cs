using NotesApp.DTOs;

namespace NotesApp.Services.Interfaces
{
    public interface INoteService
    {
        Task<List<NoteListDto>> GetUserNotesListAsync(int userId);
        Task CreateNoteAsync(NoteDto model);
    }
}
