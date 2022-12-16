using AA.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AA.Common.Data
{
    public class Repository
    {
        public static List<AppUser> Users = new List<AppUser>()
        {
            new AppUser()
            {
                Name = "Miles",
                Password = "test123",
                EmailId = "test1@test.com",
                DateOfBirth = DateTime.Now.AddYears(-20),
                Role =  AppUserRole.Admin
            },
             new AppUser()
            {
                Name = "Peter",
                Password = "test123",
                EmailId = "test2@test.com",
                DateOfBirth = DateTime.Now.AddYears(-10),
                Role =  AppUserRole.Restricted
            }
        };

        public static IEnumerable<WeatherForecast> GetWeatherData()
        {
            string[] summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = summaries[rng.Next(summaries.Length)]
            })
            .ToArray();
        }
    }
}
