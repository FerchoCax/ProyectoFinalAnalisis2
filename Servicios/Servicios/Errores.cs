using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
namespace Servicios.Servicios
{
    public  class Errores
    {
        public Errores()
        {}
        public ObjectResult respuestaDeError(string mensaje,Exception e)
        {
            string er = "";
            try
            {
                var lineNumber = 0;
                const string lineSearch = ":line ";
                if(e.StackTrace != null) { 
                    var index = e.StackTrace.LastIndexOf(lineSearch);
                    if (index != -1)
                    {
                        var lineNumberText = e.StackTrace.Substring(index + lineSearch.Length);
                        if (int.TryParse(lineNumberText, out lineNumber))
                        {
                        }
                    }
                    if (lineNumber != 0)
                    {
                        er = ". En la linea: " + lineNumber;
                    }
                }
            }
            catch
            {
                er = "";
            }
                

            string error = @". Error:  " + (e.InnerException != null ? e.InnerException.Message : e.Message) + @"".Replace("\\\\", "//");
            error = error + er;
            string retorno = @"{""error"": """ + mensaje + error.Replace("\"", "'")+ "\"}";
            retorno = retorno.Replace("\\", "/"); 
            
            return new ObjectResult(JObject.Parse(retorno)) { StatusCode = 500 };
        }

        public ObjectResult respuestaDeError(string mensaje)
        {
            string retorno = "{\"error\":\"" + mensaje + "\"}";
            return new ObjectResult(JObject.Parse(retorno)) { StatusCode = 500 };
        }
    }
}
