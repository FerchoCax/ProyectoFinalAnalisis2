using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
namespace Servicios.Interfaces
{
    public interface ISucursal
    {
        Task<IActionResult> CrearSucursal(Sucursale sucursal);
        Task<IActionResult> GetSucursales();
    }
}
