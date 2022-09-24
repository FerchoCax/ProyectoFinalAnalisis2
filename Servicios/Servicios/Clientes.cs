using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Servicios
{
    public class Clientes: IClientes
    {
        private readonly Errores _error;
        private readonly DataBaseContext _context;
        public Clientes(DataBaseContext ctx)
        {
            _error = new Errores();
            _context = ctx;
        }

        public async Task<IActionResult> CrearCliente(Cliente cliente)
        {
            try
            {
                string newPass = Encrypt.GetSHA256(cliente.Password);
                cliente.Password = newPass;
                cliente.FechaIng = DateTime.Now;
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return new ObjectResult(1) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return _error.respuestaDeError("Error el momento de crear el usuario", ex);
            }
        }

        public async Task<IActionResult> GetClientes()
        {
            try
            {
                return new ObjectResult(await _context.Clientes.ToListAsync());
            }catch(Exception ex)
            {
                return _error.respuestaDeError("Error al obtener el listado de clientes", ex);
            }
        }


    }
}
