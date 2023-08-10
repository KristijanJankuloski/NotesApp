using NotesApp.Domain.Models;
using NotesApp.DTOs.UserDtos;

namespace NotesApp.Services.Interfaces
{
    public interface IUserService
    {
        Task DeleteUserByIdAsync(int id);
        Task<User> ChangePasswordAsync(UserChangePasswordDto model, int id);
    }
}
