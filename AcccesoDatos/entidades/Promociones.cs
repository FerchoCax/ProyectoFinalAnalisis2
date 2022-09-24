using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos
{
    public partial class Promociones
    {
        public Promociones()
        {
            Facturas = new HashSet<Factura>();
        }
        public int id_promocion { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string estado { get; set; }
        public DateTime fecha_ing { get; set; }
        public string usuario_ing { get; set; }
        public DateTime? fecha_act { get; set; }
        public string usuario_act { get; set; }

        public virtual ICollection <Factura> Facturas { get; set; }
    }
}
