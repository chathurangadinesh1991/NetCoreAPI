using System.Collections.Generic;

namespace NetCoreAPI.Persistence.Models
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public string UserId { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string UserRole { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
