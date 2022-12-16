using AA.Common.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace AuthorizationDemo.API.Authorization.JWT
{
    public class TokenGenerator
    {
        public static string GenerateToken(string userName)
        {
            // Create claims on login
            var claims = ClaimsGenerator.CreateJWTClaims(userName);

            // Create Signing Credentials
            var signingCredentials = new SigningCredentials(TokenMetadata.SymmetricKey, TokenMetadata.Algorithm);

            // Creating Json Web Token
            var token = new JwtSecurityToken(
                TokenMetadata.Issuer,
                TokenMetadata.Audience,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signingCredentials
                );

            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenJson;
        }
    }
}
