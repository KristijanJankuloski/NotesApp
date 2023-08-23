using NotesApp.DataAccess.Repositories.Interfaces;
using NotesApp.Domain.Models;
using NotesApp.DTOs.UserDtos;
using NotesApp.Mappers;
using NotesApp.Services.Helpers;
using NotesApp.Services.Interfaces;

namespace NotesApp.Services.Implementaitons
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CheckToken(int userId, string token)
        {
            User user = await _userRepository.GetByIdAsync(userId);
            return user.LastToken == token;
        }

        public User LoginUser(UserLoginDto model)
        {
            User user = RegisterUser(new UserRegisterDto() { Username = model.Username, Email = "test@email.com", Password = model.Password, PasswordRepeated = model.Password });
            if (!PasswordHelper.VerifyPassword(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            return user;
        }

        public async Task<User> LoginUserAsync(UserLoginDto model)
        {
            User foundUser = await _userRepository.GetByUsernameAsync(model.Username);
            if (foundUser == null)
            {
                return null;
            }
            if (!PasswordHelper.VerifyPassword(model.Password, foundUser.PasswordHash, foundUser.PasswordSalt))
            {
                return null;
            }
            return foundUser;
        }

        public User RegisterUser(UserRegisterDto model)
        {
            if (model.Password != model.PasswordRepeated)
            {
                return null;
            }
            User user = new User();
            user.Username = model.Username;
            user.Email = model.Email;
            PasswordHelper.CreatePasswordHash(model.Password, out byte[] passHash, out byte[] passSalt);
            user.PasswordHash = passHash;
            user.PasswordSalt = passSalt;
            return user;
        }

        public async Task<User> RegisterUserAsync(UserRegisterDto model)
        {
            if (model.Password != model.PasswordRepeated)
            {
                return null;
            }
            User user = model.ToUser();
            PasswordHelper.CreatePasswordHash(model.Password, out byte[] passHash, out byte[] passSalt);
            user.PasswordHash = passHash;
            user.PasswordSalt = passSalt;
            await _userRepository.InsertAsync(user);
            return user;
        }

        public async Task UpdateLastToken(int userId, string token)
        {
            User user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return;
            }
            user.LastToken = token;
            await _userRepository.UpdateAsync(user);
        }
    }
}
