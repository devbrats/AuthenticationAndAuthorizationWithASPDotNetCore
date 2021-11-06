using AA.Common.Data;
using AA.Common.Models;
using AAWithOAuth.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AAWithOAuth.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserCredentials userCredentials)
        {
            var user = Repository.GetUser(userCredentials.UserName);

            if (user != null && user.Password.Equals(userCredentials.Password))
            {
                // Create claims on login
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, "test"),
                    new Claim("UserName",userCredentials.UserName)
                };

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

            return NotFound();
        }
    }
}
