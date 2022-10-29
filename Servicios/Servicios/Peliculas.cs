using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios.Interfaces;
using AccesoDatos;
using Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Servicios.Servicios
{
	 public class Peliculas:IPeliculas
	{
		private readonly DataBaseContext _context;
		private Errores _error;
		public Peliculas(DataBaseContext ctx)
		{
			 _context = ctx;
			_error = new Errores();
		}

		public async Task<IActionResult> CrearPelicula(AccesoDatos.Pelicula pelicula) 
		{
            try
            {
				pelicula.FechaIng = DateTime.Now;
				await _context.AddAsync(pelicula);
				await _context.SaveChangesAsync();

				return new ObjectResult(new { estado = 1 }) { StatusCode = 200 };
            }
            catch (Exception ex)
            {

				return _error.respuestaDeError("Error al momento de crear la peicula", ex);
			}
		}
	
		public async Task<IActionResult> GetPeliculas()
        {
            try
            {
				List<Pelicula> Peliculas = await _context.Peliculas.ToListAsync();
				return new ObjectResult(Peliculas) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
				return _error.respuestaDeError("Error al momento de listar las peliculas", ex);
            }
        }
	}
}
