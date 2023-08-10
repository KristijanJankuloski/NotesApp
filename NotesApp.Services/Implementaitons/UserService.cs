using NotesApp.DataAccess.Repositories.Interfaces;
using NotesApp.Domain.Models;
using NotesApp.DTOs.UserDtos;
using NotesApp.Services.Helpers;
using NotesApp.Services.Interfaces;

namespace NotesApp.Services.Implementaitons
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly INoteRepository _noteRepository;
        public UserService(IUserRepository userRepository, INoteRepository noteRepository)
        {
            _userRepository = userRepository;
            _noteRepository = noteRepository;
        }

        public async Task DeleteUserByIdAsync(int id)
        {
            await _noteRepository.DeleteAllByUserIdAsync(id);
            await _userRepository.DeleteByIdAsync(id);
        }

        public async Task<User> ChangePasswordAsync(UserChangePasswordDto model, int id)
        {
            if (string.IsNullOrEmpty(model.OldPassword) || string.IsNullOrEmpty(model.NewPassword))
                throw new Exception("Fields not provided");

            if (model.OldPassword == model.NewPassword)
                throw new Exception("New passowrd cannon match old password");

            if (model.NewPassword != model.NewPasswordRepeated)
                throw new Exception("New password does not match");

            User user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new Exception("No user found");

            if (!PasswordHelper.VerifyPassword(model.OldPassword, user.PasswordHash, user.PasswordSalt))
                throw new Exception("Invalid old password");

            PasswordHelper.CreatePasswordHash(model.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _userRepository.UpdateAsync(user);
            return user;
        }
    }
}
