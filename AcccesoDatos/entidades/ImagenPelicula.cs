using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace AccesoDatos
{
    public partial class ImagenPelicula
    {
        
        public int cod_imagen { get; set; }
        public int cod_pelicula { get; set; }
        public byte[] imagen { get; set; }
        public int tipo_imagen { get; set; }

        public virtual TipoImagen codTipoImagenNavigator { get; set; }
        public virtual Pelicula codPeliculaNavigation { get; set; } 
    }
}
