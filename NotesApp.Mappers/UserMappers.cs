using NotesApp.Domain.Models;
using NotesApp.DTOs.UserDtos;
using System.Runtime.CompilerServices;

namespace NotesApp.Mappers
{
    public static class UserMappers
    {
        public static User ToUser(this UserRegisterDto model)
        {
            return new User
            {
                Username = model.Username,
                Email = model.Email
            };
        }
    }
}
