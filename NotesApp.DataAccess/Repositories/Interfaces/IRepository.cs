using NotesApp.Domain.Models;

namespace NotesApp.DataAccess.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task InsertAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task DeleteByIdAsync(int id);
        Task UpdateAsync(T entity);
    }
}
