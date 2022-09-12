using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos
{
    public partial class Departamento
    {
        public Departamento()
        {
            Municipios = new HashSet<Municipio>();
        }

        public int CodDepartamento { get; set; }
        public string Nombre { get; set; }
        public string UsuarioIng { get; set; }
        public DateTime FechaIng { get; set; }
        public string UsuarioAct { get; set; }
        public DateTime? FechaAct { get; set; }

        public virtual ICollection<Municipio> Municipios { get; set; }
    }
}
