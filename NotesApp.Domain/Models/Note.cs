using System.ComponentModel.DataAnnotations;

namespace NotesApp.Domain.Models
{
    public class Note : BaseEntity
    {
        [MaxLength(30)]
        public string Title { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
