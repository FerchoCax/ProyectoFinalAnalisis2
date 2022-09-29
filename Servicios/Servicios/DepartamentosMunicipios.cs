using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios.Interfaces;
using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Servicios.Servicios
{
    public class DepartamentosMunicipios:IDepartamentosMunicipios
    {
        private readonly DataBaseContext _context;
        private Errores _error;
        public DepartamentosMunicipios(DataBaseContext ctx)
        {
            _context = ctx;
            _error = new Errores();
        }

        public async Task<IActionResult> GetDepartamentos()
        {
            try
            {

                List<Departamento> Departamentos = await _context.Departamentos.ToListAsync();
                return new ObjectResult(Departamentos) { StatusCode = 200 };

            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("No se pudieron obtener los departamentos", ex);
            }
        }
        public async Task<IActionResult> GetMunicipios(int codDepartamento)
        {
            try
            {
                var Municipios = await _context.Municipios.Where(e => e.CodDepartamento == codDepartamento).ToListAsync();
                return new ObjectResult(Municipios) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("No se pudieron obtener los municipios", ex);
            }
        }
    }
}
