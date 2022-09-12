using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos
{
    public partial class Asiento
    {
        public Asiento()
        {
            Boletos = new HashSet<Boleto>();
        }

        public int CodAsiento { get; set; }
        public int CodTipoAsiento { get; set; }
        public int CodSala { get; set; }
        public string Fila { get; set; }
        public string Numero { get; set; }
        public DateTime UsuarioIng { get; set; }
        public string FechaIng { get; set; }
        public DateTime? UsuarioAct { get; set; }
        public string FechaAct { get; set; }

        public virtual Sala CodSalaNavigation { get; set; }
        public virtual TiposAsiento CodTipoAsientoNavigation { get; set; }
        public virtual ICollection<Boleto> Boletos { get; set; }
    }
}
