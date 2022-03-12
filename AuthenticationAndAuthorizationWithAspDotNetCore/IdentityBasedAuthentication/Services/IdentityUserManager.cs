using AA.Common.Models;
using AA.Common.Services;
using IdentityBasedAuthentication.Data;
using Microsoft.AspNetCore.Identity;
using System;

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

        public bool CreateUser(UserRegistrationDetails userRegistrationDetails)
        {
            var user = new AppIdUser()
            {
                UserName = userRegistrationDetails.Name,
                Email = userRegistrationDetails.EmailID,
                DateOfBirth = userRegistrationDetails.DateOfBirth
            };
            var result =  _userManager.CreateAsync(user, userRegistrationDetails.Password).Result;
            return result.Succeeded;
        }

        public AppUser FindUserByEmail(string emaildID)
        {
            var appIdUser = (AppIdUser) _userManager.FindByEmailAsync(emaildID).Result;
            var user = new AppUser()
            {
                Name = appIdUser.UserName,
                EmailId = appIdUser.Email,
                DateOfBirth = appIdUser.DateOfBirth,
            };
            return user;
        }

        public bool SignIn(AppUser user, string password)
        {
            var appIdUser = _userManager.FindByEmailAsync(user.EmailId).Result;

            return _signInManager.PasswordSignInAsync(appIdUser, password, false, false).Result.Succeeded;
        }

        public void SignOut()
        {
            _signInManager.SignOutAsync();
        }
    }
}
