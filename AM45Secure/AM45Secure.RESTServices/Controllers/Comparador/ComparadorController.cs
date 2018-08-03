using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using AM45Secure.Business.IBusiness.IComparador;
using Zero.Handlers.Response;
using AM45Secure.Commons.Modelos.comunes;
using AM45Secure.Commons.Modelos.Comparador;
using AM45Secure.Commons.Modelos.Cotizador;
using System.Collections.Generic;
using System.Linq;
using AM45Secure.Business.IBusiness.Tickets;

namespace AM45Secure.RESTServices.Controllers.Comparador
{
    //[Authorize]
    public class ComparadorController : ApiController
    {
        private readonly IComparadorBusiness iComparadorBusiness;

        public ComparadorController(IComparadorBusiness iComparadorBusiness)
        {
            this.iComparadorBusiness = iComparadorBusiness;
        }

        [HttpPost]
        public SingleResponse<CargaInicialModel> CargaInicial(CotizarModel cotizarModel)
        {
            return iComparadorBusiness.CargaInicial(cotizarModel);
        }
        [HttpPost]
        public SingleResponse<ComparadorModel> CargaComparador(CotizarModel cotizarModel)
        {
            return iComparadorBusiness.CargaComparador(cotizarModel);
        }

        [HttpPost]
        public SingleResponse<CabeceraCotizacionModel> ConsultarCabeceraCotizacion(CotizarModel cotizarModel)
        {
            return iComparadorBusiness.ConsultarCabeceraCotizacion(cotizarModel);
        }
        [HttpGet]
        public HttpResponseMessage ConsultarReporteCotizacion(int cotizacionId, bool flexible, int paqueteId, int solicitudId, int numero, string tkn)
        {


            //Comntario
            HttpResponseMessage result;
            try
            {
                string archivo = "Cotizacion" + cotizacionId.ToString();
                RepCotizacionModel repCotizacionModel = new RepCotizacionModel();
                repCotizacionModel.CotizacionId = cotizacionId;
                repCotizacionModel.Flexible = flexible;
                repCotizacionModel.Numero = numero;
                repCotizacionModel.SoliciudId = solicitudId;
                repCotizacionModel.PaqueteId = paqueteId;
                repCotizacionModel.Tkn = tkn;
                var bytes = iComparadorBusiness.ConsultaReporteCotizacion(repCotizacionModel);
                Stream stream = new MemoryStream(bytes);
                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = archivo + ".pdf"
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            }
            catch (Exception ex)
            {
                result = Request.CreateResponse(HttpStatusCode.InternalServerError.ToString() + ex.Message);
            }
            return result;
        }



        [HttpPost]
        public bool EnviarMailReporteCotizacion(EnvioReporteEmail datosEmail)
        {


            datosEmail.destinatarios = datosEmail.destinatarios.Replace("[", "").Replace("]", "").Replace("\"", "");
            var ListDestinatarios = datosEmail.destinatarios.Split(',').ToList();
            ListDestinatarios.RemoveAll(c => c.Length < 2);

            try
            {
                string archivo = "Cotizacion" + datosEmail.cotizacionId.ToString();
                RepCotizacionModel repCotizacionModel = new RepCotizacionModel();
                repCotizacionModel.CotizacionId = datosEmail.cotizacionId;
                repCotizacionModel.Flexible = datosEmail.flexible;
                repCotizacionModel.Numero = datosEmail.numero;
                repCotizacionModel.SoliciudId = datosEmail.solicitudId;
                repCotizacionModel.PaqueteId = datosEmail.paqueteId;
                repCotizacionModel.Tkn = datosEmail.tkn;
                var bytes = iComparadorBusiness.ConsultaReporteCotizacion(repCotizacionModel, true, ListDestinatarios);



            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }




        [HttpGet]
        public HttpResponseMessage descargaReporteEmail(int opcionFiltro)
        {
            //Comntario
            HttpResponseMessage result;
            try
            {


                var bytes = iComparadorBusiness.descargaReporteEmail(opcionFiltro);
                Stream stream = new MemoryStream(bytes);
                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "ReporteCotizacionEmail" + ".xls"
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xls");

            }
            catch (Exception ex)
            {
                //Logger.Error("Error al imprimir el reporte de tickets... ", ex);
                result = Request.CreateResponse(HttpStatusCode.InternalServerError + ex.Message);
            }
            return result;
        }

    }
}