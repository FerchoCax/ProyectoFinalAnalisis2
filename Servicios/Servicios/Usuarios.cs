using AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Servicios
{
    public class Usuarios:IUsuarios
    {
        private readonly Errores _error;
        private readonly DataBaseContext _context;
        public Usuarios(DataBaseContext ctx)
        {
            _error = new Errores();
            _context = ctx;
        }

        public async Task<IActionResult> CrearUsuario(Usuario usuario)
        {
            try
            {
                string newPass = Encrypt.GetSHA256(usuario.Password);
                usuario.Password = newPass;
                usuario.FechaIng = DateTime.Now;
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return new ObjectResult(1) { StatusCode = 200 };
            }catch(Exception ex)
            {
                return _error.respuestaDeError("Error el momento de crear el usuario", ex);
            }
        }

    }
}
