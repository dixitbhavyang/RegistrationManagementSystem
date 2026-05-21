namespace RegistrationManagementSystem.API.Models
{
    public class UserDocument
    {
        public int Id { get; set; }
        public int RegistrationId { get; set; }
        public Registration Registration { get; set; } = null!;
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
