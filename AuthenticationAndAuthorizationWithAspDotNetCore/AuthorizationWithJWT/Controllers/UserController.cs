using AA.Common.Models;
using AA.Common.Services;
using AuthorizationWithJWT.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace AuthorizationWithJWT.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Login([FromBody] UserCredentials userCredentials)
        {
            var user = await _userManager.FindUserByEmail(userCredentials.UserName);

            if (user != null)
            {
                bool isUserSignedIn = await _userManager.SignIn(user, userCredentials.Password);
                if (isUserSignedIn)
                {
                    // Create claims on login
                    var claims = ClaimsGenerator.CreateJWTClaims(userCredentials.UserName);

                    // Create Signing Credentials
                    var signingCredentials = new SigningCredentials(AuthorizationHelper.SymmetricKey, AuthorizationHelper.Algorithm);

                    // Creating Json Web Token
                    var token = new JwtSecurityToken(
                        AuthorizationHelper.Issuer,
                        AuthorizationHelper.Audience,
                        claims,
                        notBefore: DateTime.Now,
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials
                        );

                    var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(new { token = tokenJson });
                    // This token along with keyword "Bearer" is sent in Authorization header to  validate request.
                }
            }

            return NotFound();
        }
    }
}
