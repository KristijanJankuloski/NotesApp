using NotesApp.DataAccess.Repositories.Interfaces;
using NotesApp.Domain.Models;
using NotesApp.DTOs.NotesDtos;
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

        public async Task DeleteNoteByIdAsync(int id)
        {
            await _noteRepository.DeleteByIdAsync(id);
        }

        public async Task<Note> GetNoteById(int id)
        {
            return await _noteRepository.GetByIdAsync(id);
        }

        public async Task<List<NoteListDto>> GetUserNotesListAsync(int userId)
        {
            List<Note> notes = await _noteRepository.GetAllByUserIdAsync(userId);
            return notes.Select(x => x.ToNoteListDto()).ToList();
        }

        public async Task UpdateNoteAsync(NoteDto note)
        {
            Note noteToUpdate = note.ToNote();
            await _noteRepository.UpdateAsync(noteToUpdate);
        }
    }
}
