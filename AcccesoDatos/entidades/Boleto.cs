using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos
{
    public partial class Boleto
    {
        public Boleto()
        {
            BoletosFacturas = new HashSet<BoletosFactura>();
        }

        public int CodBoleto { get; set; }
        public int CodFuncion { get; set; }
        public int CodAsiento { get; set; }
        public int CodTipoBoleto { get; set; }
        public string Estado { get; set; }
        public string UsuarioIng { get; set; }
        public DateTime FechaIng { get; set; }
        public string UsuarioAct { get; set; }
        public DateTime? FechaAct { get; set; }

        public virtual Asiento CodAsientoNavigation { get; set; }
        public virtual Funcione CodFuncionNavigation { get; set; }
        public virtual TipoBoleto CodTipoBoletoNavigation { get; set; }
        public virtual ICollection<BoletosFactura> BoletosFacturas { get; set; }
    }
}
