using AA.Common.Models;
using AA.Common.Services;
using AuthorizationDemo.API.Authorization.JWT;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthorizationDemo.API.Authorization
{
    public class AuthorizationService
    {
        public static OkObjectResult Authorize(AuthorizationType type, AppUser user, Action<ClaimsPrincipal> signInHandler = null)
        {
            if (type == AuthorizationType.JWT)
            {
                var token = TokenGenerator.GenerateToken(user.EmailId);

                return new OkObjectResult(new { token });
                // This token along with keyword "Bearer" is sent in Authorization header to  validate request.
            }
            else
            {
                var userPrincipal = ClaimsGenerator.CreateClaims(user);

                signInHandler?.Invoke(userPrincipal);

                return new OkObjectResult("User Authenticated !\n" + user.ToString());
            }
        }
    }
}
