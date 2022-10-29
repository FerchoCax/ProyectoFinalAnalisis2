using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servicios.Interfaces;
namespace Servicios.Servicios
{
    public class ImagenesPeliculas:IImagenesPelicula
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly Errores _errores;
        private readonly string bucket = "analisis-sistemas2";
        private readonly string urlBucket = "https://storage.googleapis.com/";
        public ImagenesPeliculas(DataBaseContext ctx)
        {
            _dataBaseContext = ctx;
            _errores = new Errores();
        }

        public async Task<IActionResult> AgregarImagen(ImagenPelicula imagenPelicula)
        {
            try
            {
                imagenPelicula.urlImagen = urlBucket + bucket+"/" +imagenPelicula.cod_pelicula + "-" + imagenPelicula.nombreimagen;
                SubirImagen(imagenPelicula.imagenbase64, imagenPelicula.cod_pelicula + "-" + imagenPelicula.nombreimagen.Trim());
                imagenPelicula.imagenbase64 = "";
                _dataBaseContext.ImagenesPelicula.Add(imagenPelicula);
                await _dataBaseContext.SaveChangesAsync();
                return new ObjectResult(new { estado = 1 }) { StatusCode = 200 };
            }
            catch(Exception ex)
            {
                return _errores.respuestaDeError("Error al momento de agregar la imagen", ex);
            }
        }

        public Google.Apis.Storage.v1.Data.Object SubirImagen(string base64File, string fileName)
        {
            GoogleCredential _credential = GoogleCredential.FromFile("file.json");
            var storage = StorageClient.Create(_credential);
            byte[] buffer = Convert.FromBase64String(base64File);
            MemoryStream stream = new MemoryStream(buffer);
            string fileTipe = "";
            if (fileName.Contains(".png"))
            {
                fileTipe = "image/png";
            }
            else if (fileName.Contains(".jpeg") || fileName.Contains(".jpg"))
            {
                fileTipe = "image/jpeg";
            }
            var respuesta = storage.UploadObject(bucket, fileName, fileTipe, stream);

            return respuesta;
        }
        public async Task<IActionResult> ListarImagenesPelicula(int idPelicula)
        {
            try
            {
                var imagenes = await _dataBaseContext.ImagenesPelicula.Where(e => e.cod_pelicula == idPelicula)
                    .Include(e => e.codPeliculaNavigation)
                    .Include(e => e.codTipoImagenNavigator).ToListAsync();
                return new ObjectResult(imagenes) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return _errores.respuestaDeError("Error al momento de listar las imagenes", ex);
            }
        }

        public async Task<IActionResult> ListarPeliculas()
        {
            try
            {
                return new ObjectResult(await _dataBaseContext.Peliculas
                    .Include(e => e.Imagenes)
                    .ThenInclude(n => n.codTipoImagenNavigator).ToListAsync());
            }catch(Exception ex)
            {
                return _errores.respuestaDeError("Error al momento de listar las peliculas",ex);
            }
        }
    }
}
