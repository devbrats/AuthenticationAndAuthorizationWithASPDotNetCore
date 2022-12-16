using System;

namespace AA.Common.Models
{
    public class AppUser
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public AppUserRole Role { get; set; }

        public override string ToString()
        {
            return $"Name : {Name}\nEmailId : {EmailId}\nRole : {Role}";
        }
    }

    public enum AppUserRole
    {
        Restricted,
        Admin,
    }
}
