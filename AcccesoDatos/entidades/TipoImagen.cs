using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace AccesoDatos
{
    public partial class TipoImagen
    {
        public TipoImagen()
        {
            Imagenes = new HashSet<ImagenPelicula>();
        }
        public int cod_tipo_imagen { get; set; }
        public string nombre {get; set; }
        public  DateTime fecha_ing { get; set; }
        public string usuario_ing { get; set; }

        public virtual ICollection<ImagenPelicula> Imagenes { get; set; }
    }
}
