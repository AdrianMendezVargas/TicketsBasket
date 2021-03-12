using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TicketsBasket.Infrastructure.Options;

namespace TicketsBasket.Api.Controllers {
    [Authorize(Roles = "Organizer")]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IdentityOptions _identity;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IdentityOptions identity) {
            _logger = logger;
            _identity = identity;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get() {

            Debug.WriteLine(_identity.UserId);

            var rng = new Random();
            return Enumerable.Range(1 , 5).Select(index => new WeatherForecast {
                Date = DateTime.Now.AddDays(index) ,
                TemperatureC = rng.Next(-20 , 55) ,
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
