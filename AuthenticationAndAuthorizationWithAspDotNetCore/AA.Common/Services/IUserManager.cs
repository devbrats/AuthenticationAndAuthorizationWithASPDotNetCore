using AA.Common.Data;
using AA.Common.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AA.Common.Services
{
    public interface IUserManager
    {
        Task<AppUser> FindUserByEmail(string emaildID);

        Task<bool> SignIn(AppUser user, string password);

        Task SignOut();

        Task<bool> CreateUser(UserRegistrationDetails userRegistrationDetails);
    }

    public class CommonUserManager : IUserManager
    {
        public Task<bool> CreateUser(UserRegistrationDetails userRegistrationDetails)
        {
            return Task.Run(() =>
            {
                return false;
            });
           
        }

        public Task<AppUser> FindUserByEmail(string emaildID)
        {
            return Task.Run(() =>
            {
                return Repository.Users.FirstOrDefault(x => x.EmailId.Equals(emaildID, StringComparison.InvariantCultureIgnoreCase));
            });
            
        }

        public Task<bool> SignIn(AppUser user, string password)
        {
            return Task.Run(() =>
            {
                return user != null && user.Password.Equals(password);
            });
            
        }

        public Task SignOut()
        {
            return Task.Run(() =>
            {
            });
        }
    }

}
