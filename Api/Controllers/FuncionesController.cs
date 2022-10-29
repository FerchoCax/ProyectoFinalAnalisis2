using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;

namespace Apii.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FuncionesController : Controller
    {
        private readonly IFunciones _funciones;

        public FuncionesController(IFunciones func)
        {
            _funciones = func;
        }
        [HttpPost("CrearFunciones")]
        public async Task<IActionResult> CrearFunciones(int codPelicula, int codSala, DateTime fechaInicio, DateTime fechaFin)
        {
            return await _funciones.CrearFunciones(codPelicula, codSala, fechaInicio, fechaFin);
        }

        [HttpGet("GetFuncionesSala")]
        public async Task<IActionResult> GetFuncionesSala(int idSala)
        {
            return await _funciones.ListarFunciones(idSala);
        }

        //[HttpGet("GetFunciones")]
        //public async Task<IActionResult> GetFuncionesPelicula(int idPelicula, int sala)
        //{

        //}
    }
}
