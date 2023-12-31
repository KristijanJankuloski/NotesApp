﻿using NotesApp.Domain.Enums;
using NotesApp.Domain.Models;
using NotesApp.DTOs.NotesDtos;

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
                Color = (int)note.Color
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
                CreateDate = note.CreationDate,
                Color = (int)note.Color
            };
        }

        public static Note ToNote(this NoteDto note)
        {
            return new Note
            {
                Title = note.Title,
                Text = note.Text,
                UserId = note.UserId,
                CreationDate = note.CreateDate,
                Color = (Color)note.Color
            };
        }
    }
}
