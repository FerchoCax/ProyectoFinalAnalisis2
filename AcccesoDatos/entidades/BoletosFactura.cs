using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos
{
    public partial class BoletosFactura
    {
        public int CodBoletoFactura { get; set; }
        public int CodBoleto { get; set; }
        public int CodFactura { get; set; }
        public string UsuarioIng { get; set; }
        public DateTime FechaIng { get; set; }
        public string UsuarioAct { get; set; }
        public DateTime? FechaAct { get; set; }

        public virtual Boleto CodBoletoNavigation { get; set; }
        public virtual Factura CodFacturaNavigation { get; set; }
    }
}
