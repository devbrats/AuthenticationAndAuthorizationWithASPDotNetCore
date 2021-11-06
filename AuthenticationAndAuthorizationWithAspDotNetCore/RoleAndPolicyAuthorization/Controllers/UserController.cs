using AA.Common.Data;
using AA.Common.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityBasedAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Index");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentials userCredentials)
        {
            // GetUser method is simulating user authentication and returning a app user
            var user = Repository.GetUser(userCredentials.UserName);

            if(user!=null && user.Password.Equals(userCredentials.Password))
            {
                var userPrincipal = CreateClaims(user);

                await HttpContext.SignInAsync(userPrincipal);

                return Ok("User Authenticated !\n" + user.ToString());
            }
            return NotFound("Invalid Credentials");
          
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return Redirect("~/api/User");
        }

        private ClaimsPrincipal CreateClaims(AppUser user)
        {
            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(user.Name))
            {
                claims.Add(new Claim(ClaimTypes.Name, user.Name));
            }
            if (!string.IsNullOrEmpty(user.EmailId))
            {
                claims.Add(new Claim(ClaimTypes.Email, user.EmailId));
            }
            if (user.DateOfBirth != null)
            {
                claims.Add(new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString()));
            }
            if (!string.IsNullOrEmpty(user.Role.ToString()))
            {
                claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));
            }

            var identity = new ClaimsIdentity(claims, "UserIdentity");
            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity[] { identity });
            return userPrincipal;
        }

    }
}
