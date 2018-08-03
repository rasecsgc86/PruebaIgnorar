using AM45Secure.Business.IBusiness.IImprimir;
using AM45Secure.Commons.Modelos.Emitir;
using AM45Secure.Commons.Modelos.Imprimir;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using AM45Secure.Commons.Modelos.Comparador;
using AM45Secure.Commons.Modelos.Comunes;
using Zero.Handlers.Response;

namespace AM45Secure.RESTServices.Controllers.Imprimir
{
    public class ImprimirController : ApiController
    {
        private readonly IImprimirBusiness iImprimirBusiness;

        public ImprimirController(IImprimirBusiness iImprimirBusiness)
        {
            this.iImprimirBusiness = iImprimirBusiness;
        }

        [HttpPost]
        public SingleResponse<FolletosModel> ConsultarFolletos(PolizaModel poliza)
        {
            return iImprimirBusiness.ConsultarFolletos(poliza);
        }

        [HttpGet]
        public HttpResponseMessage DescargarArchivo(string rutaArchivo)
        {
            HttpResponseMessage result = null;
            try
            {
                string nombreArchivo = Path.GetFileName(rutaArchivo);
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

        [HttpPost]
        public SingleResponse<AsegPaqueteModel> ConsultaAsegPaquete(AsegPaqueteModel numeroModel)
        {
            return iImprimirBusiness.ConsultaAsegPaquete(numeroModel);
        }

        [HttpPost]
        public SingleResponse<CondicionesGralesModel> ConsultaCondicionesGrales(VersionesModel versiones)
        {
            return iImprimirBusiness.ConsultaCondicionesGrales(versiones);
        }
    }
}