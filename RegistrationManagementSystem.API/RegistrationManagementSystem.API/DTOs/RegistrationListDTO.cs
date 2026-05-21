namespace RegistrationManagementSystem.API.DTOs
{
    public class RegistrationListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Hobbies { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Pincode { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<DocumentDTO> Documents { get; set; } = new List<DocumentDTO>();
    }
}
