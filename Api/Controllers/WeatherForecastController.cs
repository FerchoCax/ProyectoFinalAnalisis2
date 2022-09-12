using Microsoft.AspNetCore.Mvc;
using AccesoDatos;
namespace Api.Controllers
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
        private DataBaseContext _context;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, DataBaseContext db)
        {
            _logger = logger;
            _context = db;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            return Ok(_context.Clientes.ToList());
        }
    }
}