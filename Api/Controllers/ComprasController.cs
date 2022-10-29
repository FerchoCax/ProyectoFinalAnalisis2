using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AccesoDatos;
using Servicios.Interfaces;
using System.Net.Mail;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace Apii.Controllers
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

        [HttpPost("ComprarBoletos")]
        public async Task<IActionResult> ComprarBoletos(TodoCompra compra)
        {
            return await _compras.ComprarBoletos(compra);
        }

        [HttpGet("GetInfoCliente")]
        public async Task<IActionResult> GetInfoCliente(int idCliente)
        {
            return await _compras.GetInfoCliente(idCliente);
        }

        [HttpPost("ValidarBoleto")]
        public async Task<IActionResult> ValidarBoleto(int idFuncion, int idBoleto)
        {
            return await _compras.ValidarBoleto(idBoleto, idFuncion);
        }
        [HttpGet("GetPeliculaFuncion")]
        public async Task<IActionResult> GetPeliculaFuncion(int codFuncion)
        {
            return await _compras.GetInfoPeliculaFuncin(codFuncion);
        }

    }
}
