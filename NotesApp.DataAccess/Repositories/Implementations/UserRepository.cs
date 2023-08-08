using Microsoft.EntityFrameworkCore;
using NotesApp.DataAccess.Context;
using NotesApp.DataAccess.Repositories.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.DataAccess.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly NotesDbContext _context;

        public UserRepository(NotesDbContext context)
        {
            _context = context;
        }
        public async Task DeleteByIdAsync(int id)
        {
            User userToDelete = await _context.Users.Include(u => u.Notes).FirstOrDefaultAsync(u => u.Id == id);
            if (userToDelete == null)
            {
                throw new Exception("User not found");
            }
            if(userToDelete.Notes.Count > 0)
            {
                throw new Exception("User has undeleted notes");
            }
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.Include(u => u.Notes).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            User foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            return foundUser;
        }

        public async Task InsertAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
