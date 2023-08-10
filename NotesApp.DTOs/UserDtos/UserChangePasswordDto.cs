namespace NotesApp.DTOs.UserDtos
{
    public class UserChangePasswordDto
    {
        public int Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordRepeated { get; set; }
    }
}
