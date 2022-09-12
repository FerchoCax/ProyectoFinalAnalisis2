using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos
{
    public partial class Sucursale
    {
        public Sucursale()
        {
            Salas = new HashSet<Sala>();
            SucursalesUsuarios = new HashSet<SucursalesUsuario>();
        }

        public int CodSucursal { get; set; }
        public int CodMunicipio { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Nombre { get; set; }
        public string Activa { get; set; }
        public string UsuarioIng { get; set; }
        public DateTime FechaIng { get; set; }
        public string UsuarioAct { get; set; }
        public DateTime? FechaAct { get; set; }

        public virtual Municipio CodMunicipioNavigation { get; set; }
        public virtual ICollection<Sala> Salas { get; set; }
        public virtual ICollection<SucursalesUsuario> SucursalesUsuarios { get; set; }
    }
}
