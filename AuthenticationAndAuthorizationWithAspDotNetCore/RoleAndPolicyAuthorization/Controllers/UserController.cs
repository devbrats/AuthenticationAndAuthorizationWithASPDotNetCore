using AA.Common.Models;
using AA.Common.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityBasedAuthentication.Controllers
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
            // GetUser method is simulating user authentication and returning a app user
            var user = await _userManager.FindUserByEmail(userCredentials.UserName);

            if (user != null)
            {
                bool isUserSignedIn = await _userManager.SignIn(user, userCredentials.Password);
                if (isUserSignedIn)
                {
                    var userPrincipal = ClaimsGenerator.CreateClaims(user);

                    await HttpContext.SignInAsync(userPrincipal);

                    return Ok("User Authenticated !\n" + user.ToString());
                }
            }
            return NotFound("Invalid Credentials");
          
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return Redirect("~/api/User");
        }

    }
}
