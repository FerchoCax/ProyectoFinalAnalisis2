using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servicios.Interfaces;
namespace Servicios.Servicios
{
    public class Compras:ICompras
    {
        private DataBaseContext _dataBaseContext;
        private Errores _errores;
        public Compras(DataBaseContext ctx)
        {
            _dataBaseContext = ctx;
            _errores = new Errores();
        }

        public async Task<IActionResult> GetPeliculasSucursal(int idSucursal)
        {
            try
            {
                var codPeliculas = await (from sala in _dataBaseContext.Salas
                                       join funcion in _dataBaseContext.Funciones on sala.CodSala equals funcion.CodSala
                                       join pelicula in _dataBaseContext.Peliculas on funcion.CodPelicula equals pelicula.CodPelicula
                                       where sala.CodSucursal == idSucursal
                                       && funcion.FechaHoraInicio >= DateTime.Now && funcion.FechaHoraFin <= DateTime.Now.AddDays(3)
                                       select
                                       pelicula.CodPelicula
                                       ).Distinct().ToArrayAsync();

                var Peliculas = await _dataBaseContext.Peliculas.Where(p => codPeliculas.Contains(p.CodPelicula))
                                       .Include(e => e.CodClasificacionNavigation)
                                       .Include(e => e.Imagenes)
                                       .ThenInclude(n => n.codTipoImagenNavigator)
                                       .Include(e => e.Funciones.Where(a => a.FechaHoraInicio >= DateTime.Now && a.FechaHoraFin <= DateTime.Now.AddDays(3)).OrderBy(j => j.FechaHoraInicio))
                                       .ToListAsync();

                                       
                return new ObjectResult(Peliculas) { StatusCode = 200 };
                                       
            }catch(Exception ex)
            {
                return _errores.respuestaDeError("Error al momento de cargar las peliculas", ex);
            }
        }

        public async Task<IActionResult> GetAsientosSala(int idSala, int idFuncion)
        {
            try
            {
                var asientos = _dataBaseContext.Asientos.Where(i => i.CodSala == idSala).Select(e => new {e.CodAsiento}).ToArray();
                List <int> codAsientos = new List<int>();
                foreach(var asi in asientos)
                {
                    codAsientos.Add(asi.CodAsiento);
                }
                var Sala = await _dataBaseContext.Salas.Where(e => e.CodSala == idSala)
                           .Include(n => n.Asientos)
                           .ThenInclude(m => m.Boletos.Where(g => codAsientos.Contains(g.CodAsiento))).FirstOrDefaultAsync();


                return new ObjectResult(Sala) { StatusCode= 200};
            }
            catch (Exception ex) 
            {
                return _errores.respuestaDeError("Error al momento de cargas los hacientos de la sala", ex);
            }
        }

    }
}
