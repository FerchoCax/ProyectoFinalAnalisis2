using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Microsoft.AspNetCore.Mvc;

namespace Servicios.Interfaces
{
    public interface ICompras
    {
        public Task<IActionResult> GetPeliculasSucursal(int idSucursal);
        public Task<IActionResult> GetAsientosSala(int idSala, int idFuncion);
        public Task<IActionResult> ComprarBoletos(TodoCompra compra);
        public Task<IActionResult> GetInfoCliente(int idCliente);
        public Task<IActionResult> ValidarBoleto(int codAsiento, int codFuncion);
        public Task<IActionResult> GetInfoPeliculaFuncin(int idFuncion);

    }
}
