using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController : Controller
    {
        public readonly IRoles _roles;
        public RolesController(IRoles role)
        {
            _roles = role;
        }
        [HttpPost("CrearRol")]
        public async Task<IActionResult> CrearRol(Role rol)
        {
            return await _roles.CrearRol(rol);
            
        }
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            return await _roles.GetRoles();
        }

        [HttpPost("DesactivarRol")]
        public async Task<IActionResult> DesactivarRol(int idRol)
        {
            return await _roles.DesactivarRol(idRol);
        }
    }
}
