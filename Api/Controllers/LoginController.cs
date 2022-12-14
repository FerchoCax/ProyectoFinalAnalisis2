using AccesoDatos;
using AccesoDatos.entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servicios.Interfaces;

namespace Apii.Controllers
{
    [ApiController]
    [Route("")]
    public class LoginController : Controller
    {
        private IJtAuth aut;
        private readonly DataBaseContext _context;
        public LoginController(IJtAuth auts, DataBaseContext ctx)
        {
            aut = auts;
            _context = ctx;
        }

        [HttpPost("Login/{tipo}")]
        public IActionResult Login(Login login, string tipo)
        {
            LoginReturn user = aut.GetUser(login.username, login.password, tipo);
            if (!user.username.Equals(null))
            {
                var token = aut.Autentication(login.username, login.password, tipo);
                if (token == null)
                {
                    return Unauthorized();
                }
                user.token = token;
                if (tipo == "U")
                {
                    Usuario us = _context.Usuarios.Where(e => e.Username == login.username).First();
                    user.Nombres = us.Nombres;
                    user.apellidos = us.Apellidos;
                    user.codUser = us.CodUsuario;

                }
                else if (tipo == "C")
                {
                    Cliente cl = _context.Clientes.Where(e => e.Username == login.username).First();
                    user.Nombres = cl.Nombres;
                    user.apellidos = cl.Apellidos;
                    user.codUser = cl.CodCliente;
                }

                user.tipo = tipo == "C" ? "CLIENTE" : tipo == "U" ? "USUARIO" : "";
                return Ok(user);
            }
            return Unauthorized();
        }

        [HttpGet("Prueba")]
        public IActionResult Prueba()
        {
            var r = _context.valorString.FromSqlRaw("select curdate() as valor").FirstOrDefault();
            return Ok(r);

        }

    }
}
