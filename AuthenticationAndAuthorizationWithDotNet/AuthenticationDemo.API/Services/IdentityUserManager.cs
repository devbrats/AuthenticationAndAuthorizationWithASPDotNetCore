using AA.Common.Models;
using AA.Common.Services;
using AuthenticationDemo.API.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AuthenticationDemo.API.Services
{
    public class IdentityUserManager : IUserManager
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public IdentityUserManager(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public Task<bool> CreateUser(UserRegistrationDetails userRegistrationDetails)
        {
            return Task.Run(() =>
            {
                var user = new User()
                {
                    UserName = userRegistrationDetails.Name,
                    Email = userRegistrationDetails.EmailID,
                    DateOfBirth = userRegistrationDetails.DateOfBirth
                };
                var result = _userManager.CreateAsync(user, userRegistrationDetails.Password).Result;
                return result.Succeeded;
            });

        }

        public Task<AppUser> FindUserByEmail(string emaildID)
        {
            return Task.Run(() =>
            {
                var appIdUser = (User)_userManager.FindByEmailAsync(emaildID).Result;
                var user = new AppUser()
                {
                    Name = appIdUser.UserName,
                    EmailId = appIdUser.Email,
                    DateOfBirth = appIdUser.DateOfBirth,
                };
                return user;
            });

        }

        public Task<bool> SignIn(AppUser user, string password)
        {
            return Task.Run(() =>
            {
                var appIdUser = _userManager.FindByEmailAsync(user.EmailId).Result;
                return _signInManager.PasswordSignInAsync(appIdUser, password, false, false).Result.Succeeded;
            });

        }

        public Task SignOut()
        {
            return Task.Run(() =>
            {
                _signInManager.SignOutAsync();
            });
        }
    }
}
