using System.ComponentModel.DataAnnotations;


namespace NotesApp.Domain.Models
{
    public class User : BaseEntity
    {
        [MaxLength(30)]
        public string Username { get; set; }
        [MaxLength(30)]
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<Note> Notes { get; set; } = new();
        public string? LastToken { get; set; }
    }
}
