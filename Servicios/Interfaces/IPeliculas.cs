using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
namespace Servicios.Interfaces
{
    public interface IPeliculas
    {
        Task<IActionResult> CrearPelicula(IPeliculas pelicula);
        Task<IActionResult> GetPeliculas();
    }
}
