using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos
{
    public partial class Factura
    {
        public Factura()
        {
            BoletosFacturas = new HashSet<BoletosFactura>();
        }

        public int CodFactura { get; set; }
        public int CodCliente { get; set; }
        public int CodMetodoPago { get; set; }
        public double Total { get; set; }
        public double Iva { get; set; }
        public string Estado { get; set; }
        public string UsuarioIng { get; set; }
        public DateTime FechaIng { get; set; }
        public string UsuarioAct { get; set; }
        public DateTime? FechaAct { get; set; }

        public virtual Cliente CodClienteNavigation { get; set; }
        public virtual MetodosPago CodMetodoPagoNavigation { get; set; }
        public virtual ICollection<BoletosFactura> BoletosFacturas { get; set; }
    }
}
