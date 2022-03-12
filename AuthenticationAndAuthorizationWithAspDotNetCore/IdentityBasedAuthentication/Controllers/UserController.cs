using AA.Common.Models;
using AA.Common.Services;
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
            var user = _userManager.FindUserByEmail(userCredentials.UserName);

            if (user != null)
            {
                bool isUserSignedIn = _userManager.SignIn(user, userCredentials.Password);
                if (isUserSignedIn)
                {
                    return Ok("User Authenticated " + user.ToString());
                }
            }
            return NotFound();

        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            _userManager.SignOut();
            return Redirect("~/api/User");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRegistrationDetails userRegistrationDetails)
        {
            var registrationResult =  _userManager.CreateUser(userRegistrationDetails);

            if (registrationResult)
            {
                return StatusCode(201, "User Registered Successfully.");
            }
            else
            {
                return BadRequest();
            }

        }

    }
}
