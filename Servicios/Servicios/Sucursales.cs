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
    public class Sucursales:ISucursal
    {
        private readonly DataBaseContext _context;
        private Errores _error;

        public Sucursales(DataBaseContext ctx)
        {
            _context = ctx;
            _error = new Errores();
        }

        public async Task<IActionResult> CrearSucursal(Sucursale sucursal)
        {
            try
            {
                sucursal.FechaIng = DateTime.Now;
                await _context.AddAsync(sucursal);
                await _context.SaveChangesAsync();

                return new ObjectResult(new { estado = 1 }) { StatusCode = 200 };

            }catch(Exception ex)
            {
                return _error.respuestaDeError("Error al momento de crear la sucursal", ex);
            }
        }
        public async Task<IActionResult> GetSucursales()
        {
            try
            {
                List<Sucursale> sucursales = await _context.Sucursales.ToListAsync();
                return new ObjectResult(sucursales) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("Error al momento de listar las sucursales", ex);
            }
        }
    }
}
