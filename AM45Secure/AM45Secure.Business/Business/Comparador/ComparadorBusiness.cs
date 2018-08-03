using AM45Secure.Business.IBusiness.IComparador;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Recursos;
using AM45Secure.DataAccess.IDataAccess.IComparador;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Net.Mail;
using System.Web;
using AM45Secure.Commons.Constantes.Comunes;
using AM45Secure.Commons.Modelos.comunes;
using AM45Secure.Commons.Modelos.Comparador;
using AM45Secure.Commons.Modelos.Cotizador;
using AM45Secure.Commons.Utils;
using MMC.Library.Business;
using MMC.Library.Business.AutoMarsh.ProcesoEmision;
using Zero.Exceptions;
using Zero.Handlers.Response;
using Zero.Utils;
using Zero.Utils.Models;
using Microsoft.Reporting.WebForms;
using AM45Secure.DataAccess.IDataAccess.ICotizador;
using AM45Secure.DataAccess.IDataAccess.IEmitir;
using System.Linq;
using AM45Secure.Commons.Modelos.Tickets;

namespace AM45Secure.Business.Business.Comparador
{
    public class ComparadorBusiness : IComparadorBusiness
    {
        private readonly IComparadorDataAccess iComparadorDataAccess;
        private readonly ICotizadorDataAccess iCotizadorDataAccess;
        private readonly IEmitirDataAccess iEmitirDataAccess;
        private readonly string ZERO = "0";

        public ComparadorBusiness(IComparadorDataAccess iComparadorDataAccess, ICotizadorDataAccess iCotizadorDataAccess, IEmitirDataAccess iEmitirDataAccess)
        {
            this.iComparadorDataAccess = iComparadorDataAccess;
            this.iCotizadorDataAccess = iCotizadorDataAccess;
            this.iEmitirDataAccess = iEmitirDataAccess;
        }

        public SingleResponse<IList<SolicitudCotizacionModel>> ConsultarSolicitudCotizacion(CotizarModel cotizarModel)
        {
            SingleResponse<IList<SolicitudCotizacionModel>> response = new SingleResponse<IList<SolicitudCotizacionModel>>();
            try
            {
                #region ModelValidations

                if (null == cotizarModel)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }
                IList<Validation> validations = ValidatorZero.Validate(cotizarModel, new OptionsValidation()
                {
                    ValidateIntCero = false
                });
                if (validations.Count > 0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion ModelValidations

                /*validaciones de negocio */
                /* manipulaciond de informacio <insert. delete> */
                IList<SolicitudCotizacionModel> solicitudCotizacion = iComparadorDataAccess.ConsultarSolicitudCotizacion(cotizarModel.DatosCotizacionModel);

                response.Done(solicitudCotizacion, string.Empty);
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

        public SingleResponse<IList<FormasPagoProductoModel>> ConsultarFormasPagoProducto(SolicitudCotizacionModel solicitudCotizacionModel)
        {
            SingleResponse<IList<FormasPagoProductoModel>> response = new SingleResponse<IList<FormasPagoProductoModel>>();
            try
            {
                #region ModelValidations

                if (null == solicitudCotizacionModel)
                {
                    throw new DomainException(CodesBenchmark.ERR_02_00);
                }
                IList<Validation> validations = ValidatorZero.Validate(solicitudCotizacionModel, new OptionsValidation()
                {
                    ValidateIntCero = false
                });
                if (validations.Count > 0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion ModelValidations

                /*validaciones de negocio */
                /* manipulaciond de informacio <insert. delete> */
                IList<FormasPagoProductoModel> listFormasPagoProducto = iComparadorDataAccess.ConsultarFormasPagoProductos(solicitudCotizacionModel);
                response.Done(listFormasPagoProducto, string.Empty);
                response.ThrowIfNotOk();
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

        /*********************************************************************
        * @description: Metodo donde se obtiene las aseguradoras por cada producto,
        *  paquetes por aseguradora,primas por cotizacion, coberturas por paquete 
        * @params : IdSolicitud
        */

        public SingleResponse<IList<AseguradorasProductoModel>> ConsultarAseguradorasProducto(SolicitudCotizacionModel solicitudCotizacionModel, NeCotizacionModel neCotizacion)
        {
            SingleResponse<IList<AseguradorasProductoModel>> response = new SingleResponse<IList<AseguradorasProductoModel>>();
            try
            {
                #region ModelValidations

                if (null == solicitudCotizacionModel)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }
                IList<Validation> validations = ValidatorZero.Validate(solicitudCotizacionModel, new OptionsValidation()
                {
                    ValidateIntCero = false
                });
                if (validations.Count > 0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion ModelValidations

                /*validaciones de negocio */
                /* manipulaciond de informacio <insert. delete> */
                IList<AseguradorasProductoModel> listAseguradorasProducto = iComparadorDataAccess.ConsultarAseguradorasProducto(solicitudCotizacionModel, neCotizacion);
                response.Done(listAseguradorasProducto, string.Empty);
                response.ThrowIfNotOk();
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

        public SingleResponse<NeCotizacionModel> ConsultarNeCotizacion(CotizarModel cotizarModel)
        {
            SingleResponse<NeCotizacionModel> response = new SingleResponse<NeCotizacionModel>();
            try
            {
                #region ModelValidations

                if (null == cotizarModel)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }
                IList<Validation> validations = ValidatorZero.Validate(cotizarModel, new OptionsValidation()
                {
                    ValidateIntCero = false
                });
                if (validations.Count > 0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion ModelValidations

                /*validaciones de negocio */
                /* manipulaciond de informacio <insert. delete> */
                NeCotizacionModel solicitudCotizacion = iComparadorDataAccess.ConsultarNeCotizacion(cotizarModel);
                response.Done(solicitudCotizacion, string.Empty);
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

        public SingleResponse<CargaInicialModel> CargaInicial(CotizarModel cotizarModel)
        {
            SingleResponse<CargaInicialModel> response = new SingleResponse<CargaInicialModel>();
            try
            {
                #region ModelValidations

                if (null == cotizarModel)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }
                IList<Validation> validations = ValidatorZero.Validate(cotizarModel, new OptionsValidation()
                {
                    ValidateIntCero = false
                });
                if (validations.Count > 0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion ModelValidations

                CargaInicialModel comparadorModel = new CargaInicialModel();
                SingleResponse<IList<SolicitudCotizacionModel>> solicitudCotizacionSr = ConsultarSolicitudCotizacion(cotizarModel);
                if (solicitudCotizacionSr.Response.Count == 0)
                {
                    throw new DomainException(CodesBenchmark.ERR_02_00);
                }
                SingleResponse<IList<FormasPagoProductoModel>> formasPagoProductoSr = ConsultarFormasPagoProducto(solicitudCotizacionSr.Response[0]);
                comparadorModel.NotasImportantes = iComparadorDataAccess.ConsultarNotasImportantes(solicitudCotizacionSr.Response[0]);
                solicitudCotizacionSr.ThrowIfNotOk();
                formasPagoProductoSr.ThrowIfNotOk();

                comparadorModel.SolicitudCotizacion = solicitudCotizacionSr.Response[0];
                if (comparadorModel.SolicitudCotizacion == null)
                    response.ThrowIfNotOk();
                comparadorModel.FormasPagoProducto = formasPagoProductoSr.Response;
                response.Done(comparadorModel, string.Empty);
                response.ThrowIfNotOk();
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

        public SingleResponse<ComparadorModel> CargaComparador(CotizarModel cotizarModel)
        {
            SingleResponse<ComparadorModel> response = new SingleResponse<ComparadorModel>();
            ComparadorModel comparadorModel = new ComparadorModel();

            try
            {
                #region ModelValidations

                if (null == cotizarModel)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }
                IList<Validation> validations = ValidatorZero.Validate(cotizarModel, new OptionsValidation()
                {
                    ValidateIntCero = false
                });
                if (validations.Count > 0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion ModelValidations

                SingleResponse<IList<SolicitudCotizacionModel>> solicitudCotizacionSr = ConsultarSolicitudCotizacion(cotizarModel);
                solicitudCotizacionSr.ThrowIfNotOk();

                if (null == solicitudCotizacionSr.Response || solicitudCotizacionSr.Response.Count == 0)
                {
                    throw new DomainException(CodesBenchmark.ERR_02_00);
                }

                SolicitudCotizacionModel solicitudCotizacion = solicitudCotizacionSr.Response[0];
                SingleResponse<NeCotizacionModel> neCotizacionSr = ConsultarNeCotizacion(cotizarModel);
                neCotizacionSr.ThrowIfNotOk();

                if (neCotizacionSr.Response == null)
                {
                    List<RutaGenerarArchivo> objRutas = new List<RutaGenerarArchivo>();
                    RutaGenerarArchivo objRuta = new RutaGenerarArchivo
                    {
                        NombreAseguradora = "ABBA",
                        RutaArchivo = ConfigurationManager.AppSettings["RutaABA"],
                        EntradaArchivo = ConfigurationManager.AppSettings["ArchivoABAEntrada"],
                        NombreZip = ConfigurationManager.AppSettings["ArchivoZipABA"],
                        SalidaArchivo = ConfigurationManager.AppSettings["ArchivoABASalida"]
                    };
                    objRutas.Add(objRuta);
                    objRuta = new RutaGenerarArchivo
                    {
                        NombreAseguradora = "QLT",
                        RutaArchivo = ConfigurationManager.AppSettings["RutaQLT"],
                        EntradaArchivo = ConfigurationManager.AppSettings["ArchivoQLT"],
                        NombreZip = ConfigurationManager.AppSettings["ArchivoZipQLT"],
                        SalidaArchivo = ConfigurationManager.AppSettings["ArchivoQLTSalida"]
                    };
                    objRutas.Add(objRuta);

                    CotizacionMultiple cotizacionMultiple = new CotizacionMultiple(DataAccess.DataAccess.Generic.DataAccess.GetConnectionString());
                    cotizacionMultiple.GrabarCotizacionCabecera(
                                                                solicitudCotizacion.MonedaId,
                                                                cotizarModel.DatosCotizacionModel.FormaPagoId.ToString(),
                                                                solicitudCotizacion.InicioVigencia.ToString(),
                                                                solicitudCotizacion.FinVigencia.ToString(),
                                                                solicitudCotizacion.Marca,
                                                                solicitudCotizacion.SubMarca,
                                                                solicitudCotizacion.Descripcion,
                                                                solicitudCotizacion.Modelo,
                                                                solicitudCotizacion.EstadoCirculacion,
                                                                solicitudCotizacion.ValorFactura.ToString().Replace(".00", ""),
                                                                solicitudCotizacion.AgenciaId.ToString(),
                                                                solicitudCotizacion.ClienteId.ToString(),
                                                                solicitudCotizacion.UsuarioId.ToString(),
                                                                solicitudCotizacion.ClaveVehiculoMarsh,
                                                                string.Empty,
                                                                solicitudCotizacion.Ocupantes.ToString(),
                                                                string.Empty,
                                                                ZERO,
                                                                solicitudCotizacion.Plazo.ToString(),
                                                                solicitudCotizacion.UsoId.ToString(),
                                                                solicitudCotizacion.ServicioId.ToString(),
                                                                solicitudCotizacion.IdTipoVehiculo.ToString(),
                                                                ZERO,
                                                                ZERO,
                                                                solicitudCotizacion.UsoId.ToString(),
                                                                ZERO,
                                                                ZERO,
                                                                ZERO,
                                                                solicitudCotizacion.LoJack.ToString(),
                                                                ZERO,
                                                                ZERO,
                                                                ZERO,
                                                                solicitudCotizacion.SumaEEspecial.ToString(),
                                                                solicitudCotizacion.SumaAdaptaciones.ToString(),
                                                                solicitudCotizacion.DescripcionEe,
                                                                solicitudCotizacion.DescripcionAdaptaciones,
                                                                solicitudCotizacion.SolicitudId,
                                                                ZERO);

                    neCotizacionSr = ConsultarNeCotizacion(cotizarModel);
                    neCotizacionSr.ThrowIfNotOk();
                    solicitudCotizacion.ExisteDm = iCotizadorDataAccess.ExisteDaniosMaterialesCotizacionFlex(solicitudCotizacion.SolicitudId);

                    iComparadorDataAccess.ActualizaTipoArrendamientoCargaRemolquesCoptizacion(solicitudCotizacion, neCotizacionSr.Response.CotizacionId);

                    SmtpClient smtpClient = SendMailUtil.GetInstance().GetSmtpClient();
                    CotizacionIndividualQLT cotizacionIndividualQlt = new CotizacionIndividualQLT(ConfigurationManager.ConnectionStrings["conexion"].ToString(), ConfigurationManager.AppSettings["Seguridad"], objRutas, smtpClient);
                    DataSet solicitudCotizacionDataSet = new DataSet();
                    solicitudCotizacionDataSet.Tables.Add(solicitudCotizacion.ToDataTable());
                    EntCodigoPostal entCodigoPostal = null;

                    if (!string.IsNullOrEmpty(solicitudCotizacion.CodigoPostal))
                    {
                        entCodigoPostal = new EntCodigoPostal
                        {
                            Estatus = 1,
                            Pais = solicitudCotizacion.Pais,
                            Estado = solicitudCotizacion.Estado,
                            IdEstado = solicitudCotizacion.EstadoId.ToString(),
                            Delgacion = solicitudCotizacion.Delegacion,
                            Colonia = solicitudCotizacion.Colonia,
                            IdMunicipio = solicitudCotizacion.IdMunicipio.ToString(),
                            CodigoPostal = solicitudCotizacion.CodigoPostal,
                            Asentamiento = solicitudCotizacion.Asentamiento.ToString(),
                            AgenciaId = solicitudCotizacion.AgenciaId,
                            SolicitudCotizacionId = solicitudCotizacion.SolicitudId
                        };
                    }

                    //cotizacionIndividualQlt.CotizacionAseguradora(neCotizacionSr.Response.CotizacionId.ToString(), solicitudCotizacionDataSet, entCodigoPostal, "hdoc0iu1nunwpe55avo2i255", cotizarModel.DatosCotizacionModel.FormaPagoId.ToString(), cotizarModel.DatosCotizacionModel.SolicitudId.ToString());
                    DataSet dtCotizacion = cotizacionIndividualQlt.CotizacionAseguradora(neCotizacionSr.Response.CotizacionId.ToString(), solicitudCotizacionDataSet, entCodigoPostal, "hdoc0iu1nunwpe55avo2i255", cotizarModel.DatosCotizacionModel.FormaPagoId.ToString(), cotizarModel.DatosCotizacionModel.SolicitudId.ToString());

                    bool error = false;
                    if (dtCotizacion != null && dtCotizacion.Tables[0].Rows.Count > 2)
                    {
                        foreach (DataRow row in dtCotizacion.Tables[0].Rows)
                        {
                            Logger.Error(row["Errores"] + " :: " + row["MessageError"]);
                            if ((bool)row["IsErrorGeneral"])
                            {
                                error = true;
                                break;
                            }
                        }
                    }

                    if (error)
                    {
                        throw new DomainException(CodesBenchmark.ERR_02_04);
                    }

                    //if (dtCotizacion != null && dtCotizacion.Tables[0].Rows.Count>1)
                    //{
                    //    foreach (DataRow row in dtCotizacion.Tables[0].Rows)
                    //    {
                    //        Logger.Error(row["Errores"] + " :: " + row["MessageError"]);

                    //        if (row.ItemArray[0].Equals(true) || row.ItemArray[1].Equals(true))
                    //        {
                    //            ErroresModel error = new ErroresModel();
                    //            error.IsError = (bool) row.ItemArray[0];
                    //            error.IsErrorGeneral = (bool) row.ItemArray[1];
                    //            error.Error = row.ItemArray[2].ToString();
                    //            error.MessageError = row.ItemArray[3].ToString();
                    //            comparadorModel.Errores.Add(error);
                    //        }
                    //    }
                    //}
                }

                SingleResponse<IList<AseguradorasProductoModel>> listAseguradoraSr = ConsultarAseguradorasProducto(solicitudCotizacion, neCotizacionSr.Response);
                listAseguradoraSr.ThrowIfNotOk();

                comparadorModel.ListNeCotizacion = neCotizacionSr.Response;
                comparadorModel.AseguradorasProducto = listAseguradoraSr.Response;
                response.Done(comparadorModel, string.Empty);
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

        /*********************************************************************
                * @description: Armar la cabecera
                * @params : cotizarModel, donde viene como parametro el IdSolicitud
                */

        public SingleResponse<CabeceraCotizacionModel> ConsultarCabeceraCotizacion(CotizarModel cotizarModel)
        {
            SingleResponse<CabeceraCotizacionModel> response = new SingleResponse<CabeceraCotizacionModel>();
            try
            {
                #region ModelValidations

                if (null == cotizarModel)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }
                IList<Validation> validations = ValidatorZero.Validate(cotizarModel, new OptionsValidation()
                {
                    ValidateIntCero = false
                });
                if (validations.Count > 0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion ModelValidations

                SingleResponse<IList<SolicitudCotizacionModel>> solicitudCotizacionSr = ConsultarSolicitudCotizacion(cotizarModel);
                if (solicitudCotizacionSr.Response.Count == 0)
                {
                    throw new DomainException(CodesBenchmark.ERR_02_00);
                }

                SolicitudCotizacionModel solicitudCotizacionModel = solicitudCotizacionSr.Response[0];
                solicitudCotizacionModel.Tkn = cotizarModel.Tkn;

                #region Cabecera

                CabeceraCotizacionModel cabecera = new CabeceraCotizacionModel
                {
                    Panel = new PanelCotizadorModel()
                };

                if (solicitudCotizacionModel != null)
                {
                    //Cliente
                    cabecera.Cliente = ArmaClienteModel(solicitudCotizacionModel);
                    //Vehiculo
                    cabecera.Vehiculo = ArmaVehiculoModel(solicitudCotizacionModel);
                    //Cotizacion
                    cabecera.Cotizacion = ArmaCotizacionModel(solicitudCotizacionModel);

                    cabecera.Panel.Coberturas = iComparadorDataAccess.ConsultaCoberturasEspeciales(solicitudCotizacionModel);
                }

                #endregion

                response.Done(cabecera, string.Empty);
                response.ThrowIfNotOk();
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

        public DatosClienteModel ArmaClienteModel(SolicitudCotizacionModel solicitudCotizacionModel)
        {
            DatosClienteModel datosCliente = new DatosClienteModel();
            SingleResponse<CotizanteModel> cotizanteSr = ConsultarDatosCotizante(solicitudCotizacionModel);
            SingleResponse<ClientesModel> clienteSr = ConsultarDatosCliente(solicitudCotizacionModel);
            SingleResponse<ProductoModel> productoSr = ConsultarDatosProducto(solicitudCotizacionModel);
            SingleResponse<AgenciasModel> agencia = ConsultaAgencia(solicitudCotizacionModel);
            cotizanteSr.ThrowIfNotOk();
            clienteSr.ThrowIfNotOk();
            productoSr.ThrowIfNotOk();
            datosCliente.Cotizante = cotizanteSr.Response;
            datosCliente.Cliente = clienteSr.Response;
            datosCliente.ProductoFlex = productoSr.Response.Flexible;
            datosCliente.Producto = productoSr.Response;
            datosCliente.Agencia = agencia.Response;

            if (solicitudCotizacionModel.TipoArrendamiento > 0)
            {
                datosCliente.TipoArrendamiento = iComparadorDataAccess.ConsultaElementosPorIdInterno(solicitudCotizacionModel.TipoArrendamiento, ConstElementos.Arrendamiento)[0];
                datosCliente.TipoArrendamientoRegla = new ValoresReglaModel
                {
                    ValorId = datosCliente.TipoArrendamiento.IdInterno.ToString(),
                    Valor = datosCliente.TipoArrendamiento.Nombre
                };
            }

            return datosCliente;
        }

        public DatosVehiculoModel ArmaVehiculoModel(SolicitudCotizacionModel solicitudCotizacionModel)
        {
            DatosVehiculoModel datosVehiculo = new DatosVehiculoModel
            {
                Remolque = new ElementoModel(),
                Pasajero = new PasajerosModel(),
                Servicio = new ClaveValorModel(),
                LoJackModel = new ElementoModel(),
                Antiguedad = new ClaveValorModel(),
                ElAntiguedad = new ElementoModel(),
                ServicioInterno = new ElementoModel(),
            };
            VersionesModel version = new VersionesModel
            {
                Descripcion = solicitudCotizacionModel.Descripcion,
                ClaveInterna = solicitudCotizacionModel.ClaveVehiculoMarsh,
                ClaveMarsh = solicitudCotizacionModel.ClaveVehiculoMarsh
            };

            switch (solicitudCotizacionModel.TipoCarga)
            {
                case "A":
                    solicitudCotizacionModel.TipoCarga = "A - NO ES PELIGROSA";
                    break;
                case "B":
                    solicitudCotizacionModel.TipoCarga = "B - PELIGROSA";
                    break;
                case "C":
                    solicitudCotizacionModel.TipoCarga = "C - MUY PELIGROSA";
                    break;
            }

            //datosVehiculo = ConsultaReglaCargas(solicitudCotizacionModel, datosVehiculo);
            datosVehiculo.ServicioInterno = iEmitirDataAccess.ConsultaElementosPorElementoId(solicitudCotizacionModel.ServicioId)[0];
            datosVehiculo.Servicio = ArmarClaveValorModel(datosVehiculo.ServicioInterno.Comodin, solicitudCotizacionModel.Servicio.ToUpper());
            datosVehiculo.Servicio.ServicioId = solicitudCotizacionModel.ServicioId.ToString();
            datosVehiculo.ElAntiguedad = iEmitirDataAccess.ConsultaElementosPorElementoId(solicitudCotizacionModel.TipoVehiculoId)[0];
            datosVehiculo.Antiguedad = ArmarClaveValorModel(solicitudCotizacionModel.TipoVehiculoId.ToString(), datosVehiculo.ElAntiguedad.Nombre);
            datosVehiculo.Version = version;
            datosVehiculo.Modelo = ArmarClaveValorModel("0", solicitudCotizacionModel.Modelo);
            datosVehiculo.SubMarca = ArmarClaveValorModel("0", solicitudCotizacionModel.SubMarca);
            datosVehiculo.Valor = solicitudCotizacionModel.ValorFactura.ToString();
            datosVehiculo.TipoUnidad = ArmarClaveValorModel(solicitudCotizacionModel.IdTipoVehiculo.ToString(), solicitudCotizacionModel.TipoVehiculo.ToUpper());
            datosVehiculo.Armadora = ArmarClaveValorModel("0", solicitudCotizacionModel.Marca);
            datosVehiculo.RemolqueInt = solicitudCotizacionModel.Remolques;
            datosVehiculo.Remolque.ElementoId = solicitudCotizacionModel.Remolques;
            datosVehiculo.Remolque.Nombre = solicitudCotizacionModel.Remolques.ToString();
            datosVehiculo.Carga = solicitudCotizacionModel.TipoCarga != null ? ArmarClaveValorModel("0", solicitudCotizacionModel.TipoCarga) : null;
            datosVehiculo.ShowCargas = datosVehiculo.Carga != null;
            datosVehiculo.ShowRemolques = datosVehiculo.RemolqueInt != 0;
            datosVehiculo.Pasajero.Pasajeros = solicitudCotizacionModel.Ocupantes;
            datosVehiculo.LoJackModel = (solicitudCotizacionModel.LoJack == 0) ? null : iEmitirDataAccess.ConsultaElementosPorElementoId(solicitudCotizacionModel.LoJack)[0];

            return datosVehiculo;
        }

        public DatosCotizacionModel ArmaCotizacionModel(SolicitudCotizacionModel solicitudCotizacionModel)
        {
            CodigoPostalModel codigoPostal = new CodigoPostalModel();
            DatosCotizacionModel datosCotizacion = new DatosCotizacionModel
            {
                Udi = ArmarClaveValorModel("0", solicitudCotizacionModel.PorcentajeUdi),
                Plazo = ArmarClaveValorModel("0", solicitudCotizacionModel.Plazo.ToString()),
                Estado = new RegionCodigoPostalModel(),
                CP = new RegionCodigoPostalModel()
            };

            datosCotizacion.Estado.Estado = solicitudCotizacionModel.EstadoCirculacion == "" ? null : solicitudCotizacionModel.EstadoCirculacion;
            datosCotizacion.Estado.EstadoId = solicitudCotizacionModel.EstadoCirculacionId;

            if (!string.IsNullOrEmpty(solicitudCotizacionModel.CodigoPostal)
                && !string.IsNullOrEmpty(solicitudCotizacionModel.Colonia))
            {
                codigoPostal.CodigoPostal = solicitudCotizacionModel.CodigoPostal;
                codigoPostal.Colonia = solicitudCotizacionModel.Colonia;
                datosCotizacion.CP = iCotizadorDataAccess.ConsultarCodigoPostal(codigoPostal)[0];
            }
            datosCotizacion.InicioVigencia = solicitudCotizacionModel.InicioVigencia;
            datosCotizacion.FinVigencia = solicitudCotizacionModel.FinVigencia;
            return datosCotizacion;
        }

        public ClaveValorModel ArmarClaveValorModel(string valorId, string valor)
        {
            ClaveValorModel claveValorModel = new ClaveValorModel
            {
                Valor = valor,
                ValorId = valorId
            };
            return claveValorModel;
        }

        public SingleResponse<CotizanteModel> ConsultarDatosCotizante(SolicitudCotizacionModel solicitudCotizacionModel)
        {
            SingleResponse<CotizanteModel> response = new SingleResponse<CotizanteModel>();
            try
            {
                #region ModelValidations

                if (null == solicitudCotizacionModel)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }
                IList<Validation> validations = ValidatorZero.Validate(solicitudCotizacionModel, new OptionsValidation()
                {
                    ValidateIntCero = false
                });
                if (validations.Count > 0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion ModelValidations

                /*validaciones de negocio */
                /* manipulaciond de informacio <insert. delete> */
                CotizanteModel datosCotizante = iComparadorDataAccess.ConsultarDatosCotizante(solicitudCotizacionModel);
                response.Done(datosCotizante, string.Empty);
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

        public SingleResponse<ClientesModel> ConsultarDatosCliente(SolicitudCotizacionModel solicitudCotizacionModel)
        {
            SingleResponse<ClientesModel> response = new SingleResponse<ClientesModel>();
            try
            {
                #region ModelValidations

                if (null == solicitudCotizacionModel)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }
                IList<Validation> validations = ValidatorZero.Validate(solicitudCotizacionModel, new OptionsValidation()
                {
                    ValidateIntCero = false
                });
                if (validations.Count > 0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion ModelValidations

                /*validaciones de negocio */
                /* manipulaciond de informacio <insert. delete> */
                ClientesModel datosCliente = iComparadorDataAccess.ConsultarDatosCliente(solicitudCotizacionModel);
                response.Done(datosCliente, string.Empty);
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

        public SingleResponse<ProductoModel> ConsultarDatosProducto(SolicitudCotizacionModel solicitudCotizacionModel)
        {
            SingleResponse<ProductoModel> response = new SingleResponse<ProductoModel>();
            try
            {
                #region ModelValidations

                if (null == solicitudCotizacionModel)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }
                IList<Validation> validations = ValidatorZero.Validate(solicitudCotizacionModel, new OptionsValidation()
                {
                    ValidateIntCero = false
                });
                if (validations.Count > 0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion ModelValidations

                /*validaciones de negocio */
                /* manipulaciond de informacio <insert. delete> */
                ProductoModel datosProducto = iComparadorDataAccess.ConsultarDatosProducto(solicitudCotizacionModel);
                response.Done(datosProducto, string.Empty);
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



        public byte[] ConsultaReporteCotizacion(RepCotizacionModel repCotizacionModel, bool esEmail = false, List<string> destinatarios = null)
        {



            byte[] bytes = null;
            try
            {
                // Variables
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                // Setup the report viewer object and get the array of bytes
                ReportViewer viewer = new ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                AMQADataSet.DatosCotizacionDataTable datosCotizacion = new AMQADataSet.DatosCotizacionDataTable();
                //obtener los datos de la cotizacion para llenar los dataSet
                List<ReporteCotizacionModel> reporte = ArmaCuerpoReporte(repCotizacionModel);
                List<ReporteCoberturasModel> listaCoberturas = iComparadorDataAccess.ConsultaCoberturasReporteNuevo(repCotizacionModel);
                if (reporte.Count > 0)
                {
                    reporte[0].Especial = listaCoberturas.Contains(new ReporteCoberturasModel
                    {
                        Nombre = "Equipo Especial"
                    });
                    reporte[0].Adaptaciones = listaCoberturas.Contains(new ReporteCoberturasModel
                    {
                        Nombre = "Adaptaciones"
                    });
                    ReportDataSource reporteCot = new ReportDataSource("DatosCotizacion", reporte);
                    ReportDataSource reporteCob = new ReportDataSource("Coberturas", listaCoberturas);
                    viewer.LocalReport.EnableExternalImages = true;
                    /*parametros*/
                    ReportParameter logo = new ReportParameter("logo", ConfigurationManager.AppSettings["ImgLogo"], true);
                    ReportParameter aseguradora = new ReportParameter("aseguradora", ConfigurationManager.AppSettings["ImgReport"] + reporte[0].AseguradoraId + ".png", true);
                    ReportParameter cliente = new ReportParameter("cliente", ConfigurationManager.AppSettings["ImgReport"] + reporte[0].ClienteId + ".png", true);
                    viewer.LocalReport.ReportPath = ConfigurationManager.AppSettings["Reporte"];
                    viewer.LocalReport.SetParameters(new ReportParameter[]
                                                     {
                                                         logo,
                                                         aseguradora,
                                                         cliente
                                                     });
                    /*DataSource*/
                    viewer.LocalReport.DataSources.Add(reporteCot);
                    viewer.LocalReport.DataSources.Add(reporteCob);
                    bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                    if (esEmail)
                    {
                        var DatosUsuarioCotizante = new PersonaEmail();
                        DatosUsuarioCotizante.Tkn = repCotizacionModel.Tkn;
                        DatosUsuarioCotizante.PersonaId = DatosUsuarioCotizante.GetIdUsuarioSesion();
                        obtenerDatosEmail(DatosUsuarioCotizante);

                        var cuerpoCorreo = string.Empty;
                        var asuntoCorreo = string.Empty;
                        //Trae informacion de cliente y producto de la solicitud
                        var cpXs = iComparadorDataAccess.obtenerClienteProductoXSolictud(repCotizacionModel.SoliciudId);
                        //Obtiene la estructura del corro, de la regla de negocio
                        var cc = iComparadorDataAccess.obtenerEstructuraCorreo(cpXs.IdCliente,cpXs.IdProducto);
                        //SI tiene configurada un regla de negocio con correo
                        if (cc != null)
                        {
                            asuntoCorreo = cc.Asunto;
                            cuerpoCorreo = cc.Mensaje + "\n" + cc.Despedida;
                        }
                        // si no hay una regla de negocio, toma los valores por default
                        else
                        {
                            asuntoCorreo = Recursos.RecursosBusiness.AsuntoCorreo;
                            cuerpoCorreo = Recursos.RecursosBusiness.CuerpoCorreo;
                        }


                        destinatarios.Add(DatosUsuarioCotizante.Correo);
                        MailModel mailModel = new MailModel
                        {
                            
                            Body = cuerpoCorreo,
                            Subject = asuntoCorreo
                        };
                        List<string> correos = destinatarios;

                        mailModel.MailsTo = correos;

                        SendMailUtil.GetInstance().SendMailCotizacionPDF(mailModel, repCotizacionModel.Numero.ToString(), bytes);
                        destinatarios.Remove(DatosUsuarioCotizante.Correo);
                        BitacoraEncioCorreo(repCotizacionModel.CotizacionId, repCotizacionModel.SoliciudId, repCotizacionModel.Numero, destinatarios, DatosUsuarioCotizante.Nombre, DatosUsuarioCotizante.Correo);
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error al generar el reporte de Cotizacion o Envio de correo por Email.", ex);
                throw new Exception("Error al generar el reporte de Cotizacion o Envio de correo por Email");
            }

            return bytes;
        }




        public void obtenerDatosEmail(PersonaEmail DatosUsuarioCotizante)
        {
            var pm = iComparadorDataAccess.obtenerDatosPersonaEmail(DatosUsuarioCotizante);
        }
        public bool BitacoraEncioCorreo(int cotizacionId, int solicitudId, int numero, List<string> destinatarios, string cotizante, string correoCotizante)
        {
            try
            {

                var resultaBitacoraEmail = iComparadorDataAccess.BitacoraEncioCorreo(cotizacionId, solicitudId, numero, destinatarios, cotizante, correoCotizante);

            }
            catch (Exception)
            {


                return false;
            }

            return true;
        }


        public SingleResponse<AgenciasModel> ConsultaAgencia(SolicitudCotizacionModel solicitudCotizacionModel)
        {
            SingleResponse<AgenciasModel> response = new SingleResponse<AgenciasModel>();
            try
            {
                #region ModelValidations

                if (null == solicitudCotizacionModel)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }
                IList<Validation> validations = ValidatorZero.Validate(solicitudCotizacionModel, new OptionsValidation()
                {
                    ValidateIntCero = false
                });
                if (validations.Count > 0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion ModelValidations

                /*validaciones de negocio */
                /* manipulaciond de informacio <insert. delete> */
                AgenciasModel datosAgencia = iComparadorDataAccess.ConsultarDatosAgencia(solicitudCotizacionModel);
                response.Done(datosAgencia, string.Empty);
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

        //public DatosVehiculoModel ConsultaReglaCargas(SolicitudCotizacionModel solicitudCotizacionModel, DatosVehiculoModel datosVehiculo)
        //{
        //    CotizadorModel cotizador = new CotizadorModel();
        //    SolicitudReglaNegocioModel solicitudRegla = new SolicitudReglaNegocioModel();
        //    solicitudRegla.IdRegla = 6596;
        //    solicitudRegla.IdProducto = Convert.ToString(solicitudCotizacionModel.ProductoId);
        //    cotizador.SolicitudRegla = solicitudRegla;
        //    datosVehiculo.ShowCargas = false;
        //    datosVehiculo.ShowRemolques = false;
        //    cotizador.Tkn = solicitudCotizacionModel.Tkn;
        //    IList<ValoresReglaModel> reglaList = new List<ValoresReglaModel>();
        //    reglaList = iCotizadorDataAccess.ConsultaReglaNegocio(cotizador);
        //    if (reglaList.Count>0)
        //    {
        //        datosVehiculo.ShowCargas = (reglaList[0].ValorId == "1") ? true : false;
        //        datosVehiculo.ShowRemolques = Convert.ToBoolean(reglaList[0].HabilitaRemolques);
        //    }

        //    return datosVehiculo;
        //}

        private List<ReporteCotizacionModel> ArmaCuerpoReporte(RepCotizacionModel repCotizacionModel)
        {
            CotizarModel modeloCabecera = new CotizarModel();
            DatosSolicitudModel datosCotizacion = new DatosSolicitudModel();
            CabeceraCotizacionModel cabecera = new CabeceraCotizacionModel();
            SingleResponse<CabeceraCotizacionModel> cabeceraSingle = new SingleResponse<CabeceraCotizacionModel>();
            ReporteCotizacionModel reporte = new ReporteCotizacionModel();
            List<ReporteCotizacionModel> reporteQry = new List<ReporteCotizacionModel>();
            List<ReporteCotizacionModel> reporteFin = new List<ReporteCotizacionModel>();
            datosCotizacion.SolicitudId = repCotizacionModel.SoliciudId;
            modeloCabecera.DatosCotizacionModel = datosCotizacion;
            modeloCabecera.Tkn = repCotizacionModel.Tkn;
            cabeceraSingle = ConsultarCabeceraCotizacion(modeloCabecera);
            if (cabeceraSingle != null)
            {
                reporteQry = iComparadorDataAccess.ConsultarCotizacionRep(repCotizacionModel);
                if (reporteQry.Count > 0)
                {
                    cabecera = cabeceraSingle.Response;
                    reporte.SolicitudId = cabecera.IdSolicitud;
                    reporte.ClienteId = cabecera.Cliente.Cliente.ClienteId;
                    reporte.CotizacionId = repCotizacionModel.Numero;
                    reporte.ClaveVehiculoMarsh = reporteQry[0].ClaveVehiculoMarsh;
                    reporte.AseguradoraId = reporteQry[0].AseguradoraId;
                    reporte.Remolques = cabecera.Vehiculo.RemolqueInt;
                    reporte.Carga = cabecera.Vehiculo.Carga?.Valor;
                    reporte.ShowCargas = cabecera.Vehiculo.ShowCargas;
                    reporte.ShowRemolques = cabecera.Vehiculo.ShowRemolques;
                    // reporte.ServicioId = cabecera.Vehiculo.Servicio.ServicioId;
                    reporte.ValorFactura = $"{Convert.ToDecimal(cabecera.Vehiculo.Valor):C2}";
                    // reporte.UsoId = remove
                    reporte.Modelo = cabecera.Vehiculo.Modelo.Valor;
                    reporte.Marca = reporteQry[0].Marca;
                    reporte.Submarca = cabecera.Vehiculo.SubMarca.Valor;
                    reporte.Descripcion = cabecera.Vehiculo.Version.Descripcion;
                    reporte.EstadoCirculacion = reporteQry[0].EstadoCirculacion;
                    reporte.MondaId = reporteQry[0].EstadoCirculacion;
                    reporte.LoJack = (cabecera.Vehiculo.LoJack == 34) ? "NO" : "SI";
                    //reporte.UsuarioId remove
                    //reporte.CotizacionId//
                    reporte.ClaveVehiculoMarsh = reporteQry[0].ClaveVehiculoMarsh;
                    reporte.TipoVehiculo = cabecera.Vehiculo.TipoUnidad.Valor.ToUpper();
                    reporte.Cliente = cabecera.Cliente.Cliente.Cliente;
                    reporte.FechaRegistro = Convert.ToDateTime(reporteQry[0].FechaRegistro).ToString("dd/MM/yyyy HH:mm:ss");
                    reporte.InicioVigencia = cabecera.Cotizacion.InicioVigencia.ToString("dd/MM/yyyy");
                    reporte.FinVigencia = cabecera.Cotizacion.FinVigencia.ToString("dd/MM/yyyy");
                    //reporte.FechaRegistroString = reporteQry[0].FechaRegistro.ToString("dd/MM/yyyy HH:mm:ss");
                    //reporte.InicioVigenciaString = cabecera.Cotizacion.InicioVigencia.ToString("d");
                    //reporte.FinVigenciaString = cabecera.Cotizacion.FinVigencia.ToString("d");
                    reporte.Cotizante = (cabecera.Cliente.Cotizante != null) ? cabecera.Cliente.Cotizante.Nombre + ' ' + cabecera.Cliente.Cotizante.ApellidoP + ' ' + cabecera.Cliente.Cotizante.ApellidoM : "";
                    reporte.Nombre = reporteQry[0].Nombre;
                    reporte.PrimaNeta = reporteQry[0].PrimaNeta;
                    reporte.Recargo = reporteQry[0].Recargo;
                    reporte.Derechos = reporteQry[0].Derechos;
                    reporte.Iva = reporteQry[0].Iva;
                    reporte.PrimaTotal = reporteQry[0].PrimaTotal;
                    reporte.RecargoFraccionado = reporteQry[0].RecargoFraccionado;
                    reporte.Especial = reporteQry[0].Especial;
                    reporte.Adaptaciones = reporteQry[0].Adaptaciones;
                    reporte.ClaveAseg = reporteQry[0].ClaveAseg;
                    reporte.Subtotal = reporteQry[0].Subtotal;
                    reporte.PaqueteN = reporteQry[0].PaqueteN;
                    reporte.PaqueteId = reporteQry[0].PaqueteId;
                    reporte.Nombre = reporteQry[0].Nombre; //Forma de pago
                    reporte.Pasajeros = cabecera.Vehiculo.Pasajero.Pasajeros;
                    reporteFin.Add(reporte);
                }
                else
                {
                    throw new DalException(CodesBenchmark.ERR_02_07);
                }
            }
            else
            {
                throw new DalException(CodesBenchmark.ERR_02_06);
            }

            return reporteFin;
        }

        public byte[] descargaReporteEmail(int opcionFiltro)
        {
            byte[] bytes = null;
            try
            {


                // Variables
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                // Setup the report viewer object and get the array of bytes
                ReportViewer viewer = new ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                //obtener los datos de la cotizacion para llenar los dataSet
                IList<ReporteCorresExcel> reporte = iComparadorDataAccess.ConsultarDatosReporteCorreo(opcionFiltro);
                ReportDataSource reporteExcel = new ReportDataSource("DataSetReporteCorreo", reporte);
                viewer.LocalReport.DataSources.Add(reporteExcel);
                viewer.LocalReport.ReportPath = ConfigurationManager.AppSettings["ReporteEmail"];
                bytes = viewer.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            }
            catch (Exception ex)
            {
                Logger.Error("Error al imprimir el reporte de tickets... ", ex);
                bytes = null;
            }

            return bytes;
        }




    }
}