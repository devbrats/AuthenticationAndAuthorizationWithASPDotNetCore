using AA.Common.Models;
using AA.Common.Services;
using AuthenticationDemo.API.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthenticationDemo.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IRepository _repository;

        public UserController(IUserManager userManager, IRepository repository)
        {
            _userManager = userManager;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetUsers());
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
                    return Ok("User Authenticated " + user.ToString());
                }
            }
            return NotFound();

        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            await _userManager.SignOut();
            return Redirect("~/api/User");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRegistrationDetails userRegistrationDetails)
        {
            var registrationResult =  await _userManager.CreateUser(userRegistrationDetails);

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
