using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface ISalas
    {
        public Task<IActionResult> CrearSala(Sala sala);
        public Task<IActionResult> ListarSalasSucursal(int idSucursal);
    }
}
