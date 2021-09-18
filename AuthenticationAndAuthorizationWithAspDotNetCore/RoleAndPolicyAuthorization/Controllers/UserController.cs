using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RoleAndPolicyAuthorization.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityBasedAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("login")]
        public async Task<IActionResult> Login(string emailId, string password, string role)
        {
            // GetUser method is simulating user authentication and returning a app user
            var user = GetUser(emailId, role);

            var userPrincipal = CreateClaims(user);

            await HttpContext.SignInAsync(userPrincipal);

            return Ok("User Authenticated !");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return Redirect("~/api/Home");
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
            if (user.DateOfBirth!=null)
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

        private AppUser GetUser(string emailId, string role)
        {
            return new AppUser()
            {
                Name = "Devbrat",
                Password = "test123",
                EmailId = "test@test.com",
                DateOfBirth = DateTime.Now.AddYears(-20),
                Role = role?.ToUpper()=="ADMIN"?AppUserRole.Admin:AppUserRole.Restricted
            };
        }

    }
}
