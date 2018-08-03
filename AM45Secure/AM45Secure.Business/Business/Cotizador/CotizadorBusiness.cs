using System;
using System.Collections.Generic;
using AM45Secure.Business.IBusiness.IComparador;
using AM45Secure.Business.IBusiness.ICotizador;
using AM45Secure.Commons.Constantes.Comunes;
using AM45Secure.Commons.Modelos.comunes;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Cotizador;
using AM45Secure.Commons.Recursos;
using AM45Secure.DataAccess.IDataAccess.ICotizador;
using Zero.Exceptions;
using Zero.Handlers.Response;
using Zero.Utils;
using Zero.Utils.Models;

namespace AM45Secure.Business.Business.Cotizador
{
    public class CotizadorBusiness : ICotizadorBusiness
    {
        private readonly ICotizadorDataAccess iCotizadorDataAccess;
        private readonly IComparadorBusiness iComparadorBusiness;

        public CotizadorBusiness(ICotizadorDataAccess iCotizadorDataAccess, IComparadorBusiness iComparadorBusiness)
        {
            this.iCotizadorDataAccess = iCotizadorDataAccess;
            this.iComparadorBusiness = iComparadorBusiness;
        }

        public SingleResponse<IList<ProductoModel>> ConsultarProductosCliente(CotizadorModel cotizadorModel)
        {
            //AgenciaCotizacion agenciaCotizacion = new AgenciaCotizacion("connectionBD");
            //agenciaCotizacion.GrabadoSolicitudCotizacion;
            SingleResponse<IList<ProductoModel>> response = new SingleResponse<IList<ProductoModel>>();
            try
            {
                #region ModelValidations

                if (null == cotizadorModel)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }
                IList<Validation> validations = ValidatorZero.Validate(cotizadorModel.ClienteModel, new OptionsValidation()
                                                                                                    {
                                                                                                        ValidateIntCero = false,
                                                                                                        ExcludeOptionals = true
                                                                                                    });
                if (validations.Count>0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion ModelValidations

                #region manipulaciond de informacio <insert. delete>

                IList<ProductoModel> productos = iCotizadorDataAccess.ConsultarProductosCliente(cotizadorModel);
                response.Done(productos, string.Empty);

                #endregion
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

        public SingleResponse<IList<ClientesModel>> ConsultarCliente(CotizadorModel cotizadorModel)
        {
            SingleResponse<IList<ClientesModel>> response = new SingleResponse<IList<ClientesModel>>();
            try
            {
                #region ModelValidations

                if (null == cotizadorModel)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }
                IList<Validation> validations = ValidatorZero.Validate(cotizadorModel.ClienteModel, new OptionsValidation()
                                                                                                    {
                                                                                                        ValidateIntCero = false,
                                                                                                        ExcludeOptionals = true
                                                                                                    });
                if (validations.Count>0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion ModelValidations

                #region manipulaciond de informacio <insert. delete>

                IList<ClientesModel> clientes = cotizadorModel.GetIdPerfilUsuarioSesion() == ConstTipoPersonas.Agencia ? iCotizadorDataAccess.ConsultarClientesDeAgencia(cotizadorModel) : iCotizadorDataAccess.ConsultarCliente(cotizadorModel);
                response.Done(clientes, string.Empty);

                #endregion
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

        public SingleResponse<IList<RegionCodigoPostalModel>> ConsultarCodigoPostal(CotizadorModel cotizadorModel)
        {
            SingleResponse<IList<RegionCodigoPostalModel>> response = new SingleResponse<IList<RegionCodigoPostalModel>>();
            try
            {
                #region ModelValidations

                if (null == cotizadorModel)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }

                IList<Validation> validations = ValidatorZero.Validate(cotizadorModel, new OptionsValidation()
                                                                                       {
                                                                                           ValidateIntCero = false,
                                                                                           ExcludeOptionals = true
                                                                                       });

                if (validations.Count>0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion ModelValidations

                #region validaciones de negocio

                #endregion

                #region manipulaciond de informacio <insert. delete>

                IList<RegionCodigoPostalModel> regiones = iCotizadorDataAccess.ConsultarCodigoPostal(cotizadorModel.CodigoPostalModel);
                response.Done(regiones, string.Empty);

                #endregion
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

        public SingleResponse<IList<ValoresReglaModel>> ConsultaReglaNegocio(CotizadorModel cotizadorModel)
        {
            SingleResponse<IList<ValoresReglaModel>> response = new SingleResponse<IList<ValoresReglaModel>>();
            try
            {
                #region ModelValidations

                if (null == cotizadorModel)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }

                IList<Validation> validations = ValidatorZero.Validate(cotizadorModel, new OptionsValidation()
                                                                                       {
                                                                                           ValidateIntCero = false
                                                                                       });

                if (validations.Count>0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion

                #region validaciones de negocio

                #endregion

                #region manipulaciond de informacio <insert. delete>

                IList<ValoresReglaModel> regla = iCotizadorDataAccess.ConsultaReglaNegocio(cotizadorModel);
                response.Done(regla, string.Empty);

                #endregion
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

        public SingleResponse<CotizadorModel> ConsultaPanelCotizacionFlex(CotizadorModel cotizadorModel)
        {
            SingleResponse<CotizadorModel> response = new SingleResponse<CotizadorModel>();
            try
            {
                #region ValidacionesRequires

                if (null == cotizadorModel)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }
                IList<Validation> validations = ValidatorZero.Validate(cotizadorModel.PanelCotizadorModel, new OptionsValidation()
                                                                                                           {
                                                                                                               ValidateIntCero = true,
                                                                                                               ExcludeOptionals = true
                                                                                                           });
                if (0<validations.Count)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion

                CotizadorModel cotizador = new CotizadorModel
                                           {
                                               PanelCotizadorModel = iCotizadorDataAccess.ConsultaPanelCotizacionFlex(cotizadorModel)
                                           };

                response.Done(cotizador, string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }
            return response;
        }

        public SingleResponse<CotizadorModel> FiltraPanelCotizacionFlex(CotizadorModel cotizadorModel)
        {
            SingleResponse<CotizadorModel> response = new SingleResponse<CotizadorModel>();
            try
            {
                #region ValidacionesRequires

                if (null == cotizadorModel)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }

                IList<Validation> validations = ValidatorZero.Validate(cotizadorModel.PanelCotizadorModel, new OptionsValidation()
                                                                                                           {
                                                                                                               ValidateIntCero = true
                                                                                                           });
                if (0<validations.Count)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion

                CotizadorModel cotizador = new CotizadorModel
                                           {
                                               PanelCotizadorModel = iCotizadorDataAccess.FiltraPanelCotizacionFlex(cotizadorModel)
                                           };

                response.Done(cotizador, string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }
            return response;
        }

        public SingleResponse<IList<VersionesModel>> ConsultarVersiones(CotizadorModel cotizadorModel)
        {
            SingleResponse<IList<VersionesModel>> response = new SingleResponse<IList<VersionesModel>>();
            try
            {
                #region ModelValidations

                if (null == cotizadorModel)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }

                IList<Validation> validations = ValidatorZero.Validate(cotizadorModel, new OptionsValidation()
                                                                                       {
                                                                                           ValidateIntCero = false,
                                                                                           ExcludeOptionals = true
                                                                                       });

                if (validations.Count>0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion ModelValidations

                #region validaciones de negocio

                #endregion

                #region manipulaciond de informacio <insert. delete>

                IList<VersionesModel> versiones = iCotizadorDataAccess.ConsultarVersiones(cotizadorModel.SolicitudVersiones);
                response.Done(versiones, string.Empty);

                #endregion
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

        public SingleResponse<CotizadorModel> CargaElementos()
        {
            SingleResponse<CotizadorModel> response = new SingleResponse<CotizadorModel>();
            try
            {
                CotizadorModel cotizadorModel = new CotizadorModel();
                ElementoModel elementoModel = new ElementoModel
                                              {
                                                  CatalogoId = ConstElementos.SiNo
                                              };

                #region  Cosulta LoJack

                IList<ElementoModel> listaJack = iCotizadorDataAccess.CargaElementos(elementoModel);
                cotizadorModel.LoJack = listaJack;

                #endregion

                #region  Cosulta No Remolques

                IList<ElementoModel> listaRemolque = new List<ElementoModel>();
                ElementoModel remolque = new ElementoModel()
                                         {
                                             ElementoId = 1,
                                             Nombre = "1"
                                         };
                ElementoModel remolque2 = new ElementoModel()
                                          {
                                              ElementoId = 2,
                                              Nombre = "2"
                                          };

                listaRemolque.Add(remolque);
                listaRemolque.Add(remolque2);
                cotizadorModel.Remolque = listaRemolque;

                #endregion

                response.Done(cotizadorModel, String.Empty);
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

        public SingleResponse<IList<PasajerosModel>> ConsultaPasajeros(CotizadorModel cotizadorModel)
        {
            SingleResponse<IList<PasajerosModel>> response = new SingleResponse<IList<PasajerosModel>>();
            try
            {
                #region ModelValidations

                if (null == cotizadorModel)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }
                IList<Validation> validations = ValidatorZero.Validate(cotizadorModel, new OptionsValidation()
                                                                                       {
                                                                                           ValidateIntCero = false,
                                                                                           ExcludeOptionals = true
                                                                                       });
                if (validations.Count>0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion ModelValidations

                #region validaciones de negocio

                #endregion

                #region manipulaciond de informacio <insert. delete>

                IList<PasajerosModel> pasajeros = iCotizadorDataAccess.ConsultaPasajeros(cotizadorModel.SolicitudPasajeros);
                response.Done(pasajeros, string.Empty);

                #endregion
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

        public SingleResponse<IList<AgenciasModel>> ConsultaAgencias(CotizadorModel cotizadorModel)
        {
            SingleResponse<IList<AgenciasModel>> response = new SingleResponse<IList<AgenciasModel>>();
            try
            {
                #region ModelValidations

                if (null == cotizadorModel)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }

                IList<Validation> validations = ValidatorZero.Validate(cotizadorModel, new OptionsValidation()
                                                                                       {
                                                                                           ValidateIntCero = false,
                                                                                           ExcludeOptionals = true
                                                                                       });

                if (validations.Count>0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion ModelValidations

                #region validaciones de negocio

                #endregion

                #region manipulaciond de informacio <insert. delete>

                IList<AgenciasModel> versiones = iCotizadorDataAccess.ConsultaAgencias(cotizadorModel.ClientProdAgenAseg);
                response.Done(versiones, string.Empty);

                #endregion
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

        public SingleResponse<CabeceraCotizacionModel> EjecutaGrabadoSolicitudCotizacion(CabeceraCotizacionModel cabeceraCotizacionModel)
        {
            SingleResponse<CabeceraCotizacionModel> response = new SingleResponse<CabeceraCotizacionModel>();
            try
            {
                #region ModelValidations

                if (null == cabeceraCotizacionModel)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }

                IList<Validation> validations = ValidatorZero.Validate(cabeceraCotizacionModel, new OptionsValidation()
                                                                                                {
                                                                                                    ValidateIntCero = false
                                                                                                });

                if (validations.Count>0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion

                #region validaciones de negocio

                ClaveValorModel carga = new ClaveValorModel
                                        {
                                            Valor = cabeceraCotizacionModel.Vehiculo.ShowCargas ? cabeceraCotizacionModel.Vehiculo.Carga.Valor.Substring(0, 1) : null
                                        };

                if (cabeceraCotizacionModel.Vehiculo.Valor == null)
                {
                    cabeceraCotizacionModel.Vehiculo.Valor = "0.00";
                }

                cabeceraCotizacionModel.Vehiculo.Valor = cabeceraCotizacionModel.Vehiculo.Valor.Replace("$", string.Empty).Replace(",", string.Empty);
                cabeceraCotizacionModel.Vehiculo.Carga = carga;

                foreach (var cobertura in cabeceraCotizacionModel.Panel.Coberturas)
                {
                    if (cobertura.IsSeleccionada && cobertura.IsEspecial)
                    {
                        cobertura.FiltroValorRangoDeducible = cobertura.FiltroValorRangoDeducible.Replace("$", string.Empty).Replace(",", string.Empty);
                    }
                }

                #endregion

                #region manipulaciond de informacio <insert. delete>

                CabeceraCotizacionModel cabeceraCotizacionModelR = iCotizadorDataAccess.EjecutaGrabadoSolicitudCotizacion(cabeceraCotizacionModel);
                response.Done(cabeceraCotizacionModelR, string.Empty);

                #endregion
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

       

        public SingleResponse<IList<ValoresReglaModel>> ConsultaReglaUdi(CotizadorModel cotizadorModel)
        {
            SingleResponse<IList<ValoresReglaModel>> response = new SingleResponse<IList<ValoresReglaModel>>();
            try
            {
                #region ModelValidations

                if (null == cotizadorModel)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }

                IList<Validation> validations = ValidatorZero.Validate(cotizadorModel, new OptionsValidation()
                                                                                       {
                                                                                           ValidateIntCero = false
                                                                                       });

                if (validations.Count>0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion

                #region validaciones de negocio

                #endregion

                #region manipulaciond de informacio <insert. delete>

                IList<ValoresReglaModel> regla = iCotizadorDataAccess.ConsultaReglaUdi(cotizadorModel);
                response.Done(regla, string.Empty);

                #endregion
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

        public SingleResponse<FechaFinVigenciaModel> CalculaPlazos(CotizadorModel cotizadorModel)
        {
            SingleResponse<FechaFinVigenciaModel> response = new SingleResponse<FechaFinVigenciaModel>();
            try
            {
                FechaFinVigenciaModel fechaFinVigencia = new FechaFinVigenciaModel
                                                         {
                                                             FechaFinVigencia = Convert.ToDateTime(cotizadorModel.Plazos.FechaIniVigencia).AddMonths(cotizadorModel.Plazos.Plazos)
                                                         };

                response.Done(fechaFinVigencia, String.Empty);
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

        public SingleResponse<IList<UsuarioPerfilModel>> ValidaUsuarioPerfil(CotizadorModel cotizadorModel)
        {
            SingleResponse<IList<UsuarioPerfilModel>> response = new SingleResponse<IList<UsuarioPerfilModel>>();
            try
            {
                #region ModelValidations

                if (null == cotizadorModel)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }

                IList<Validation> validations = ValidatorZero.Validate(cotizadorModel, new OptionsValidation()
                                                                                       {
                                                                                           ValidateIntCero = false
                                                                                       });

                if (validations.Count>0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion

                #region validaciones de negocio

                #endregion

                #region manipulaciond de informacio <insert. delete>

                IList<UsuarioPerfilModel> usuarioPerfil = iCotizadorDataAccess.ValidaUsuarioPerfil(cotizadorModel);
                response.Done(usuarioPerfil, string.Empty);

                #endregion
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

        public SingleResponse<List<FechaSistemaModel>> ConsultaFechaSistema()
        {
            SingleResponse<List<FechaSistemaModel>> response = new SingleResponse<List<FechaSistemaModel>>();
            try
            {
                #region manipulaciond de informacio <insert. delete>

                List<FechaSistemaModel> fechaSistema = iCotizadorDataAccess.ConsultaFechaSistema();
                response.Done(fechaSistema, string.Empty);

                #endregion
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

        public SingleResponse<int> ConsultarCantidadCliente(CotizadorModel cotizadorModel)
        {
            SingleResponse<int> response = new SingleResponse<int>();
            try
            {
                #region ModelValidations

                if (null == cotizadorModel)
                {
                    throw new DomainException(CodesCotizador.ERR_01_06);
                }
                IList<Validation> validations = ValidatorZero.Validate(cotizadorModel.ClienteModel, new OptionsValidation()
                                                                                                    {
                                                                                                        ValidateIntCero = false,
                                                                                                        ExcludeOptionals = true
                                                                                                    });
                if (validations.Count>0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion ModelValidations

                #region manipulaciond de informacio <insert. delete>

                int noClientes = iCotizadorDataAccess.ConsultarCantidadCliente(cotizadorModel);
                response.Done(noClientes, string.Empty);

                #endregion
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

        public SingleResponse<RecargaDatosCotizacionModel> RecargaDatosCotizacion(SolicitudCotizacionModel solicitudCotizacionModel)
        {
            SingleResponse<RecargaDatosCotizacionModel> response = new SingleResponse<RecargaDatosCotizacionModel>();
            RecargaDatosCotizacionModel recargaDatosCotizacionModel = new RecargaDatosCotizacionModel
                                                                      {
                                                                          PreCargaCotizador = new CotizadorModel()
                                                                      };
            try
            {
                IList<Validation> validations = ValidatorZero.Validate(solicitudCotizacionModel, new OptionsValidation()
                                                                                                 {
                                                                                                     ValidateIntCero = true,
                                                                                                     ExcludeOptionals = false
                                                                                                 });
                if (validations.Count>0)
                {
                    throw new DomainValidationsException(validations);
                }

                CotizarModel cotizarModel = new CotizarModel
                                            {
                                                Tkn = solicitudCotizacionModel.Tkn,
                                                DatosCotizacionModel = new DatosSolicitudModel
                                                                       {
                                                                           SolicitudId = solicitudCotizacionModel.SolicitudId
                                                                       }
                                            };
                SingleResponse<CabeceraCotizacionModel> cabeceraResponse = iComparadorBusiness.ConsultarCabeceraCotizacion(cotizarModel);
                cabeceraResponse.ThrowIfNotOk();

                #region Consulta Info Cotizacion [Botón Regresar - Comparador]

                CotizadorModel cotizador = new CotizadorModel
                                           {
                                               ClienteModel = cabeceraResponse.Response.Cliente.Cliente,
                                               ClientProdAgenAseg = new ClientProdAgenAsegModel(),
                                               SolicitudVersiones = new SolicitudVersionesModel(),
                                               SolicitudPasajeros = new SolicitudPasajerosModel(),
                                               TipoArrendamiento = new List<ValoresReglaModel>(),
                                               SolicitudRegla = new SolicitudReglaNegocioModel(),
                                               PanelCotizadorModel = new PanelCotizadorModel(),
                                               TipoUnidad = new List<ValoresReglaModel>(),
                                               Antiguedad = new List<ValoresReglaModel>(),
                                               Armadoras = new List<ValoresReglaModel>(),
                                               Submarcas = new List<ValoresReglaModel>(),
                                               Servicio = new List<ValoresReglaModel>(),
                                               Modelos = new List<ValoresReglaModel>(),
                                               Cargas = new List<ValoresReglaModel>(),
                                               Productos = new List<ProductoModel>(),
                                               Agencias = new List<AgenciasModel>(),
                                               Tkn = solicitudCotizacionModel.Tkn
                                           };

                #region Carga de Productos

                cotizador.ClienteModel.ProductoFlex = (float) cabeceraResponse.Response.Cliente.ProductoFlex;
                cotizador.Productos = ConsultarProductosCliente(cotizador).Response;

                #endregion

                #region Carga Agencias

                cotizador.ClientProdAgenAseg.ClienteId = cotizador.ClienteModel.ClienteId;
                cotizador.ClientProdAgenAseg.Tkn = solicitudCotizacionModel.Tkn;
                cotizador.Agencias = ConsultaAgencias(cotizador).Response;

                #endregion

                #region Carga Tipo de Arrendamiento

                cotizador.SolicitudRegla.IdRegla = ConstValoresReglas.IdTipoArrendamiento;
                cotizador.SolicitudRegla.IdCliente = cotizador.ClienteModel.ClienteId.ToString();
                cotizador.TipoArrendamiento = iCotizadorDataAccess.ConsultaReglaNegocio(cotizador);

                #endregion

                #region Carga Tipo de Unidad

                cotizador.SolicitudRegla.IdRegla = ConstValoresReglas.IdTipoVehiculo;
                cotizador.SolicitudRegla.IdProducto = cabeceraResponse.Response.Cliente.Producto.ProductoId.ToString();
                cotizador.SolicitudRegla.ProductoFlex = (int) cabeceraResponse.Response.Cliente.ProductoFlex;
                cotizador.TipoUnidad = iCotizadorDataAccess.ConsultaReglaNegocio(cotizador);

                #endregion

                #region Carga Antiguedad [Nuevo/Usado]

                cotizador.SolicitudRegla.IdRegla = ConstValoresReglas.IdAntiguedad;
                cotizador.SolicitudRegla.IdTipoVehiculo = cabeceraResponse.Response.Vehiculo.TipoUnidad.ValorId;
                cotizador.SolicitudRegla.TipoVehiculo = cabeceraResponse.Response.Vehiculo.TipoUnidad.ValorId;
                cotizador.Antiguedad = iCotizadorDataAccess.ConsultaReglaNegocio(cotizador);

                #endregion

                #region Carga Uso de Unidad [Servicio]

                cotizador.SolicitudRegla.IdRegla = ConstValoresReglas.IdServicio;
                cotizador.SolicitudRegla.EstadoVehiculo = cabeceraResponse.Response.Vehiculo.Antiguedad.Valor;
                cotizador.Servicio = iCotizadorDataAccess.ConsultaReglaNegocio(cotizador);

                #endregion

                #region Carga Modelo [2016/2017....]

                cotizador.SolicitudRegla.IdRegla = ConstValoresReglas.IdModelos;
                cotizador.SolicitudRegla.Servicio = cabeceraResponse.Response.Vehiculo.ServicioInterno.Comodin;
                cotizador.Modelos = iCotizadorDataAccess.ConsultaReglaNegocio(cotizador);

                #endregion

                #region Carga Armadoras [Marcas]

                cotizador.SolicitudRegla.IdRegla = ConstValoresReglas.IdArmadoras;
                cotizador.SolicitudRegla.Modelo = cabeceraResponse.Response.Vehiculo.Modelo.Valor;
                cotizador.Armadoras = iCotizadorDataAccess.ConsultaReglaNegocio(cotizador);

                #endregion

                #region Carga Submarcas

                cotizador.SolicitudRegla.IdRegla = ConstValoresReglas.IdSubMarcas;
                cotizador.SolicitudRegla.Marca = cabeceraResponse.Response.Vehiculo.Armadora.Valor;
                cotizador.Submarcas = iCotizadorDataAccess.ConsultaReglaNegocio(cotizador);

                #endregion

                #region Carga Versiones

                cotizador.SolicitudVersiones.Filtro = Convert.ToInt32(cotizador.Armadoras[0].ValorId);
                cotizador.SolicitudVersiones.Tipo = cabeceraResponse.Response.Vehiculo.TipoUnidad.ValorId;
                cotizador.SolicitudVersiones.Servicio = cabeceraResponse.Response.Vehiculo.ServicioInterno.Comodin;
                cotizador.SolicitudVersiones.Modelo = Convert.ToInt32(cabeceraResponse.Response.Vehiculo.Modelo.Valor);
                cotizador.SolicitudVersiones.Marca = cabeceraResponse.Response.Vehiculo.Armadora.Valor;
                cotizador.SolicitudVersiones.Submarca = cabeceraResponse.Response.Vehiculo.SubMarca.Valor;
                cotizador.Versiones = iCotizadorDataAccess.ConsultarVersiones(cotizador.SolicitudVersiones);

                #endregion

                #region Carga Pasajeros

                cotizador.SolicitudPasajeros.TipoId = cabeceraResponse.Response.Vehiculo.TipoUnidad.ValorId;
                cotizador.SolicitudPasajeros.Tipo = cabeceraResponse.Response.Vehiculo.TipoUnidad.Valor.ToUpper();
                cotizador.SolicitudPasajeros.Producto = cabeceraResponse.Response.Cliente.Producto.NombreProducto.ToUpper();
                cotizador.SolicitudPasajeros.EsFlexible = Convert.ToBoolean(cabeceraResponse.Response.Cliente.ProductoFlex);
                cotizador.SolicitudPasajeros.Servicio = cabeceraResponse.Response.Vehiculo.ServicioInterno.Comodin;
                cotizador.SolicitudPasajeros.Modelo = Convert.ToInt32(cabeceraResponse.Response.Vehiculo.Modelo.Valor);
                cotizador.SolicitudPasajeros.Marca = cabeceraResponse.Response.Vehiculo.Armadora.Valor.ToUpper();
                cotizador.SolicitudPasajeros.Submarca = cabeceraResponse.Response.Vehiculo.SubMarca.Valor.ToUpper();
                cotizador.SolicitudPasajeros.Version = cabeceraResponse.Response.Vehiculo.Version.Descripcion.ToUpper();
                cotizador.Pasajeros = iCotizadorDataAccess.ConsultaPasajeros(cotizador.SolicitudPasajeros);

                #endregion

                #region Carga Tipos de Carga

                cotizador.SolicitudRegla.IdRegla = ConstValoresReglas.IdCargas;
                cotizador.Cargas = iCotizadorDataAccess.ConsultaReglaNegocio(cotizador);

                #endregion

                #region Carga Plazos

                cotizador.SolicitudRegla.IdRegla = ConstValoresReglas.IdPlazos;
                cotizador.Plazo = iCotizadorDataAccess.ConsultaReglaNegocio(cotizador);

                #endregion

                #region Estados de Circulación

                cotizador.SolicitudRegla.IdRegla = ConstValoresReglas.IdEstados;
                cotizador.Estados = iCotizadorDataAccess.ConsultaReglaNegocio(cotizador);

                #endregion

                #region Carga UDI's

                cotizador.SolicitudRegla.IdRegla = ConstValoresReglas.IdUdi;
                cotizador.Udi = iCotizadorDataAccess.ConsultaReglaUdi(cotizador);

                #endregion

                #region Carga Panel Coberturas/Aseguradoras

                if (Convert.ToInt32(cabeceraResponse.Response.Cliente.Producto.Flexible) == 1)
                {
                    cotizador.PanelCotizadorModel.UDI = cabeceraResponse.Response.Cotizacion.Udi.Valor;
                    cotizador.PanelCotizadorModel.IdProducto = cabeceraResponse.Response.Cliente.Producto.ProductoId;
                    cotizador.PanelCotizadorModel.IdTipoVehiculo = Convert.ToInt32(cabeceraResponse.Response.Vehiculo.TipoUnidad.ValorId);
                    cotizador.PanelCotizadorModel.IdCondicionVehiculo = Convert.ToInt32(cabeceraResponse.Response.Vehiculo.Antiguedad.ValorId);
                    cotizador.PanelCotizadorModel.IdTipoServicioVehiculo = cabeceraResponse.Response.Vehiculo.Servicio.ValorId;
                    cotizador.PanelCotizadorModel.Submarca = cabeceraResponse.Response.Vehiculo.SubMarca.Valor;
                    cotizador.PanelCotizadorModel.TipoCarga = cabeceraResponse.Response.Vehiculo.Carga?.Valor.Substring(0, 1);
                    cotizador.PanelCotizadorModel = iCotizadorDataAccess.ConsultaPanelCotizacionFlex(cotizador);

                    IList<CoberturaModel> coberturasCotizadas = iCotizadorDataAccess.ConsultaCoberturasCotizadas(solicitudCotizacionModel);

                    foreach (var cobertura in cotizador.PanelCotizadorModel.Coberturas)
                    {
                        foreach (var coberturaCotizada in coberturasCotizadas)
                        {
                            if (cobertura.IdCobertura != coberturaCotizada.IdCobertura) continue;
                            cobertura.IsEspecial = coberturaCotizada.IsEspecial;
                            cobertura.IsFija = coberturaCotizada.IsFija;
                            cobertura.IsSeleccionada = coberturaCotizada.IsSeleccionada;

                            if (cobertura.NombreCobertura.IndexOf("Pasajero", StringComparison.Ordinal)>-1)
                            {
                                var auxFiltroSuma = coberturaCotizada.FiltroValorRangoSuma;
                                var auxFiltroDeducible = coberturaCotizada.FiltroValorRangoDeducible;

                                cobertura.FiltroValorRangoSuma = auxFiltroDeducible;
                                cobertura.FiltroValorRangoDeducible = auxFiltroSuma;
                            }
                            else
                            {
                                cobertura.FiltroValorRangoSuma = coberturaCotizada.FiltroValorRangoSuma;
                                cobertura.FiltroValorRangoDeducible = coberturaCotizada.FiltroValorRangoDeducible;
                            }
                        }
                    }
                }

                #endregion

                #endregion

                recargaDatosCotizacionModel.CabeceraCotizacionModel = cabeceraResponse.Response;
                recargaDatosCotizacionModel.PreCargaCotizador = cotizador;

                response.Done(recargaDatosCotizacionModel, String.Empty);
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
                response.Error(new DomainException(CodesCotizador.ERR_01_07, e));
            }
            return response;
        }

        public SingleResponse<IList<CoberturaModel>> ConsultaCoberturasCotizadas(SolicitudCotizacionModel solicitudCotizacion)
        {
            SingleResponse<IList<CoberturaModel>> response = new SingleResponse<IList<CoberturaModel>>();
            try
            {
                #region ModelValidations

                if (null == solicitudCotizacion)
                {
                    throw new DomainException(CodesCotizador.ERR_01_06);
                }
                IList<Validation> validations = ValidatorZero.Validate(solicitudCotizacion, new OptionsValidation()
                                                                                            {
                                                                                                ValidateIntCero = false,
                                                                                                ExcludeOptionals = true
                                                                                            });
                if (validations.Count>0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion ModelValidations

                #region manipulaciond de informacio <insert. delete>

                IList<CoberturaModel> coberturas = iCotizadorDataAccess.ConsultaCoberturasCotizadas(solicitudCotizacion);
                response.Done(coberturas, string.Empty);

                #endregion
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

        public SingleResponse<LimiteValorFacturaModel> ValidaLimiteValorFactura(LimiteValorFacturaModel limiteValorFactura)
        {
            SingleResponse<LimiteValorFacturaModel> response = new SingleResponse<LimiteValorFacturaModel>();
            try
            {
                #region ModelValidations

                if (null == limiteValorFactura)
                {
                    throw new DomainException(CodesCotizador.ERR_01_06);
                }
                IList<Validation> validations = ValidatorZero.Validate(limiteValorFactura, new OptionsValidation()
                                                                                           {
                                                                                               ValidateIntCero = false,
                                                                                               ExcludeOptionals = true
                                                                                           });
                if (validations.Count>0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion ModelValidations

                #region Validaciones de negocio

                string valorFactura = limiteValorFactura.ValorFactura.Replace("$", string.Empty).Replace(",", string.Empty);

                if (limiteValorFactura.LimiteValorFactura != 0)
                {
                    //valorFactura = valorFactura.Replace(',', '');
                    limiteValorFactura.IsOk = Convert.ToDecimal(valorFactura)<=limiteValorFactura.LimiteValorFactura;

                    if (!limiteValorFactura.IsOk)
                    {
                        string sCodeFinal = CodesCotizador.INF_01_08.Replace("{{Valor}}", limiteValorFactura.ValorFactura).Replace("{{Limite}}", $"{limiteValorFactura.LimiteValorFactura:C2}");
                        throw new DomainException(sCodeFinal);
                    }
                }

                limiteValorFactura.ValorFactura = valorFactura;

                #endregion Validaciones de negocio

                response.Done(limiteValorFactura, string.Empty);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesCotizador.ERR_01_10, e));
            }

            return response;
        }

        public SingleResponse<LimiteValorFacturaModel> ValidaLimiteAdaptaciones(CotizadorModel cotizador)
        {
            SingleResponse<LimiteValorFacturaModel> response = new SingleResponse<LimiteValorFacturaModel>();
            try
            {
                #region ModelValidations

                if (null == cotizador)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }

                IList<Validation> validations = ValidatorZero.Validate(cotizador, new OptionsValidation()
                                                                                  {
                                                                                      ValidateIntCero = false
                                                                                  });

                if (validations.Count>0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion ModelValidations

                #region consulta o manipulaciond de informacio <Select, Insert o Delete>

                IList<ValoresReglaModel> regla = iCotizadorDataAccess.ConsultaReglaNegocio(cotizador);

                #endregion consulta o manipulaciond de informacio <Select, Insert o Delete>

                #region Validaciones de negocio

                if (regla.Count>0)
                {
                    decimal porcentaje = cotizador.LimiteValorFactura.LimiteValorFactura * Convert.ToDecimal(regla[0].Valor);
                    string valorFactura = cotizador.LimiteValorFactura.ValorFactura.Replace("$", string.Empty).Replace(",", string.Empty);

                    cotizador.LimiteValorFactura.IsOk = (regla[0].ValorId == "1") ? Convert.ToDecimal(valorFactura)<=porcentaje : Convert.ToDecimal(valorFactura)<porcentaje;

                    if (!cotizador.LimiteValorFactura.IsOk)
                    {
                        string sCodeFinal = CodesCotizador.INF_01_09.Replace("{{Cobertura}}", regla[0].Producto);
                        throw new DomainException(sCodeFinal);
                    }
                }
                else
                {
                    throw new DomainException(CodesCotizador.ERR_01_12);
                }

                #endregion Validaciones de negocio

                response.Done(cotizador.LimiteValorFactura, string.Empty);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesCotizador.ERR_01_11, e));
            }

            return response;
        }
    }
}