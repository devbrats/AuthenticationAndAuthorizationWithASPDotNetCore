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
        [HttpGet("login")]
        public IActionResult Login(string userName, string password)
        {
            // Create claims on login
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "test"),
                new Claim("Test","TestID")
            };

            // Create Signing Credentials
            var signingCredentials = new SigningCredentials(AuthorizationHelper.SymmetricKey, AuthorizationHelper.Algorithm);

            // Creating Json Web Token
            var token = new JwtSecurityToken(
                AuthorizationHelper.Issuer,
                AuthorizationHelper.Audience,
                claims,
                notBefore : DateTime.Now,
                expires:DateTime.Now.AddHours(1),
                signingCredentials
                );

            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok( new { token = tokenJson } );
        }

    }
}
