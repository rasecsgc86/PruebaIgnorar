using System;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AM45Secure.Business.IBusiness.IEmitir;
using AM45Secure.Commons.Modelos.Emitir;
using AM45Secure.Commons.Recursos;
using AM45Secure.DataAccess.IDataAccess.IEmitir;
using Zero.Exceptions;
using Zero.Handlers.Response;
using AM45Secure.Commons.Constantes.Comunes;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Cotizador;
using AM45Secure.Commons.Utils;
using AM45Secure.DataAccess.IDataAccess.IComparador;
using AM45Secure.DataAccess.IDataAccess.ICotizador;
using MMC.Library.Business.EmitirCotizacion;
using Zero.Utils;
using Zero.Utils.Models;
using AM45Secure.Commons.Modelos.Comparador;
using AM45Secure.Commons.Modelos.comunes;
using MMC.Library.Business;
using System.Configuration;
using MMC.Library.Business.AutoMarsh.ProcesoEmision;
using System.Net.Mail;
using AM45Secure.DataAccess.DataAccess.CapaDatos;
using AM45Secure.Commons.Modelos.Configurador;
using Newtonsoft.Json;

namespace AM45Secure.Business.Business.Emitir
{
    public class EmitirBusiness : IEmitirBusiness
    {

        /* INDRA FJQP Implementacion de Emisión Multiple */
        // GET: Vehiculos
        DataAccess.DataAccess.CapaDatos.db dblayer = new DataAccess.DataAccess.CapaDatos.db();

        private readonly IEmitirDataAccess iEmitirDataAccess;
        private readonly IComparadorDataAccess iComparadorDataAccess;
        private readonly ICotizadorDataAccess iCotizadorDataAccess;
        private readonly string ZERO = "0";

        public EmitirBusiness(IEmitirDataAccess iEmitirDataAccess, IComparadorDataAccess iComparadorDataAccess, ICotizadorDataAccess iCotizadorDataAccess)
        {
            this.iEmitirDataAccess = iEmitirDataAccess;
            this.iComparadorDataAccess = iComparadorDataAccess;
            this.iCotizadorDataAccess = iCotizadorDataAccess;
        }

        public SingleResponse<EmitirModel> CargaInicial()
        {
            SingleResponse<EmitirModel> response = new SingleResponse<EmitirModel>();
            try
            {
                EmitirModel emitirModel = new EmitirModel();
                ElementoModel elementoModel = new ElementoModel();

                #region Carga Nacionalidad       

                try
                {
                    elementoModel.CatalogoId = ConstElementos.Nacionalidad;
                    emitirModel.NacionalidadList = iEmitirDataAccess.ConsultaElementosPorCatalogoId(elementoModel);
                }

                catch (DalException e)
                {
                    throw new DomainException(CodesEmision.ERR_00_03, e);
                }

                #endregion

                #region Carga Género

                try
                {
                    elementoModel.CatalogoId = ConstElementos.Genero;
                    emitirModel.GeneroList = iEmitirDataAccess.ConsultaElementosPorCatalogoId(elementoModel);
                }
                catch (DalException e)
                {
                    throw new DomainException(CodesEmision.ERR_00_04, e);
                }

                #endregion

                #region Carga Paises

                try
                {
                    elementoModel.CatalogoId = ConstElementos.Paises;
                    emitirModel.PaisNacimientoList = iEmitirDataAccess.ConsultaElementosPorCatalogoId(elementoModel);
                }
                catch (DalException e)
                {
                    throw new DomainException(CodesEmision.ERR_00_05, e);
                }

                #endregion

                #region Carga Docto de Identificación

                try
                {
                    elementoModel.CatalogoId = ConstElementos.DoctoIdentificación;
                    emitirModel.DoctoIdentifiacionList = iEmitirDataAccess.ConsultaElementosPorCatalogoId(elementoModel);
                }
                catch (DalException e)
                {
                    throw new DomainException(CodesEmision.ERR_00_06, e);
                }

                #endregion

                #region Carga Entidad Nacimiento

                try
                {
                    elementoModel.CatalogoId = ConstElementos.EntidadNacimiento;
                    emitirModel.EntidadNacimientoList = iEmitirDataAccess.ConsultaElementosPorCatalogoId(elementoModel);
                }
                catch (DalException e)
                {
                    throw new DalException(CodesEmision.ERR_00_07, e);
                }

                #endregion

                #region Carga Profesión

                try
                {
                    elementoModel.CatalogoId = ConstElementos.Profesion;
                    emitirModel.ProfesionList = iEmitirDataAccess.ConsultaElementosPorCatalogoId(elementoModel);
                }
                catch (DalException e)
                {
                    throw new DalException(CodesEmision.ERR_00_08, e);
                }

                #endregion

                #region Carga Ocupación

                try
                {
                    elementoModel.CatalogoId = ConstElementos.Ocupacion;
                    emitirModel.OcupacionList = iEmitirDataAccess.ConsultaElementosPorCatalogoId(elementoModel);
                }
                catch (DalException e)
                {
                    throw new DalException(CodesEmision.ERR_00_09, e);
                }

                #endregion

                #region Carga Giro de Negocio

                try
                {
                    elementoModel.CatalogoId = ConstElementos.GiroNegocio;
                    emitirModel.GiroNegocioList = iEmitirDataAccess.ConsultaElementosPorCatalogoId(elementoModel);
                }
                catch (DalException e)
                {
                    throw new DalException(CodesEmision.ERR_00_10, e);
                }

                #endregion

                #region Carga SiNo

                try
                {
                    elementoModel.CatalogoId = ConstElementos.SiNo;
                    emitirModel.MandoEnGobiernoList = iEmitirDataAccess.ConsultaElementosPorCatalogoId(elementoModel);
                }
                catch (DalException e)
                {
                    throw new DalException(CodesEmision.ERR_00_11, e);
                }

                #endregion

                #region Carga Regimen Fiscal Empresarial

                try
                {
                    elementoModel.CatalogoId = ConstElementos.RegimenFiscalEmpresarial;
                    emitirModel.RegimenFiscalEmpresarialList = iEmitirDataAccess.ConsultaElementosPorCatalogoId(elementoModel);
                }
                catch (DalException e)
                {
                    throw new DalException(CodesEmision.ERR_00_12, e);
                }

                #endregion

                response.Done(emitirModel, String.Empty);
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
                response.Error(new DomainException(CodesEmision.ERR_00_13, e));
            }
            return response;
        }

        /* INDRA FJQP Implementacion de Emisión Multiple */
        //CrearEmision Normal
        public SingleResponse<IList<ContratanteModel>> CrearEmision(ContratanteModel contratanteModel)
        {

            EmitirCot emitirCotizacion = new EmitirCot();
            string connetionString = DataAccess.DataAccess.Generic.DataAccess.GetConnectionString();

            SingleResponse<IList<ContratanteModel>> response = new SingleResponse<IList<ContratanteModel>>();
            try
            {
                Regex rgx = new Regex(@"^[a-zA-Z0-9\-]*$");

                if (!rgx.IsMatch(contratanteModel.Agencia.NumContrato))
                {
                    throw new DomainException(CodesEmision.INF_00_03);
                }

                IList<ContratanteModel> contratante = new List<ContratanteModel>();
                NeIncisoEndoso neIncisosEndoso = new NeIncisoEndoso();
                ProductoModel producto = new ProductoModel();

                DataSet dataSet = new DataSet();
                DatosContratanteModel datosContratanteModel = contratanteModel.Contratante;
                ComplementariaModel informacionComplementaria = contratanteModel.DatosComplementarios;
                DireccionModel direccionModel = contratanteModel.Direccion;
                VehiculoModel vehiculoModel = contratanteModel.Vehiculo;
                AgenciaModel agenciaModel = contratanteModel.Agencia;
                DatosEmitirModel datosEmitir = iEmitirDataAccess.ConsultaDatosCotizacion(contratanteModel.Solicitud);
                var fechaN = datosContratanteModel.FechaNacimiento.Split('T');
                DateTime fecha = Convert.ToDateTime(fechaN[0]);

                producto.ProductoId = datosEmitir.ProductoId;
                datosContratanteModel.FechaNacimiento = fecha.Year + "-" + fecha.Month + "-" + fecha.Day;
                datosContratanteModel.Nombre = datosContratanteModel.Nombre.Split('-')[0];
                datosEmitir.Cliente = datosEmitir.Cliente.Split('-')[0];
                datosEmitir.InfoComplementaria = contratanteModel.Solicitud.InfoComplementaria;
                datosEmitir.PerfilId = contratanteModel.GetIdPerfilUsuarioSesion();
                datosEmitir.UsuarioId = contratanteModel.GetIdUsuarioSesion();
                datosEmitir.TipoArrendamiento = contratanteModel.Solicitud.TipoArrendamiento;
                datosEmitir.ProductoFlex = Convert.ToBoolean(iEmitirDataAccess.ConsultaProductosFlex(producto).Flexible);

                DatosSolicitudModel datosSolicitud = new DatosSolicitudModel()
                {
                    SolicitudId = contratanteModel.Solicitud.SolicitudId,
                    FormaPagoId = datosEmitir.FormaPago
                };

                IList<SolicitudCotizacionModel> listaSolicitud = iComparadorDataAccess.ConsultarSolicitudCotizacion(datosSolicitud);

                dataSet.Tables.Add(datosContratanteModel.ToDataTable());
                dataSet.Tables.Add(informacionComplementaria.ToDataTable());
                dataSet.Tables.Add(direccionModel.ToDataTable());
                dataSet.Tables.Add(vehiculoModel.ToDataTable());
                dataSet.Tables.Add(agenciaModel.ToDataTable());
                dataSet.Tables.Add(datosEmitir.ToDataTable());

                if (listaSolicitud.Count > 0)
                {
                    dataSet.Tables.Add(listaSolicitud[0].ToDataTable());
                }
                //*aqui se genera la poliza
                #region Ejecuta Emision de la cotizacion 

                string poliza = emitirCotizacion.Emite(dataSet, connetionString);

                if (poliza.IndexOf("Error:", StringComparison.Ordinal) != -1) // Valida si la emision arrojo un error al tratar de generar la poliza
                {
                    throw new DomainException(poliza.Replace("Error: ", "").Replace("Could not find stored procedure", "No se encontró el procedimiento almacenado")); // Envia el error
                }

                neIncisosEndoso.Poliza = poliza; // Mapea la poliza generada

                IList<NeIncisoEndoso> incisoEndosos = iEmitirDataAccess.ConsultaNeIncisosEndoso(neIncisosEndoso); // Consulta tabla neIncisosEndoso
                if (incisoEndosos != null) // Valida si encontro registros
                {
                    contratante.Add(new ContratanteModel());
                    contratante[0].NeIncisosEndoso = incisoEndosos[0];
                }

                /* INDRA FJQP Encontrack */
                if (contratanteModel.ConfirmedEncontrack == 1)
                {
                    dblayer.InsertEncontrack(Convert.ToInt32(datosEmitir.CotizacionId), Convert.ToInt32(1), Convert.ToString(datosEmitir.SolicitudInt), Convert.ToString(datosEmitir.Numero), Convert.ToString(datosEmitir.CotizacionId), neIncisosEndoso.Poliza, neIncisosEndoso.Inciso, Convert.ToInt32(neIncisosEndoso.Endoso), 1);
                }
                /* INDRA FJQP Encontrack */

                #endregion

                response.Done(contratante, String.Empty);
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
                response.Error(new DomainException(CodesEmision.ERR_00_14, e));
            }
            return response;
        }

        /* INDRA FJQP Implementacion de Emisión Multiple */
        //CrearEmision Aqui modificar Multiple
        public SingleResponse<IList<ContratanteModel>> CrearEmisionMultipleC(ContratanteModel contratanteModel)
        {
            EmitirCot emitirCotizacion = new EmitirCot();
            string connetionString = DataAccess.DataAccess.Generic.DataAccess.GetConnectionString();

            SingleResponse<IList<ContratanteModel>> response = new SingleResponse<IList<ContratanteModel>>();
            try
            {
                Regex rgx = new Regex(@"^[a-zA-Z0-9\-]*$");

                if (!rgx.IsMatch(contratanteModel.Agencia.NumContrato))
                {
                    throw new DomainException(CodesEmision.INF_00_03);
                }

                IList<ContratanteModel> contratante = new List<ContratanteModel>();
                NeIncisoEndoso neIncisosEndoso = new NeIncisoEndoso();
                ProductoModel producto = new ProductoModel();

                DataSet dataSet = new DataSet();
                DatosContratanteModel datosContratanteModel = contratanteModel.Contratante;
                ComplementariaModel informacionComplementaria = contratanteModel.DatosComplementarios;
                DireccionModel direccionModel = contratanteModel.Direccion;
                VehiculoModel vehiculoModel = contratanteModel.Vehiculo;
                AgenciaModel agenciaModel = contratanteModel.Agencia;
                DatosEmitirModel datosEmitir = iEmitirDataAccess.ConsultaDatosCotizacion(contratanteModel.Solicitud);
                var fechaN = datosContratanteModel.FechaNacimiento.Split('T');
                DateTime fecha = Convert.ToDateTime(fechaN[0]);

                producto.ProductoId = datosEmitir.ProductoId;
                datosContratanteModel.FechaNacimiento = fecha.Year + "-" + fecha.Month + "-" + fecha.Day;
                datosContratanteModel.Nombre = datosContratanteModel.Nombre.Split('-')[0];
                datosEmitir.Cliente = datosEmitir.Cliente.Split('-')[0];
                datosEmitir.InfoComplementaria = contratanteModel.Solicitud.InfoComplementaria;
                datosEmitir.PerfilId = contratanteModel.GetIdPerfilUsuarioSesion();
                datosEmitir.UsuarioId = contratanteModel.GetIdUsuarioSesion();
                datosEmitir.TipoArrendamiento = contratanteModel.Solicitud.TipoArrendamiento;
                datosEmitir.ProductoFlex = Convert.ToBoolean(iEmitirDataAccess.ConsultaProductosFlex(producto).Flexible);

                DatosSolicitudModel datosSolicitud = new DatosSolicitudModel()
                {
                    SolicitudId = contratanteModel.Solicitud.SolicitudId,
                    FormaPagoId = datosEmitir.FormaPago
                };

                IList<SolicitudCotizacionModel> listaSolicitud = iComparadorDataAccess.ConsultarSolicitudCotizacion(datosSolicitud);

                dataSet.Tables.Add(datosContratanteModel.ToDataTable());
                dataSet.Tables.Add(informacionComplementaria.ToDataTable());
                dataSet.Tables.Add(direccionModel.ToDataTable());
                dataSet.Tables.Add(vehiculoModel.ToDataTable());
                dataSet.Tables.Add(agenciaModel.ToDataTable());
                dataSet.Tables.Add(datosEmitir.ToDataTable());

                if (listaSolicitud.Count > 0)
                {
                    dataSet.Tables.Add(listaSolicitud[0].ToDataTable());
                }
                //*aqui se genera la poliza
                #region Ejecuta Emision de la cotizacion 

                string poliza = emitirCotizacion.Emite(dataSet, connetionString);

                if (poliza.IndexOf("Error:", StringComparison.Ordinal) != -1) // Valida si la emision arrojo un error al tratar de generar la poliza
                {
                    throw new DomainException(poliza.Replace("Error: ", "").Replace("Could not find stored procedure", "No se encontró el procedimiento almacenado")); // Envia el error
                }

                neIncisosEndoso.Poliza = poliza; // Mapea la poliza generada

                IList<NeIncisoEndoso> incisoEndosos = iEmitirDataAccess.ConsultaNeIncisosEndoso(neIncisosEndoso); // Consulta tabla neIncisosEndoso
                if (incisoEndosos != null) // Valida si encontro registros
                {
                    contratante.Add(new ContratanteModel());
                    contratante[0].NeIncisosEndoso = incisoEndosos[0];
                }

                #endregion

                response.Done(contratante, String.Empty);
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
                response.Error(new DomainException(CodesEmision.ERR_00_14, e));
            }

            return response;

        }

        public SingleResponse<bool> ConsultaValoresPrima(SolicitudPrimaCotizacion solicitudPrima)
        {
            SingleResponse<bool> response = new SingleResponse<bool>();
            try
            {
                DatosEmitirModel datosEmitir = iEmitirDataAccess.ConsultaDatosCotizacion(solicitudPrima);

                response.Done((datosEmitir != null), String.Empty);
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
                response.Error(new DomainException(CodesEmision.ERR_00_15, e));
            }
            return response;
        }

        public SingleResponse<IList<NeIncisoEndoso>> ConsultaNeIncisosEndoso(NeIncisoEndoso neIncisosEndoso)
        {
            //AgenciaCotizacion agenciaCotizacion = new AgenciaCotizacion("connectionBD");
            //agenciaCotizacion.GrabadoSolicitudCotizacion;
            SingleResponse<IList<NeIncisoEndoso>> response = new SingleResponse<IList<NeIncisoEndoso>>();
            try
            {
                #region ModelValidations

                if (null == neIncisosEndoso)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }

                #endregion ModelValidations

                #region manipulaciond de informacio <insert. delete>

                IList<NeIncisoEndoso> contratante = iEmitirDataAccess.ConsultaNeIncisosEndoso(neIncisosEndoso);
                response.Done(contratante, string.Empty);

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
                response.Error(new DomainException(CodesEmision.ERR_00_15, e));
            }
            return response;
        }

        public SingleResponse<IList<InformacionClienteModel>> ConsultaInformacionCliente(CotizadorModel cotizador)
        {
            SingleResponse<IList<InformacionClienteModel>> response = new SingleResponse<IList<InformacionClienteModel>>();
            IList<InformacionClienteModel> infoCliente = new List<InformacionClienteModel>();
            InformacionClienteModel informacionCliente = new InformacionClienteModel();
            InfContratanteModel infContratante = new InfContratanteModel();
            CodigoPostalModel codigoPostal = new CodigoPostalModel();
            DireccionModel direccion = new DireccionModel();

            try
            {
                #region ModelValidations

                if (null == cotizador)
                {
                    throw new DomainException(Codes.ERR_00_16);
                }

                #endregion ModelValidations

                #region manipulaciond de informacio <insert. delete>

                IList<ValoresReglaModel> infoClienteList = iCotizadorDataAccess.ConsultaReglaNegocio(cotizador);

                if (infoClienteList.Count > 0)
                {
                    #region Consulta Dirección

                    codigoPostal.CodigoPostal = infoClienteList[0].CodigoPostal;
                    codigoPostal.Colonia = infoClienteList[0].Colonia;
                    IList<RegionCodigoPostalModel> region = iCotizadorDataAccess.ConsultarCodigoPostal(codigoPostal);
                    direccion.Pais = infoClienteList[0].Pais;
                    direccion.Estado = region[0].Estado;
                    direccion.EstadoId = Convert.ToString(region[0].EstadoId);
                    direccion.Delegacion = region[0].Delegacion;
                    direccion.DelegacionId = Convert.ToString(region[0].DelegacionId);
                    direccion.Calle = infoClienteList[0].Domicilio;
                    direccion.Numero = infoClienteList[0].NoExterior;
                    direccion.Colonia = infoClienteList[0].Colonia;
                    direccion.CodigoPostal = infoClienteList[0].CodigoPostal;
                    informacionCliente.Direccion = direccion;

                    #endregion

                    #region Consulta Contratante

                    infContratante.PersonaId = cotizador.SolicitudRegla.IdCliente;
                    IList<ElementoModel> tipoPersona = iEmitirDataAccess.ConsultaElementosPorElementoId(Convert.ToInt32(infoClienteList[0].ValorId));
                    infContratante.TipoPersona = tipoPersona[0].Nombre;
                    infContratante.Nombre = infoClienteList[0].Cliente;
                    infContratante.Paterno = "";
                    infContratante.Materno = "";
                    infContratante.FechaNacimiento = infoClienteList[0].FechaRetro;
                    infContratante.RFC = infoClienteList[0].Valor;
                    IList<ElementoModel> nacionalidadList = iEmitirDataAccess.ConsultaElementosPorElementoId(Convert.ToInt32(infoClienteList[0].ClienteId));
                    infContratante.Nacionalidad = nacionalidadList[0];
                    infContratante.Genero = "";
                    infContratante.Telefono = "";
                    infContratante.Telefono2 = "";
                    informacionCliente.Contratante = infContratante;

                    #endregion

                    infoCliente.Add(informacionCliente);
                }

                #endregion

                response.Done(infoCliente, string.Empty);
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
                response.Error(new DomainException(CodesEmision.ERR_00_16, e));
            }
            return response;
        }

        public SingleResponse<IList<VehiculoModel>> ConsultaSerie(VehiculoModel vehiculo)
        {
            SingleResponse<IList<VehiculoModel>> response = new SingleResponse<IList<VehiculoModel>>();
            try
            {
                #region ModelValidations

                if (null == vehiculo)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }

                Regex rgx = new Regex(@"^[^ioqñIOQÑ´°[<>{}()¡¿\\\/|;:.,\-_\+~!?@#$%^=&*'\""/;`%]*$");

                if (!rgx.IsMatch(vehiculo.Serie))
                {
                    throw new DomainException(CodesEmision.INF_00_02);
                }

                if (vehiculo.Serie.Length != 17)
                {
                    throw new DomainException(CodesEmision.INF_00_01);
                }

                #endregion ModelValidations

                #region manipulaciond de informacio <insert. delete>

                IList<VehiculoModel> vehiculoList = iEmitirDataAccess.ConsultaSerie(vehiculo);
                response.Done(vehiculoList, string.Empty);

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
                response.Error(new DomainException(CodesEmision.ERR_00_17, e));
            }
            return response;
        }

        /* INDRA FJQP Implementacion de Emisión Multiple */
        public SingleResponse<IList<VehiculoGrabModel>> ConsultaSerieGrab(VehiculoGrabModel vehiculo)
        {
            SingleResponse<IList<VehiculoGrabModel>> response = new SingleResponse<IList<VehiculoGrabModel>>();
            try
            {
                #region ModelValidations

                if (null == vehiculo)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }

                Regex rgx = new Regex(@"^[^ioqñIOQÑ´°[<>{}()¡¿\\\/|;:.,\-_\+~!?@#$%^=&*'\""/;`%]*$");

                if (!rgx.IsMatch(vehiculo.Serie))
                {

                    //throw new DomainException(CodesEmision.INF_00_02);
                    throw new DomainException("La serie #" + vehiculo.IndiceSerie + " capturada incorrecta, contiene caracteres (IOQÑ,·%.$ etc.) no validos.");
                }

                if (vehiculo.Serie.Length != 17)
                {
                    //throw new DomainException(CodesEmision.INF_00_01);
                    throw new DomainException("La serie #" + vehiculo.IndiceSerie + " capturada incorrecta, debe estar compuesta de 17 caracteres.");

                }

                #endregion ModelValidations

                #region manipulaciond de informacio <insert. delete>

                IList<VehiculoGrabModel> vehiculoList = iEmitirDataAccess.ConsultaSerieGrab(vehiculo);
                response.Done(vehiculoList, string.Empty);

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
                response.Error(new DomainException(CodesEmision.ERR_00_17, e));
            }
            return response;
        }

        /* INDRA FJQP Implementacion de Emisión Multiple */
        public SingleResponse<IList<ContratanteModel>> CrearEmisionMultiple(ContratanteModel contratanteModel)
        {
            SingleResponse<IList<ContratanteModel>> response = new SingleResponse<IList<ContratanteModel>>();

            SingleResponse<IList<ContratanteModel>> PolSal = new SingleResponse<IList<ContratanteModel>>();

            try
            {
                int iNoCot = 0;
                int iEstatus = 0;

                iNoCot = contratanteModel.Solicitud.SolicitudId;
                iEstatus = 1;


                DataSet ds = dblayer.Getrecord(iNoCot, iEstatus);

                List<VehiculosCapturaEmision> listreg = new List<VehiculosCapturaEmision>();

                List<VehiculosCapturaEmision> datosCliente = new List<VehiculosCapturaEmision>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    listreg.Add(new VehiculosCapturaEmision

                    {

                        idNoCotizacion = Convert.ToInt32(dr["idNoCotizacion"]),
                        idNoConsec = Convert.ToInt32(dr["idNoConsec"]),
                        sNoSerie = Convert.ToString(dr["sNoSerie"]),
                        sNoMotor = Convert.ToString(dr["sNoMotor"]),
                        sPlacas = Convert.ToString(dr["sPlacas"]),
                        sContrato = Convert.ToString(dr["sContrato"]),
                        iEstatusReg = Convert.ToInt32(dr["iEstatusReg"]),
                        sDescEstatus = Convert.ToString(dr["sDescEstatus"]),
                        sConductor = Convert.ToString(dr["sConductor"]),
                        sSolicitud = Convert.ToString(dr["sSolicitud"]),
                        sQLTS = Convert.ToString(dr["sQLTS"]),
                        sCotizacion = Convert.ToString(dr["sCotizacion"]),
                        iInciso = Convert.ToInt32(dr["iInciso"]),
                        iEndoso = Convert.ToInt32(dr["iEndoso"])

                    }

                    );


                    contratanteModel.CabeceraCotHeredada.Tkn = contratanteModel.Tkn;



                    if (Convert.ToInt32(dr["idNoConsec"]) == 1)
                    {
                        PolSal = CrearEmisionMultipleC(contratanteModel);

                        if (PolSal.Response[0].NeIncisosEndoso.Poliza == "0")
                        {
                            dblayer.Update_VehiculoDBNuevo(Convert.ToInt32(dr["idNoCotizacion"]), Convert.ToInt32(dr["idNoConsec"]), Convert.ToString(dr["sSolicitud"]), Convert.ToString(dr["sQLTS"]), Convert.ToString(dr["sCotizacion"]), PolSal.Response[0].NeIncisosEndoso.Poliza, 3, 0, 0);
                        }
                        else
                        {
                            dblayer.Update_VehiculoDBNuevo(Convert.ToInt32(dr["idNoCotizacion"]), Convert.ToInt32(dr["idNoConsec"]), Convert.ToString(dr["sSolicitud"]), Convert.ToString(dr["sQLTS"]), Convert.ToString(dr["sCotizacion"]), PolSal.Response[0].NeIncisosEndoso.Poliza, 2, PolSal.Response[0].NeIncisosEndoso.Inciso, Convert.ToInt32(PolSal.Response[0].NeIncisosEndoso.Endoso));
                        }

                        /* INDRA FJQP Encontrack */
                        if (contratanteModel.ConfirmedEncontrack == 1)
                        {
                            dblayer.InsertEncontrack(Convert.ToInt32(dr["idNoCotizacion"]), Convert.ToInt32(dr["idNoConsec"]), Convert.ToString(dr["sSolicitud"]), Convert.ToString(dr["sQLTS"]), Convert.ToString(dr["sCotizacion"]), PolSal.Response[0].NeIncisosEndoso.Poliza, PolSal.Response[0].NeIncisosEndoso.Inciso, Convert.ToInt32(PolSal.Response[0].NeIncisosEndoso.Endoso), 1);
                        }
                        /* INDRA FJQP Encontrack */

                    }
                    else
                    {

                        contratanteModel.CabeceraCotHeredada.Cotizacion.CP = null;

                        SingleResponse<CabeceraCotizacionModel> DatSal = EjecutaGrabadoSolicitudCotizacion(contratanteModel.CabeceraCotHeredada);

                        dblayer.Update_VehiculoDBNuevo(Convert.ToInt32(dr["idNoCotizacion"]), Convert.ToInt32(dr["idNoConsec"]), DatSal.Response.IdSolicitud.ToString(), "", Convert.ToString(dr["sQLTS"]), "", Convert.ToInt32(dr["iEstatusReg"]), Convert.ToInt32(dr["iInciso"]), Convert.ToInt32(dr["iEndoso"]));

                        int FolioSolicitud = DatSal.Response.IdSolicitud;
                        int FormaPagoId = 72;

                        contratanteModel.CotizarModelHeredada.DatosCotizacionModel.SolicitudId = FolioSolicitud;
                        contratanteModel.CotizarModelHeredada.DatosCotizacionModel.FormaPagoId = FormaPagoId;

                        SingleResponse<ComparadorModel> DatSalCot = CargaComparador(contratanteModel.CotizarModelHeredada);


                        dblayer.Update_VehiculoDBNuevo(Convert.ToInt32(dr["idNoCotizacion"]), Convert.ToInt32(dr["idNoConsec"]), DatSal.Response.IdSolicitud.ToString(), DatSalCot.Response.AseguradorasProducto[0].ListaPaquetes[0].Numero.ToString(), DatSalCot.Response.ListNeCotizacion.CotizacionId.ToString(), "", Convert.ToInt32(dr["iEstatusReg"]), Convert.ToInt32(dr["iInciso"]), Convert.ToInt32(dr["iEndoso"]));

                        contratanteModel.Solicitud.CotizacionId = DatSalCot.Response.ListNeCotizacion.CotizacionId;
                        contratanteModel.Solicitud.SolicitudId = DatSal.Response.IdSolicitud;
                        contratanteModel.Solicitud.Numero = DatSalCot.Response.AseguradorasProducto[0].ListaPaquetes[0].Numero;
                        contratanteModel.Vehiculo.Conductor = Convert.ToString(dr["sConductor"]);
                        contratanteModel.Vehiculo.Motor = Convert.ToString(dr["sNoMotor"]);
                        contratanteModel.Vehiculo.Placas = Convert.ToString(dr["sPlacas"]);
                        contratanteModel.Vehiculo.Serie = Convert.ToString(dr["sNoSerie"]);
                        contratanteModel.Agencia.NumContrato = Convert.ToString(dr["sContrato"]);

                        PolSal = CrearEmisionMultipleC(contratanteModel);

                        if (PolSal.Response[0].NeIncisosEndoso.Poliza == "0")
                        {
                            dblayer.Update_VehiculoDBNuevo(Convert.ToInt32(dr["idNoCotizacion"]), Convert.ToInt32(dr["idNoConsec"]), DatSal.Response.IdSolicitud.ToString(), DatSalCot.Response.AseguradorasProducto[0].ListaPaquetes[0].Numero.ToString(), DatSalCot.Response.ListNeCotizacion.CotizacionId.ToString(), PolSal.Response[0].NeIncisosEndoso.Poliza, 3, 0, 0);
                        }
                        else
                        {
                            dblayer.Update_VehiculoDBNuevo(Convert.ToInt32(dr["idNoCotizacion"]), Convert.ToInt32(dr["idNoConsec"]), DatSal.Response.IdSolicitud.ToString(), DatSalCot.Response.AseguradorasProducto[0].ListaPaquetes[0].Numero.ToString(), DatSalCot.Response.ListNeCotizacion.CotizacionId.ToString(), PolSal.Response[0].NeIncisosEndoso.Poliza, 2, PolSal.Response[0].NeIncisosEndoso.Inciso, Convert.ToInt32(PolSal.Response[0].NeIncisosEndoso.Endoso));
                        }

                        /* INDRA FJQP Encontrack */
                        if (contratanteModel.ConfirmedEncontrack == 1)
                        {
                            dblayer.InsertEncontrack(Convert.ToInt32(dr["idNoCotizacion"]), Convert.ToInt32(dr["idNoConsec"]), DatSal.Response.IdSolicitud.ToString(), DatSalCot.Response.AseguradorasProducto[0].ListaPaquetes[0].Numero.ToString(), DatSalCot.Response.ListNeCotizacion.CotizacionId.ToString(), PolSal.Response[0].NeIncisosEndoso.Poliza, PolSal.Response[0].NeIncisosEndoso.Inciso, Convert.ToInt32(PolSal.Response[0].NeIncisosEndoso.Endoso), 1);
                        }
                        /* INDRA FJQP Encontrack */

                    }

                }

                datosCliente = listreg;

                response.Done(PolSal.Response, String.Empty);

            }
            catch (Exception e)
            {
                throw new DalException("Error:::", e);
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

                if (validations.Count > 0)
                {
                    throw new DomainValidationsException(validations);
                }

                #endregion

                #region validaciones de negocio

                ClaveValorModel carga = new ClaveValorModel
                {
                    Valor = cabeceraCotizacionModel.Vehiculo.ShowCargas ? cabeceraCotizacionModel.Vehiculo.Carga.Valor.Substring(0, 1) : null
                };
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



                    if (dtCotizacion != null && dtCotizacion.Tables[0].Rows.Count > 1)
                    {

                        string Err = "";
                        // FJQP INDRA
                        foreach (DataRow row in dtCotizacion.Tables[0].Rows)
                        {
                            Logger.Error(row["Errores"] + " :: " + row["MessageError"]);
                            if (row.ItemArray[2].ToString() != "Cotizacion generada con exito")
                            {
                                Err = Err + ' ' + row.ItemArray[2].ToString() + ' ' + row.ItemArray[3].ToString();
                            }

                            if (row.ItemArray[2].ToString() != "Error Cotizacion QLT Deducible" && row.ItemArray[3].ToString() != "Error Cotizacion QLT Deducible")
                            {
                                Err = Err + ' ' + row.ItemArray[2].ToString() + ' ' + row.ItemArray[3].ToString();
                            }
                        }
                        comparadorModel.Errores = Err;
                    }
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

        /* INDRA FJQP Implementacion de Emisión Multiple */
        public SingleResponse<IList<DirectivasModel>> RecuperaInfoDirectivas(DirectivasModel directivasModel)
        {
            SingleResponse<IList<DirectivasModel>> response = new SingleResponse<IList<DirectivasModel>>();

            try
            {
                int NoCob = 0;
                int NoProd = 0;
                int Accion = 1;
                int Producto = 0;
                int TipoVehiculo = 0;
                int TipoServicioVehiculo = 0;

                NoCob = directivasModel.IdCobertura;
                NoProd = directivasModel.idProductoFlex;

                Producto = directivasModel.idProducto;
                TipoVehiculo = directivasModel.idTipoVehiculo;
                TipoServicioVehiculo = directivasModel.idTipoServicioVehiculo;

                DataSet ds = dblayer.GetCoberturas(Accion, NoCob, NoProd, Producto, TipoVehiculo, TipoServicioVehiculo);

                List<DirectivasModel> listreg = new List<DirectivasModel>();
                RangosModel rangosModel = new RangosModel();
                IList<string> rangoSumas = new List<string>();
                IList<string> rangoDeducibles = new List<string>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    listreg.Add(new DirectivasModel
                    {

                        iAccion = Convert.ToInt32(dr["iAccion"]),
                        IdCobertura = Convert.ToInt32(dr["IdCobertura"]),
                        Nombre = Convert.ToString(dr["Nombre"]),
                        idProductoFlex = Convert.ToInt32(dr["idProductoFlex"]),
                        idProductoFlexAseguradora = Convert.ToInt32(dr["idProductoFlexAseguradora"]),
                        CoberturaFija = Convert.ToInt32(dr["CoberturaFija"]),
                        PerfilCoberturaFija = Convert.ToInt32(dr["PerfilCoberturaFija"]),
                        SumaAseguradaDefault = Convert.ToString(dr["SumaAseguradaDefault"]),
                        PerfilSumaAsegurada = Convert.ToInt32(dr["PerfilSumaAsegurada"]),
                        DeducibleDefault = Convert.ToString(dr["DeducibleDefault"]),
                        PerfilDeducible = Convert.ToInt32(dr["PerfilDeducible"]),
                        ToolTipCobertura = Convert.ToString(dr["ToolTipCobertura"]),
                        isEspecial = Convert.ToInt32(dr["isEspecial"]),
                        ParametroDeducible = Convert.ToString(dr["ParametroDeducible"]),
                        ParametroSA = Convert.ToString(dr["ParametroSA"]),
                        rangosModel = JsonConvert.DeserializeObject<RangosModel>(Convert.ToString(dr["Rangos"])),
                        idProducto = Convert.ToInt32(dr["idProducto"]),
                        idTipoVehiculo = Convert.ToInt32(dr["idTipoVehiculo"]),
                        idTipoServicioVehiculo = Convert.ToInt32(dr["idTipoServicioVehiculo"]),
                    }

                    );

                }

                response.Done(listreg, String.Empty);

            }
            catch (Exception e)
            {
                throw new DalException("Error:::", e);
            }

            return response;
        }

        /* INDRA FJQP Implementacion de Emisión Multiple */
        public SingleResponse<IList<DirectivasModel>> RecuperaListaCoberturas(DirectivasModel directivasModel)
        {
            SingleResponse<IList<DirectivasModel>> response = new SingleResponse<IList<DirectivasModel>>();

            try
            {
                int NoCob = 0;
                int NoProd = 0;
                int Accion = 0;
                int Producto = 0;
                int TipoVehiculo = 0;
                int TipoServicioVehiculo = 0;

                NoProd = directivasModel.idProductoFlex;
                NoCob = directivasModel.IdCobertura;

                Producto = directivasModel.idProducto;
                TipoVehiculo = directivasModel.idTipoVehiculo;
                TipoServicioVehiculo = directivasModel.idTipoServicioVehiculo;

                DataSet ds = dblayer.GetCoberturas(Accion, NoCob, NoProd, Producto, TipoVehiculo, TipoServicioVehiculo);

                RangosModel rangosModel = new RangosModel();
                IList<string> rangoSumas = new List<string>();
                IList<string> rangoDeducibles = new List<string>();

                List<DirectivasModel> listreg = new List<DirectivasModel>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    listreg.Add(new DirectivasModel
                    {

                        iAccion = Convert.ToInt32(dr["iAccion"]),
                        IdCobertura = Convert.ToInt32(dr["IdCobertura"]),
                        Nombre = Convert.ToString(dr["Nombre"]),
                        idProductoFlex = Convert.ToInt32(dr["idProductoFlex"]),
                        idProductoFlexAseguradora = Convert.ToInt32(dr["idProductoFlexAseguradora"]),
                        CoberturaFija = Convert.ToInt32(dr["CoberturaFija"]),
                        PerfilCoberturaFija = Convert.ToInt32(dr["PerfilCoberturaFija"]),
                        SumaAseguradaDefault = Convert.ToString(dr["SumaAseguradaDefault"]),
                        PerfilSumaAsegurada = Convert.ToInt32(dr["PerfilSumaAsegurada"]),
                        DeducibleDefault = Convert.ToString(dr["DeducibleDefault"]),
                        PerfilDeducible = Convert.ToInt32(dr["PerfilDeducible"]),
                        ToolTipCobertura = Convert.ToString(dr["ToolTipCobertura"]),
                        isEspecial = Convert.ToInt32(dr["isEspecial"]),
                        ParametroDeducible = Convert.ToString(dr["ParametroDeducible"]),
                        ParametroSA = Convert.ToString(dr["ParametroSA"]),
                        rangosModel = JsonConvert.DeserializeObject<RangosModel>(Convert.ToString(dr["Rangos"])),
                        idProducto = Convert.ToInt32(dr["idProducto"]),
                        idTipoVehiculo = Convert.ToInt32(dr["idTipoVehiculo"]),
                        idTipoServicioVehiculo = Convert.ToInt32(dr["idTipoServicioVehiculo"]),
                    }
                 );

                }

                response.Done(listreg, String.Empty);

            }
            catch (Exception e)
            {
                throw new DalException("Error:::", e);
            }

            return response;
        }

        /* INDRA FJQP Implementacion de Emisión Multiple */
        public SingleResponse<IList<DirectivasModel>> AlmacenaInfoDirectivas(DirectivasModel directivasModel)
        {
            SingleResponse<IList<DirectivasModel>> response = new SingleResponse<IList<DirectivasModel>>();

            try
            {
                int NoCob = 0;
                int NoProd = 0;
                int NoProdFlexAseg = 0;
                int Accion = 2;
                int CoberturaFija = 0;
                int PerfilCoberturaFija = 0;
                string SumaAseguradaDefault = "";
                int PerfilSumaAsegurada = 0;
                string DeducibleDefault = "";
                int PerfilDeducible = 0;
                string ToolTipCobertura = "";
                int IsEspecial = 0;
                int Producto = 0;
                int TipoVehiculo = 0;
                int TipoServicioVehiculo = 0;

                NoCob = directivasModel.IdCobertura;
                NoProd = directivasModel.idProductoFlex;
                NoProdFlexAseg = directivasModel.idProductoFlexAseguradora;
                CoberturaFija = directivasModel.CoberturaFija;
                PerfilCoberturaFija = directivasModel.PerfilCoberturaFija;
                SumaAseguradaDefault = directivasModel.SumaAseguradaDefault;
                PerfilSumaAsegurada = directivasModel.PerfilSumaAsegurada;
                DeducibleDefault = directivasModel.DeducibleDefault;
                PerfilDeducible = directivasModel.PerfilDeducible;
                ToolTipCobertura = directivasModel.ToolTipCobertura;
                IsEspecial = directivasModel.isEspecial;

                Producto = directivasModel.idProducto;
                TipoVehiculo = directivasModel.idTipoVehiculo;
                TipoServicioVehiculo = directivasModel.idTipoServicioVehiculo;



                DataSet ds = dblayer.UpdateCoberturas(Accion, NoCob, NoProd, NoProdFlexAseg, CoberturaFija, PerfilCoberturaFija, SumaAseguradaDefault, PerfilSumaAsegurada, DeducibleDefault, PerfilDeducible, ToolTipCobertura, IsEspecial, Producto, TipoVehiculo, TipoServicioVehiculo);

                List<DirectivasModel> listreg = new List<DirectivasModel>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    listreg.Add(new DirectivasModel
                    {

                        iAccion = Convert.ToInt32(dr["iAccion"]),
                        IdCobertura = Convert.ToInt32(dr["IdCobertura"]),
                        Nombre = Convert.ToString(dr["Nombre"]),
                        idProductoFlex = Convert.ToInt32(dr["idProductoFlex"]),
                        idProductoFlexAseguradora = Convert.ToInt32(dr["idProductoFlexAseguradora"]),
                        CoberturaFija = Convert.ToInt32(dr["CoberturaFija"]),
                        PerfilCoberturaFija = Convert.ToInt32(dr["PerfilCoberturaFija"]),
                        SumaAseguradaDefault = Convert.ToString(dr["SumaAseguradaDefault"]),
                        PerfilSumaAsegurada = Convert.ToInt32(dr["PerfilSumaAsegurada"]),
                        DeducibleDefault = Convert.ToString(dr["DeducibleDefault"]),
                        PerfilDeducible = Convert.ToInt32(dr["PerfilDeducible"]),
                        ToolTipCobertura = Convert.ToString(dr["ToolTipCobertura"]),
                        isEspecial = Convert.ToInt32(dr["isEspecial"]),
                        ParametroDeducible = Convert.ToString(dr["ParametroDeducible"]),
                        ParametroSA = Convert.ToString(dr["ParametroSA"]),
                        idProducto = Convert.ToInt32(dr["idProducto"]),
                        idTipoVehiculo = Convert.ToInt32(dr["idTipoVehiculo"]),
                        idTipoServicioVehiculo = Convert.ToInt32(dr["idTipoServicioVehiculo"]),
                    }

                    );

                }

                response.Done(listreg, String.Empty);

            }
            catch (Exception e)
            {
                throw new DalException("Error:::", e);
            }

            return response;
        }

        /* INDRA FJQP Implementacion de config Emisión Multiple */
        public SingleResponse<Commons.Modelos.ConfigMultiple.FiltroConfigMultiple> FiltrosConfigMultiple(Commons.Modelos.ConfigMultiple.requestFiltro request)
        {
            SingleResponse<Commons.Modelos.ConfigMultiple.FiltroConfigMultiple> response = new SingleResponse<Commons.Modelos.ConfigMultiple.FiltroConfigMultiple>();
            try
            {
                var DatosUsuarioCotizante = new PersonaEmail();
                DatosUsuarioCotizante.Tkn = request.Tkn;
                DatosUsuarioCotizante.PersonaId = DatosUsuarioCotizante.GetIdUsuarioSesion();

                var respon = iEmitirDataAccess.FiltrosConfigMultiple(DatosUsuarioCotizante.PersonaId.ToString());

                response.Done(respon, string.Empty);
            }

            catch (Exception e)
            {
                //Agregar menjase de error
                response.Error(new DomainException(e.Message));
            }
            return response;
        }

        // INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos
        public SingleResponse<bool> UsuarioAdministrador(string tkn)
        {
            SingleResponse<bool> response = new SingleResponse<bool>();
            try
            {
                var DatosUsuarioCotizante = new PersonaEmail();
                DatosUsuarioCotizante.Tkn = tkn;
                DatosUsuarioCotizante.PersonaId = DatosUsuarioCotizante.GetIdUsuarioSesion();
                var respon = iEmitirDataAccess.UsuarioAdministrador(DatosUsuarioCotizante.PersonaId.ToString());
                response.Done(respon, string.Empty);
            }

            catch (Exception e)
            {
                //Agregar menjase de error
                response.Error(new DomainException(e.Message));
            }
            return response;
        }
        // INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos
        public SingleResponse<IList<configMultipleRegModel>> ConsultarConfigMultiple(ConfigRequestModel configRequestModel)
        {
            SingleResponse<IList<configMultipleRegModel>> response = new SingleResponse<IList<configMultipleRegModel>>();
            try
            {
                IList<configMultipleRegModel> listConfiguraciones = iEmitirDataAccess.ConsultarConfigMultiples(configRequestModel);
                response.Done(listConfiguraciones, string.Empty);
            }

            catch (Exception e)
            {
                //Agregar menjase de error
                response.Error(new DomainException(e.Message));
            }
            return response;
        }
        // INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos
        public SingleResponse<bool> GuardarDatosConfigMultiple(InsertConfigMultipleModel requestModel)
        {
            SingleResponse<bool> response = new SingleResponse<bool>();
            try
            {
                var DatosUsuarioCotizante = new PersonaEmail();
                DatosUsuarioCotizante.Tkn = requestModel.Tkn;
                DatosUsuarioCotizante.PersonaId = DatosUsuarioCotizante.GetIdUsuarioSesion();
                obtenerDatosEmail(DatosUsuarioCotizante);

                var respon = iEmitirDataAccess.GuardarDatosConfigMultiple(requestModel, DatosUsuarioCotizante.Nombre);
                response.Done(respon, string.Empty);
            }

            catch (Exception e)
            {
                //Agregar menjase de error
                response.Error(new DomainException(e.Message));
            }
            return response;
        }

        // INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos
        public SingleResponse<bool> EliminarDatosConfigMultiple(InsertConfigMultipleModel requestModel)
        {
            SingleResponse<bool> response = new SingleResponse<bool>();
            try
            {
                var DatosUsuarioCotizante = new PersonaEmail();
                DatosUsuarioCotizante.Tkn = requestModel.Tkn;
                DatosUsuarioCotizante.PersonaId = DatosUsuarioCotizante.GetIdUsuarioSesion();
                obtenerDatosEmail(DatosUsuarioCotizante);

                var respon = iEmitirDataAccess.EliminarDatosConfigMultiple(requestModel, DatosUsuarioCotizante.Nombre);

                response.Done(respon, string.Empty);
            }

            catch (Exception e)
            {
                //Agregar menjase de error
                response.Error(new DomainException(e.Message));
            }
            return response;
        }

        public void obtenerDatosEmail(PersonaEmail DatosUsuarioCotizante)
        {
            var pm = iComparadorDataAccess.obtenerDatosPersonaEmail(DatosUsuarioCotizante);
        }

        // INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos
        public SingleResponse<IList<configMultipleRegModel>> PermiteConfigMultiple(ConfigRequestModel configRequestModel)
        {
            SingleResponse<IList<configMultipleRegModel>> response = new SingleResponse<IList<configMultipleRegModel>>();
            try
            {
                IList<configMultipleRegModel> listConfiguraciones = iEmitirDataAccess.PermiteConfigMultiples(configRequestModel);

                response.Done(listConfiguraciones, string.Empty);
            }

            catch (Exception e)
            {
                //Agregar menjase de error
                response.Error(new DomainException(e.Message));
            }
            return response;
        }

    }
}

