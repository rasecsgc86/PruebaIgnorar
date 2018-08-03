using AM45Secure.Business.IBusiness.IimagenCliente;
using AM45Secure.Commons.Modelos.ImagenCliente;
using AM45Secure.Commons.Modelos.Manuales;
using AM45Secure.Commons.Modelos.Tickets;
using AM45Secure.Commons.Recursos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;
using Zero.Exceptions;
using Zero.Handlers.Response;

namespace AM45Secure.RESTServices.Controllers
{
    public class ImagenClienteController : ApiController
    {
        private IimagenClienteBusiness iImagenClienteBussines;
        public ImagenClienteController(IimagenClienteBusiness iImagenClienteBussines)
        {
            this.iImagenClienteBussines = iImagenClienteBussines;

        }

        public SingleResponse<ArchivoModel> CargarArchivo()
        {
            SingleResponse<ArchivoModel> response = new SingleResponse<ArchivoModel>();
            try
            {
                string nombreArchivo = "";
                ArchivoTicketsModel archivoTicketsModel = new ArchivoTicketsModel();
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                string sPath = WebConfigurationManager.AppSettings["Imagen_Cliente_PATH"];
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


                response.Done(new ArchivoModel
                {
                    Ruta = sPath + nombreArchivo,
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

        public SingleResponse<bool> GuardarDatosImagenCliente(ImageClienteModel requestModel)
        {
            return iImagenClienteBussines.GuardarDatosIMagen(requestModel);
        }


        public SingleResponse<ImageClienteModel> ObtenerImagenCliente(ImageClienteModel requestModel)
        {
          SingleResponse<ImageClienteModel> response = new SingleResponse<ImageClienteModel>();

            var datosImagen = iImagenClienteBussines.seleDatosImagen(requestModel);
            if (datosImagen.Id == 0)
            {
                response.Done(null, string.Empty);
                return response;
            }

            Byte[] bytes = File.ReadAllBytes(datosImagen.Url);
            String file = Convert.ToBase64String(bytes);
            requestModel.Id = datosImagen.Id;
            requestModel.imagen64 = file;
            requestModel.Fecha = datosImagen.Fecha;
            requestModel.Url = "";
            response.Done(requestModel, string.Empty);

            return response;
        }


        public SingleResponse<bool> ElimarDocumento(EliminarRequestModel requesModel)
        {


            return iImagenClienteBussines.ElimarDocumento(requesModel.Id);
        }
    }
}
