using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos
{
    public partial class Sala
    {
        public Sala()
        {
            Asientos = new HashSet<Asiento>();
            Funciones = new HashSet<Funcione>();
        }

        public int CodSala { get; set; }
        public int CodSucursal { get; set; }
        public int? CodTipoSala { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public string Activa { get; set; }
        public string UsuarioIng { get; set; }
        public DateTime? FechaIng { get; set; }
        public string UsuarioAct { get; set; }
        public DateTime? FechaAct { get; set; }

        public virtual Sucursale CodSucursalNavigation { get; set; }
        public virtual TiposSala CodTipoSalaNavigation { get; set; }
        public virtual ICollection<Asiento> Asientos { get; set; }
        public virtual ICollection<Funcione> Funciones { get; set; }
    }
}
