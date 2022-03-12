using AA.Common.Data;
using AA.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RoleAndPolicyAuthorization.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [Authorize(Policy = "Claim.Email")]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Repository.GetWeatherData();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult GetAdminContent()
        {
            return Ok("Admin Content");
        }
    }
}
