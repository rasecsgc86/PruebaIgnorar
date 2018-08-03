using Zero.Handlers.Response;
using AM45Secure.Commons.Modelos.comunes;
using AM45Secure.Commons.Modelos.Comparador;
using AM45Secure.Commons.Modelos.Cotizador;
using System.Collections.Generic;

namespace AM45Secure.Business.IBusiness.IComparador
{
    public interface IComparadorBusiness
    {
        SingleResponse<CargaInicialModel> CargaInicial(CotizarModel cotizarModel);
        SingleResponse<ComparadorModel> CargaComparador(CotizarModel cotizarModel);
        SingleResponse<CabeceraCotizacionModel> ConsultarCabeceraCotizacion(CotizarModel cotizarModel);
        byte[] ConsultaReporteCotizacion(RepCotizacionModel repCotizacionModel, bool esEmail = false, List<string> destinatarios = null);

        bool BitacoraEncioCorreo(int cotizacionId, int solicitudId, int numero, List<string> destinatarios, string cotizante, string correoCotizante);

        byte[] descargaReporteEmail(int opcionFiltro);
    }
}
