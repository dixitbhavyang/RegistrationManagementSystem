namespace RegistrationManagementSystem.API.DTOs
{
    public class RegisterDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public List<string> Hobbies { get; set; } = new List<string>();
        public string Address { get; set; } = string.Empty;
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string Pincode { get; set; } = string.Empty;
    }
}
