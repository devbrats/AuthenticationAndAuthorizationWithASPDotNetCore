using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RoleAndPolicyAuthorization.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("index")]
        public string Index()
        {
            return "Index";
        }

        [HttpGet("login")]
        public async Task<string> Login()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Dev"),
                new Claim(ClaimTypes.Email,"test@test.com"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var identity = new ClaimsIdentity(claims, "UserIdentity");

            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity[] { identity });

            await HttpContext.SignInAsync(userPrincipal);

            return "User Authenticated";
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("index");
        }

        [Authorize(Policy = "Claim.Email")]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public string Get_1()
        {
            return "Admin Page";
        }
    }
}
