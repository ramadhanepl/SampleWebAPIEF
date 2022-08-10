using System.Text.Json.Serialization;

namespace SampleWebAPI.DTO
{
    public class UserReadDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
