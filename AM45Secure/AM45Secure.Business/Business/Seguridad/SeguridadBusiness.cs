using System;
using System.Collections.Generic;
using AM45Secure.Business.IBusiness.ISeguridad;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Seguridad;
using AM45Secure.Commons.Recursos;
using AM45Secure.DataAccess.IDataAccess.ISeguridad;
using Zero.Exceptions;
using Zero.Handlers.Response;

namespace AM45Secure.Business.Business.Seguridad
{
    public class SeguridadBusiness : ISeguridadBusiness
    {
        private readonly ISeguridadDataAccess iSeguridadDataAccess;

        public SeguridadBusiness(ISeguridadDataAccess iSeguridadDataAccess)
        {
            this.iSeguridadDataAccess = iSeguridadDataAccess;
        }

        public SingleResponse<IList<MenuModel>> ConsultaMenus(MenuModel menu)
        {
            SingleResponse<IList<MenuModel>> response = new SingleResponse<IList<MenuModel>>();
            try
            {
                menu.PerfilId = menu.GetIdPerfilUsuarioSesion();
                menu.PersonaId = menu.GetIdUsuarioSesion();
                menu.ManejaUDI = null; /* INDRA FJQP ManejaUDI */
                IList<MenuModel> listaMenus = iSeguridadDataAccess.ConsultaMenus(menu);
                response.Done(listaMenus, Codes.INF_00_00);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_00, e));
            }
            return response;
        }

        public SingleResponse<AmVersionSistemaModel> ConsultaVesionSistema()
        {
            SingleResponse<AmVersionSistemaModel> response = new SingleResponse<AmVersionSistemaModel>();
            try
            {
                AmVersionSistemaModel version = iSeguridadDataAccess.ConsultaVesionSistema();
                response.Done(version, Codes.INF_00_00);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_00, e));
            }
            return response;
        }
    }
}
