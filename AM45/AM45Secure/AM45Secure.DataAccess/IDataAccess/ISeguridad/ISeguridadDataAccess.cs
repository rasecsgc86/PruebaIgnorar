using System.Collections.Generic;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Seguridad;

namespace AM45Secure.DataAccess.IDataAccess.ISeguridad
{
    public interface ISeguridadDataAccess
    {
        IList<MenuModel> ConsultaMenus(MenuModel menuModel);
        AmVersionSistemaModel ConsultaVesionSistema();
    }
}
