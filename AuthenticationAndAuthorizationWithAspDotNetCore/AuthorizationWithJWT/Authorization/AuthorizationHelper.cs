using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthorizationWithJWT.Authorization
{
    public class AuthorizationHelper
    {
        private static string _secret;

        public static string Issuer { get; private set; }
        public static string Audience { get; private set; }

        public static void Init(IConfiguration configuration)
        {
            _secret = configuration.GetValue<string>("JWT:Secret");
            Issuer = configuration.GetValue<string>("JWT:Issuer");
            Audience = configuration.GetValue<string>("JWT:Audience");
        }

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
