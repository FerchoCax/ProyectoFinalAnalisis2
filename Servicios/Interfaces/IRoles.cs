using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IRoles
    {
        public Task<IActionResult> CrearRol(Role rol);
        public Task<IActionResult> GetRoles();
        public Task<IActionResult> DesactivarRol(int idRol);
    }
}
