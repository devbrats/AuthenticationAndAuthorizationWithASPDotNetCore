using AA.Common.Data;
using AA.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AAWithAzureAD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Repository.GetWeatherData();
        }
    }
}
