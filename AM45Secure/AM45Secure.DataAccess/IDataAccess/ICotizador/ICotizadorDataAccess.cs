using System.Collections.Generic;
using AM45Secure.Commons.Modelos.Comparador;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Cotizador;

namespace AM45Secure.DataAccess.IDataAccess.ICotizador
{
    public interface ICotizadorDataAccess
    {
        IList<ProductoModel> ConsultarProductosCliente(CotizadorModel cotizadorModel);
        IList<ClientesModel> ConsultarCliente(CotizadorModel cotizadorModel);
        IList<RegionCodigoPostalModel> ConsultarCodigoPostal(CodigoPostalModel codigoPostalModel);
        IList<ValoresReglaModel> ConsultaReglaNegocio(CotizadorModel cotizadorModel);
        PanelCotizadorModel ConsultaPanelCotizacionFlex(CotizadorModel cotizadorModel);
        PanelCotizadorModel FiltraPanelCotizacionFlex(CotizadorModel cotizadorModel);
        IList<VersionesModel> ConsultarVersiones(SolicitudVersionesModel solicitudVersiones);
        IList<ElementoModel> CargaElementos(ElementoModel elementoModel);
        IList<PasajerosModel> ConsultaPasajeros(SolicitudPasajerosModel solicitudPasajeros);
        IList<AgenciasModel> ConsultaAgencias(ClientProdAgenAsegModel clientProdAgenAseg);
        CabeceraCotizacionModel EjecutaGrabadoSolicitudCotizacion(CabeceraCotizacionModel cabeceraCotizacionModel);
        IList<ValoresReglaModel> ConsultaReglaUdi(CotizadorModel cotizadorModel);
        IList<UsuarioPerfilModel> ValidaUsuarioPerfil(CotizadorModel cotizadorModel);
        IList<FormasPagoProductoModel> ConsultaFormasPagoProducto(int productoId);
        List<FechaSistemaModel> ConsultaFechaSistema();
        int ConsultarCantidadCliente(CotizadorModel cotizadorModel);
        IList<CoberturaModel> ConsultaCoberturasCotizadas(SolicitudCotizacionModel solicitudCotizacion);
        bool ExisteDaniosMaterialesCotizacionFlex(int idSolicitud);
        IList<ClientesModel> ConsultarClientesDeAgencia(CotizadorModel cotizadorModel);
    }
}