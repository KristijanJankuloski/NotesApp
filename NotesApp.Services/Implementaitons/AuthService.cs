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

        //public async Task<User> ChangePasswordAsync(UserChangePasswordDto model)
        //{
        //    if(string.IsNullOrEmpty(model.OldPassword) || string.IsNullOrEmpty(model.NewPassword))
        //        throw new Exception("Fields not provided");

        //    if(model.OldPassword == model.NewPassword)
        //        throw new Exception("New passowrd cannon match old password");

        //    if(model.NewPassword != model.NewPasswordRepeated)
        //        throw new Exception("New password does not match");

        //    User user = await _userRepository.GetByIdAsync(model.Id);
        //    if (user == null)
        //        throw new Exception("No user found");

        //    if(!PasswordHelper.VerifyPassword(model.OldPassword, user.PasswordHash, user.PasswordSalt))
        //        throw new Exception("Invalid old password");

        //    PasswordHelper.CreatePasswordHash(model.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
        //    user.PasswordHash = passwordHash;
        //    user.PasswordSalt = passwordSalt;
        //    await _userRepository.UpdateAsync(user);
        //    return user;
        //}

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
            if(!PasswordHelper.VerifyPassword(model.Password, foundUser.PasswordHash, foundUser.PasswordSalt))
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
            if(model.Password != model.PasswordRepeated)
            {
                return null;
            }
            User user = model.ToUser();
            PasswordHelper.CreatePasswordHash(model.Password, out byte[] passHash, out byte[] passSalt);
            user.PasswordHash = passHash;
            user.PasswordSalt= passSalt;
            await _userRepository.InsertAsync(user);
            return user;
        }
        //private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //{
        //    using (var hmac = new HMACSHA512())
        //    {
        //        passwordSalt = hmac.Key;
        //        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        //    }
        //}

        //private bool VerifyPassword(string password, byte[] hash, byte[] salt)
        //{
        //    using (var hmac = new HMACSHA512(salt))
        //    {
        //        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        //        return computedHash.SequenceEqual(hash);
        //    }
        //}
    }
}
