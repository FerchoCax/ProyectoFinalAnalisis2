using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos
{
    public partial class Funcione
    {
        public Funcione()
        {
            Boletos = new HashSet<Boleto>();
        }

        public int CodFuncion { get; set; }
        public int CodSala { get; set; }
        public int CodPelicula { get; set; }
        public string Estado { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFin { get; set; }
        public string UsuarioIng { get; set; }
        public DateTime FechaIng { get; set; }
        public string UsuarioAct { get; set; }
        public DateTime? FechaAct { get; set; }

        public virtual Sala CodSalaNavigation { get; set; }
        public virtual ICollection<Boleto> Boletos { get; set; }

        public virtual Pelicula CodPeliculaNavigator { get; set; }
    }
}
