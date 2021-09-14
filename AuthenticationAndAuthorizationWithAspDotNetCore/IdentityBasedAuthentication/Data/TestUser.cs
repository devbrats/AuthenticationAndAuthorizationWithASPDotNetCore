using Microsoft.AspNetCore.Identity;

namespace IdentityBasedAuthentication.Data
{
    // In case of Custom User replace Custom User with Identity User

    public class TestUser:IdentityUser
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Type { get { return "Test User"; } }
    }
}
