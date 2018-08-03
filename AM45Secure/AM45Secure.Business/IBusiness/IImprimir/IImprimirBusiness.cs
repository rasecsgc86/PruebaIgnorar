using AM45Secure.Commons.Modelos.Emitir;
using AM45Secure.Commons.Modelos.Imprimir;
using AM45Secure.Commons.Modelos.Comparador;
using AM45Secure.Commons.Modelos.Comunes;
using Zero.Handlers.Response;

namespace AM45Secure.Business.IBusiness.IImprimir
{
    public interface IImprimirBusiness
    {
        SingleResponse<FolletosModel> ConsultarFolletos(PolizaModel poliza);
        SingleResponse<AsegPaqueteModel> ConsultaAsegPaquete(AsegPaqueteModel numeroModel);
        SingleResponse<CondicionesGralesModel> ConsultaCondicionesGrales(VersionesModel versiones);
    }
}
