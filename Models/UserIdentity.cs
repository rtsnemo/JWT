using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace JWT.Models
{
    public class UserIdentity
    {
        private readonly IHttpContextAccessor contextAccessor;
        public UserIdentity(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }
        public string UserId => contextAccessor.HttpContext!.User.FindFirstValue(JwtRegisteredClaimNames.Sid);
    }
}
