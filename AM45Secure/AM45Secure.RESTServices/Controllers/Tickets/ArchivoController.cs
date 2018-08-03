using System;
using AM45Secure.Business.IBusiness.Tickets;
using AM45Secure.Commons.Modelos.Tickets;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Recursos;
using Zero.Exceptions;
using Zero.Handlers.Response;
using System.Net;
using Microsoft.SqlServer.Server;
using System.Net.Http.Headers;
using System.Text;

namespace AM45Secure.RESTServices.Controllers.Tickets
{
    //[Authorize]
    public class ArchivoController : ApiController
    {
        private readonly IGestionBussiness iGestionBussiness;

        public ArchivoController(IGestionBussiness iGestionBussiness)
        {
            this.iGestionBussiness = iGestionBussiness;
        }

        [HttpPost]
        public SingleResponse<bool> EliminarArchivo(ArchivoTicketsModel archivoTicketsModel)
        {
            SingleResponse<bool> response = new SingleResponse<bool>();
            try
            {
                return iGestionBussiness.EliminarArchivo(archivoTicketsModel);
            }
            catch (Exception)
            {
                response.Error(new DomainException(Codes.ERR_00_00));
            }
            return response;
        }

        [HttpGet]
        public HttpResponseMessage DescargarArchivo(string rutaArchivo, string nombreArchivo)
        {
            HttpResponseMessage result = null;
            try
            {

                if (!File.Exists(rutaArchivo))
                {
                    result = Request.CreateResponse(HttpStatusCode.Gone);
                }
                else
                {
                    // Serve the file to the client
                    result = Request.CreateResponse(HttpStatusCode.OK);
                    result.Content = new StreamContent(new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read));
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = Uri.EscapeDataString(nombreArchivo)
                    };

                }
                
            }
            catch (Exception ex)
            {
                result = Request.CreateResponse(HttpStatusCode.InternalServerError + ex.Message);
            }
            return result;
        }
    }
}