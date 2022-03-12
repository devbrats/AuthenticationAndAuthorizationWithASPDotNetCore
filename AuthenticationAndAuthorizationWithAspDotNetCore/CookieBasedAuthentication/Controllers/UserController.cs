using AA.Common.Models;
using AA.Common.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace CookieBasedAuthentication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("authenticate")]
        public IActionResult Login([FromBody]UserCredentials userCredentials)
        {
            var user = _userManager.FindUserByEmail(userCredentials.UserName);

            if (user != null)
            {
                bool isUserSignedIn = _userManager.SignIn(user, userCredentials.Password);
                if (isUserSignedIn)
                {
                    // Create claims for cookie
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name,user.Name),
                        new Claim(ClaimTypes.Email,user.EmailId),
                    };

                    var identity = new ClaimsIdentity(claims, "AppUserIdentity");
                    var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity[] { identity });

                    // Sign In
                    HttpContext.SignInAsync(userPrincipal);

                    return Ok("User Authenticated \n" + user.ToString());

                }
            }

            return NotFound("Invalid User");
           
        }
    }
}
