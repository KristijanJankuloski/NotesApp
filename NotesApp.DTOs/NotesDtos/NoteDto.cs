namespace NotesApp.DTOs.NotesDtos
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int Color { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
