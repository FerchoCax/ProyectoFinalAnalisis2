using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos
{
    public partial class Role
    {
        public Role()
        {
            RolesUsuarios = new HashSet<RolesUsuario>();
        }

        public int ColRol { get; set; }
        public string NombreRol { get; set; }
        public string Estado { get; set; }
        public string UsuarioIng { get; set; }
        public DateTime FechaIng { get; set; }
        public string UsuarioAct { get; set; }
        public DateTime? FechaAct { get; set; }

        public virtual ICollection<RolesUsuario> RolesUsuarios { get; set; }
    }
}
