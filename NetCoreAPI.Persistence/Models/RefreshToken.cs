namespace NetCoreAPI.Persistence.Models
{
    public partial class RefreshToken
    {
        public int TokenId { get; set; }
        public string UserId { get; set; }
        public bool IsActive { get; set; }
        public string Token { get; set; }

        public virtual UserInfo User { get; set; }
    }
}
