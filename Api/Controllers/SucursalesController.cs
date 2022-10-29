using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;
using AccesoDatos;

namespace Apii.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SucursalesController : Controller
    {
        private readonly ISucursal _sucursal;

        public SucursalesController(ISucursal suculenta)
        {
            _sucursal = suculenta;
        }

        [HttpGet("GetSucursales")]
        public async Task<IActionResult> GetSucursales()
        {
            return await _sucursal.GetSucursales();
        }

        [HttpPost("CrearSucursal")]
        public async Task<IActionResult> CrearSucursal(Sucursale sucursal)
        {
            return await _sucursal.CrearSucursal(sucursal);
        }
    }
}
