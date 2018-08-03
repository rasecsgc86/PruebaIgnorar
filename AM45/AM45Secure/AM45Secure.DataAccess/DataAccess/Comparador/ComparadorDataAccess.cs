using System;
using System.Collections.Generic;
using System.Linq;
using AM45Secure.Commons.Modelos.Comparador;
using AM45Secure.DataAccess.IDataAccess.IComparador;
using AM45Secure.DataAccess.IDataAccess.IGeneric;
using Zero.Exceptions;
using AM45Secure.Commons.Recursos;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.DataAccess.Entidades.Comparador;
using Zero.Ado.Models;
using AM45Secure.DataAccess.Entidades.Comunes;
using System.Data.SqlClient;
using AM45Secure.Commons.Constantes.Comunes;
using System.Data;
using System.Text;
using AM45Secure.Commons.Constantes.Querys;
using AM45Secure.DataAccess.Entidades.Cotizador;
using AM45Secure.Commons.Modelos.comunes;
using AM45Secure.Commons.Modelos.Tickets;

namespace AM45Secure.DataAccess.DataAccess.Comparador
{
    public class ComparadorDataAccess : IComparadorDataAccess
    {
        private readonly IGenericDataAccess iGenericDataAccess;
        int cotizable;

        public ComparadorDataAccess(IGenericDataAccess iGenericDataAccess)
        {
            this.iGenericDataAccess = iGenericDataAccess;
        }

        /*********************************************************************
         * Metodo para consultar los datos de solicitud de la cotizacion(SolicitudCotizacion)
         */

        public IList<SolicitudCotizacionModel> ConsultarSolicitudCotizacion(DatosSolicitudModel datosCotizacionModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<VwCotSelSolicitudCotizacionServTipUnidad> tSolicitudCotizacion = iGenericDataAccess.Consultar(
                                                                                                                    new VwCotSelSolicitudCotizacionServTipUnidad()
                                                                                                                    {
                                                                                                                        SolicitudId = datosCotizacionModel.SolicitudId
                                                                                                                    },
                                                                                                                    new OptionsQueryZero()
                                                                                                                    {
                                                                                                                        ExcludeNumericsDefaults = true,
                                                                                                                        ExcludeBool = true
                                                                                                                    });
                iGenericDataAccess.CloseConnection();
                IList<SolicitudCotizacionModel> solicitudCotizacionList = tSolicitudCotizacion.Select(
                                                                                                      x => new SolicitudCotizacionModel()
                                                                                                      {
                                                                                                          SolicitudId = x.SolicitudId,
                                                                                                          ProductoId = x.ProductoId,
                                                                                                          ClienteId = x.ClienteId,
                                                                                                          TipoVehiculoId = x.TipoVehiculoId,
                                                                                                          Renovacion = x.Renovacion,
                                                                                                          ServicioId = x.ServicioId,
                                                                                                          ValorFactura = x.ValorFactura,
                                                                                                          UsoId = x.UsoId,
                                                                                                          Modelo = x.Modelo,
                                                                                                          Marca = x.Marca,
                                                                                                          SubMarca = x.SubMarca,
                                                                                                          Descripcion = x.Descripcion,
                                                                                                          EstadoCirculacion = x.EstadoCirculacion,
                                                                                                          EstadoCirculacionId = x.EstadoCirculacionId,
                                                                                                          Plazo = x.Plazo,
                                                                                                          MonedaId = x.MonedaId,
                                                                                                          LoJack = x.LoJack,
                                                                                                          AgenciaId = x.AgenciaId,
                                                                                                          UsuarioId = x.UsuarioId,
                                                                                                          SumaEEspecial = x.SumaeEspicial,
                                                                                                          DescripcionEe = x.DescripcionEe,
                                                                                                          SumaAdaptaciones = x.SumaAdaptaciones,
                                                                                                          DescripcionAdaptaciones = x.DescripcionAdaptaciones,
                                                                                                          CotizacionId = x.CotizacionId,
                                                                                                          ClaveVehiculoMarsh = x.ClaveVehiculoMarsh,
                                                                                                          InicioVigencia = x.InicioVigencia,
                                                                                                          FinVigencia = x.FinVigencia,
                                                                                                          Derechos = x.Derechos,
                                                                                                          Ocupantes = x.Ocupantes,
                                                                                                          Deducible = x.Deducible,
                                                                                                          DeducibleOpcion = x.DeducibleOpcion,
                                                                                                          DeducibleDm = x.DeducibleDm,
                                                                                                          DeducibleRt = x.DeducibleRt,
                                                                                                          TarifaIdProducto = x.TarifaIdProducto,
                                                                                                          PorcentajeUdi = x.PorcentajeUdi,
                                                                                                          Gnp = x.Gnp,
                                                                                                          Qlt = x.Qlt,
                                                                                                          Rsa = x.Rsa,
                                                                                                          Mfr = x.Mfr,
                                                                                                          Axa = x.Axa,
                                                                                                          Udis = x.Udi,
                                                                                                          Kilometraje = x.Kilometraje,
                                                                                                          Bbva = x.Bbva,
                                                                                                          Aba = x.Aba,
                                                                                                          Hdi = x.Hdi,
                                                                                                          Zurich = x.Zurich,
                                                                                                          CotizaWssf = x.CotizaWssf,
                                                                                                          TipoPago = x.TipoPago,
                                                                                                          PolizaRen = x.PolizaRen,
                                                                                                          IncisoRen = x.IncisoRen,
                                                                                                          TipoCarga = x.TipoCarga,
                                                                                                          Remolques = x.Remolques,
                                                                                                          TipoArrendamiento = x.TipoArrendamiento,
                                                                                                          Servicio = x.Servicio,
                                                                                                          IdServicio = x.IdServicio,
                                                                                                          TipoVehiculo = x.TipoVehiculo,
                                                                                                          IdTipoVehiculo = x.IdTipoVehiculo,
                                                                                                          Flexible = x.Flexible,
                                                                                                          CodigoPostal = x.CodigoPostal,
                                                                                                          Pais = x.Pais,
                                                                                                          Estado = x.Estado,
                                                                                                          EstadoId = x.EstadoId,
                                                                                                          Municipio = x.Municipio,
                                                                                                          Delegacion = x.Delegacion,
                                                                                                          Colonia = x.Colonia,
                                                                                                          Asentamiento = x.Asentamiento,
                                                                                                          IdMunicipio = x.IdMunicipio
                                                                                                      }).ToList();
                return solicitudCotizacionList;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        /*********************************************************************
        * @description: Metodo para consultar las formas de pago del producto
        * @params : ProductoId del objeto de solicitud de cotización
        */

        public IList<FormasPagoProductoModel> ConsultarFormasPagoProductos(SolicitudCotizacionModel solicitudCotizacionModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<VwCotSelFormasPagoProducto> formasPagoProductos = iGenericDataAccess.Consultar(
                                                                                                     new VwCotSelFormasPagoProducto()
                                                                                                     {
                                                                                                         ProductoId = solicitudCotizacionModel.ProductoId
                                                                                                     },
                                                                                                     new OptionsQueryZero()
                                                                                                     {
                                                                                                         ExcludeNumericsDefaults = true,
                                                                                                         ExcludeBool = true
                                                                                                     }
                                                                                                    );
                iGenericDataAccess.CloseConnection();
                IList<FormasPagoProductoModel> listFormasPagoProducto = formasPagoProductos.Select(
                                                                                                   x => new FormasPagoProductoModel()
                                                                                                   {
                                                                                                       FormaPagoId = x.FormaPagoId,
                                                                                                       Nombre = x.Nombre
                                                                                                   }).ToList();
                return listFormasPagoProducto;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        public IList<AseguradorasProductoModel> ConsultarAseguradorasProducto(SolicitudCotizacionModel solicitudCotizacionModel, NeCotizacionModel neCotizacion)
        {
            /*Se ejecuta el procedimiento almacenado de la traza del cliente CotSelAseguradorasPorProducto 
              donde recibe parametros el clienteId, productoId y si aplica o no LoJack */

            List<AseguradorasProductoModel> listaAsegProducto = new List<AseguradorasProductoModel>();

            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpCotSelAseguradorasPorProducto
                };
                command.Parameters.Add("@pProductoId", SqlDbType.BigInt).Value = solicitudCotizacionModel.ProductoId;
                command.Parameters.Add("@pClienteID", SqlDbType.BigInt).Value = solicitudCotizacionModel.ClienteId;
                command.Parameters.Add("@pLoJack", SqlDbType.NVarChar).Value = (solicitudCotizacionModel.LoJack) == 33 ? '1' : '0';

                SqlDataReader aseguradorasPorProducto = iGenericDataAccess.StoredProcedure(command);
                if (aseguradorasPorProducto.HasRows)
                {
                    while (aseguradorasPorProducto.Read())
                    {
                        cotizable = ConsultarAsegCotUsuario(solicitudCotizacionModel, Convert.ToInt32(aseguradorasPorProducto["AseguradoraID"]));
                        if (cotizable == 1)
                        {
                            AseguradorasProductoModel aseg = new AseguradorasProductoModel();
                            aseg.ProductoId = Convert.ToInt32(aseguradorasPorProducto["ProductoId"]);
                            aseg.AseguradoraId = Convert.ToInt32(aseguradorasPorProducto["AseguradoraID"]);
                            aseg.ListaPaquetes = ConsultaPaquetes(solicitudCotizacionModel, neCotizacion, aseg.AseguradoraId);
                            listaAsegProducto.Add(aseg);
                        }
                        iGenericDataAccess.OpenConnection();
                    }
                }
            }
            catch (DalException)
            {
                throw;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }

            return listaAsegProducto;
        }

        public int ConsultarAsegCotUsuario(SolicitudCotizacionModel solicitudCotizacionModel, int aseguradoraId)
        {
            /*Se ejecuta el procedimiento almacenado de la traza del cliente AseguradorasCotizablesUsuarios
              donde recibe parametros 
              @UsuarioID = 177080, -- int
              @AseguradoraID = 222, -- int
              @ProductoID = 252 -- int */
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpAseguradorasCotizablesUsuarios
                };
                command.Parameters.Add("@UsuarioID", SqlDbType.BigInt).Value = solicitudCotizacionModel.UsuarioId;
                command.Parameters.Add("@AseguradoraID", SqlDbType.BigInt).Value = aseguradoraId;
                command.Parameters.Add("@ProductoID", SqlDbType.BigInt).Value = solicitudCotizacionModel.ProductoId;

                SqlDataReader aseguradoraCotizable = iGenericDataAccess.StoredProcedure(command);
                if (aseguradoraCotizable.HasRows)
                {
                    while (aseguradoraCotizable.Read())
                    {
                        cotizable = Convert.ToInt32(aseguradoraCotizable[0]);
                    }
                }
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }

            return cotizable;
        }

        /*********************************************************************
        * @description: Metodo que ejecuta los procedimientos almacenados para consultar los paquetes cotizabls por aseguradora
        * @params : SolicitudModel
        */

        /*private List<PaqueteModel> ConsultaPaquetesProductos(SolicitudCotizacionModel solicitudCotizacionModel)
        {
            List<PaqueteModel> listaPaquetes = new List<PaqueteModel>();
            try
            {
                SqlCommand command = new SqlCommand
                                     {
                                         CommandText = ConstStoredProcedures.EtiquetasEncabezados
                                     };
                command.Parameters.Add("@ProductoID", SqlDbType.BigInt).Value = solicitudCotizacionModel.ProductoId;

                SqlDataReader paquetesProductos = iGenericDataAccess.StoredProcedure(command);
                if (paquetesProductos.HasRows)
                {
                    while (paquetesProductos.Read())
                    {
                        PaqueteModel paquete = new PaqueteModel();
                        paquete.ElementoId = Convert.ToInt32(paquetesProductos["ElementoID"]);
                        paquete.Nombre = Convert.ToString(paquetesProductos["Nombre"]);
                        listaPaquetes.Add(paquete);
                    }
                    iGenericDataAccess.CloseConnection();
                }
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
            return listaPaquetes;
        }*/

        public List<PaqueteModel> ConsultaPaquetesCotizable(SolicitudCotizacionModel solicitudCotizacionModel, int aseguradoraId)
        {
            /*
             *  @producto = 252, -- numeric(18, 0)
                @AseguradoraID = 222, -- numeric(18, 0)
                @tipo = '6645', -- varchar(5)
                @tipoAuto = '2634', -- varchar(5)
                @modelo = '2014', -- varchar(10)
                @adicional = '2634' -- varchar(20)
             */
            List<PaqueteModel> listaPaquetes = new List<PaqueteModel>();
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpPaquetesCotizables
                };
                command.Parameters.Add("@Producto", SqlDbType.BigInt).Value = solicitudCotizacionModel.ProductoId;
                command.Parameters.Add("@AseguradoraID", SqlDbType.BigInt).Value = aseguradoraId;
                command.Parameters.Add("@tipo", SqlDbType.NVarChar).Value = solicitudCotizacionModel.UsoId;
                command.Parameters.Add("@tipoAuto", SqlDbType.NVarChar).Value = solicitudCotizacionModel.TipoVehiculoId;
                command.Parameters.Add("@modelo", SqlDbType.NVarChar).Value = solicitudCotizacionModel.Modelo;
                command.Parameters.Add("@adicional", SqlDbType.NVarChar).Value = solicitudCotizacionModel.TipoVehiculoId;

                SqlDataReader paquetesCotizable = iGenericDataAccess.StoredProcedure(command);
                if (paquetesCotizable.HasRows)
                {
                    while (paquetesCotizable.Read())
                    {
                        PaqueteModel paquete = new PaqueteModel();
                        paquete.ElementoId = Convert.ToInt32(paquetesCotizable["paqueteid"]);
                        paquete.Nombre = Convert.ToString(paquetesCotizable["ProductoID"]);
                        listaPaquetes.Add(paquete);
                    }
                    iGenericDataAccess.CloseConnection();
                }
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
            return listaPaquetes;
        }

        private List<PaqueteModel> ConsultaPaquetes(SolicitudCotizacionModel solicitudCotizacionModel, NeCotizacionModel neCotizacion, int aseguradoraId)
        {
            List<PaqueteModel> listaPaquetes = new List<PaqueteModel>();
            List<PaqueteModel> listaCotizable = ConsultaPaquetesCotizable(solicitudCotizacionModel, aseguradoraId);
            if (listaCotizable.Count == 0)
            {
                throw new DalException(CodesBenchmark.ERR_02_02);
            }
            //List<PaqueteModel> listaProductos = ConsultaPaquetesProductos(solicitudCotizacionModel);
            Dictionary<int, string> paqueDictionary = ConsultaNombrePaqueteComparador(listaCotizable, solicitudCotizacionModel.ProductoId);
            Dictionary<int, string> paqueDictionaryCompleto = ConsultaNombrePaqueteComparadorCompleto(listaCotizable);
            foreach (PaqueteModel pk in listaCotizable)
            {
                /*foreach (PaqueteModel pkp in listaProductos)
                {
                    if (pk.ElementoId == pkp.ElementoId)
                    {*/
                PaqueteModel paquete = new PaqueteModel();
                paquete.ElementoId = pk.ElementoId;
                paquete.Nombre = (solicitudCotizacionModel.Flexible) ? ConstTipoPersonas.Paquete : paqueDictionary.ContainsKey(pk.ElementoId) ? paqueDictionary[pk.ElementoId] : paqueDictionaryCompleto[pk.ElementoId];
                paquete.AseguradoraId = aseguradoraId;
                paquete.Flexible = solicitudCotizacionModel.Flexible;
                //paquete = (!solicitudCotizacionModel.Flexible) ? ConsultaCoberturaPaqueteAsegCot(paquete, solicitudCotizacionModel, neCotizacion) : ConsultaCoberturaPaqueteAsegFlex(paquete, solicitudCotizacionModel, neCotizacion);
                paquete = ConsultaCoberturaPaqueteAsegNuevo(paquete, solicitudCotizacionModel, neCotizacion);
                if (paquete != null)
                {
                    paquete.ElementoId = pk.ElementoId;
                    paquete.Nombre = (solicitudCotizacionModel.Flexible) ? ConstTipoPersonas.Paquete : paqueDictionary.ContainsKey(pk.ElementoId) ? paqueDictionary[pk.ElementoId] : paqueDictionaryCompleto[pk.ElementoId];
                    paquete.AseguradoraId = aseguradoraId;
                    paquete.Flexible = solicitudCotizacionModel.Flexible;
                    listaPaquetes.Add(paquete);
                }

                /*break;
            }
        }*/
            }
            return listaPaquetes;
        }

        public int ConsultaPaquetesAseguradoras(int aseguradoraId, int paqueteId)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<NePaquetesAseguradora> aseguradoraPaquetes = iGenericDataAccess.Consultar(
                                                                                                new NePaquetesAseguradora()
                                                                                                {
                                                                                                    AseguradoraId = aseguradoraId,
                                                                                                    PaqueteId = paqueteId
                                                                                                },
                                                                                                new OptionsQueryZero()
                                                                                                {
                                                                                                    ExcludeNumericsDefaults = true,
                                                                                                    ExcludeBool = true
                                                                                                }
                                                                                               );
                iGenericDataAccess.CloseConnection();
                IList<NePaquetesAseguradora> listAseguradoraPaquete = aseguradoraPaquetes.Select(
                                                                                                 x => new NePaquetesAseguradora()
                                                                                                 {
                                                                                                     AseguradoraId = x.AseguradoraId,
                                                                                                     PaqueteId = x.PaqueteId
                                                                                                 }).ToList();
                if (listAseguradoraPaquete.Count > 0)
                {
                    return listAseguradoraPaquete[0].AseguradoraId;
                }
                return 0;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        public NeCotizacionModel ConsultarNeCotizacion(CotizarModel cotizarModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<NeCotizacion> neCotizacion = iGenericDataAccess.Consultar(
                                                                                new NeCotizacion()
                                                                                {
                                                                                    FolioSolicitud = cotizarModel.DatosCotizacionModel.SolicitudId,
                                                                                    FormaPagoId = cotizarModel.DatosCotizacionModel.FormaPagoId
                                                                                },
                                                                                new OptionsQueryZero()
                                                                                {
                                                                                    ExcludeNumericsDefaults = true,
                                                                                    ExcludeBool = true
                                                                                }
                                                                               );
                iGenericDataAccess.CloseConnection();
                IList<NeCotizacionModel> listaNeCotizacion = neCotizacion.Select(
                                                                                 x => new NeCotizacionModel()
                                                                                 {
                                                                                     CotizacionId = x.CotizacionId,
                                                                                     FechaRegistro = x.FechaRegistro,
                                                                                     PolizaAnterior = x.PolizaAnterior,
                                                                                     FormaPagoId = x.FormaPagoId,
                                                                                     MonedaId = x.MonedaId,
                                                                                     InicioVigencia = x.InicioVigencia,
                                                                                     FinVigencia = x.FinVigencia,
                                                                                     Marca = x.Marca,
                                                                                     Submarca = x.Submarca,
                                                                                     Vehiculo = x.Vehiculo,
                                                                                     Modelo = x.Modelo,
                                                                                     EstadoCirculacion = x.EstadoCirculacion,
                                                                                     ValorFactura = x.ValorFactura,
                                                                                     Ocupantes = x.Ocupantes,
                                                                                     DiasVigencia = x.DiasVigencia,
                                                                                     StatusId = x.StatusId,
                                                                                     AgenciaId = x.AgenciaId,
                                                                                     ClienteId = x.ClienteId,
                                                                                     UsuarioId = x.UsuarioId,
                                                                                     ClaveVehiculo = x.ClaveVehiculo,
                                                                                     Poliza = x.Poliza,
                                                                                     EjecutivoId = x.EjecutivoId,
                                                                                     BitacoraCargaId = x.BitacoraCargaId,
                                                                                     PlazoSeguro = x.PlazoSeguro,
                                                                                     PlazoCredito = x.PlazoCredito,
                                                                                     FolioOriginal = x.FolioOriginal,
                                                                                     Uso = x.Uso,
                                                                                     Servicio = x.Servicio,
                                                                                     TipoVehiculo = x.TipoVehiculo,
                                                                                     EstadoVehiculo = x.EstadoVehiculo,
                                                                                     EEspecial = x.EEspecial,
                                                                                     Adaptaciones = x.Adaptaciones,
                                                                                     FlotillaId = x.FlotillaId,
                                                                                     GrupoId = x.GrupoId,
                                                                                     GrupoEconomico = x.GrupoEconomico,
                                                                                     LoJack = x.LoJack,
                                                                                     VehiculoLegalizado = x.VehiculoLegalizado,
                                                                                     Renovacion = x.Renovacion,
                                                                                     InformacionLegalizado = x.InformacionLegalizado,
                                                                                     FolioSolicitud = x.FolioSolicitud,
                                                                                     SeguroGratis = x.SeguroGratis,
                                                                                     Kilometraje = x.Kilometraje,
                                                                                     DiasPlazoAbierto = x.DiasPlazoAbierto,
                                                                                     CotizaWssf = x.CotizaWssf,
                                                                                     ClavePlan = x.ClavePlan,
                                                                                     //DeduciblesConfigurables = x.DeduciblesConfigurables,
                                                                                     //CoberturaSa = x.CoberturaSa,
                                                                                     //SumaAseguradaCobertura = x.SumaAseguradaCobertura,
                                                                                     TipoCarga = x.TipoCarga,
                                                                                     Remolques = x.Remolques,
                                                                                     TipoArrendamiento = x.TipoArrendamiento,
                                                                                     NumeroFactura = x.NumeroFactura,
                                                                                     FechaFactura = x.FechaFactura,
                                                                                     ParametrosMapfre = x.ParametrosMapfre
                                                                                 }).ToList();
                if (listaNeCotizacion.Count > 0)
                    return listaNeCotizacion[0];
                else
                    return null;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        /*********************************************************************
        * @description: Metodo para consultar las coberturas de los productos clasicos
        * @params : SolicitudModel
        */

        /*private PaqueteModel ConsultaCoberturaPaqueteAseg(PaqueteModel pkt, SolicitudCotizacionModel solicitud, NeCotizacionModel neCotizacion)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<VwCotSelCobPaqAsegProd> cobPaqAseg = iGenericDataAccess.Consultar(CQuerysCotizador.QryCobPaqAsegProd,
                                                                                        new VwCotSelCobPaqAsegProd()
                                                                                        {
                                                                                            CotizacionId = neCotizacion.CotizacionId,
                                                                                            AseguradoraId = pkt.AseguradoraId,
                                                                                            PaqueteId = pkt.ElementoId,
                                                                                            ProductoId = solicitud.ProductoId
                                                                                        },
                                                                                        new OptionsQueryZero()
                                                                                        {
                                                                                            ExcludeNumericsDefaults = true,
                                                                                            ExcludeBool = true
                                                                                        }
                                                                                       );
                iGenericDataAccess.CloseConnection();
                IList<PaqueteModel> paquete = cobPaqAseg.GroupBy(paq => new
                                                                        {
                                                                            paq.PaqueteId,
                                                                            paq.Numero,
                                                                            paq.PrimaTotal,
                                                                            paq.CotizacionId
                                                                        }, (paq, group) => new
                                                                                           {
                                                                                               PaqueteIdKey = paq.PaqueteId,
                                                                                               NumeroKey = paq.Numero,
                                                                                               PrimaTotalKey = paq.PrimaTotal,
                                                                                               CotizacionIdKey = paq.CotizacionId
                                                                                           }).Select(x => new PaqueteModel()
                                                                                                          {
                                                                                                              ElementoId = x.PaqueteIdKey,
                                                                                                              Numero = x.NumeroKey,
                                                                                                              Monto = x.PrimaTotalKey,
                                                                                                              CotizacionId = neCotizacion.CotizacionId,
                                                                                                              ListaCoberturasModel = cobPaqAseg.Select(
                                                                                                                                                       y => new CoberturacModel()
                                                                                                                                                            {
                                                                                                                                                                AseguradoraId = y.AseguradoraId,
                                                                                                                                                                PaqueteId = y.PaqueteId,
                                                                                                                                                                CoberturaId = y.CoberturaId,
                                                                                                                                                                TipoId = y.TipoId,
                                                                                                                                                                Nombre = y.Nombre,
                                                                                                                                                                Homologacion = y.Homologacion,
                                                                                                                                                                SumaAsegurada = y.SumaAseguradaC,
                                                                                                                                                                SumaAseguradaInicial = y.SumaAseguradaInicialC,
                                                                                                                                                                Deducible = y.DeducibleC,
                                                                                                                                                                PrimaNeta = y.PrimaNetaC
                                                                                                                                                            }).ToList()
                                                                                                          }).ToList();

                if (paquete.Count>0)
                    return paquete[0];

                return null;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
        }*/

        /*private PaqueteModel ConsultaCoberturaPaqueteAsegFlex(PaqueteModel pkt, SolicitudCotizacionModel solicitud, NeCotizacionModel neCotizacion)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<VwCotSelCobPaqAsegProd> cobPaqAseg = iGenericDataAccess.Consultar(CQuerysCotizador.QryCobPaqAsegProdFlex,
                                                                                        new VwCotSelCobPaqAsegProd()
                                                                                        {
                                                                                            CotizacionId = neCotizacion.CotizacionId,
                                                                                            IdServicio = solicitud.ServicioId,
                                                                                            IdAntiguedad = solicitud.TipoVehiculoId,
                                                                                            IdTipoVehiculo = solicitud.UsoId,
                                                                                            PaqueteId = pkt.ElementoId,
                                                                                            AseguradoraId = pkt.AseguradoraId,
                                                                                            ProductoId = solicitud.ProductoId
                                                                                        },
                                                                                        new OptionsQueryZero()
                                                                                        {
                                                                                            ExcludeNumericsDefaults = true,
                                                                                            ExcludeBool = true
                                                                                        }
                                                                                       );
                iGenericDataAccess.CloseConnection();
                IList<PaqueteModel> paquete = cobPaqAseg.GroupBy(paq => new
                                                                        {
                                                                            paq.PaqueteId,
                                                                            paq.Numero,
                                                                            paq.PrimaTotal,
                                                                            paq.CotizacionId
                                                                        }, (paq, group) => new
                                                                                           {
                                                                                               PaqueteIdKey = paq.PaqueteId,
                                                                                               NumeroKey = paq.Numero,
                                                                                               PrimaTotalKey = paq.PrimaTotal,
                                                                                               CotizacionIdKey = paq.CotizacionId
                                                                                           }).Select(x => new PaqueteModel()
                                                                                                          {
                                                                                                              ElementoId = x.PaqueteIdKey,
                                                                                                              Numero = x.NumeroKey,
                                                                                                              Monto = x.PrimaTotalKey,
                                                                                                              CotizacionId = neCotizacion.CotizacionId,
                                                                                                              ListaCoberturasModel = cobPaqAseg.Select(
                                                                                                                                                       y => new CoberturacModel()
                                                                                                                                                            {
                                                                                                                                                                AseguradoraId = y.AseguradoraId,
                                                                                                                                                                PaqueteId = y.PaqueteId,
                                                                                                                                                                CoberturaId = y.CoberturaId,
                                                                                                                                                                TipoId = y.TipoId,
                                                                                                                                                                Nombre = y.Nombre,
                                                                                                                                                                Homologacion = y.Homologacion,
                                                                                                                                                                SumaAsegurada = y.SumaAseguradaC,
                                                                                                                                                                SumaAseguradaInicial = y.SumaAseguradaInicialC,
                                                                                                                                                                Deducible = y.DeducibleC,
                                                                                                                                                                PrimaNeta = y.PrimaNetaC,
                                                                                                                                                                Detalle = y.Detalle,
                                                                                                                                                                Tooltip = y.Tooltip
                                                                                                                                                            }).ToList()
                                                                                                          }).ToList();

                if (paquete.Count>0)
                    return paquete[0];
                else
                    return null;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
        }*/

        /*********************************************************************
        * @description: Metodos para armar la cabecera de solicitud cotizacion
        * @params : IdSolicitud
        */

        public CotizanteModel ConsultarDatosCotizante(SolicitudCotizacionModel solicitudCotizacionModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<DatosCotizante> datosCotizantes = iGenericDataAccess.Consultar(
                                                                                     new DatosCotizante()
                                                                                     {
                                                                                         IdSolicitud = solicitudCotizacionModel.SolicitudId
                                                                                     },
                                                                                     new OptionsQueryZero()
                                                                                     {
                                                                                         ExcludeNumericsDefaults = true,
                                                                                         ExcludeBool = true
                                                                                     }
                                                                                    );
                iGenericDataAccess.CloseConnection();
                IList<CotizanteModel> listDatosCotizante = datosCotizantes.Select(
                                                                                  x => new CotizanteModel()
                                                                                  {
                                                                                      Nombre = x.Nombre,
                                                                                      RazonSocial = x.RazonSocial,
                                                                                      ApellidoP = x.Paterno,
                                                                                      ApellidoM = x.Materno,
                                                                                      Telefono = x.Telefono,
                                                                                      CorreoElectronico = x.Correo,
                                                                                      TipoPersona = x.TipoPersona
                                                                                  }).ToList();
                if (listDatosCotizante.Count > 0)
                    return listDatosCotizante[0];
                else
                    return null;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        public ClientesModel ConsultarDatosCliente(SolicitudCotizacionModel solicitudCotizacionModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<VwCotSelClientesUsuario> clienteCot = iGenericDataAccess.Consultar(
                                                                                         new VwCotSelClientesUsuario()
                                                                                         {
                                                                                             Id = solicitudCotizacionModel.ClienteId.ToString()
                                                                                         },
                                                                                         new OptionsQueryZero()
                                                                                         {
                                                                                             ExcludeNumericsDefaults = true,
                                                                                             ExcludeBool = true
                                                                                         }
                                                                                        );
                iGenericDataAccess.CloseConnection();
                IList<ClientesModel> listDatosCliente = clienteCot.Select(
                                                                          x => new ClientesModel()
                                                                          {
                                                                              ClienteId = Convert.ToInt32(x.Id),
                                                                              Cliente = x.Nombre
                                                                          }).ToList();
                if (listDatosCliente.Count > 0)
                    return listDatosCliente[0];
                else
                    return null;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        public ProductoModel ConsultarDatosProducto(SolicitudCotizacionModel solicitudCotizacionModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<VwCotSelProductosClienteUsuario> productoCot = iGenericDataAccess.Consultar(CQuerysCotizador.QryProductosClienteUsuarioProducto,
                                                                                                  new VwCotSelProductosClienteUsuario()
                                                                                                  {
                                                                                                      ClienteId = solicitudCotizacionModel.ClienteId.ToString(),
                                                                                                      UsuarioId = solicitudCotizacionModel.GetIdUsuarioSesion().ToString(),
                                                                                                      PerfilId = solicitudCotizacionModel.GetIdPerfilUsuarioSesion().ToString(),
                                                                                                      ProductoId = solicitudCotizacionModel.ProductoId.ToString()
                                                                                                  },
                                                                                                  new OptionsQueryZero()
                                                                                                  {
                                                                                                      ExcludeNumericsDefaults = true,
                                                                                                      ExcludeBool = true
                                                                                                  }
                                                                                                 );
                iGenericDataAccess.CloseConnection();
                IList<ProductoModel> listDatosProducto = productoCot.Select(
                                                                            x => new ProductoModel()
                                                                            {
                                                                                ProductoId = Convert.ToInt32(x.ProductoId),
                                                                                NombreProducto = x.Producto,
                                                                                Flexible = Convert.ToDouble(solicitudCotizacionModel.Flexible),
                                                                                Cp = x.Cp
                                                                            }).ToList();
                if (listDatosProducto.Count > 0)
                    return listDatosProducto[0];
                else
                    return null;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        /*********************************************************************
        * @description: Metodos para la creación del reporte
        * @params : CotizacionID y si es flexible
        */

        public List<ReporteCotizacionModel> ConsultarCotizacionRep(RepCotizacionModel repParams)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                List<ReporteCotizacionModel> listReporte = new List<ReporteCotizacionModel>();

                IList<VwCotSelRepCotizacion> reporte = iGenericDataAccess.Consultar(CQuerysCotizador.QrySolicitudReporte,
                                                                                    new VwCotSelRepCotizacion()
                                                                                    {
                                                                                        SolicitudId = repParams.SoliciudId,
                                                                                        CotizacionId = repParams.CotizacionId,
                                                                                        Numero = repParams.Numero
                                                                                    },
                                                                                    new OptionsQueryZero()
                                                                                    {
                                                                                        ExcludeNumericsDefaults = true,
                                                                                        ExcludeBool = true
                                                                                    }
                                                                                   );
                iGenericDataAccess.CloseConnection();
                ReporteCotizacionModel rep = reporte.Select(
                                                            x => new ReporteCotizacionModel()
                                                            {
                                                                AseguradoraId = x.AseguradoraId,
                                                                Marca = x.Marca,
                                                                MondaId = x.MondaId,
                                                                CotizacionId = x.CotizacionId,
                                                                FechaRegistro = x.FechaRegistro.ToString("dd/MM/yyyy HH:mm:ss"),
                                                                Nombre = x.Nombre,
                                                                PrimaNeta = x.PrimaNeta,
                                                                Recargo = x.Recargo,
                                                                Derechos = x.Derechos,
                                                                Iva = x.Iva,
                                                                PrimaTotal = x.PrimaTotal,
                                                                RecargoFraccionado = x.RecargoFraccionado,
                                                                ClaveAseg = x.ClaveAseg,
                                                                Subtotal = x.PrimaTotal - x.Iva,
                                                                PaqueteId = x.PaqueteId,
                                                                EstadoCirculacion = x.EstadoCirculacion,
                                                                ClaveVehiculoMarsh = x.ClaveVehiculoMarsh
                                                            }).First();
                DatosSolicitudModel datos = new DatosSolicitudModel();
                datos.SolicitudId = repParams.SoliciudId;
                IList<SolicitudCotizacionModel> listSolicitud = ConsultarSolicitudCotizacion(datos);
                if (listSolicitud.Count == 0 || listSolicitud == null)
                {
                    throw new DalException(CodesBenchmark.ERR_02_05);
                }
                SolicitudCotizacionModel solicitudCotizacionModel = listSolicitud[0];
                List<PaqueteModel> listaCotizable = ConsultaPaquetesCotizable(solicitudCotizacionModel, rep.AseguradoraId);
                Dictionary<int, string> paqueDictionary = ConsultaNombrePaqueteComparador(listaCotizable, solicitudCotizacionModel.ProductoId);
                Dictionary<int, string> paqueDictionaryCompleto = ConsultaNombrePaqueteComparadorCompleto(listaCotizable);
                rep.PaqueteN = (solicitudCotizacionModel.Flexible) ? ConstTipoPersonas.Paquete : paqueDictionary.ContainsKey(rep.PaqueteId) ? paqueDictionary[rep.PaqueteId] : paqueDictionaryCompleto[rep.PaqueteId];
                listReporte.Add(rep);
                return listReporte;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        public List<ReporteCoberturasModel> ConsultaCoberturasReporte(RepCotizacionModel repParams)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                if (repParams.Flexible)
                {
                    DatosSolicitudModel datos = new DatosSolicitudModel();
                    datos.SolicitudId = repParams.SoliciudId;
                    IList<SolicitudCotizacionModel> soli = ConsultarSolicitudCotizacion(datos);
                    SolicitudCotizacionModel solicitud = (soli.Count > 0) ? soli[0] : null;
                    if (solicitud != null)
                    {
                        iGenericDataAccess.OpenConnection();
                        IList<VwCotSelCobPaqAsegProd> cobClasico = iGenericDataAccess.Consultar(CQuerysCotizador.QryCobPaqAsegPrdoFRep,
                                                                                                new VwCotSelCobPaqAsegProd()
                                                                                                {
                                                                                                    CotizacionId = repParams.CotizacionId,
                                                                                                    PaqueteId = repParams.PaqueteId,
                                                                                                    Numero = repParams.Numero,
                                                                                                    IdServicio = solicitud.ServicioId,
                                                                                                    IdAntiguedad = solicitud.TipoVehiculoId,
                                                                                                    IdTipoVehiculo = solicitud.UsoId,
                                                                                                    ProductoId = solicitud.ProductoId
                                                                                                },
                                                                                                new OptionsQueryZero()
                                                                                                {
                                                                                                    ExcludeNumericsDefaults = true,
                                                                                                    ExcludeBool = true
                                                                                                }
                                                                                               );
                        iGenericDataAccess.CloseConnection();
                        List<ReporteCoberturasModel> listCoberturasClasico = cobClasico.Select(
                                                                                               x => new ReporteCoberturasModel()
                                                                                               {
                                                                                                   CotizacionId = x.CotizacionId,
                                                                                                   PaqueteN = (repParams.Flexible) ? ConstTipoPersonas.Paquete : x.Paquete,
                                                                                                   Nombre = x.Nombre,
                                                                                                   SumaAsegurada = x.SumaAsegurada,
                                                                                                   PrimaNeta = x.PrimaNeta,
                                                                                                   Deducible = x.Deducible,
                                                                                                   SumaAseguradaC = x.SumaAseguradaC.Split('.')[0],
                                                                                                   PrimaNetaC = x.PrimaNetaC,
                                                                                                   DeducibleC = x.DeducibleC,
                                                                                                   Derechos = x.Derechos,
                                                                                                   Iva = x.Iva,
                                                                                                   PrimaTotal = x.PrimaTotal,
                                                                                                   RecargoFraccionado = x.RecargoFraccionado,
                                                                                                   Homologacion = (x.Homologacion == string.Empty) ? x.Nombre : x.Homologacion
                                                                                               }).ToList();
                        return listCoberturasClasico;
                    }
                    return null;
                }
                else
                {
                    iGenericDataAccess.OpenConnection();
                    IList<VwCotSelCobPaqAsegProd> cobClasico = iGenericDataAccess.Consultar(CQuerysCotizador.QryCobPaqAsegPrdoCRep,
                                                                                            new VwCotSelCobPaqAsegProd()
                                                                                            {
                                                                                                CotizacionId = repParams.CotizacionId,
                                                                                                PaqueteId = repParams.PaqueteId,
                                                                                                Numero = repParams.Numero
                                                                                            },
                                                                                            new OptionsQueryZero()
                                                                                            {
                                                                                                ExcludeNumericsDefaults = true,
                                                                                                ExcludeBool = true
                                                                                            }
                                                                                           );
                    iGenericDataAccess.CloseConnection();
                    List<ReporteCoberturasModel> listCoberturasClasico = cobClasico.Select(
                                                                                           x => new ReporteCoberturasModel()
                                                                                           {
                                                                                               CotizacionId = x.CotizacionId,
                                                                                               Nombre = x.Nombre,
                                                                                               PaqueteN = x.Paquete,
                                                                                               SumaAsegurada = x.SumaAsegurada,
                                                                                               PrimaNeta = x.PrimaNeta,
                                                                                               Iva = x.Iva,
                                                                                               PrimaTotal = x.PrimaTotal,
                                                                                               Deducible = x.Deducible,
                                                                                               SumaAseguradaC = x.SumaAseguradaC,
                                                                                               PrimaNetaC = x.PrimaNetaC,
                                                                                               DeducibleC = x.DeducibleC,
                                                                                               Derechos = x.Derechos,
                                                                                               RecargoFraccionado = x.RecargoFraccionado,
                                                                                               Homologacion = (x.Homologacion == string.Empty) ? x.Nombre : x.Homologacion
                                                                                           }).ToList();
                    return listCoberturasClasico;
                }
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        public Dictionary<int, string> ConsultaNombrePaqueteComparador(List<PaqueteModel> idsPaquetes, int idProducto)
        {
            Dictionary<int, string> paquetes = new Dictionary<int, string>();
            string sql = "SELECT Valor as Nombre , Campo12 AS ElementoID FROM dbo.ValoresReglas where ReglaID IN(SELECT ElementoID from dbo.Elementos where CatalogoID = 66 AND IdInterno = 171) AND Campo4 = " + idProducto + "  AND Campo12 in ( ";
            try
            {
                iGenericDataAccess.OpenConnection();
                foreach (PaqueteModel idPaquete in idsPaquetes)
                {
                    sql += idPaquete.ElementoId + ",";
                }
                sql = sql.Substring(0, sql.Length - 1);
                sql += ")";
                IList<Elementos> paquetesEle = iGenericDataAccess.ExecuteQuery<Elementos>(sql);
                iGenericDataAccess.CloseConnection();
                foreach (Elementos paq in paquetesEle)
                {
                    paquetes.Add(paq.ElementoId, paq.Nombre);
                }
            }
            catch (Exception e)
            {
                throw new DalException(CodesBenchmark.ERR_02_01, e);
            }
            return paquetes;
        }

        public Dictionary<int, string> ConsultaNombrePaqueteComparadorCompleto(List<PaqueteModel> idsPaquetes)
        {
            Dictionary<int, string> paquetes = new Dictionary<int, string>();
            string sql = "SELECT ElementoID, Nombre FROM dbo.Elementos WHERE CatalogoID = 124 AND ElementoID in ( ";
            try
            {
                iGenericDataAccess.OpenConnection();
                foreach (PaqueteModel idPaquete in idsPaquetes)
                {
                    sql += idPaquete.ElementoId + ",";
                }
                sql = sql.Substring(0, sql.Length - 1);
                sql += ")";
                IList<Elementos> paquetesEle = iGenericDataAccess.ExecuteQuery<Elementos>(sql);
                iGenericDataAccess.CloseConnection();
                foreach (Elementos paq in paquetesEle)
                {
                    paquetes.Add(paq.ElementoId, paq.Nombre);
                }
            }
            catch (Exception e)
            {
                throw new DalException(CodesBenchmark.ERR_02_01, e);
            }
            return paquetes;
        }


        /*********************************************************************
        * @description: Metodo para consultar las coberturas de los productos clasicos
        * @params : SolicitudModel
        */

        private PaqueteModel ConsultaCoberturaPaqueteAsegNuevo(PaqueteModel pkt, SolicitudCotizacionModel solicitud, NeCotizacionModel neCotizacion)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<NePrimasCotizacion> primas = iGenericDataAccess.Consultar(new NePrimasCotizacion
                {
                    CotizacionId = neCotizacion.CotizacionId,
                    AseguradoraId = pkt.AseguradoraId,
                    PaqueteId = pkt.ElementoId,
                    ProductoId = solicitud.ProductoId
                }, new OptionsQueryZero()
                {
                    ExcludeNumericsDefaults = true
                });
                PaqueteModel paqueteModel = null;
                foreach (NePrimasCotizacion prima in primas)
                {
                    IList<SpEmiSelDetalleCoberturasIndividuales> coberturas = iGenericDataAccess.ExecuteStoredProcedure(new SpEmiSelDetalleCoberturasIndividuales
                    {
                        PCotizacionId = neCotizacion.CotizacionId.ToString(),
                        PNumero = prima.Numero.ToString()
                    });

                    paqueteModel = new PaqueteModel
                    {
                        ElementoId = prima.PaqueteId,
                        Numero = prima.Numero,
                        Monto = prima.PrimaTotal,
                        CotizacionId = neCotizacion.CotizacionId,
                        ListaCoberturasModel = coberturas.Select(
                                                                                y => new CoberturacModel()
                                                                                {
                                                                                    AseguradoraId = prima.AseguradoraId,
                                                                                    PaqueteId = prima.PaqueteId,
                                                                                    CoberturaId = y.CoberturaId,
                                                                                    //TipoId = y.TipoId,
                                                                                    Nombre = y.Cobertura,
                                                                                    Homologacion = y.Cobertura,
                                                                                    SumaAsegurada = y.SumaAsegurada,
                                                                                    SumaAseguradaInicial = y.SumaAsegurada,
                                                                                    Deducible = y.Deducible,
                                                                                    PrimaNeta = Convert.ToDecimal(y.PrimaNeta),
                                                                                    Detalle = (string.IsNullOrEmpty(y.Detalle) || y.Detalle == "NULL") ? "" : y.Detalle
                                                                                }).ToList()
                    };
                }
                iGenericDataAccess.CloseConnection();
                return paqueteModel;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        public List<ReporteCoberturasModel> ConsultaCoberturasReporteNuevo(RepCotizacionModel repParams)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<SpEmiSelDetalleCoberturasIndividuales> coberturas = iGenericDataAccess.ExecuteStoredProcedure(new SpEmiSelDetalleCoberturasIndividuales
                {
                    PCotizacionId = repParams.CotizacionId.ToString(),
                    PNumero = repParams.Numero.ToString()
                });
                iGenericDataAccess.CloseConnection();

                List<ReporteCoberturasModel> listCoberturasClasico = coberturas.Select(
                                                                                       x => new ReporteCoberturasModel()
                                                                                       {
                                                                                           CotizacionId = x.CotizacionId,
                                                                                           Nombre = x.Cobertura,
                                                                                           //PaqueteN = x.Paquete,
                                                                                           SumaAsegurada = x.SumaAsegurada,
                                                                                           PrimaNeta = Convert.ToDecimal(x.PrimaNeta),
                                                                                           //Iva = x.Iva,
                                                                                           //PrimaTotal = x.PrimaTotal,
                                                                                           Deducible = x.Deducible,
                                                                                           SumaAseguradaC = x.SumaAsegurada,
                                                                                           PrimaNetaC = Convert.ToDecimal(x.PrimaNeta),
                                                                                           //DeducibleC = x.DeducibleC,
                                                                                           DeducibleC = x.Deducible,
                                                                                           //Derechos = x.Derechos,
                                                                                           //RecargoFraccionado = x.RecargoFraccionado,
                                                                                           //Homologacion = (x.Homologacion == string.Empty) ? x.Nombre : x.Homologacion
                                                                                           Homologacion = x.Cobertura
                                                                                       }).ToList();
                return listCoberturasClasico;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        public void ActualizaTipoArrendamientoCargaRemolquesCoptizacion(SolicitudCotizacionModel solicitudCotizacionModel,
                                                                        int cotizacionId)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                NeCotizacion neCotizacion = iGenericDataAccess.Consultar(new NeCotizacion()
                {
                    CotizacionId = cotizacionId
                }, new OptionsQueryZero()
                {
                    ExcludeNumericsDefaults = true,
                    ExcludeBool = true
                })[0];
                neCotizacion.TipoCarga = solicitudCotizacionModel.TipoCarga;
                neCotizacion.Ocupantes = solicitudCotizacionModel.Ocupantes;
                neCotizacion.TipoArrendamiento = solicitudCotizacionModel.TipoArrendamiento;
                neCotizacion.Remolques = solicitudCotizacionModel.Remolques;

                iGenericDataAccess.Actualizar(neCotizacion);
                iGenericDataAccess.CloseConnection();
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesBenchmark.ERR_02_03, e);
            }
        }

        public string ConsultarNotasImportantes(SolicitudCotizacionModel solicitud)
        {
            try
            {
                string notasImportantes;
                StringBuilder consulta = new StringBuilder();
                if (solicitud.Flexible)
                {
                    consulta.Append("SELECT NotasImportantes FROM dbo.ProductosFlex ");
                    consulta.Append(" WHERE ProductoID = " + solicitud.ProductoId);
                    consulta.Append(" AND IdCondicionVehiculo = " + solicitud.TipoVehiculoId);
                    consulta.Append(" AND IdTipoServicioVehiculo =  " + solicitud.ServicioId);
                    consulta.Append(" AND IdTipoVehiculo =  " + solicitud.UsoId);
                }
                else
                {
                    consulta.Append("SELECT NotasImportantes FROM dbo.Productos");
                    consulta.Append(" WHERE ProductoID = " + solicitud.ProductoId);
                }
                iGenericDataAccess.OpenConnection();
                IList<NotasImportantesE> listNotas = iGenericDataAccess.ExecuteQuery<NotasImportantesE>(consulta.ToString());
                iGenericDataAccess.CloseConnection();
                if (listNotas.Count > 0)
                {
                    notasImportantes = listNotas[0].NotasImportantes;
                }
                else
                {
                    notasImportantes = null;
                }
                return notasImportantes;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        public IList<ElementoModel> ConsultaElementosPorIdInterno(int idInterno, int catalogoId)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<Elementos> elementos = iGenericDataAccess.Consultar(
                                                                          new Elementos()
                                                                          {
                                                                              CatalogoId = catalogoId,
                                                                              IdInterno = idInterno
                                                                          },
                                                                          new OptionsQueryZero()
                                                                          {
                                                                              ExcludeNumericsDefaults = true,
                                                                              ExcludeBool = true
                                                                          });
                iGenericDataAccess.CloseConnection();
                IList<ElementoModel> elementosList = elementos.Select(
                                                                      x => new ElementoModel()
                                                                      {
                                                                          CatalogoId = x.CatalogoId,
                                                                          ElementoId = x.ElementoId,
                                                                          IdInterno = x.IdInterno,
                                                                          Nombre = x.Nombre.ToUpper()
                                                                      }).ToList();
                return elementosList;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesEmision.ERR_00_00, e);
            }
        }

        public AgenciasModel ConsultarDatosAgencia(SolicitudCotizacionModel solicitudCotizacionModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<NePersonas> datosAgencia = iGenericDataAccess.Consultar(
                                                                              new NePersonas()
                                                                              {
                                                                                  PersonaId = solicitudCotizacionModel.AgenciaId
                                                                              },
                                                                              new OptionsQueryZero()
                                                                              {
                                                                                  ExcludeNumericsDefaults = true,
                                                                                  ExcludeBool = true
                                                                              }
                                                                             );
                iGenericDataAccess.CloseConnection();
                IList<AgenciasModel> listDatosAgencia = datosAgencia.Select(
                                                                            x => new AgenciasModel()
                                                                            {
                                                                                IdAgencia = x.PersonaId,
                                                                                Agencia = x.Nombre
                                                                            }).ToList();
                if (listDatosAgencia.Count > 0) return listDatosAgencia[0];

                return null;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        public IList<CoberturaModel> ConsultaCoberturasEspeciales(SolicitudCotizacionModel solicitudCotizacion)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<VwCotSelCoberturasCotizadas> neCoberturasCotizacion = iGenericDataAccess.Consultar(
                                                                                                         new VwCotSelCoberturasCotizadas()
                                                                                                         {
                                                                                                             IdSolicitud = solicitudCotizacion.SolicitudId
                                                                                                         },
                                                                                                         new OptionsQueryZero()
                                                                                                         {
                                                                                                             ExcludeNumericsDefaults = true,
                                                                                                             ExcludeBool = true,
                                                                                                             WhereComplementary = "IsEspecial = 1"
                                                                                                         });
                iGenericDataAccess.CloseConnection();
                IList<CoberturaModel> coberturasList = neCoberturasCotizacion.Select(
                                                                                     x => new CoberturaModel()
                                                                                     {
                                                                                         IdCobertura = x.IdCobertura,
                                                                                         NombreCobertura = x.NombreCobertura,
                                                                                         FiltroValorRangoSuma = x.SumaAsegurada,
                                                                                         FiltroValorRangoDeducible = x.Deducible,
                                                                                         IsEspecial = x.IsEspecial,
                                                                                         IsFija = x.IsBasica,
                                                                                         IsSeleccionada = x.Seleccionada
                                                                                     }).ToList();
                return coberturasList;
            }
            catch (DomainException e)
            {
                throw new DomainException(e.Mensaje);
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        public bool BitacoraEncioCorreo(int cotizacionId, int solicitudId, int numero, List<string> destinatarios, string cotizante, string correoCotizante)
        {

            var destinatariosCadena = string.Join(",", destinatarios);


            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpGrabarBitacoraEmails
                };
                command.Parameters.Add("@cotizacionId", SqlDbType.NVarChar).Value = cotizacionId;
                command.Parameters.Add("@solicitudId", SqlDbType.NVarChar).Value = solicitudId;
                command.Parameters.Add("@numero", SqlDbType.NVarChar).Value = numero;
                command.Parameters.Add("@destinatarios", SqlDbType.NVarChar).Value = destinatariosCadena;
                command.Parameters.Add("@cotizante", SqlDbType.NVarChar).Value = cotizante;
                command.Parameters.Add("@correoCotizante", SqlDbType.NVarChar).Value = correoCotizante;

                SqlDataReader aseguradorasPorProducto = iGenericDataAccess.StoredProcedure(command);
                if (aseguradorasPorProducto.HasRows)
                {


                }
            }
            catch (DalException e)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public PersonaEmail obtenerDatosPersonaEmail(PersonaEmail perosonaEmail)
        {

            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpObtieneDatosCotizanteMailFlex
                };
                command.Parameters.Add("@PersonID", SqlDbType.BigInt).Value = perosonaEmail.PersonaId;


                SqlDataReader personaEmail = iGenericDataAccess.StoredProcedure(command);
                if (personaEmail.HasRows)
                {
                    while (personaEmail.Read())
                    {

                        perosonaEmail.Nombre = Convert.ToString(personaEmail["Nombre"]) + " " + Convert.ToString(personaEmail["Paterno"]) + " "
                            + Convert.ToString(personaEmail["Materno"]) + ":" + Convert.ToString(personaEmail["Alias"]);
                        perosonaEmail.Correo = Convert.ToString(personaEmail["Mail"]);

                    }
                    iGenericDataAccess.CloseConnection();
                }
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }


            return perosonaEmail;
        }

        public IList<ReporteCorresExcel> ConsultarDatosReporteCorreo(int opcionFiltro)
        {
            List<ReporteCorresExcel> listDatos = new List<ReporteCorresExcel>();
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpObtieneDatosReporteEmail
                };
                command.Parameters.Add("@opcion", SqlDbType.BigInt).Value = opcionFiltro;


                SqlDataReader datosReporteEmail = iGenericDataAccess.StoredProcedure(command);
                if (datosReporteEmail.HasRows)
                {
                    while (datosReporteEmail.Read())
                    {
                        listDatos.Add(new ReporteCorresExcel
                        {
                            Cotizante = Convert.ToString(datosReporteEmail["Cotizante"]),
                            destinatarios = Convert.ToString(datosReporteEmail["destinatarios"]),
                            Fecha = Convert.ToString(datosReporteEmail["Fecha"]),
                            numero = Convert.ToString(datosReporteEmail["numero"])
                        });




                    }
                    iGenericDataAccess.CloseConnection();
                }
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }


            return listDatos;
        }



        public ClienteProductoSolicitudMoldel obtenerClienteProductoXSolictud(int IdSolicitud)
        {

            var ClienteProducto = new ClienteProductoSolicitudMoldel();

            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpSelClienteProductoXSolicitudFlex
                };
                command.Parameters.Add("@IdSolicutud", SqlDbType.BigInt).Value = IdSolicitud;


                SqlDataReader datosStored = iGenericDataAccess.StoredProcedure(command);
                if (datosStored.HasRows)
                {
                    while (datosStored.Read())
                    {

                        ClienteProducto.IdCliente = Convert.ToInt32(datosStored["ClienteID"]);
                        ClienteProducto.IdProducto = Convert.ToInt32(datosStored["ProductoID"]);

                    }
                    iGenericDataAccess.CloseConnection();
                }
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }


            return ClienteProducto;
        }


        public EstructuraCorreoModel obtenerEstructuraCorreo(int idCliente,int idProducto)
        {

            var Correo = new EstructuraCorreoModel();
            string valorRegla = string.Empty;
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpSelReglaNegocioCorreoFlex
                };
                command.Parameters.Add("@IdCliente", SqlDbType.BigInt).Value = idCliente;
                command.Parameters.Add("@IdProducto", SqlDbType.BigInt).Value = idProducto;


                SqlDataReader datosStored = iGenericDataAccess.StoredProcedure(command);
                if (datosStored.HasRows)
                {
                    while (datosStored.Read())
                    {

                        valorRegla = Convert.ToString(datosStored["Valor"]);
                      

                    }
                    iGenericDataAccess.CloseConnection();
                }

                if (valorRegla.Length > 1)
                {
                  

                    string [] DatosCorreo = valorRegla.Split('@');
                    if (DatosCorreo.Length!= 3)
                    {
                        throw new Exception("La regla de negocio CORREO COTIZADOR FLEXIBLE " +
                            "Para los ambitos Cliente y Producto enviados, no cumple con la estructura solicitada   asunto@mensaje@espedida");
                    }
                    Correo.Asunto = DatosCorreo[0].ToString();
                    Correo.Mensaje = DatosCorreo[1].ToString();
                    Correo.Despedida = DatosCorreo[2].ToString();
                }
                else
                {
                    Correo = null;
                }
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }


            return Correo;
        }
    }
}