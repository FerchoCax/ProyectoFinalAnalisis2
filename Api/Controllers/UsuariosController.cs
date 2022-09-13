using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : Controller
    {
        public readonly IUsuarios _usuarios;
        public UsuariosController(IUsuarios user)
        {
            _usuarios = user;
        }

        [HttpPost("CrearUsuario")]
        public async Task<IActionResult> CrearUsuario(Usuario user)
        {
            return await _usuarios.CrearUsuario(user);
        }
    }
}
