using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using CookieBasedAuthentication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CookieBasedAuthentication.Controllers
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

        [HttpGet("login")]
        public string Login()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Dev"),
                new Claim(ClaimTypes.Email,"test@test.com"),
            };

            var identity = new ClaimsIdentity(claims, "TestUserIdentity");

            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity[] { identity });
            HttpContext.SignInAsync(userPrincipal);

            return "User Authenticated";
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
