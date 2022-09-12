using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos
{
    public partial class TipoBoleto
    {
        public TipoBoleto()
        {
            Boletos = new HashSet<Boleto>();
        }

        public int CodTipoBoleto { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public string Activo { get; set; }
        public string UsuarioIng { get; set; }
        public DateTime FechaIng { get; set; }
        public string UsuarioAct { get; set; }
        public DateTime? FechaAct { get; set; }

        public virtual ICollection<Boleto> Boletos { get; set; }
    }
}
