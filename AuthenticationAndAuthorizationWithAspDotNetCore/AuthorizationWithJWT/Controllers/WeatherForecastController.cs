using AA.Common.Data;
using AA.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AuthorizationWithJWT.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return WeatherData.Get();
        }
    }
}
