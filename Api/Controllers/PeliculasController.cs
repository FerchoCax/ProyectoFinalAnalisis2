using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;
namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeliculasController : Controller
    {
        private readonly IPeliculas _Pel;

        public PeliculasController(IPeliculas DM)
        {
            _Pel = DM;
        }

        [HttpGet("GetPeliculas")]

        public async Task<IActionResult> GetPeliculas()
        {
            return await _Pel.GetPeliculas();
        }

        [HttpPost("CrearPelicula")]
        {
            return await _Pel.CrearPelicula(pelicula);   
        }
    }
}