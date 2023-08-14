namespace NotesApp.DTOs
{
    public class ErrorDto
    {
        public bool HasError { get; set; }
        public string Message { get; set; }

        public ErrorDto(string? message)
        {
            HasError = true;
            Message = message != null? message : "Generic error";
        }
    }
}
