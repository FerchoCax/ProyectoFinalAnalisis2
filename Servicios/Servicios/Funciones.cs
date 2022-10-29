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
    public class Funciones:IFunciones
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly Errores _errores;
        public Funciones(DataBaseContext ctx)
        {
            _dataBaseContext = ctx;
            _errores = new Errores();
        }

        public async Task<IActionResult> CrearFunciones(int idPelicula, int codSala, DateTime fechaInicio, DateTime FechaFin)
        {
            try
            {
                var pelicula = await _dataBaseContext.Peliculas.Where(e => e.CodPelicula == idPelicula).FirstOrDefaultAsync();
                int horaDuracion = pelicula.Horas;
                int miuntosPelicua = pelicula.Minutos != null ? pelicula.Minutos : 0;
                int horaInicio = 10;
                int horaFinal = 23;
                DateTime Ahora = DateTime.Now;
                DateTime inicioFuncion = fechaInicio.Date.AddHours(10);
                List<Funcione> funciones = new List<Funcione>();
                while (inicioFuncion < FechaFin.Date.AddDays(1))
                {
                    while (horaInicio<=horaFinal)
                    {
                        Funcione func = new Funcione();
                        func.FechaIng = Ahora;
                        func.CodPelicula = pelicula.CodPelicula;
                        func.CodSala = codSala;
                        func.FechaHoraInicio = inicioFuncion;
                        func.FechaHoraFin = inicioFuncion.AddHours(horaDuracion).AddMinutes(miuntosPelicua);
                        func.UsuarioIng = "fcaxaj";
                        func.Estado = "P";
                        horaInicio = horaInicio + horaDuracion + 1;
                        inicioFuncion = inicioFuncion.Date.AddHours(horaInicio);
                        funciones.Add(func);
                    }
                    horaInicio = 10;
                    if(inicioFuncion.Hour <8)
                    {
                        inicioFuncion = inicioFuncion.Date.AddHours(10);
                    }
                    else
                    {
                        inicioFuncion = inicioFuncion.Date.AddDays(1).AddHours(10);
                    }


                }
                await _dataBaseContext.Funciones.AddRangeAsync(funciones);
                _dataBaseContext.SaveChanges();
                return new ObjectResult(new {estado = 1}) { StatusCode = 200 };

            }catch(Exception ex)
            {
                return _errores.respuestaDeError("Error al momento de crear las funciones",ex);
            }
        }
        public async Task<IActionResult> ListarFunciones(int sala)
        {
            try
            {
                var funciones = await _dataBaseContext.Funciones.Where(e => e.CodSala == sala).ToListAsync();

                return new ObjectResult(funciones) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return _errores.respuestaDeError("Error al momento de crear las listar las funciones", ex);
            }
        }
        public async Task<IActionResult> CambiarEstadoFuncion(int idFuncion)
        {
            try
            {
                return new ObjectResult(1) { StatusCode = 200 };

            }
            catch (Exception ex)
            {
                return _errores.respuestaDeError("Error al momento de cambiar el estado de la funcion", ex);
            }
        }
    }
}
