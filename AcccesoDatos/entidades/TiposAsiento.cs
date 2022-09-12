using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos
{
    public partial class TiposAsiento
    {
        public TiposAsiento()
        {
            Asientos = new HashSet<Asiento>();
        }

        public int CodTipoAsiento { get; set; }
        public string Descripcion { get; set; }
        public string Activo { get; set; }
        public string UsuarioIng { get; set; }
        public DateTime FechaIng { get; set; }
        public string UsuarioAct { get; set; }
        public DateTime? FechaAct { get; set; }

        public virtual ICollection<Asiento> Asientos { get; set; }
    }
}
