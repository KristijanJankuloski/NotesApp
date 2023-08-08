using NotesApp.DataAccess.Repositories.Interfaces;
using NotesApp.Domain.Models;
using NotesApp.DTOs;
using NotesApp.Mappers;
using NotesApp.Services.Interfaces;

namespace NotesApp.Services.Implementaitons
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        public NoteService(INoteRepository noteReposirory)
        {
            _noteRepository = noteReposirory;
        }

        public async Task CreateNoteAsync(NoteDto model)
        {
            Note note = model.ToNote();
            await _noteRepository.InsertAsync(note);
        }

        public async Task<List<NoteListDto>> GetUserNotesListAsync(int userId)
        {
            List<Note> notes = await _noteRepository.GetAllByUserIdAsync(userId);
            return notes.Select(x => x.ToNoteListDto()).ToList();
        }
    }
}
