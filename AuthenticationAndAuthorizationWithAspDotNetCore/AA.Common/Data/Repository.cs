using AA.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AA.Common.Data
{
    public class Repository
    {
        private static List<AppUser> _users = new List<AppUser>()
        {
            new AppUser()
            {
                Name = "Miles",
                Password = "test123",
                EmailId = "test1@test.com",
                DateOfBirth = DateTime.Now.AddYears(-20),
                Role =  AppUserRole.Admin
            },
             new AppUser()
            {
                Name = "Peter",
                Password = "test123",
                EmailId = "test2@test.com",
                DateOfBirth = DateTime.Now.AddYears(-10),
                Role =  AppUserRole.Restricted
            }
        };

        public static AppUser GetUser(string emailId)
        {
            return _users.FirstOrDefault(x => x.EmailId.Equals(emailId, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
