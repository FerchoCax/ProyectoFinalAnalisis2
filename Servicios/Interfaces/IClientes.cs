using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IClientes
    {
        public Task<IActionResult> CrearCliente(Cliente cliente);
    }
}
