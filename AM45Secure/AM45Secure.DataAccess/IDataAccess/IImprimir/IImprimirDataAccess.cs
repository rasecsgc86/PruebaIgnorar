using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Emitir;
using AM45Secure.Commons.Modelos.Imprimir;

namespace AM45Secure.DataAccess.IDataAccess.IImprimir
{
    public interface IImprimirDataAccess
    {
        AsegPaqueteModel ConsultaAsegPaquete(AsegPaqueteModel numeroModel);
        PersonaIncisoModel ConsultaNePersonasInciso(PolizaModel poliza);
    }
}
