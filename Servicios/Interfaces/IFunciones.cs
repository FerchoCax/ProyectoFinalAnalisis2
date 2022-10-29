using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IFunciones
    {
        public Task<IActionResult> CrearFunciones(int idPelicula, int codSala, DateTime fechaInicio, DateTime FechaFin);
        public Task<IActionResult> ListarFunciones(int sala);
        public Task<IActionResult> CambiarEstadoFuncion(int idFuncion);
    }
}
