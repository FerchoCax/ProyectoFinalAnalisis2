using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos
{
    public partial class ClasificacionPelicula
    {
        public ClasificacionPelicula()
        {
            Peliculas = new HashSet<Pelicula>();
        }

        public int CodClasificacion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Activa { get; set; }
        public string UsuarioIng { get; set; }
        public DateTime FechaIng { get; set; }
        public string UsuarioAct { get; set; }
        public DateTime? FechaAct { get; set; }

        public virtual ICollection<Pelicula> Peliculas { get; set; }
    }
}
