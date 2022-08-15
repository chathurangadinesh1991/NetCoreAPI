using System.Text.Json.Serialization;

namespace NetCoreAPI.Domain.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public string UserRole { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        [JsonIgnore]
        public RefreshTokenResponse RefreshToken { get; set; }
    }
}
