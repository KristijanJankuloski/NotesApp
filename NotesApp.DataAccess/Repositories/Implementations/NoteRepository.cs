using Microsoft.EntityFrameworkCore;
using NotesApp.DataAccess.Context;
using NotesApp.DataAccess.Repositories.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.DataAccess.Repositories.Implementations
{
    public class NoteRepository : INoteRepository
    {
        private readonly NotesDbContext _context;

        public NoteRepository(NotesDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAllByUserIdAsync(int userId)
        {
            List<Note> notes = await _context.Notes.Where(n => n.UserId == userId).ToListAsync();
            foreach (var note in notes)
            {
                _context.Notes.Remove(note);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            Note noteToDelete = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);
            if (noteToDelete == null)
            {
                throw new Exception("No note found");
            }
            _context.Notes.Remove(noteToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Note>> GetAllAsync()
        {
            return await _context.Notes.ToListAsync();
        }

        public async Task<List<Note>> GetAllByUserIdAsync(int userId)
        {
            return await _context.Notes
                .Include(x => x.User)
                .Where(n => n.UserId == userId)
                .ToListAsync();
        }

        public async Task<Note> GetByIdAsync(int id)
        {
            return await _context.Notes.Include(n => n.User).FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task InsertAsync(Note entity)
        {
            await _context.Notes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Note entity)
        {
            _context.Notes.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
