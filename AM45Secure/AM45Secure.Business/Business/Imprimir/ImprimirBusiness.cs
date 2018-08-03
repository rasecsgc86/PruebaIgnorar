using AM45Secure.Business.IBusiness.IImprimir;
using AM45Secure.Commons.Constantes.Comunes;
using AM45Secure.Commons.Modelos.Comparador;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Emitir;
using AM45Secure.Commons.Modelos.Imprimir;
using AM45Secure.Commons.Recursos;
using AM45Secure.DataAccess.IDataAccess.IComparador;
using AM45Secure.DataAccess.IDataAccess.IImprimir;
using System;
using System.Collections.Generic;
using System.Configuration;
using Zero.Exceptions;
using Zero.Handlers.Response;
using Zero.Utils;
using Zero.Utils.Models;

namespace AM45Secure.Business.Business.Imprimir
{
    public class ImprimirBusiness : IImprimirBusiness
    {
        private readonly IImprimirDataAccess iImprimirDataAccess;
        private readonly IComparadorDataAccess iComparadorDataAccess;

        public ImprimirBusiness(IImprimirDataAccess iImprimirDataAccess, IComparadorDataAccess iComparadorDataAccess)
        {
            this.iImprimirDataAccess = iImprimirDataAccess;
            this.iComparadorDataAccess = iComparadorDataAccess;
        }

        /* INDRA FJQP --- Emision Multiple , Encontrak*/
        public SingleResponse<FolletosModel> ConsultarFolletos(PolizaModel poliza)
        {
            SingleResponse<FolletosModel> response = new SingleResponse<FolletosModel>();

            try
            {
                #region ModelValidations

                if (null == poliza)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }
                IList<Validation> validations = ValidatorZero.Validate(poliza, new OptionsValidation()
                {
                    ValidateIntCero = false,
                    ExcludeOptionals = true
                });
                if (validations.Count>0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion

                FolletosModel folletos = new FolletosModel
                {
                    FolletoMarsh = ConfigurationManager.AppSettings["FolletoMarsh"],
                    FolletoAseguradora = ConfigurationManager.AppSettings["FolletoAseguradora"],
                    FolletoZurich = ConfigurationManager.AppSettings["FolletoZurich"],
                    MuestraZurich = ConsultaNePersonasInciso(poliza).Response.Tipo != 212,
                    QLTSEncontrack = ConfigurationManager.AppSettings["QLTSEncontrack"]
                };

                response.Done(folletos, string.Empty);
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
                response.Error(new DomainException(CodesImprimir.ERR_00_01, e));
            }
            return response;
        }

        public SingleResponse<AsegPaqueteModel> ConsultaAsegPaquete(AsegPaqueteModel numeroModel)
        {
            SingleResponse<AsegPaqueteModel> response = new SingleResponse<AsegPaqueteModel>();
            try
            {
                #region ModelValidations

                if (null == numeroModel)
                {
                    throw new DomainException(Codes.ERR_00_01);
                }

                #endregion ModelValidations

                AsegPaqueteModel contratante = iImprimirDataAccess.ConsultaAsegPaquete(numeroModel);
                if (contratante != null)
                {
                    contratante = ConsultaPaquete(numeroModel, contratante);
                }
                response.Done(contratante, string.Empty);
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
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }
            return response;
        }

        private AsegPaqueteModel ConsultaPaquete(AsegPaqueteModel numeroModel, AsegPaqueteModel aseg)
        {
            AsegPaqueteModel paquete = new AsegPaqueteModel();
            DatosSolicitudModel datos = new DatosSolicitudModel();
            datos.SolicitudId = numeroModel.SolicitudId;
            IList<SolicitudCotizacionModel> listSolicitud = iComparadorDataAccess.ConsultarSolicitudCotizacion(datos);
            if (listSolicitud.Count == 0 || listSolicitud == null)
            {
                throw new DalException(CodesBenchmark.ERR_02_05);
            }
            SolicitudCotizacionModel solicitudCotizacionModel = listSolicitud[0];
            List<PaqueteModel> listaCotizable = iComparadorDataAccess.ConsultaPaquetesCotizable(solicitudCotizacionModel, aseg.AseguradoraId);
            Dictionary<int, string> paqueDictionary = iComparadorDataAccess.ConsultaNombrePaqueteComparador(listaCotizable, solicitudCotizacionModel.ProductoId);
            Dictionary<int, string> paqueDictionaryCompleto = iComparadorDataAccess.ConsultaNombrePaqueteComparadorCompleto(listaCotizable);
            paquete.Paquete = (solicitudCotizacionModel.Flexible) ? ConstTipoPersonas.Paquete : paqueDictionary.ContainsKey(aseg.PaqueteId) ? paqueDictionary[aseg.PaqueteId] : paqueDictionaryCompleto[aseg.PaqueteId];
            paquete.Aseguradora = aseg.Aseguradora;
            paquete.AseguradoraId = aseg.AseguradoraId;
            paquete.PaqueteId = aseg.PaqueteId;

            return paquete;
        }

        private SingleResponse<PersonaIncisoModel> ConsultaNePersonasInciso(PolizaModel poliza)
        {
            SingleResponse<PersonaIncisoModel> response = new SingleResponse<PersonaIncisoModel>();

            try
            {
                PersonaIncisoModel personasInciso = iImprimirDataAccess.ConsultaNePersonasInciso(poliza);
                response.Done(personasInciso, string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesImprimir.ERR_00_01, e));
            }
            return response;
        }

        public SingleResponse<CondicionesGralesModel> ConsultaCondicionesGrales(VersionesModel versiones)
        {
            SingleResponse<CondicionesGralesModel> response = new SingleResponse<CondicionesGralesModel>();

            try
            {
                #region ModelValidations

                if (null == versiones)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }

                #endregion

                CondicionesGralesModel condicionesGralesModel = new CondicionesGralesModel()
                {
                    CondicionesGralesQlt = ConfigurationManager.AppSettings["CondicionesGralesQlt"],
                    CondicionesGralesAutos = ConfigurationManager.AppSettings["CondicionesGralesAutos"],
                    CondicionesGralesCamiones = ConfigurationManager.AppSettings["CondicionesGralesCamiones"],
                    MuestraCondicionesGralesAutos = versiones.VehiculoId == 757,
                };

                response.Done(condicionesGralesModel, string.Empty);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesImprimir.ERR_00_03, e));
            }
            return response;
        }
    }
}