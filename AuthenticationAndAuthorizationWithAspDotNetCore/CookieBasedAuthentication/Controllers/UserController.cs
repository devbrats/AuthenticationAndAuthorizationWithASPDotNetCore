using AA.Common.Models;
using AA.Common.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Login([FromBody]UserCredentials userCredentials)
        {
            var user = await _userManager.FindUserByEmail(userCredentials.UserName);

            if (user != null)
            {
                bool isUserSignedIn = await _userManager.SignIn(user, userCredentials.Password);
                if (isUserSignedIn)
                {
                    // Create claims for cookie
                    var claims = ClaimsGenerator.CreateClaims(user);

                    // Sign In
                    await HttpContext.SignInAsync(claims);

                    return Ok("User Authenticated \n" + user.ToString());

                }
            }

            return NotFound("Invalid User");
           
        }
    }
}
