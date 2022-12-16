using AA.Common.Models;
using AA.Common.Services;
using IdentityBasedAuthentication.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace IdentityBasedAuthentication.Services
{
    public class IdentityUserManager : IUserManager
    {
        private UserManager<AppIdUser> _userManager;
        private SignInManager<AppIdUser> _signInManager;

        public IdentityUserManager(UserManager<AppIdUser> userManager, SignInManager<AppIdUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public Task<bool> CreateUser(UserRegistrationDetails userRegistrationDetails)
        {
            return Task.Run(() =>
            {
                var user = new AppIdUser()
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
                var appIdUser = (AppIdUser)_userManager.FindByEmailAsync(emaildID).Result;
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
