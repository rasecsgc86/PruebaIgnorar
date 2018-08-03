using AM45Secure.Commons.Modelos.Manuales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM45Secure.DataAccess.IDataAccess.IManuales
{
    public interface IManualesDataAccess
    {

        IList<ManualesModel> ConsultarManuales(ManualRequest manualModel);
        IList<CategoriaModel> ConsultarCategoria();
        bool GuardarDatosDocumento(InsertManualRequest requestModel, string usuario); 
        bool ElimarDocumento(int Id);
        FiltroManuales FiltrosDocumentos(string IdUsuario);
       bool UsuarioAdministrador(string userID);
    }
}
