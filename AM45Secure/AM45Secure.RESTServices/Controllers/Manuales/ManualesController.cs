using AM45Secure.Business.Business.Manuales;
using AM45Secure.Business.IBusiness.IManuales;
using AM45Secure.Commons.Modelos.Manuales;
using AM45Secure.Commons.Modelos.Tickets;
using AM45Secure.Commons.Recursos;
using AM45Secure.DataAccess.IDataAccess.IManuales;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;
using Zero.Exceptions;
using Zero.Handlers.Response;

namespace AM45Secure.RESTServices.Controllers.Manuales
{
    public class ManualesController : ApiController
    {
        private  IManualesBussines iManualesBussines;


        public ManualesController(IManualesBussines iManualesBussines)
        {
            this.iManualesBussines = iManualesBussines;

        }

        public SingleResponse<bool> usuarioAdministador(requestFiltro request)
        {

            return iManualesBussines.UsuarioAdministrador(request.Tkn);
        }

        [HttpPost]
        public SingleResponse<FiltroManuales> FiltrosDocumentos(requestFiltro request)
        {


            return iManualesBussines.FiltrosDocumentos(request);
        }




        [HttpGet]
        public HttpResponseMessage DescargarArchivo(string rutaArchivo)
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
                        FileName = Uri.EscapeDataString(rutaArchivo.Substring(rutaArchivo.LastIndexOf('\\')))
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
        public SingleResponse<IList<ManualesModel>> ConsultarManuales(ManualRequest manualModel)
        {
 

            return iManualesBussines.ConsultarManuale(manualModel);
        }




        public SingleResponse<bool> ElimarDocumento(EliminarRequestModel requesModel)
        {


            return iManualesBussines.ElimarDocumento(requesModel.Id);
        }

        public SingleResponse<IList<CategoriaModel>> ConsultarCategoria()
        {


            return iManualesBussines.ConsultarCategoria();
        }



        public SingleResponse<bool> GuardarDatosDocumento(InsertManualRequest requestModel)
        {


            return iManualesBussines.GuardarDatosDocumento(requestModel);
        }

        public SingleResponse<ArchivoModel> CargarArchivo()
        {
            SingleResponse<ArchivoModel> response = new SingleResponse<ArchivoModel>();
            try
            {
                string nombreArchivo = "";
                ArchivoTicketsModel archivoTicketsModel = new ArchivoTicketsModel();
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                string sPath = WebConfigurationManager.AppSettings["Achivos_FILES_PATH"];
                int iUploadedCnt = 0;
                string filename = "";
                for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                {
                    System.Web.HttpPostedFile hpf = hfc[iCnt];

                    if (hpf.ContentLength > 0)
                    {
                        if (!Directory.Exists(sPath))
                        {
                            Directory.CreateDirectory(sPath);
                        }
                        if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                        {
                            hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                            nombreArchivo = Path.GetFileName(hpf.FileName);
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
                            var a = Path.GetFileNameWithoutExtension(hpf.FileName);
                            nombreArchivo = filename.Substring(filename.IndexOf(Path.GetFileNameWithoutExtension(hpf.FileName)));
                        }
                        archivoTicketsModel.NombreArchivo = hpf.FileName;
                        iUploadedCnt = iUploadedCnt + 1;
                        archivoTicketsModel.RutaArchivo = sPath;
                       //response = iGestionBussiness.GuardarArchivo(archivoTicketsModel);
                    }
                }


                response.Done(new ArchivoModel {
                    Ruta = sPath+nombreArchivo,
                    NombreArchivo = nombreArchivo
                }, string.Empty);
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
    }
}
