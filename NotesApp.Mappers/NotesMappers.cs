using NotesApp.Domain.Models;
using NotesApp.DTOs;

namespace NotesApp.Mappers
{
    public static class NotesMappers
    {
        public static NoteListDto ToNoteListDto(this Note note)
        {
            return new NoteListDto
            {
                Id = note.Id,
                Title = note.Title,
                Text = note.Text,
                UserId = note.UserId,
            };
        }

        public static NoteDto ToNoteDto(this Note note)
        {
            return new NoteDto
            {
                Id = note.Id,
                Title = note.Title,
                Text = note.Text,
                UserId = note.UserId,
                CreateDate = note.CreationDate
            };
        }

        public static Note ToNote(this NoteDto note)
        {
            return new Note
            {
                Title = note.Title,
                Text = note.Text,
                UserId = note.UserId,
                CreationDate = note.CreateDate
            };
        }
    }
}
