using System;
using System.Collections.Generic;
using System.Linq;
using AM45Secure.Commons.Constantes.Querys;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Seguridad;
using AM45Secure.Commons.Recursos;
using AM45Secure.DataAccess.Entidades.Comunes;
using AM45Secure.DataAccess.Entidades.Seguridad;
using AM45Secure.DataAccess.IDataAccess.IGeneric;
using AM45Secure.DataAccess.IDataAccess.ISeguridad;
using Zero.Exceptions;

namespace AM45Secure.DataAccess.DataAccess.Seguridad
{
    public class SeguridadDataAccess : ISeguridadDataAccess
    {
        private readonly IGenericDataAccess iGenericDataAccess;

        public SeguridadDataAccess(IGenericDataAccess iGenericDataAccess)
        {
            this.iGenericDataAccess = iGenericDataAccess;
        }

        public IList<MenuModel> ConsultaMenus(MenuModel menuModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<SpTicConsultaMenus> spTicConsultaMenus = iGenericDataAccess.ExecuteStoredProcedure(new SpTicConsultaMenus
                { PerfilId = menuModel.PerfilId.ToString(), PersonaId = menuModel.PersonaId.ToString() });
                iGenericDataAccess.CloseConnection();
                IList<MenuModel> listaMenus = spTicConsultaMenus.Select(x => new MenuModel()
                {
                    ClaveMenu = x.ClaveMenu,
                    NombreUsuario = x.NombreUsuario, /* INDRA FJQP Mostrar nombre de usuario pantalla de inicio*/
                    PerfilId = menuModel.PerfilId,   /* INDRA FJQP Mostrar Perfil que se Firma*/
                    PersonaId = menuModel.PersonaId,  /* INDRA FJQP Mostrar Persona que se Firma*/
                    ManejaUDI = x.ManejaUDI  /* INDRA FJQP ManejaUDI */
                }).ToList();
                return listaMenus;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_00, e);
            }
        }

        public AmVersionSistemaModel ConsultaVesionSistema()
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<AmVersionSistema> version = iGenericDataAccess.ExecuteQuery<AmVersionSistema>(CQuerysSeguridad.QryVersionSistema);
                iGenericDataAccess.CloseConnection();

                IList<AmVersionSistemaModel> versionSistema = version.Select(x => new AmVersionSistemaModel
                {
                    IdVersion = x.IdVersion,
                    Version = x.Version,
                    Descripcion = x.Descripcion,
                    Fecha = x.Fecha,
                    Ot = x.Ot
                }).ToList();
                return versionSistema[0];
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_00, e);
            }
        }
    }
}
