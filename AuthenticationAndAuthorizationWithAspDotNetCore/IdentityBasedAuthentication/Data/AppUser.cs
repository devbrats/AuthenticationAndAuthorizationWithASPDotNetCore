using Microsoft.AspNetCore.Identity;
using System;

namespace IdentityBasedAuthentication.Data
{
    // In case of Custom User replace Custom User with Identity User

    public class AppUser:IdentityUser
    {
        public DateTime DateOfBirth { get; set; }
        public string Type { get { return "App User"; } }
    }
}
