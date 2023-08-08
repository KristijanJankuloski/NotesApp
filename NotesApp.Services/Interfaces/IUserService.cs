namespace NotesApp.Services.Interfaces
{
    public interface IUserService
    {
        Task DeleteUserByIdAsync(int id);
    }
}
