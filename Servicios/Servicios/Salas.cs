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
    public class Salas:ISalas
    {
        private readonly DataBaseContext _context;
        private readonly Errores _error;
        public Salas(DataBaseContext ctx)
        {
            _context = ctx;
            _error = new Errores();
        }
        public async Task<IActionResult> CrearSala(Sala sala)
        {
            try
            {
                using var transacion = _context.Database.BeginTransaction();
                DateTime ahora = DateTime.Now;
                sala.FechaIng = ahora;
                sala.UsuarioIng = "fcaxaj";
                CrearAsientosSala(sala);
                await _context.AddAsync(sala);
                await _context.SaveChangesAsync();
                transacion.Commit();
                return new ObjectResult(new { estado = 2 }) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("Error al momento de crear la sala",ex);
            }
        }

        private void CrearAsientosSala(Sala sala)
        {
            string[] filasSalaGrande = { "A", "B", "C", "B", "D", "E", "F", "G", "H", "I" };
            string[] filasSalaMediana = { "A", "B", "C", "B", "D", "E", "F", "G" };
            string[] filasSalaPequenia = { "A", "B", "C", "B", "D", "E" };
            var ultimaSala = _context.Salas.ToList();
            int NextSala = 1;
            if (ultimaSala.Count != 0)
            {
                NextSala = ultimaSala.Max(s => s.CodSala)+1;
            }
            
            if (sala.CodTipoSala == 1)
            {
                foreach(string letra in filasSalaGrande)
                {
                    int numero = 1;
                    int[] asientosEspeciales = { 1, 15 };
                    while (numero <= 15)
                    {
                        Asiento asi = new Asiento();
                        asi.CodSala = NextSala;
                        asi.Numero = numero.ToString();
                        asi.Fila = letra;
                        asi.UsuarioIng = (DateTime)sala.FechaIng;
                        asi.FechaIng = "fcaxaj";
                        if((letra == "F" || letra == "G") && asientosEspeciales.Contains(numero))
                        {
                            asi.CodTipoAsiento = 2;
                        }
                        else
                        {
                            asi.CodTipoAsiento = 1;
                        }
                        numero++;
                        sala.Asientos.Add(asi);
                    }
                    
                }
            }
            else if(sala.CodTipoSala == 2)
            {
                foreach (string letra in filasSalaMediana)
                {
                    int numero = 1;
                    int[] asientosEspeciales = { 1, 15 };
                    while (numero <= 15)
                    {
                        Asiento asi = new Asiento();
                        asi.CodSala = NextSala;
                        asi.Numero = numero.ToString();
                        asi.Fila = letra;
                        asi.UsuarioIng = (DateTime)sala.FechaIng;
                        asi.FechaIng = "fcaxaj";
                        if ((letra == "E" || letra == "F") && asientosEspeciales.Contains(numero))
                        {
                            asi.CodTipoAsiento = 2;
                        }
                        else
                        {
                            asi.CodTipoAsiento = 1;
                        }
                        numero++;

                        sala.Asientos.Add(asi);
                    }

                }
            }
            else if (sala.CodTipoSala == 3)
            {
                foreach (string letra in filasSalaPequenia)
                {
                    int numero = 1;
                    int[] asientosEspeciales = { 1, 15 };
                    while (numero <= 15)
                    {
                        Asiento asi = new Asiento();
                        asi.CodSala = NextSala;
                        asi.Numero = numero.ToString();
                        asi.Fila = letra;
                        asi.UsuarioIng = (DateTime)sala.FechaIng;
                        asi.FechaIng = "fcaxaj";
                        if ((letra == "B" || letra == "D") && asientosEspeciales.Contains(numero))
                        {
                            asi.CodTipoAsiento = 2;
                        }
                        else
                        {
                            asi.CodTipoAsiento = 1;
                        }
                        numero++;

                        sala.Asientos.Add(asi);
                    }

                }
            }
        }
        public async Task<IActionResult> ListarSalasSucursal(int idSucursal)
        {
            try
            {
                var salas = await _context.Salas.Where(e => e.CodSucursal == idSucursal).ToListAsync();
                return new ObjectResult(salas) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("Error al momento de listar las salas");
            }
        }
    }
}
