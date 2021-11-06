using AA.Common.Models;
using IdentityBasedAuthentication.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityBasedAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<AppIdUser> _userManager;
        private SignInManager<AppIdUser> _signInManager;

        public UserController( UserManager<AppIdUser> userManager, SignInManager<AppIdUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Index");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentials userCredentials)
        {
            var user = (AppIdUser)await _userManager.FindByEmailAsync(userCredentials.UserName);
            if (user != null)
            {
                var res = await _signInManager.PasswordSignInAsync(user, userCredentials.Password, false, false);
                if (res.Succeeded)
                {
                    return Ok("User Authenticated " + user.ToString());
                }
            }
            return NotFound();

        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/api/User");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRegistrationDetails userRegistrationDetails)
        {
            var user = new AppIdUser()
            {
                UserName = userRegistrationDetails.Name,
                Email = userRegistrationDetails.EmailID,
                DateOfBirth = userRegistrationDetails.DateOfBirth
            };

            var registrationResult = await _userManager.CreateAsync(user, userRegistrationDetails.Password);

            if (registrationResult.Succeeded)
            {
                return StatusCode(201, "User Registered Successfully.");
            }
            else
            {
                return BadRequest(registrationResult.Errors);
            }

        }

    }
}
