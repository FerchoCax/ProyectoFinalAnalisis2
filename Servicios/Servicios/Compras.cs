using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Servicios.Interfaces;
namespace Servicios.Servicios
{
    public class Compras:ICompras
    {
        private DataBaseContext _dataBaseContext;
        private Errores _errores;
        public Compras(DataBaseContext ctx)
        {
            _dataBaseContext = ctx;
            _errores = new Errores();
        }

        public async Task<IActionResult> GetPeliculasSucursal(int idSucursal)
        {
            try
            {
                var codPeliculas = await (from sala in _dataBaseContext.Salas
                                       join funcion in _dataBaseContext.Funciones on sala.CodSala equals funcion.CodSala
                                       join pelicula in _dataBaseContext.Peliculas on funcion.CodPelicula equals pelicula.CodPelicula
                                       where sala.CodSucursal == idSucursal
                                       && funcion.FechaHoraInicio >= DateTime.Now && funcion.FechaHoraFin <= DateTime.Now.AddDays(3)
                                       select
                                       pelicula.CodPelicula
                                       ).Distinct().ToArrayAsync();

                var Peliculas = await _dataBaseContext.Peliculas.Where(p => codPeliculas.Contains(p.CodPelicula))
                                       .Include(e => e.CodClasificacionNavigation)
                                       .Include(e => e.Imagenes)
                                       .ThenInclude(n => n.codTipoImagenNavigator)
                                       .Include(e => e.Funciones.Where(a => a.FechaHoraInicio >= DateTime.Now && a.FechaHoraFin <= DateTime.Now.AddDays(3)).OrderBy(j => j.FechaHoraInicio))
                                       .ToListAsync();

                                       
                return new ObjectResult(Peliculas) { StatusCode = 200 };
                                       
            }catch(Exception ex)
            {
                return _errores.respuestaDeError("Error al momento de cargar las peliculas", ex);
            }
        }

        public async Task<IActionResult> GetAsientosSala(int idSala, int idFuncion)
        {
            try
            {
                var asientos = _dataBaseContext.Asientos.Where(i => i.CodSala == idSala).Select(e => new {e.CodAsiento}).ToArray();
                List <int> codAsientos = new List<int>();
                foreach(var asi in asientos)
                {
                    codAsientos.Add(asi.CodAsiento);
                }
                var Sala = await _dataBaseContext.Salas.Where(e => e.CodSala == idSala)
                           .Include(n => n.Asientos)
                           .ThenInclude(m => m.Boletos.Where(g => codAsientos.Contains(g.CodAsiento))).FirstOrDefaultAsync();


                return new ObjectResult(Sala) { StatusCode= 200};
            }
            catch (Exception ex) 
            {
                return _errores.respuestaDeError("Error al momento de cargas los hacientos de la sala", ex);
            }
        }

        private async Task<int> nextVal(string nombreTabla)
        {
            string query = @"SELECT AUTO_INCREMENT as valor
                                                                 FROM information_schema.TABLES
                                                                 WHERE TABLE_SCHEMA = ""db_cinema""
                                                                AND TABLE_NAME = """+nombreTabla+@"""";
            var valor = await _dataBaseContext.valorEntero.FromSqlRaw(query).ToListAsync();
            return valor.Max(e => e.valor);
        }

        public async Task<IActionResult> ComprarBoletos(TodoCompra compra)
        {
            using var transacion = _dataBaseContext.Database.BeginTransaction();

            try
            {
                //using var transacion = _dataBaseContext.Database.BeginTransaction();
                var inicioCorrelativoBoleto = _dataBaseContext.Boletos.Max(e => e.CodBoleto) + 1;
                var inicioCorrelativoBoletosFactura = await nextVal("boletos_factura") + 1;
                var siguienteFactura = _dataBaseContext.Facturas.Max(e => e.CodFactura) + 1;
                int canitdadBoltos = compra.boletos.Length;
                DateTime ahora = DateTime.Now;
                foreach(var boleto in compra.boletos)
                {
                    boleto.FechaIng = ahora;
                }
                compra.factura.FechaIng = ahora;
                _dataBaseContext.Facturas.Add(compra.factura);
                _dataBaseContext.Boletos.AddRange(compra.boletos);
                _dataBaseContext.SaveChanges();
                //transacion.Commit();
                int codFuncion=0;
                foreach (Boleto bol in compra.boletos)
                {
                    BoletosFactura boletoFact = new BoletosFactura();
                    codFuncion = bol.CodFuncion;
                    boletoFact.CodBoleto = inicioCorrelativoBoleto;
                    boletoFact.CodFactura = siguienteFactura;
                    boletoFact.UsuarioIng = "fcaxaj";
                    boletoFact.FechaIng = ahora;
                    _dataBaseContext.BoletosFacturas.Add(boletoFact);
                    inicioCorrelativoBoleto = inicioCorrelativoBoleto + 1;
                }

                _dataBaseContext.SaveChanges();

                var Cliente = _dataBaseContext.Clientes.Where(e => e.CodCliente == compra.factura.CodCliente).FirstOrDefault();
                var infoFuncion = await (from funciones in _dataBaseContext.Funciones
                                join pelicula in _dataBaseContext.Peliculas on funciones.CodPelicula equals pelicula.CodPelicula
                                where
                                funciones.CodFuncion == codFuncion
                                select new
                                {
                                    pelicula,
                                    funciones
                                }
                                
                                ).FirstOrDefaultAsync();

                if(!string.IsNullOrEmpty(Cliente.Correo))
                { 
                   
                    var doc = new Document(PageSize.A6);
                    MemoryStream memoryStream = new MemoryStream();
                    PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);

                    doc.Open();
                    foreach (Boleto bol in compra.boletos)
                    {
                        doc.Add(new Paragraph("Boleto"));
                        doc.Add(new Paragraph(infoFuncion.pelicula.Nombre));
                        JObject objeto = new JObject()
                        {
                            ["codAsiento"] = bol.CodAsiento,
                            ["codFuncion"] = bol.CodFuncion
                        };
                        BarcodeQRCode barcode = new BarcodeQRCode(JsonConvert.SerializeObject(objeto), 1000, 1000, null);
                        Image ccdeQrImage = barcode.GetImage();
                        ccdeQrImage.ScaleAbsolute(200, 200);
                        doc.Add(ccdeQrImage);
                        doc.Add(new Paragraph("Fecha y hora: "+infoFuncion.funciones.FechaHoraInicio.ToString("dd/MM/yyyy HH:mm")));
                        doc.NewPage();
                    }
                   

                    writer.CloseStream = false;
                    doc.Close();
                    memoryStream.Position = 0;

                    string emailOrigen = "cinemaanalisis@gmail.com";
                    string emailDestino = Cliente.Correo;
                    string contrasenia = "gsgnxdythijvaxcd";
                    MailMessage oMail = new MailMessage(emailOrigen, emailDestino,"Compra de Boletos", "Muchas gracias por tu compra!!!");
                    oMail.IsBodyHtml = true;
                    Attachment atc = new Attachment(memoryStream, "Boleto.pdf");
                    oMail.Attachments.Add(atc);
                    SmtpClient client = new SmtpClient("smtp.gmail.com");
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    client.Credentials = new System.Net.NetworkCredential(emailOrigen, contrasenia);
                    doc.Close();

                    client.Send(oMail);

                    client.Dispose();
                }
                transacion.Commit();
            


                return new ObjectResult(new { estado = 1 }) { StatusCode = 200 };
            }
            catch(Exception ex)
            {
                transacion.Rollback();

                return _errores.respuestaDeError("Error al momento de realizar la compra", ex);
            }
        }

        public async Task<IActionResult> GetInfoCliente(int idCliente)
        {
            try
            {
                return new ObjectResult(await _dataBaseContext.Clientes.FirstOrDefaultAsync(e => e.CodCliente == idCliente)) { StatusCode = 200 };
            }catch(Exception ex)
            {
                return _errores.respuestaDeError("Error al momento de obtener la informacion del cliente", ex);
            }
        }
    }
}
