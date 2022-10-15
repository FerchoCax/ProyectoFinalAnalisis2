using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Microsoft.AspNetCore.Mvc;

namespace Servicios.Interfaces
{
    public interface IImagenesPelicula
    {
        public Task<IActionResult> AgregarImagen(ImagenPelicula imagenPelicula);
        public Task<IActionResult> ListarImagenesPelicula(int idPelicula);
        public Task<IActionResult> ListarPeliculas();
    }
}
