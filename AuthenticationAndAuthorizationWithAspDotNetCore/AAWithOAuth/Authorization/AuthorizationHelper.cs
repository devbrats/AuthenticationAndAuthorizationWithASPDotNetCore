using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AAWithOAuth.Authorization
{
    public class AuthorizationHelper
    {
        private static string _secret = "A big secret key to accomodate minimum length policy for the key";

        public const string Issuer = "https://localhost:44344/";
        public const string Audience = "https://localhost:44344/";

        public static SymmetricSecurityKey SymmetricKey 
        { 
            get
            {
                var secretBytes = Encoding.UTF8.GetBytes(_secret);
                return new SymmetricSecurityKey(secretBytes);
            }
        }

        public static string Algorithm
        {
            get
            {
                return SecurityAlgorithms.HmacSha256;
            }
        }
    }
}
