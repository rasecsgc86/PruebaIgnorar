using AM45Secure.Business.IBusiness.IConfigurador;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Configurador;
using AM45Secure.Commons.Modelos.Manuales;
using AM45Secure.Commons.Recursos;
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

namespace AM45Secure.RESTServices.Controllers.Configurador
{
    public class ConfiguradorController : ApiController
    {
        private readonly IConfiguradorBusiness iConfiguradorBusiness;

        public ConfiguradorController(IConfiguradorBusiness iConfiguradorBusiness)
        {
            this.iConfiguradorBusiness = iConfiguradorBusiness;
        }

        [HttpPost]
        public SingleResponse<IList<nePersonasModel>> ConsultarClientesConfigurador()
        {
            return iConfiguradorBusiness.ConsultarClientesConfigurador();
        }

        [HttpPost]

        public SingleResponse<IList<ProductoModel>> ConsultaProductosFlexibles()
        {
            return iConfiguradorBusiness.ConsultaProductosFlexibles();
        }

        [HttpPost]

        public SingleResponse<IList<ElementoModel>> ConsultaTipoAuto()
        {
            return iConfiguradorBusiness.ConsultaTipoAuto();
        }

        [HttpPost]

        public SingleResponse<IList<ElementoModel>> ConsultaTipoServicio()
        {
            return iConfiguradorBusiness.ConsultaTipoServicio();
        }
        [HttpPost]
        public SingleResponse<IList<ElementoModel>> ConsultaTipoSeguro()
        {
            return iConfiguradorBusiness.ConsultaTipoSeguro();
        }
        [HttpPost]
        public SingleResponse<IList<ElementoModel>> EsNuevo()
        {
            return iConfiguradorBusiness.EsNuevo();
        }
        [HttpPost]
        public SingleResponse<IList<ElementoModel>> CargoEnLinea()
        {
            return iConfiguradorBusiness.CargoEnLinea();
        }
        [HttpPost]
        public SingleResponse<IList<ElementoModel>> ConsultaAseguradoras()
        {
            return iConfiguradorBusiness.ConsultaAseguradoras();
        }
        [HttpPost]
        public SingleResponse<IList<ElementoModel>> ConsultaEstatus()
        {
            return iConfiguradorBusiness.ConsultaEstatus();
        }

        [HttpPost]

        public SingleResponse<ProductoFlexModel> GuardaProductoFlexible(ConfiguradorModel productoFlexModel)
        {
            return iConfiguradorBusiness.GuardaProductoFlexible(productoFlexModel);
        }

        // INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos
        [HttpPost]

        public SingleResponse<IList<PerfilesUsuarioModel>> ConsultaPerfilesSistema()
        {
            return iConfiguradorBusiness.ConsultaPerfilesSistema();
        }

        [HttpPost]

        public SingleResponse<IList<UsuariosPerfil>> ConsultaUsuarioPorPerfil(ConfiguradorModel usuariosPerfilModel)
        {
            return iConfiguradorBusiness.ConsultaUsuarioPorPerfil(usuariosPerfilModel);
        }

        [HttpPost]

        public SingleResponse<PerfilesFlexibleModel> GuardaUsuarioFlexible(ConfiguradorModel perfilesFlexibleModel)
        {
            return iConfiguradorBusiness.GuardaUsuarioFlexible(perfilesFlexibleModel);
        }

        [HttpPost]
        public SingleResponse<IList<PerfilesFlexModel>> ConsultarUsuariosFlexibles()
        {

            return iConfiguradorBusiness.ConsultarUsuariosFlexibles();
        }

        [HttpPost]

        public SingleResponse<IList<FormaPagoModel>> ConsultarFormasPagoLista()
        {
            return iConfiguradorBusiness.ConsultarFormasPagoLista();
        }

        [HttpPost]

        public SingleResponse<IList<FormasPagoProductoAseguradoraModel>> ConsultarFormasPagoAseguradoraLista()
        {
            return iConfiguradorBusiness.ConsultarFormasPagoAseguradoraLista();
        }

        [HttpPost]
        public SingleResponse<PerfilesFlexibleModel> ActualizaStatusUdi(ConfiguradorModel perfilesFlexibleModel)
        {
            return iConfiguradorBusiness.ActualizaStatusUdi(perfilesFlexibleModel);
        }

        [HttpPost]
        public SingleResponse<FormasPagoProductoModel> ActualizaPredeterminadoPago(ConfiguradorModel formasPagoProductoModel)
        {
            return iConfiguradorBusiness.ActualizaPredeterminadoPago(formasPagoProductoModel);
        }

        [HttpPost]

        public SingleResponse<CoberModel> ActualizarHomologacionTooltip(ConfiguradorModel configuradorModel)
        {
            return iConfiguradorBusiness.ActualizarHomologacionTooltip(configuradorModel);
        }

        [HttpPost]
        public SingleResponse<PerfilesFlexibleModel> EliminarUsuarioFlexible(ConfiguradorModel perfilesFlexibleModel)
        {
            return iConfiguradorBusiness.EliminarUsuarioFlexible(perfilesFlexibleModel);
        }

        [HttpPost]

        public SingleResponse<FormasPagoProductoModel> EliminarFormaDePago(ConfiguradorModel formasPagoProductoModel)
        {
            return iConfiguradorBusiness.EliminarFormaDePago(formasPagoProductoModel);
        }

        [HttpPost]

        public SingleResponse<IList<ElementoModel>> ConsultaFormasPago()
        {
            return iConfiguradorBusiness.ConsultaFormasPago();
        }

        [HttpPost]

        public SingleResponse<FormasPagoProductoModel> GrabarFormaPagoProducto(ConfiguradorModel formasPagoProductoModel)
        {
            return iConfiguradorBusiness.GrabarFormaPagoProducto(formasPagoProductoModel);
        }

        [HttpPost]
        public SingleResponse<FormasPagoProductoAseguradora> GrabarFormasPagoProductoAseguradora(ConfiguradorModel formasPagoProductoAseguradora)
        {
            return iConfiguradorBusiness.GrabarFormasPagoProductoAseguradora(formasPagoProductoAseguradora);
        }

        [HttpPost]
        public SingleResponse<FormasPagoProductoAseguradora> EliminarFormaPagoProductoAseguradora(ConfiguradorModel formasPagoProductoAseguradora)
        {
            return iConfiguradorBusiness.EliminarFormaPagoProductoAseguradora(formasPagoProductoAseguradora);
        }

        [HttpPost]
        public SingleResponse<ConfiguradorModel> ConsultaPanelConfiguradorFlex(ConfiguradorModel configuradorModel)
        {
            return iConfiguradorBusiness.ConsultaPanelConfiguradorFlex(configuradorModel);
        }

        [HttpPost]
        public SingleResponse<RangosModel> ConsultaRangosSumasAseguradas(ConfiguradorModel configuradorModel)
        {
            return iConfiguradorBusiness.ConsultaRangosSumasAseguradas(configuradorModel);
        }

        [HttpPost]
        public SingleResponse<CoberModel> ActualizaRangosSumas(ConfiguradorModel configuradorModel)
        {
            return iConfiguradorBusiness.ActualizaRangosSumas(configuradorModel);
        }

        [HttpPost]

        public SingleResponse<CoberModel> ActualizaRangosDeducibles(ConfiguradorModel configuradorModel)
        {
            return iConfiguradorBusiness.ActualizaRangosDeducibles(configuradorModel);
        }

        public SingleResponse<ConfiguradorModel> ConsultarEnmascaradoDeducibles(ConfiguradorModel configuradorModel)
        {
            return iConfiguradorBusiness.ConsultarEnmascaradoDeducibles(configuradorModel);
        }

        [HttpPost]

        public SingleResponse<DocumentosPorCoberturaModel> GuardaDocumentoCobertura(ConfiguradorModel configuradorModel)
        {
            try
            {







            }
            catch (Exception ex)
            {

            }

            return iConfiguradorBusiness.GuardarDocumentoCobertura(configuradorModel);
        }

        public SingleResponse<ArchivoModel> CargaArchivoCobertura()
        {
            SingleResponse<ArchivoModel> response = new SingleResponse<ArchivoModel>();

            try
            {

                string nombreArchivo = "";
              
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                string sPath = WebConfigurationManager.AppSettings["Documentos_coberturas"];
               
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
        public SingleResponse<IList<DocumentosCoberModel>> ConsultaDocumentosPorCobertura(ConfiguradorModel configuradorModel)
        {
            return iConfiguradorBusiness.ConsultaDocumentosPorCobertura(configuradorModel);
        }

        [HttpPost]

        public SingleResponse<TextoAuxiliarUsoVehiculoModel> GuardaTextoAuxiliarUso(ConfiguradorModel configuradorModel)
        {
            return iConfiguradorBusiness.GuardaTextoAuxiliarUso(configuradorModel);
        }

        [HttpPost]
        public SingleResponse<IList<DocumentosCoberModel>> ConsultaDocumentosTodos()
        {
            return iConfiguradorBusiness.ConsultaDocumentosTodos();
        }

        /*INDRA FJQP Implementacion de Emisión Multiple*/
        [HttpPost]
        public SingleResponse<IList<DirectivasModel>> RecuperaInfoDirectivas(DirectivasModel directivasModel)
        {
            return iConfiguradorBusiness.RecuperaInfoDirectivas(directivasModel);
        }
        /*INDRA FJQP Implementacion de Emisión Multiple*/
        [HttpPost]
        public SingleResponse<IList<DirectivasModel>> RecuperaListaCoberturas(DirectivasModel directivasModel)
        {
            return iConfiguradorBusiness.RecuperaListaCoberturas(directivasModel);
        }

    }
}