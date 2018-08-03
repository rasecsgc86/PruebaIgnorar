using AM45Secure.Commons.Modelos.Manuales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Handlers.Response;

namespace AM45Secure.Business.IBusiness.IManuales
{
  public  interface IManualesBussines
    {

        SingleResponse<IList<ManualesModel>> ConsultarManuale(ManualRequest manualModel); 
        SingleResponse<IList<CategoriaModel>> ConsultarCategoria();

        SingleResponse<bool> GuardarDatosDocumento(InsertManualRequest requestModel);

        SingleResponse<bool> ElimarDocumento(int Id);
        SingleResponse<FiltroManuales> FiltrosDocumentos(requestFiltro request);
        SingleResponse<bool> UsuarioAdministrador(string tkn);
    }
}
