using System.Text.Json.Serialization;

namespace NetCoreAPI.Domain.Models
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JwtToken { get; set; }
        public string UserRole { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }

        public AuthenticateResponse(User user, string jwtToken, string refreshToken)
        {
            Id = user.UserId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            JwtToken = jwtToken;
            UserRole = user.UserRole;
            RefreshToken = refreshToken;
        }
    }
}
