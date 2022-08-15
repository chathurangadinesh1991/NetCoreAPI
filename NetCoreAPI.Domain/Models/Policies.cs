using Microsoft.AspNetCore.Authorization;

namespace NetCoreAPI.Domain.Models
{
    public class Policies
    {
        public const string Users = "User";
        public const string Followers = "Follower";
        public const string Writers = "Writer";
        public const string Editors = "Editor";
        public const string Moderators = "Moderator";

        public static AuthorizationPolicy UsersPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireClaim(Users).Build();
        }

        public static AuthorizationPolicy FollowersPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Followers).Build();
        }

        public static AuthorizationPolicy WritersPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Writers).Build();
        }

        public static AuthorizationPolicy EditorsPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Editors).Build();
        }

        public static AuthorizationPolicy ModeratorsPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Moderators).Build();
        }
    }
}
