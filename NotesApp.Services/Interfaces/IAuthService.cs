using NotesApp.Domain.Models;
using NotesApp.DTOs.UserDtos;

namespace NotesApp.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterUserAsync(UserRegisterDto model);
        Task<User> LoginUserAsync(UserLoginDto model);
        User RegisterUser(UserRegisterDto model);
        User LoginUser(UserLoginDto model);
        Task UpdateLastToken(int userId,  string token);
        Task<bool> CheckToken(int userId, string token);
        //Task<User> ChangePasswordAsync(UserChangePasswordDto model);
    }
}
