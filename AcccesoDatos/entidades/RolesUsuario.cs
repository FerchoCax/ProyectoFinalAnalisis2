using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos
{
    public partial class RolesUsuario
    {
        public int CodUsuario { get; set; }
        public int CodRol { get; set; }
        public string Estado { get; set; }
        public string UsuarioIng { get; set; }
        public DateTime FechaIng { get; set; }
        public string UsuarioAct { get; set; }
        public DateTime? FechaAct { get; set; }

        public virtual Role CodRolNavigation { get; set; }
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
