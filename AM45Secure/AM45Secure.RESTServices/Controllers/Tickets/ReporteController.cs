using System;
using AM45Secure.Business.IBusiness.Tickets;
using AM45Secure.Commons.Modelos.Tickets;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.WebPages;
using AM45Secure.Commons.Modelos.Comparador;
using Zero.Handlers.Response;
using Zero.Utils;

namespace AM45Secure.RESTServices.Controllers.Tickets
{
    public class ReporteController : ApiController
    {
        private readonly IReporteBusiness iReporteBusiness;

        public ReporteController(IReporteBusiness iReporteBusiness)
        {
            this.iReporteBusiness = iReporteBusiness;
        }

        [HttpPost]
        public SingleResponse<IList<ReporteModel>> ConsultarTicketsReporte (ReporteModel reporteModel)
        {
            return iReporteBusiness.ConsultarTicketsReporte(reporteModel);
        }
        [HttpPost]
        public SingleResponse<IList<EstatusModel>> ConsultarEstatusTickets()
        {
            return iReporteBusiness.ConsultarEstatusTickets();
        }
        [HttpGet]
        public HttpResponseMessage ConsultarTicketsReporteExcel(string descripcionEstatus, string fechaInicio, string fechaFin)
        {
            //Comntario
            HttpResponseMessage result;
            try
            {
                DateTime fecha = DateTime.Today;
                ReporteModel rep = new ReporteModel();
                rep.DescripcionEstatus =(descripcionEstatus=="null" || descripcionEstatus== "undefined" || descripcionEstatus == string.Empty)?null:descripcionEstatus;
                rep.FechaInicio = (fechaInicio == "null" || fechaInicio == "undefined" || fechaInicio == string.Empty) ? null : fechaInicio; 
                rep.FechaFin = (fechaFin == "null" || fechaFin == "undefined" || fechaFin == string.Empty) ? null : fechaFin; 
                string archivo = "ReporteTicket" + fecha.Day + fecha.Month + fecha.Year;
                
                var bytes = iReporteBusiness.ConsultarTicketsReporteExcel(rep);
                Stream stream = new MemoryStream(bytes);
                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = archivo + ".xls"
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xls");

            }
            catch (Exception ex)
            {
                Logger.Error("Error al imprimir el reporte de tickets... ", ex);
                result = Request.CreateResponse(HttpStatusCode.InternalServerError + ex.Message);
            }
            return result;
        }

    }
}