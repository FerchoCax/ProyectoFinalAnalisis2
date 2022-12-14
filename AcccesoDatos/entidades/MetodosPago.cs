using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos
{
    public partial class MetodosPago
    {
        public MetodosPago()
        {
            Facturas = new HashSet<Factura>();
        }

        public int CodMetodoPago { get; set; }
        public string Descripcion { get; set; }
        public string Activo { get; set; }
        public string UsuarioIng { get; set; }
        public DateTime FechaIng { get; set; }
        public string UsuarioAct { get; set; }
        public DateTime? FechaAct { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
