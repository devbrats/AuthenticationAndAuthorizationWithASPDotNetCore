using Microsoft.AspNetCore.Identity;
using System;

namespace AuthenticationDemo.API.Data
{
    // In case of Custom User replace Custom User with Identity User

    public class User:IdentityUser
    {
        public DateTime DateOfBirth { get; set; }
        public string Type { get { return "App User"; } }
    }
}
