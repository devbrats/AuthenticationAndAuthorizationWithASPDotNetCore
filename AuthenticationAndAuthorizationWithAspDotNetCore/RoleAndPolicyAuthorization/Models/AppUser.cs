using System;

namespace RoleAndPolicyAuthorization.Models
{
    public class AppUser
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public AppUserRole Role { get; set; }
    }

    public enum AppUserRole
    {
        Restricted,
        Admin,
    }
}
