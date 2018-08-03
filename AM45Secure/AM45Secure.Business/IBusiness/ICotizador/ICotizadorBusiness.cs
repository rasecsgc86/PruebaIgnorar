using System.Collections.Generic;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Cotizador;
using Zero.Handlers.Response;

namespace AM45Secure.Business.IBusiness.ICotizador
{
    public interface ICotizadorBusiness
    {
        SingleResponse<IList<ProductoModel>> ConsultarProductosCliente(CotizadorModel cotizadorModel);

        SingleResponse<IList<ClientesModel>> ConsultarCliente(CotizadorModel cotizadorModel);

        SingleResponse<IList<RegionCodigoPostalModel>> ConsultarCodigoPostal(CotizadorModel cotizadorModel);

        SingleResponse<IList<ValoresReglaModel>> ConsultaReglaNegocio(CotizadorModel cotizadorModel);

        SingleResponse<CotizadorModel> ConsultaPanelCotizacionFlex(CotizadorModel cotizadorModel);

        SingleResponse<CotizadorModel> FiltraPanelCotizacionFlex(CotizadorModel cotizadorModel);

        SingleResponse<IList<VersionesModel>> ConsultarVersiones(CotizadorModel cotizadorModel);

        SingleResponse<CotizadorModel> CargaElementos();

        SingleResponse<IList<PasajerosModel>> ConsultaPasajeros(CotizadorModel cotizadorModel);

        SingleResponse<IList<AgenciasModel>> ConsultaAgencias(CotizadorModel cotizadorModel);

        SingleResponse<CabeceraCotizacionModel> EjecutaGrabadoSolicitudCotizacion(CabeceraCotizacionModel cabeceraCotizacionModel);

        SingleResponse<IList<ValoresReglaModel>> ConsultaReglaUdi(CotizadorModel cotizadorModel);

        SingleResponse<FechaFinVigenciaModel> CalculaPlazos(CotizadorModel cotizadorModel);

        SingleResponse<IList<UsuarioPerfilModel>> ValidaUsuarioPerfil(CotizadorModel cotizadorModel);

        SingleResponse<List<FechaSistemaModel>> ConsultaFechaSistema();
        SingleResponse<int> ConsultarCantidadCliente(CotizadorModel cotizadorModel);

        SingleResponse<RecargaDatosCotizacionModel> RecargaDatosCotizacion(SolicitudCotizacionModel solicitudCotizacionModel);

        SingleResponse<IList<CoberturaModel>> ConsultaCoberturasCotizadas(SolicitudCotizacionModel solicitudCotizacion);

        SingleResponse<LimiteValorFacturaModel> ValidaLimiteValorFactura(LimiteValorFacturaModel limiteValorFactura);

        SingleResponse<LimiteValorFacturaModel> ValidaLimiteAdaptaciones(CotizadorModel cotizador);
    }
}