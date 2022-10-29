using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : Controller
    {
        public readonly IClientes _clientes;
        public ClientesController(IClientes cliente)
        {
            _clientes = cliente;
        }

        [HttpPost("CrearCliente")]
        public async Task<IActionResult> CrearUsuario(Cliente cliente)
        {
            return await _clientes.CrearCliente(cliente);
        }

        [HttpGet("GetClientes")]
        public async Task<IActionResult> GetCientes()
        {
            return await _clientes.GetClientes();
        }
        [HttpGet("GetComprasCliente")]
        public async Task<IActionResult> GetComprasCliente(int idCliente)
        {
            return await _clientes.GetComprasCliente(idCliente);
        }

       

    }
}
