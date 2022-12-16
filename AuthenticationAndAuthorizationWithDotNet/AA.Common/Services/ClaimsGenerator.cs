using AA.Common.Models;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AA.Common.Services
{
    public class ClaimsGenerator
    {
        public static Claim[] CreateJWTClaims(string userName)
        {
            return new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "JWT"),
                new Claim("UserName", userName)
            };
        }

        public static ClaimsPrincipal CreateClaims(AppUser user)
        {
            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(user.Name))
            {
                claims.Add(new Claim(ClaimTypes.Name, user.Name));
            }
            if (!string.IsNullOrEmpty(user.EmailId))
            {
                claims.Add(new Claim(ClaimTypes.Email, user.EmailId));
            }
            if (user.DateOfBirth != null)
            {
                claims.Add(new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString()));
            }
            if (!string.IsNullOrEmpty(user.Role.ToString()))
            {
                claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));
            }

            ClaimsIdentity identity = new ClaimsIdentity(claims, "UserIdentity");
            ClaimsPrincipal userPrincipal = new ClaimsPrincipal(new ClaimsIdentity[] { identity });

            return userPrincipal;
        }
    }

}
