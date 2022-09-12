using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos
{
    public partial class SucursalesUsuario
    {
        public int CodSucursal { get; set; }
        public int CodUsuario { get; set; }
        public string Estado { get; set; }
        public string UsuarioIng { get; set; }
        public DateTime FechaIng { get; set; }
        public string UsuarioAct { get; set; }
        public DateTime? FechaAct { get; set; }

        public virtual Sucursale CodSucursalNavigation { get; set; }
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
