using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityBasedAuthentication.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdentityBasedAuthentication.Controllers
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
        private readonly UserManager<TestUser> _userManager;
        private readonly SignInManager<TestUser> _signInManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, UserManager<TestUser>userManager, SignInManager<TestUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
             return Ok("Index");
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login(string userName="Test", string password="abc")
        {
            var user =(TestUser) await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var res = await _signInManager.PasswordSignInAsync(user,password,false, false);
                if (res.Succeeded)
                {
                    return Ok("User Authenticated "+user.Type);
                }
            }
            return NotFound();

        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index");
        }

        [HttpGet("register")]
        public async Task<IActionResult> Register(string userName, string password)
        {
            var user = new TestUser()
            {
                UserName = "Test",
            };
            var res = await _userManager.CreateAsync(user, "abc");

            if (res.Succeeded)
            {
                return RedirectToAction("login");
            }
            else
            {
                return RedirectToAction("index"); ;
            }

        }

        [Authorize]
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
    }
}
