using System;

namespace IdentityBasedAuthentication.Models
{
    public class UserRegistrationDetails
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string EmailID { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
