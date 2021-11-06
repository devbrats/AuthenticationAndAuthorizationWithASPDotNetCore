using AA.Common.Data;
using AA.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RoleAndPolicyAuthorization.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [Authorize(Policy = "Claim.Email")]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return WeatherData.Get();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult GetAdminContent()
        {
            return Ok("Admin Content");
        }
    }
}
