using System.Collections.Generic;
using System.Web.Http;
using AM45Secure.Business.IBusiness.ISeguridad;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Seguridad;
using Zero.Handlers.Response;

namespace AM45Secure.RESTServices.Controllers.Seguridad
{
    public class SeguridadController : ApiController
    {
        private readonly ISeguridadBusiness iSeguridadBusiness;

        public SeguridadController(ISeguridadBusiness iSeguridadBusiness)
        {
            this.iSeguridadBusiness = iSeguridadBusiness;
        }

        [HttpPost]
        public SingleResponse<IList<MenuModel>> ConsultarMenus(MenuModel menuModel)
        {
            return iSeguridadBusiness.ConsultaMenus(menuModel);
        }

        [HttpPost]
        public SingleResponse<AmVersionSistemaModel> ConsultaVesionSistema()
        {
            return iSeguridadBusiness.ConsultaVesionSistema();
        }
    }
}
