using System.Collections.Generic;
using AM45Secure.Commons.Modelos.Comparador;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.comunes;

namespace AM45Secure.DataAccess.IDataAccess.IComparador
{
    public interface IComparadorDataAccess
    {
        IList<SolicitudCotizacionModel> ConsultarSolicitudCotizacion(DatosSolicitudModel datosCotizacionModel);
        IList<FormasPagoProductoModel> ConsultarFormasPagoProductos(SolicitudCotizacionModel solicitudCotizacionModel);
        IList<AseguradorasProductoModel> ConsultarAseguradorasProducto(SolicitudCotizacionModel solicitudCotizacionModel, NeCotizacionModel neCotizacion);
        int ConsultaPaquetesAseguradoras(int aseguradoraId, int elementoId);
        NeCotizacionModel ConsultarNeCotizacion(CotizarModel cotizarModel);
        CotizanteModel ConsultarDatosCotizante(SolicitudCotizacionModel solicitudCotizacionModel);
        ClientesModel ConsultarDatosCliente(SolicitudCotizacionModel solicitudCotizacionModel);
        ProductoModel ConsultarDatosProducto(SolicitudCotizacionModel solicitudCotizacionModel);
        List<ReporteCotizacionModel> ConsultarCotizacionRep(RepCotizacionModel repParams);
        List<ReporteCoberturasModel> ConsultaCoberturasReporte(RepCotizacionModel repParams);
        List<ReporteCoberturasModel> ConsultaCoberturasReporteNuevo(RepCotizacionModel repParams);

        void ActualizaTipoArrendamientoCargaRemolquesCoptizacion(SolicitudCotizacionModel solicitudCotizacionModel,
                                                                 int cotizacionId);

        string ConsultarNotasImportantes(SolicitudCotizacionModel solicitud);
        AgenciasModel ConsultarDatosAgencia(SolicitudCotizacionModel solicitudCotizacionModel);
        IList<ElementoModel> ConsultaElementosPorIdInterno(int idInterno, int catalogoId);
        List<PaqueteModel> ConsultaPaquetesCotizable(SolicitudCotizacionModel solicitudCotizacionModel, int aseguradoraId);
        Dictionary<int, string> ConsultaNombrePaqueteComparador(List<PaqueteModel> idsPaquetes, int idProducto);
        Dictionary<int, string> ConsultaNombrePaqueteComparadorCompleto(List<PaqueteModel> idsPaquetes);
        IList<CoberturaModel> ConsultaCoberturasEspeciales(SolicitudCotizacionModel solicitudCotizacion);

        bool BitacoraEncioCorreo(int cotizacionId, int solicitudId, int numero, List<string> destinatarios, string cotizante, string correoCotizante);
        PersonaEmail obtenerDatosPersonaEmail(PersonaEmail perosonaEmail);

        IList<ReporteCorresExcel> ConsultarDatosReporteCorreo(int opcionFiltro);


        ClienteProductoSolicitudMoldel obtenerClienteProductoXSolictud(int IdSolicitud);
        EstructuraCorreoModel obtenerEstructuraCorreo(int idCliente, int idProducto);
    }
}