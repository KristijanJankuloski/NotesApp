using NotesApp.DataAccess.Repositories.Interfaces;
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
    }
}
