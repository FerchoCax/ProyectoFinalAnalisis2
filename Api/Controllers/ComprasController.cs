using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AccesoDatos;
using Servicios.Interfaces;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComprasController : Controller
    {
        private readonly ICompras _compras;
        public ComprasController(ICompras cpm)
        {
            _compras = cpm;
        }
        
        [HttpGet("GetPeliculasSucursal")]
        public async Task<IActionResult> GetPeliculasSucursal(int id)
        {
            return await _compras.GetPeliculasSucursal(id);
        }

        [HttpGet("GetAsientosSala")]
        public async Task<IActionResult> GetAsientosSala(int idSala, int idFuncion)
        {
            return await _compras.GetAsientosSala(idSala, idFuncion);
        }

    }
}
