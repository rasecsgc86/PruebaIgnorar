using System;
using AM45Secure.Business.IBusiness.Tickets;
using AM45Secure.Commons.Modelos.Tickets;
using System.Collections.Generic;
using System.IO;
using System.Web.Configuration;
using System.Web.Http;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Recursos;
using Zero.Exceptions;
using Zero.Handlers.Response;
using System.Text;

namespace AM45Secure.RESTServices.Controllers.Tickets
{
    public class GestionController : ApiController
    {
        private readonly IGestionBussiness iGestionBussiness;

        public GestionController(IGestionBussiness iGestionBussiness)
        {
            this.iGestionBussiness = iGestionBussiness;
        }

        public SingleResponse<IList<TicketModel>> ConsultarTickest(TicketModel ticketModel)
        {
            //return null;
            return iGestionBussiness.ConsultarTickets(ticketModel);
        }

        public SingleResponse<IList<ClienteProductoModel>> ConsultarClientes(ClienteProductoModel clienteProductoModel)
        {
            return iGestionBussiness.ConsultarClientes(clienteProductoModel);
        }

        [HttpPost]
        public SingleResponse<IList<AgenciasModel>> ConsultarAgencias(AgenciasClienteModel agenciasCliente)
        {
            return iGestionBussiness.ConsultarAgencias(agenciasCliente);
        }

        [HttpPost]
        public SingleResponse<bool> ConsultarSiEsClienteFlotillas(ClienteProductoModel clienteProductoModel)
        {
            return iGestionBussiness.ConsultarSiEsClienteFlotillas(clienteProductoModel);
        }

        [HttpPost]
        public SingleResponse<IList<ClienteProductoModel>> ConsultarCaratula(ClienteProductoModel clienteProductoModel)
        {
            return iGestionBussiness.ConsultarCaratula(clienteProductoModel);
        }

        [HttpPost]
        public SingleResponse<ClienteProductoModel> ConsultarResponsable(ClienteProductoModel clienteProductoModel)
        {
            return iGestionBussiness.ConsultarResponsable(clienteProductoModel);
        }

        public SingleResponse<IList<ClienteProductoModel>> ConsultarTiposTickets(ClienteProductoModel clienteProductoModel)
        {
            return iGestionBussiness.ConsultarTiposTickets(clienteProductoModel);
        }

        public SingleResponse<IList<CatOrigenTicketsModel>> ConsultarReportaA()
        {
            return iGestionBussiness.ConsultarReportaA();
        }

        public SingleResponse<IList<CatEstatusTicketsModel>> ConsultaEstatusTickets()
        {
            return iGestionBussiness.ConsultaEstatusTickets();
        }

        [HttpPost]
        public SingleResponse<bool> GuardarTicket(TicketModel ticketModel)
        {
            return iGestionBussiness.GuardarTicket(ticketModel);
        }

        [HttpPost]
        public SingleResponse<ArchivoTicketsModel> CargarArchivo()
        {
            SingleResponse<ArchivoTicketsModel> response = new SingleResponse<ArchivoTicketsModel>();
            try
            {
                ArchivoTicketsModel archivoTicketsModel = new ArchivoTicketsModel();
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                string sPath = WebConfigurationManager.AppSettings["TICKETS_FILES_PATH"] + WebConfigurationManager.AppSettings["TICKETS_FILES_REGISTRO"];
                int iUploadedCnt = 0;
                string filename = "";
                for (int iCnt = 0; iCnt<=hfc.Count - 1; iCnt++)
                {
                    System.Web.HttpPostedFile hpf = hfc[iCnt];

                    if (hpf.ContentLength>0)
                    {
                        if (!Directory.Exists(sPath))
                        {
                            Directory.CreateDirectory(sPath);
                        }
                        if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                        {
                            hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                        }
                        else
                        {
                            StringBuilder archivoBuilder = new StringBuilder();
                            archivoBuilder.Append(sPath);
                            archivoBuilder.Append(Path.GetFileNameWithoutExtension(hpf.FileName));
                            archivoBuilder.Append("_");
                            archivoBuilder.Append(DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss"));
                            archivoBuilder.Append(Path.GetExtension(hpf.FileName));
                            filename = archivoBuilder.ToString();
                            hpf.SaveAs(filename);
                        }
                        archivoTicketsModel.NombreArchivo = hpf.FileName;
                        iUploadedCnt = iUploadedCnt + 1;
                        archivoTicketsModel.RutaArchivo = sPath;
                        response = iGestionBussiness.GuardarArchivo(archivoTicketsModel);
                    }
                }
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_00));
            }
            return response;
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

        [HttpPost]
        public SingleResponse<IList<AsegModel>> ConsultaAseguradoras(ClienteAsegModel clienteAseg)
        {
            SingleResponse<IList<AsegModel>> response = new SingleResponse<IList<AsegModel>>();
            try
            {
                return iGestionBussiness.ConsultaAseguradoras(clienteAseg);
            }
            catch (Exception)
            {
                response.Error(new DomainException(Codes.ERR_00_00));
            }
            return response;
        }
    }
}