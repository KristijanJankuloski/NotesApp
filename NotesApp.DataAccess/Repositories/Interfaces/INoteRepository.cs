using NotesApp.Domain.Models;

namespace NotesApp.DataAccess.Repositories.Interfaces
{
    public interface INoteRepository : IRepository<Note>
    {
        Task<List<Note>> GetAllByUserIdAsync(int userId);
        Task DeleteAllByUserIdAsync(int userId);
    }
}
