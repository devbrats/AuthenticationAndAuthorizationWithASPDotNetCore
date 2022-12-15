using AA.Common.Models;
using AA.Common.Services;
using AuthorizationDemo.API.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthorizationWithJWT.Controllers
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

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Index");
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Login([FromBody] UserCredentials userCredentials)
        {
            var user = await _userManager.FindUserByEmail(userCredentials.UserName);

            if (user != null)
            {
                bool isUserSignedIn = await _userManager.SignIn(user, userCredentials.Password);
                if (isUserSignedIn)
                {
                   Action<ClaimsPrincipal> signInHandler = null;
                   if(ConfigurationService.AuthorizationType == AuthorizationType.RoleAndPolicy)
                    {
                        signInHandler = async (claims) =>
                        {
                            await HttpContext.SignInAsync(claims);
                        };
                    }

                    return AuthorizationService.Authorize(ConfigurationService.AuthorizationType, user, signInHandler);
                }
            }

            return NotFound();
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return Redirect("~/api/User");
        }
    }
}
