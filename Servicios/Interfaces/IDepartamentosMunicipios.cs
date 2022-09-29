using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IDepartamentosMunicipios
    {
        Task<IActionResult> GetDepartamentos();
        Task<IActionResult> GetMunicipios(int codDepartamento);
    }
}
