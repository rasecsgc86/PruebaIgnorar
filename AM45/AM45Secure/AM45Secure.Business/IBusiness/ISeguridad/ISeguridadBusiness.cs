using System.Collections.Generic;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Seguridad;
using Zero.Handlers.Response;

namespace AM45Secure.Business.IBusiness.ISeguridad
{
    public interface ISeguridadBusiness
    {
        SingleResponse<IList<MenuModel>> ConsultaMenus(MenuModel menu);
        SingleResponse<AmVersionSistemaModel> ConsultaVesionSistema();
    }
}
