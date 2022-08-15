using System.ComponentModel.DataAnnotations;

namespace NetCoreAPI.Domain.Models
{
    public class LoginRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Password { get; set; }
    }
}