using IdentityBasedAuthentication.Data;
using IdentityBasedAuthentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityBasedAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;

        public UserController( UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var user = (AppUser)await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var res = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (res.Succeeded)
                {
                    return Ok("User Authenticated " + user);
                }
            }
            return NotFound();

        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/api/Home");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDetails userRegistrationDetails)
        {
            var user = new AppUser()
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
