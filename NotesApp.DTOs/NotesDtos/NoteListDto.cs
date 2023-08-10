using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.DTOs.NotesDtos
{
    public class NoteListDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Color { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
