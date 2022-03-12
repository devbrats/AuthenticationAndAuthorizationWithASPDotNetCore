using AA.Common.Data;
using AA.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AA.Common.Services
{
    public interface IUserManager
    {
        AppUser FindUserByEmail(string emaildID);

        bool SignIn(AppUser user, string password);

        void SignOut();

        bool CreateUser(UserRegistrationDetails userRegistrationDetails);
    }

    public class CommonUserManager : IUserManager
    {
        public bool CreateUser(UserRegistrationDetails userRegistrationDetails)
        {
            return false;
        }

        public AppUser FindUserByEmail(string emaildID)
        {
            return Repository.Users.FirstOrDefault(x => x.EmailId.Equals(emaildID, StringComparison.InvariantCultureIgnoreCase));
        }

        public bool SignIn(AppUser user, string password)
        {
            return user != null && user.Password.Equals(password);
        }

        public void SignOut()
        {
            
        }
    }
}
