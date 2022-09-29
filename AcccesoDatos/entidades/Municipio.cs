using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos
{
    public partial class Municipio
    {
        public Municipio()
        {
            Sucursales = new HashSet<Sucursale>();
        }

        public int CodMunicipio { get; set; }
        public int CodDepartamento { get; set; }
        public string Nombre { get; set; }
        public string UsuarioIng { get; set; }
        public DateTime? FechaIng { get; set; }
        public string? UsuarioAct { get; set; }
        public DateTime? FechaAct { get; set; }

        public virtual Departamento CodDepartamentoNavigation { get; set; }
        public virtual ICollection<Sucursale> Sucursales { get; set; }
    }
}
