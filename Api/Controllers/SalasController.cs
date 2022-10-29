using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;
using AccesoDatos;
namespace Api.Controllers
{
    [ApiController]
    [Route("")]
    public class SalasController : Controller
    {
        private readonly ISalas _sala;
        public SalasController(ISalas sala)
        {
            _sala=sala;
        }
      
        [HttpPost("CrearSala")]
        public async Task<IActionResult>CrearSala(Sala sala)
        {
            return await _sala.CrearSala(sala);
        }

        [HttpPost("GetSalas")]
        public async Task<IActionResult> GetSalas(int idSucursal)
        {
            return await _sala.ListarSalasSucursal(idSucursal);
        }
    }
}
