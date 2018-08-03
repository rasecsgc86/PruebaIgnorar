using AM45Secure.Business.IBusiness.Tickets;
using System;
using System.Collections.Generic;
using System.Configuration;
using AM45Secure.Commons.Modelos.Comparador;
using AM45Secure.Commons.Modelos.Tickets;
using Zero.Exceptions;
using Zero.Handlers.Response;
using AM45Secure.DataAccess.IDataAccess.Tickets;
using AM45Secure.Commons.Recursos;
using Microsoft.Reporting.WebForms;
using Zero.Utils;

namespace AM45Secure.Business.Business.Tickets
{
    public class ReporteBusiness : IReporteBusiness
    {
        private readonly IReporteDataAccess iReporteDataAccess;

        public ReporteBusiness(IReporteDataAccess iReporteDataAccess)
        {
            this.iReporteDataAccess = iReporteDataAccess;
        }

        public SingleResponse<IList<EstatusModel>> ConsultarEstatusTickets()
        {
            SingleResponse<IList<EstatusModel>> response = new SingleResponse<IList<EstatusModel>>();
            try
            {
                IList<EstatusModel> lista = iReporteDataAccess.ConsultarEstatusTicketsReporte();
                response.Done(lista,string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesReportesTickets.ERR_05_02, e));
            }
            return response;
        }

        public SingleResponse<IList<ReporteModel>> ConsultarTicketsReporte(ReporteModel reporteModel)
        {
            SingleResponse<IList<ReporteModel>> response = new SingleResponse<IList<ReporteModel>>();
            try
            {
                IList<ReporteModel> reporte = iReporteDataAccess.ConsultarTicketsReporte(reporteModel);
                response.Done(reporte, string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesReportesTickets.ERR_05_02,e));
            }
            return response;
        }
        public byte[] ConsultarTicketsReporteExcel(ReporteModel reporteModel)
        {
            byte[] bytes = null;
            try
            {
                // Variables
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                // Setup the report viewer object and get the array of bytes
                ReportViewer viewer = new ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                //obtener los datos de la cotizacion para llenar los dataSet
                IList <ReporteExcelModel> reporte = iReporteDataAccess.ConsultarTicketsReporteExcel(reporteModel);
                ReportDataSource reporteExcel = new ReportDataSource("DataSetTicket", reporte);
                viewer.LocalReport.DataSources.Add(reporteExcel);
                viewer.LocalReport.ReportPath = ConfigurationManager.AppSettings["ReporteTicket"];
                bytes = viewer.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            }
            catch (Exception ex)
            {
                Logger.Error("Error al imprimir el reporte de tickets... ", ex);
                bytes = null;
            }

            return bytes;
        }
    }
}
