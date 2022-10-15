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


    }
}
