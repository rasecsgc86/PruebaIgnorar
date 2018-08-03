using AM45Secure.Commons.Constantes.Querys;
using AM45Secure.Commons.Modelos.Comparador;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Emitir;
using AM45Secure.Commons.Recursos;
using AM45Secure.DataAccess.Entidades.Emitir;
using AM45Secure.DataAccess.IDataAccess.IComparador;
using AM45Secure.DataAccess.IDataAccess.IGeneric;
using AM45Secure.DataAccess.IDataAccess.IImprimir;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM45Secure.Commons.Modelos.Imprimir;
using AM45Secure.DataAccess.Entidades.Comunes;
using Zero.Ado.Models;
using Zero.Exceptions;

namespace AM45Secure.DataAccess.DataAccess.Imprimir
{
    public class ImprimirDataAccess : IImprimirDataAccess
    {
        private readonly IGenericDataAccess iGenericDataAccess;

        public ImprimirDataAccess(IGenericDataAccess iGenericDataAccess)
        {
            this.iGenericDataAccess = iGenericDataAccess;
        }

        public AsegPaqueteModel ConsultaAsegPaquete(AsegPaqueteModel numeroModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<AseguradoraPaquete> aseguradora = iGenericDataAccess.Consultar(CQuerysCotizador.QryAsegPaquete,
                                                                                     new AseguradoraPaquete()
                                                                                     {
                                                                                         Numero = numeroModel.Numero
                                                                                     }, new OptionsQueryZero()
                                                                                        {
                                                                                            ExcludeBool = true,
                                                                                            ExcludeNumericsDefaults = true
                                                                                        });
                iGenericDataAccess.CloseConnection();

                AsegPaqueteModel asegPaquete = aseguradora.Select(x => new AsegPaqueteModel()
                                                                       {
                                                                           Aseguradora = x.Aseguradora,
                                                                           Paquete = x.Paquete,
                                                                           AseguradoraId = x.AseguradoraId,
                                                                           PaqueteId = x.PaqueteId
                                                                       }).First();

                return asegPaquete;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesEmision.ERR_00_16, e);
            }
        }

        public PersonaIncisoModel ConsultaNePersonasInciso(PolizaModel poliza)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<NePersonasInciso> personaInciso = iGenericDataAccess.Consultar(
                                                                                     new NePersonasInciso()
                                                                                     {
                                                                                         Poliza = poliza.Poliza
                                                                                     },
                                                                                     new OptionsQueryZero()
                                                                                     {
                                                                                         ExcludeNumericsDefaults = true,
                                                                                         ExcludeBool = true
                                                                                     });
                iGenericDataAccess.CloseConnection();

                IList<PersonaIncisoModel> personasIncisoList = personaInciso.Select(x => new PersonaIncisoModel()
                                                                                         {
                                                                                             Tipo = x.Tipo
                                                                                         }).ToList();

                return personasIncisoList[0];
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesImprimir.ERR_00_02, e);
            }
        }
    }
}