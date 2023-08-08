using NotesApp.Domain.Models;
using NotesApp.DTOs;

namespace NotesApp.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterUserAsync(UserRegisterDto model);
        Task<User> LoginUserAsync(UserLoginDto model);
        User RegisterUser(UserRegisterDto model);
        User LoginUser(UserLoginDto model);
    }
}
