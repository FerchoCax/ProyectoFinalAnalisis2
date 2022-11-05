using AccesoDatos;
using AccesoDatos.entidades;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Servicios
{
    public class Auth : IJtAuth
    {
        private readonly IConfiguration _config;
        private readonly DataBaseContext _context;
        public Auth(IConfiguration conf, DataBaseContext ctx)
        {
            _config = conf;
            _context = ctx; 
        }

        public string Autentication(string username, string password, string tipo)
        {
            if (!ValidateUser(username, password, tipo))
            {
                return null;
            }
            else
            {
                var TokenHandler = new JwtSecurityTokenHandler();
                var TokenKey = Encoding.ASCII.GetBytes(_config["JWTKey"]);
                var TokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                        new Claim(ClaimTypes.Name,username)
                        }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(TokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = TokenHandler.CreateToken(TokenDescriptor);
                return (TokenHandler.WriteToken(token));
            }
        }
        public bool ValidateUser(string usuario, string password, string tipo)
        {
            string pasw = Encrypt.GetSHA256(password);
            if(tipo == "C")
            {
                var user = _context.Clientes.Where(u => u.Username == usuario && u.Password == pasw).FirstOrDefault();

                if (user != null)
                {
                    return true;
                }
            }
            else if(tipo == "U")
            {
                var user = _context.Usuarios.Where(u => u.Username == usuario && u.Password == pasw).FirstOrDefault();
                if (user != null)
                {
                    return true;
                }
            }
            
            return false;
        }
        public LoginReturn GetUser(string usuario, string password, string tipo)
        {
            if (ValidateUser(usuario, password,tipo))
            {
                List<string> rolList = new List<string>();
                if(tipo == "U")
                {
                    var user = _context.Usuarios.Where(u => u.Username == usuario).FirstOrDefault();

                    var roless =  (from roles in _context.Roles
                                  join rolesUser in _context.RolesUsuarios on roles.ColRol equals rolesUser.CodRol
                                  where 
                                  rolesUser.CodUsuario == user.CodUsuario
                                  select
                                    roles
                                  ).ToList() ;
                    foreach (var role in roless)
                    {
                        rolList.Add(role.NombreRol);
                    }
                }
                return new LoginReturn { roles = rolList, username = usuario };
            }
            else
            {
                return new LoginReturn { roles = { }, username = "none" };
            }
        }
    }
}
