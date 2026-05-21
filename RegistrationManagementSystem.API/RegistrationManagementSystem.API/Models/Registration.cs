using System.Reflection.Metadata;

namespace RegistrationManagementSystem.API.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Hobbies { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int StateId { get; set; }
        public State State { get; set; } = null!;
        public int CityId { get; set; }
        public City City { get; set; } = null!;
        public string Pincode { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<RegistrationManagementSystem.API.Models.UserDocument> Documents { get; set; } = new List<RegistrationManagementSystem.API.Models.UserDocument>();
    }
}
