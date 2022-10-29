using Microsoft.AspNetCore.Mvc;
using AccesoDatos;
using Servicios.Interfaces;
namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagenesController : Controller
    {
        private readonly IImagenesPelicula _img;

        public ImagenesController(IImagenesPelicula img)
        {
            _img = img;
        }

        [HttpPost("CargarImagen")]
        public async Task<IActionResult> CargarImagen(ImagenPelicula imagenPelicula)
        {
            return await _img.AgregarImagen(imagenPelicula);
        }
        [HttpGet("GetImagenerPelicula")]
        public async Task<IActionResult> GetImagenesPelicula(int idPelicula)
        {
            return await _img.ListarImagenesPelicula(idPelicula);
        }
        [HttpGet("GetPeliculas")]
        public async Task<IActionResult> GetPeliculas()
        {
            return await _img.ListarPeliculas();
        }

    }
}
