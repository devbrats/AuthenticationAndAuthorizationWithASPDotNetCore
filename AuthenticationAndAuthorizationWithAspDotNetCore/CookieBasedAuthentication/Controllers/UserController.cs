using AA.Common.Data;
using AA.Common.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace CookieBasedAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody]UserCredentials userCredentials)
        {
            var user = Repository.GetUser(userCredentials.UserName);

            if (user != null && user.Password.Equals(userCredentials.Password))
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

            return NotFound("Invalid User");
           
        }
    }
}
