using System.Collections.Generic;
using System.Web.Http;
using AM45Secure.Business.IBusiness.ICotizador;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Cotizador;
using Zero.Handlers.Response;

namespace AM45Secure.RESTServices.Controllers.Cotizador
{
    //[Authorize]
    public class CotizadorController : ApiController
    {
        private readonly ICotizadorBusiness iCotizadorBusiness;

        public CotizadorController(ICotizadorBusiness iCotizadorBusiness)
        {
            this.iCotizadorBusiness = iCotizadorBusiness;
        }

        [HttpPost]
        public SingleResponse<IList<ProductoModel>> ConsultarProductosCliente(CotizadorModel cotizadorModel)
        {
            return iCotizadorBusiness.ConsultarProductosCliente(cotizadorModel);
        }

        [HttpPost]
        public SingleResponse<IList<ClientesModel>> ConsultarCliente(CotizadorModel cotizadorModel)
        {
            return iCotizadorBusiness.ConsultarCliente(cotizadorModel);
        }

        [HttpPost]
        public SingleResponse<IList<RegionCodigoPostalModel>> ConsultarCodigoPostal(CotizadorModel cotizadorModel)
        {
            return iCotizadorBusiness.ConsultarCodigoPostal(cotizadorModel);
        }

        [HttpPost]
        public SingleResponse<IList<ValoresReglaModel>> ConsultaReglaNegocio(CotizadorModel cotizadorModel)
        {
            return iCotizadorBusiness.ConsultaReglaNegocio(cotizadorModel);
        }

        [HttpPost]
        public SingleResponse<IList<VersionesModel>> ConsultarVersiones(CotizadorModel cotizadorModel)
        {
            return iCotizadorBusiness.ConsultarVersiones(cotizadorModel);
        }

        [HttpPost]
        public SingleResponse<CotizadorModel> ConsultaPanelCotizacionFlex(CotizadorModel cotizadorModel)
        {
            return iCotizadorBusiness.ConsultaPanelCotizacionFlex(cotizadorModel);
        }

        [HttpPost]
        public SingleResponse<CotizadorModel> FiltraPanelCotizacionFlex(CotizadorModel cotizadorModel)
        {
            return iCotizadorBusiness.FiltraPanelCotizacionFlex(cotizadorModel);
        }

        [HttpPost]
        public SingleResponse<CotizadorModel> CargaElementos()
        {
            return iCotizadorBusiness.CargaElementos();
        }

        [HttpPost]
        public SingleResponse<IList<PasajerosModel>> ConsultaPasajeros(CotizadorModel cotizadorModel)
        {
            return iCotizadorBusiness.ConsultaPasajeros(cotizadorModel);
        }

        [HttpPost]
        public SingleResponse<IList<AgenciasModel>> ConsultaAgencias(CotizadorModel cotizadorModel)
        {
            return iCotizadorBusiness.ConsultaAgencias(cotizadorModel);
        }

        [HttpPost]
        public SingleResponse<CabeceraCotizacionModel> EjecutaGrabadoSolicitudCotizacion(CabeceraCotizacionModel cabeceraCotizacionModel)
        {
            return iCotizadorBusiness.EjecutaGrabadoSolicitudCotizacion(cabeceraCotizacionModel);
        }

        [HttpPost]
        public SingleResponse<IList<ValoresReglaModel>> ConsultaReglaUdi(CotizadorModel cotizadorModel)
        {
            return iCotizadorBusiness.ConsultaReglaUdi(cotizadorModel);
        }

        [HttpPost]
        public SingleResponse<FechaFinVigenciaModel> CalculaPlazos(CotizadorModel cotizadorModel)
        {
            return iCotizadorBusiness.CalculaPlazos(cotizadorModel);
        }

        [HttpPost]
        public SingleResponse<IList<UsuarioPerfilModel>> ValidaUsuarioPerfil(CotizadorModel cotizadorModel)
        {
            return iCotizadorBusiness.ValidaUsuarioPerfil(cotizadorModel);
        }

        [HttpPost]
        public SingleResponse<List<FechaSistemaModel>> ConsultaFechaSistema()
        {
            return iCotizadorBusiness.ConsultaFechaSistema();
        }

        [HttpPost]
        public SingleResponse<int> ConsultarCantidadCliente(CotizadorModel cotizadorModel)
        {
            return iCotizadorBusiness.ConsultarCantidadCliente(cotizadorModel);
        }

        [HttpPost]
        public SingleResponse<RecargaDatosCotizacionModel> RecargaDatosCotizacion(SolicitudCotizacionModel solicitudCotizacionModel)
        {
            return iCotizadorBusiness.RecargaDatosCotizacion(solicitudCotizacionModel);
        }

        public SingleResponse<LimiteValorFacturaModel> ValidaLimiteValorFactura(LimiteValorFacturaModel limiteValorFactura)
        {
            return iCotizadorBusiness.ValidaLimiteValorFactura(limiteValorFactura);
        }

        public SingleResponse<LimiteValorFacturaModel> ValidaLimiteAdaptaciones(CotizadorModel cotizador)
        {
            return iCotizadorBusiness.ValidaLimiteAdaptaciones(cotizador);
        }
    }
}