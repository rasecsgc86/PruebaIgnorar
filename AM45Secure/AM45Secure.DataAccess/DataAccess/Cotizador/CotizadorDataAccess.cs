using System;
using System.Linq;
using System.Text;
using Zero.Ado.Models;
using Zero.Exceptions;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AM45Secure.Commons.Recursos;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Constantes.Comunes;
using AM45Secure.Commons.Modelos.Cotizador;
using AM45Secure.DataAccess.Entidades.Comunes;
using AM45Secure.DataAccess.Entidades.Cotizador;
using AM45Secure.DataAccess.IDataAccess.IGeneric;
using AM45Secure.DataAccess.IDataAccess.ICotizador;
using Newtonsoft.Json;
using AM45Secure.Commons.Modelos.Comparador;
using AM45Secure.DataAccess.Entidades.Comparador;
using System.Xml.Serialization;
using AM45Secure.Commons.Utils;
using System.IO;
using AM45Secure.Commons.Constantes.Querys;

namespace AM45Secure.DataAccess.DataAccess.Cotizador
{
    public class CotizadorDataAccess : ICotizadorDataAccess
    {
        private readonly IGenericDataAccess iGenericDataAccess;

        public SqlConnection Conexion { get; set; }

        private readonly string NA = "NA";

        private readonly string CHECKED = "CHECKED";

        private readonly string UNCHECKED = "UNCHECKED";


        public CotizadorDataAccess(IGenericDataAccess iGenericDataAccess)
        {
            this.iGenericDataAccess = iGenericDataAccess;
        }

        public IList<ProductoModel> ConsultarProductosCliente(CotizadorModel cotizadorModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();

                IList<VwCotSelProductosClienteUsuario> vwCotSelProductos = iGenericDataAccess.Consultar(CQuerysCotizador.QryProductosClienteUsuario,
                                                                                                        new VwCotSelProductosClienteUsuario
                                                                                                        {
                                                                                                            ClienteId = cotizadorModel.ClienteModel.ClienteId.ToString(),
                                                                                                            ProductoFlex = cotizadorModel.ClienteModel.ProductoFlex,
                                                                                                            UsuarioId = cotizadorModel.GetIdUsuarioSesion().ToString(),
                                                                                                            PerfilId = cotizadorModel.GetIdPerfilUsuarioSesion().ToString()
                                                                                                        },
                                                                                                        new OptionsQueryZero
                                                                                                        {
                                                                                                            ExcludeNumericsDefaults = true,
                                                                                                            ExcludeBool = true
                                                                                                        });
                iGenericDataAccess.CloseConnection();

                if (vwCotSelProductos.Count == 0)
                {
                    throw new DomainException(CodesCotizador.INF_01_06);
                }

                IList<ProductoModel> productosList = vwCotSelProductos.Select(
                                                                              x => new ProductoModel()
                                                                                   {
                                                                                       ProductoId = Convert.ToInt32(x.ProductoId),
                                                                                       NombreProducto = x.Producto,
                                                                                       Flexible = x.Flexible,
                                                                                       Cp = x.Cp
                                                                                   }).ToList().OrderBy(x => x.NombreProducto).ToList();
                return productosList;
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

        public IList<ClientesModel> ConsultarCliente(CotizadorModel cotizadorModel)
        {
            try
            {
                int noClientes = ConsultarCantidadCliente(cotizadorModel);
                //SELECT ne.PersonaID Id, ne.Nombre FROM dbo.nePersonas ne INNER JOIN (SELECT PerfilID, PersonaID, Valor FROM dbo.PerfilDatos WHERE opcion = @OpcionId AND PerfilID = @PerfilId AND PersonaID = @UsuarioId AND ISNUMERIC(Valor) = 1) pf ON ne.PersonaID = pf.Valor
                iGenericDataAccess.OpenConnection();
                string condicion = "";
                if (noClientes>20)
                {
                    condicion = " AND Nombre LIKE '%" + cotizadorModel.ClienteModel.Cliente + "%'";
                }
                IList<VwCotSelClientesUsuario> vwCotSelClientes = iGenericDataAccess.ExecuteQuery<VwCotSelClientesUsuario>("SELECT ne.PersonaID Id, ne.Nombre" +
                                                                                                                           " FROM dbo.nePersonas ne " +
                                                                                                                           " INNER JOIN (" +
                                                                                                                           "     SELECT PerfilID, PersonaID, Valor FROM dbo.PerfilDatos" +
                                                                                                                           "     WHERE opcion = " + ConstTipoPersonas.Cliente + " AND" +
                                                                                                                           "           PerfilID =" + cotizadorModel.GetIdPerfilUsuarioSesion() + " AND " +
                                                                                                                           "           PersonaID = " + cotizadorModel.GetIdUsuarioSesion() + " AND " +
                                                                                                                           "           ISNUMERIC(Valor) = 1) pf" +
                                                                                                                           " ON ne.PersonaID = pf.Valor " + condicion);

                iGenericDataAccess.CloseConnection();
                IList<ClientesModel> clientesList = vwCotSelClientes.Select(
                                                                            x => new ClientesModel
                                                                                 {
                                                                                     ClienteId = Convert.ToInt32(x.Id),
                                                                                     Cliente = x.Nombre
                                                                                 }).ToList().OrderBy(x => x.Cliente).ToList();
                return clientesList;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        public int ConsultarCantidadCliente(CotizadorModel cotizadorModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();

                IList<CountUtilidad> vwCotSelClientes = iGenericDataAccess.ExecuteQuery<CountUtilidad>("SELECT COUNT(ne.PersonaID) AS Count" +
                                                                                                       " FROM dbo.nePersonas ne " +
                                                                                                       " INNER JOIN (" +
                                                                                                       "     SELECT PerfilID, PersonaID, Valor FROM dbo.PerfilDatos" +
                                                                                                       "     WHERE opcion = " + ConstTipoPersonas.Cliente + " AND" +
                                                                                                       "           PerfilID =" + cotizadorModel.GetIdPerfilUsuarioSesion() + " AND " +
                                                                                                       "           PersonaID = " + cotizadorModel.GetIdUsuarioSesion() + " AND " +
                                                                                                       "           ISNUMERIC(Valor) = 1) pf" +
                                                                                                       " ON ne.PersonaID = pf.Valor");

                iGenericDataAccess.CloseConnection();
                CountUtilidad cliente = vwCotSelClientes.Select(
                                                                x => new CountUtilidad
                                                                     {
                                                                         Count = x.Count
                                                                     }).First();
                return cliente.Count;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesCotizador.ERR_01_05, e);
            }
        }

        public IList<RegionCodigoPostalModel> ConsultarCodigoPostal(CodigoPostalModel codigoPostalModel)
        {
            IList<RegionCodigoPostalModel> regionCodigoPostalList;
            try
            {
                iGenericDataAccess.OpenConnection();

                var sWhere = new StringBuilder();

                sWhere.Append("CodigoPostal = '" + codigoPostalModel.CodigoPostal + "'");

                // Valida si "Codigo Postal" y "Colonia" son diferentes de vacio, entonces...
                if (codigoPostalModel.CodigoPostal != "" && codigoPostalModel.Colonia != "")
                {
                    // Realiza una consulta especifica
                    sWhere.Append(" AND ");
                }
                // De lo contrario...
                else
                {
                    // Realiza una consulta donde puede o no traer algun parametro ["Codigo Postal","Colonia"]
                    sWhere.Append(" OR ");
                }

                sWhere.Append("Colonia = '" + codigoPostalModel.Colonia + "'");
                sWhere.Append(" AND  (Asentamiento <> 0 AND Mnpio <> 0)");
                IList<VwCotSelCodigoPostal> vwCotSelCodigoPostal = iGenericDataAccess.Consultar(
                                                                                                new VwCotSelCodigoPostal(),
                                                                                                new OptionsQueryZero
                                                                                                {
                                                                                                    ExcludeNumericsDefaults = true,
                                                                                                    ExcludeBool = true,
                                                                                                    WhereComplementary = sWhere.ToString()
                                                                                                });
                iGenericDataAccess.CloseConnection();
                regionCodigoPostalList = vwCotSelCodigoPostal.Select(
                                                                     x => new RegionCodigoPostalModel
                                                                          {
                                                                              PaisId = x.PaisId,
                                                                              Pais = x.Pais,
                                                                              EstadoId = x.EstadoId,
                                                                              Estado = x.Estado,
                                                                              DelegacionId = x.DelegacionId,
                                                                              Delegacion = x.Delegacion,
                                                                              Colonia = x.Colonia,
                                                                              CodigoPostal = x.CodigoPostal,
                                                                              Asentamiento = x.Asentamiento,
                                                                              Mnpio = x.Mnpio,
                                                                              ColoniaId = x.ColoniaId
                                                                          }).ToList();
                sWhere.Clear();

                if (regionCodigoPostalList.Count == 0)
                {
                    throw new DomainException(CodesCotizador.INF_01_05);
                }
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
            return regionCodigoPostalList;
        }

        public IList<ValoresReglaModel> ConsultaReglaNegocio(CotizadorModel cotizadorModel)
        {
            IList<ValoresReglaModel> reglaList = new List<ValoresReglaModel>();
            try
            {
                SqlCommand command = new SqlCommand
                                     {
                                         CommandText = ConstStoredProcedures.SpCotSelValoresReglas
                                     };
                command.Parameters.Add("@IdRegla", SqlDbType.Int, 80).Value = cotizadorModel.SolicitudRegla.IdRegla;
                command.Parameters.Add("@ProductoFlex", SqlDbType.Int, 80).Value = cotizadorModel.SolicitudRegla.ProductoFlex;
                command.Parameters.Add("@IdProducto", SqlDbType.NVarChar, 80).Value = cotizadorModel.SolicitudRegla.IdProducto;
                command.Parameters.Add("@IdCliente", SqlDbType.NVarChar, 80).Value = cotizadorModel.SolicitudRegla.IdCliente;
                command.Parameters.Add("@IdTipoVehiculo", SqlDbType.NVarChar, 80).Value = cotizadorModel.SolicitudRegla.IdTipoVehiculo;
                command.Parameters.Add("@EstadoVehiculo", SqlDbType.NVarChar, 80).Value = cotizadorModel.SolicitudRegla.EstadoVehiculo;
                command.Parameters.Add("@Servicio", SqlDbType.NVarChar, 80).Value = cotizadorModel.SolicitudRegla.Servicio;
                command.Parameters.Add("@TipoVehiculo", SqlDbType.NVarChar, 80).Value = cotizadorModel.SolicitudRegla.TipoVehiculo;
                command.Parameters.Add("@Modelo", SqlDbType.NVarChar, 80).Value = cotizadorModel.SolicitudRegla.Modelo;
                command.Parameters.Add("@Marca", SqlDbType.NVarChar, 80).Value = cotizadorModel.SolicitudRegla.Marca;
                command.Parameters.Add("@Submarca", SqlDbType.NVarChar, 80).Value = cotizadorModel.SolicitudRegla.Submarca;
                command.Parameters.Add("@Estado", SqlDbType.NVarChar, 80).Value = cotizadorModel.SolicitudRegla.Estado;
                command.Parameters.Add("@AseguradoraId", SqlDbType.Int, 80).Value = cotizadorModel.SolicitudRegla.AseguradoraId;
                command.Parameters.Add("@Usuario", SqlDbType.Int, 80).Value = cotizadorModel.GetIdUsuarioSesion();
                command.Parameters.Add("@Perfil", SqlDbType.Int, 80).Value = cotizadorModel.GetIdPerfilUsuarioSesion();
                command.Parameters.Add("@TipoArrendamiento", SqlDbType.VarChar, 200).Value = cotizadorModel.SolicitudRegla.TipoArrendamiento;
                command.Parameters.Add("@CoberturaId", SqlDbType.NVarChar, 80).Value = cotizadorModel.SolicitudRegla.IdCobertura;

                SqlDataReader reglasNegocio = iGenericDataAccess.StoredProcedure(command);
                if (reglasNegocio.HasRows)
                {
                    while (reglasNegocio.Read())
                    {
                        ValoresReglaModel valoresRegla = new ValoresReglaModel
                                                         {
                                                             ValorId = reglasNegocio["ValorId"].ToString(),
                                                             Valor = reglasNegocio["Valor"].ToString(),
                                                             ClienteId = reglasNegocio["ClienteId"].ToString(),
                                                             Cliente = reglasNegocio["Cliente"].ToString(),
                                                             ProductoId = reglasNegocio["ProductoId"].ToString(),
                                                             Producto = reglasNegocio["Producto"].ToString(),
                                                             TipoVehiculoId = reglasNegocio["TipoVehiculoId"].ToString(),
                                                             TipoVehiculo = reglasNegocio["TipoVehiculo"].ToString(),
                                                             EstadoId = Convert.ToInt32(reglasNegocio["EstadoId"].ToString()),
                                                             Estado = reglasNegocio["Estado"].ToString(),
                                                             Pais = reglasNegocio["Pais"].ToString(),
                                                             Delegacion = reglasNegocio["Delegacion"].ToString(),
                                                             Colonia = reglasNegocio["Colonia"].ToString(),
                                                             CodigoPostal = reglasNegocio["CodigoPostal"].ToString(),
                                                             Domicilio = reglasNegocio["Domicilio"].ToString(),
                                                             NoExterior = reglasNegocio["NoExterior"].ToString(),
                                                             AseguradoraId = Convert.ToInt32(reglasNegocio["AseguradoraId"].ToString()),
                                                             Aseguradora = reglasNegocio["Aseguradora"].ToString(),
                                                             HabilitaRemolques = Convert.ToInt32(reglasNegocio["HabilitaRemolques"].ToString()),
                                                             ServicioId = reglasNegocio["ServicioId"].ToString(),
                                                             Servicio = reglasNegocio["Servicio"].ToString(),
                                                             AgenteUdi = reglasNegocio["AgenteUDI"].ToString(),
                                                             FechaRetro = Convert.ToDateTime(reglasNegocio["FechaRetro"].ToString())
                                                         };
                        reglaList.Add(valoresRegla);
                    }
                }

                iGenericDataAccess.CloseConnection();

                if (reglaList.Count == 0)
                {
                    switch (cotizadorModel.SolicitudRegla.IdRegla)
                    {
                        case 1009:
                            throw new DomainException(CodesCotizador.INF_01_01 + ConstValoresReglas.Plazos);
                        case 1042:
                            throw new DomainException(CodesCotizador.INF_01_01 + ConstValoresReglas.Armadoras);
                        case 1046:
                            throw new DomainException(CodesCotizador.INF_01_01 + ConstValoresReglas.Modelos);
                        case 1155:
                            throw new DomainException(CodesCotizador.INF_01_01 + ConstValoresReglas.Servicio);
                        case 1156:
                            throw new DomainException(CodesCotizador.INF_01_01 + ConstValoresReglas.Udi);
                        case 2063:
                            throw new DomainException(CodesCotizador.INF_01_01 + ConstValoresReglas.Antiguedad);
                        case 2064:
                            throw new DomainException(CodesCotizador.INF_01_01 + ConstValoresReglas.TipoVehiculo);
                        case 5612:
                            throw new DomainException(CodesCotizador.INF_01_01 + ConstValoresReglas.LimiteFactura);
                        case 5613:
                            throw new DomainException(CodesCotizador.INF_01_01 + ConstValoresReglas.SubMarcas);
                    }
                }
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
            return reglaList;
        }

        public PanelCotizadorModel ConsultaPanelCotizacionFlex(CotizadorModel cotizadorModel)
        {
            try
            {
                var valorCoberturaRcDeducible = "";
                var valorCoberturaRcSuma = "";
                int idCoberturaManiobrasCarDes = 0;
                int idCoberturaRcComplementaria = 0;
                bool coberturasDependientes = false;
                bool coberturaRespCiv = false;

                #region Consulta Regla de Negocio "UDI" para obtener aseguradoras [De acuerdo UdiCotizacion Vs UdiReglaNegocio]

                IList<AseguradoraModel> aseguradorasUdiList = new List<AseguradoraModel>();
                IList<ValoresReglaModel> reglaUdi = ConsultaReglaNegocio(cotizadorModel);

                foreach (ValoresReglaModel regla in reglaUdi)
                {
                    if (cotizadorModel.PanelCotizadorModel.UDI == regla.Valor)
                    {
                        AseguradoraModel aseguradora = new AseguradoraModel
                                                       {
                                                           IdAseguradora = regla.AseguradoraId,
                                                           NombreAseguradora = regla.Aseguradora
                                                       };

                        aseguradorasUdiList.Add(aseguradora);
                    }
                }

                if (aseguradorasUdiList.Count == 0)
                {
                    throw new DalException(CodesCotizador.ERR_01_01);
                }

                aseguradorasUdiList = aseguradorasUdiList.GroupBy(aseg => new
                                                                          {
                                                                              aseg.IdAseguradora,
                                                                              aseg.NombreAseguradora
                                                                          }, (aseguradora, group) => new
                                                                                                     {
                                                                                                         AseguradoraIdKey = aseguradora.IdAseguradora,
                                                                                                         AseguradoraKey = aseguradora.NombreAseguradora
                                                                                                     }).Select(x => new AseguradoraModel()
                                                                                                                    {
                                                                                                                        IdAseguradora = x.AseguradoraIdKey,
                                                                                                                        NombreAseguradora = x.AseguradoraKey
                                                                                                                    }).ToList();

                #endregion

                #region Consulta Perfiles Flexibles de acuerdo al usuario y perfil logeado

                IList<UsuarioPerfilModel> usuarioPerfil = ValidaUsuarioPerfil(cotizadorModel);

                #endregion

                #region Consulta si el producto flexible contiene Submarca

                iGenericDataAccess.OpenConnection();
                IList<ProductosFlex> existeSubarca = iGenericDataAccess.Consultar(CQuerysCotizador.QryExisteSubmarca,
                                                                                  new ProductosFlex()
                                                                                  {
                                                                                      ProductoId = cotizadorModel.PanelCotizadorModel.IdProducto,
                                                                                      IdTipoVehiculo = cotizadorModel.PanelCotizadorModel.IdTipoVehiculo,
                                                                                      Servicio = cotizadorModel.PanelCotizadorModel.IdTipoServicioVehiculo,
                                                                                      IdCondicionVehiculo = cotizadorModel.PanelCotizadorModel.IdCondicionVehiculo,
                                                                                      Submarca = cotizadorModel.PanelCotizadorModel.Submarca
                                                                                  },
                                                                                  new OptionsQueryZero()
                                                                                  {
                                                                                      ExcludeNumericsDefaults = true,
                                                                                      ExcludeBool = true
                                                                                  });
                iGenericDataAccess.CloseConnection();

                #endregion

                PanelCotizadorModel panel = new PanelCotizadorModel();

                iGenericDataAccess.OpenConnection();
                IList<VwCotSelProdsFlexDistinctCobertAseg> vwCotSelProdsFlexDistinctCobertAsegList = iGenericDataAccess.Consultar(new VwCotSelProdsFlexDistinctCobertAseg()
                                                                                                                                  {
                                                                                                                                      IdProducto = cotizadorModel.PanelCotizadorModel.IdProducto,
                                                                                                                                      IdTipoVehiculo = cotizadorModel.PanelCotizadorModel.IdTipoVehiculo,
                                                                                                                                      IdCondicionVehiculo = cotizadorModel.PanelCotizadorModel.IdCondicionVehiculo,
                                                                                                                                      IdTipoServicioVehiculo = cotizadorModel.PanelCotizadorModel.IdTipoServicioVehiculo
                                                                                                                                  },
                                                                                                                                  new OptionsQueryZero()
                                                                                                                                  {
                                                                                                                                      ExcludeBool = true,
                                                                                                                                      ExcludeNumericsDefaults = true,
                                                                                                                                      WhereComplementary = existeSubarca[0].ExisteSubmarca ? "Submarca = '" + cotizadorModel.PanelCotizadorModel.Submarca + "'" : "Submarca IS NULL OR  Submarca = ''"
                                                                                                                                  });
                iGenericDataAccess.CloseConnection();

                if (vwCotSelProdsFlexDistinctCobertAsegList.Count>0)
                {
                    if (string.IsNullOrEmpty(cotizadorModel.PanelCotizadorModel.TipoCarga) || !string.IsNullOrEmpty(cotizadorModel.PanelCotizadorModel.TipoCarga) && !cotizadorModel.PanelCotizadorModel.TipoCarga.Equals(ConstElementos.CargaMuyPeligrosa))
                    {
                        iGenericDataAccess.OpenConnection();
                        IList<Elementos> coberturaRcEcologica = iGenericDataAccess.ExecuteQuery<Elementos>(CQuerysCotizador.QryCoberturaRcEcologica);
                        iGenericDataAccess.CloseConnection();
                        if (coberturaRcEcologica.Count == 0)
                        {
                            throw new DalException(CodesCotizador.ERR_01_09);
                        }
                        vwCotSelProdsFlexDistinctCobertAsegList = vwCotSelProdsFlexDistinctCobertAsegList.Select(x => new VwCotSelProdsFlexDistinctCobertAseg
                                                                                                                      {
                                                                                                                          IdProducto = x.IdProducto,
                                                                                                                          NombreProducto = x.NombreProducto,
                                                                                                                          CoberturaFija = x.CoberturaFija,
                                                                                                                          CondicionVehiculo = x.CondicionVehiculo,
                                                                                                                          DeducibleDefault = x.DeducibleDefault,
                                                                                                                          IdAseguradora = x.IdAseguradora,
                                                                                                                          IdCobertura = x.IdCobertura,
                                                                                                                          IdCondicionVehiculo = x.IdCondicionVehiculo,
                                                                                                                          IdProductoFlex = x.IdProductoFlex,
                                                                                                                          IdProductoFlexAseguradora = x.IdProductoFlexAseguradora,
                                                                                                                          IdTipoVehiculo = x.IdTipoVehiculo,
                                                                                                                          TipoVehiculo = x.TipoVehiculo,
                                                                                                                          IdTipoServicioVehiculo = x.IdTipoServicioVehiculo,
                                                                                                                          ServicioVehiculo = x.ServicioVehiculo,
                                                                                                                          SubMarca = x.SubMarca,
                                                                                                                          NombreAseguradora = x.NombreAseguradora,
                                                                                                                          NombreCobertura = x.NombreCobertura,
                                                                                                                          PerfilCoberturaFija = x.PerfilCoberturaFija,
                                                                                                                          SumaAseguradaDefault = x.SumaAseguradaDefault,
                                                                                                                          PerfilSumaAsegurada = x.PerfilSumaAsegurada,
                                                                                                                          PerfilDeducible = x.PerfilDeducible,
                                                                                                                          TooltipCobertura = x.TooltipCobertura,
                                                                                                                          IsEspecial = x.IsEspecial
                                                                                                                      }).Where(aseg => aseg.IdCobertura != coberturaRcEcologica[0].ElementoId).ToList();
                    }

                    IList<AseguradoraModel> aseguradoraModels = vwCotSelProdsFlexDistinctCobertAsegList.GroupBy(elemento => new
                                                                                                                            {
                                                                                                                                elemento.IdAseguradora,
                                                                                                                                elemento.NombreAseguradora
                                                                                                                            },
                                                                                                                (elemento,
                                                                                                                 group) => new
                                                                                                                           {
                                                                                                                               IdAseguradoraKey = elemento.IdAseguradora,
                                                                                                                               NombreAseguradoraKey = elemento.NombreAseguradora
                                                                                                                           }).Select(x => new AseguradoraModel()
                                                                                                                                          {
                                                                                                                                              IdAseguradora = x.IdAseguradoraKey,
                                                                                                                                              NombreAseguradora = x.NombreAseguradoraKey
                                                                                                                                          }).ToList();

                    #region Union y agrupacion aseguradorasUdiList Vs aseguradoraModels

                    IList<AseguradoraModel> aseguradorasModel = new List<AseguradoraModel>();

                    foreach (AseguradoraModel aseguradoraModel in aseguradoraModels)
                    {
                        foreach (AseguradoraModel aseguradoraUdi in aseguradorasUdiList)
                        {
                            if (aseguradoraModel.IdAseguradora == aseguradoraUdi.IdAseguradora)
                            {
                                aseguradorasModel.Add(aseguradoraModel);
                            }
                        }
                    }

                    #endregion

                    if (aseguradorasModel.Count>0)
                    {
                        IList<CoberturaModel> coberturaModels = vwCotSelProdsFlexDistinctCobertAsegList.GroupBy(elemento => new
                                                                                                                            {
                                                                                                                                elemento.IdCobertura,
                                                                                                                                elemento.NombreCobertura,
                                                                                                                                elemento.CoberturaFija,
                                                                                                                                elemento.SumaAseguradaDefault,
                                                                                                                                elemento.DeducibleDefault,
                                                                                                                                elemento.PerfilCoberturaFija,
                                                                                                                                elemento.PerfilDeducible,
                                                                                                                                elemento.PerfilSumaAsegurada,
                                                                                                                                elemento.IsEspecial
                                                                                                                            },
                                                                                                                (elemento,
                                                                                                                 group) => new
                                                                                                                           {
                                                                                                                               IdCoberturaKey = elemento.IdCobertura,
                                                                                                                               NombreCoberturaKey = elemento.NombreCobertura,
                                                                                                                               IsFijaKey = elemento.CoberturaFija,
                                                                                                                               SumaAseguradaDefaultKey = elemento.SumaAseguradaDefault,
                                                                                                                               DeducibleDefaultKey = elemento.DeducibleDefault,
                                                                                                                               PerfilCoberturaFijaKey = elemento.PerfilCoberturaFija,
                                                                                                                               PerfilDeducibleKey = elemento.PerfilDeducible,
                                                                                                                               PerfilSumaAseguradaKey = elemento.PerfilSumaAsegurada,
                                                                                                                               IsEspecialKey = elemento.IsEspecial
                                                                                                                           }).Select(x => new CoberturaModel()
                                                                                                                                          {
                                                                                                                                              IdCobertura = x.IdCoberturaKey,
                                                                                                                                              NombreCobertura = x.NombreCoberturaKey,
                                                                                                                                              IsFija = x.IsFijaKey,
                                                                                                                                              SumaAseguradaDefault = x.SumaAseguradaDefaultKey,
                                                                                                                                              DeducibleDefault = x.DeducibleDefaultKey,
                                                                                                                                              PerfilSumaAsegurada = x.PerfilSumaAseguradaKey,
                                                                                                                                              PerfilCoberturaFija = x.PerfilCoberturaFijaKey,
                                                                                                                                              PerfilDeducible = x.PerfilDeducibleKey,
                                                                                                                                              IsFijaPerfil = (x.PerfilCoberturaFijaKey && x.IsFijaKey && usuarioPerfil.Count>0),
                                                                                                                                              IsEspecial = x.IsEspecialKey
                                                                                                                                          }).ToList();

                        iGenericDataAccess.OpenConnection();
                        IList<VwCotSelProdsFlexCobertAseg> coberturasAseguradoras = iGenericDataAccess.Consultar(new VwCotSelProdsFlexCobertAseg()
                                                                                                                 {
                                                                                                                     IdProducto = cotizadorModel.PanelCotizadorModel.IdProducto,
                                                                                                                     IdTipoVehiculo = cotizadorModel.PanelCotizadorModel.IdTipoVehiculo,
                                                                                                                     IdCondicionVehiculo = cotizadorModel.PanelCotizadorModel.IdCondicionVehiculo,
                                                                                                                     IdTipoServicioVehiculo = cotizadorModel.PanelCotizadorModel.IdTipoServicioVehiculo
                                                                                                                 },
                                                                                                                 new OptionsQueryZero()
                                                                                                                 {
                                                                                                                     ExcludeNumericsDefaults = true,
                                                                                                                     ExcludeBool = true,
                                                                                                                     WhereComplementary = existeSubarca[0].ExisteSubmarca ? "Submarca = '" + cotizadorModel.PanelCotizadorModel.Submarca + "'" : "Submarca IS NULL OR  Submarca = ''"
                                                                                                                 }).ToList();
                        iGenericDataAccess.CloseConnection();

                        #region Se inician fors para armar la respuesta del panel de coberturas. Iniciamos iterando las coberturas distinct

                        foreach (CoberturaModel coberturaModel in coberturaModels)
                        {
                            RangosModel rangosModel = new RangosModel();
                            IList<string> rangoSumas = new List<string>();
                            IList<string> rangoDeducibles = new List<string>();
                            IList<ElementoPanelCotizadorModel> aseguradorasCobertura = new List<ElementoPanelCotizadorModel>();

                            #region Por cada cobertura iteramos las aseguradoras

                            foreach (AseguradoraModel aseguradoraModel in aseguradorasModel)
                            {
                                // Verificamos si existe la relacion de la cobertura vs aseguradora
                                bool existeRelacion = coberturasAseguradoras.Any(x => x.IdAseguradora == aseguradoraModel.IdAseguradora && x.IdCobertura == coberturaModel.IdCobertura);

                                //Calculamos el indicador de la cobertura vs aseguradora
                                string indicadorCobertura = CHECKED;

                                if (!existeRelacion && coberturaModel.IsFija)
                                {
                                    indicadorCobertura = NA;
                                }
                                else if (!existeRelacion && !coberturaModel.IsFija)
                                {
                                    indicadorCobertura = UNCHECKED;
                                }

                                //Se crea el elemento de la aseguradora de la cobertura actual
                                ElementoPanelCotizadorModel elementoPanel = new ElementoPanelCotizadorModel()
                                                                            {
                                                                                IdAseguradora = aseguradoraModel.IdAseguradora,
                                                                                NombreAseguradora = aseguradoraModel.NombreAseguradora,
                                                                                IdCobertura = coberturaModel.IdCobertura,
                                                                                NombreCobertura = coberturaModel.NombreCobertura,
                                                                                ExisteRelacion = existeRelacion,
                                                                                IndicadorCobertura = indicadorCobertura
                                                                            };

                                #region ExisteRelacionCoberturaAseguradora

                                if (existeRelacion)
                                {
                                    //Obtenemos la relacion cobertura vs aseguradora
                                    VwCotSelProdsFlexCobertAseg coberturaAseguradora = coberturasAseguradoras.FirstOrDefault(where => where.IdAseguradora == aseguradoraModel.IdAseguradora && where.IdCobertura == coberturaModel.IdCobertura);

                                    if (coberturaAseguradora != null)
                                    {
                                        coberturaModel.Dependencia = coberturaAseguradora.Dependencia;
                                        coberturaModel.Enmascaramiento = coberturaAseguradora.Enmascaramiento;
                                        elementoPanel.Detalle = coberturaAseguradora.Tooltip;
                                        #region Validaciones para saber si hay dependencias

                                        if (!coberturaRespCiv && !coberturasDependientes)
                                        {
                                            if (coberturaModel.Dependencia != "1" && coberturaModel.Dependencia != "0")
                                            {
                                                var dependencia = coberturaModel.Dependencia.Split('|');
                                                idCoberturaManiobrasCarDes = Convert.ToInt32(dependencia[0]);
                                                idCoberturaRcComplementaria = Convert.ToInt32(dependencia[1]);
                                                valorCoberturaRcDeducible = coberturaModel.DeducibleDefault;
                                                valorCoberturaRcSuma = coberturaModel.SumaAseguradaDefault;
                                                coberturaRespCiv = true;
                                            }
                                            else if (coberturaModel.Dependencia == "1")
                                            {
                                                if (coberturaModel.NombreCobertura.IndexOf("Maniobras", StringComparison.Ordinal) != 1)
                                                {
                                                    idCoberturaManiobrasCarDes = coberturaModel.IdCobertura;
                                                }
                                                else
                                                {
                                                    idCoberturaRcComplementaria = coberturaModel.IdCobertura;
                                                }

                                                coberturasDependientes = true;
                                            }
                                        }
                                        else if (coberturasDependientes && !coberturaRespCiv && coberturaModel.Dependencia != "1" && coberturaModel.Dependencia != "0")
                                        {
                                            valorCoberturaRcDeducible = coberturaModel.DeducibleDefault;
                                            valorCoberturaRcSuma = coberturaModel.SumaAseguradaDefault;
                                        }

                                        if (coberturaRespCiv)
                                        {
                                            if (idCoberturaManiobrasCarDes == coberturaModel.IdCobertura)
                                            {
                                                coberturaModel.DeducibleDefault = valorCoberturaRcDeducible;
                                                coberturaModel.SumaAseguradaDefault = valorCoberturaRcSuma;
                                            }
                                            else if (idCoberturaRcComplementaria == coberturaModel.IdCobertura)
                                            {
                                                coberturaModel.DeducibleDefault = valorCoberturaRcDeducible;
                                            }
                                        }

                                        #endregion

                                        //Obtenemos y casteamos el JSON de rangos de la coberturaAaseguradora actual
                                        RangosModel rangosModelAseguradoraCobertura = null;

                                        if (!string.IsNullOrEmpty(coberturaAseguradora.RangosJson))
                                        {
                                            rangosModelAseguradoraCobertura = JsonConvert.DeserializeObject<RangosModel>(coberturaAseguradora.RangosJson);
                                        }

                                        #region LlenadoDeRangosDeLaCoberturaDeAcuerdoAseguradora

                                        if (null != rangosModelAseguradoraCobertura)
                                        {
                                            elementoPanel.RangosModel = rangosModelAseguradoraCobertura;

                                            #region RangosSumasAseguradas

                                            if (null != rangosModelAseguradoraCobertura.RangosSumas)
                                            {
                                                foreach (string sumaAsegurada in rangosModelAseguradoraCobertura.RangosSumas)
                                                {
                                                    if (!rangoSumas.Contains(sumaAsegurada))
                                                    {
                                                        rangoSumas.Add(sumaAsegurada);
                                                    }
                                                }
                                            }

                                            #endregion

                                            #region RangosDeducibles

                                            if (null != rangosModelAseguradoraCobertura.RangosDeducibles)
                                            {
                                                foreach (string deducibleAsegurado in rangosModelAseguradoraCobertura.RangosDeducibles)
                                                {
                                                    if (!rangoDeducibles.Contains(deducibleAsegurado))
                                                    {
                                                        rangoDeducibles.Add(deducibleAsegurado);
                                                    }
                                                }
                                            }

                                            #endregion
                                        }

                                        if (coberturaModel.Enmascaramiento && coberturaModel.Dependencia != "1")
                                        {
                                            coberturaModel.FiltroValorRangoSuma = rangoSumas[0];
                                        }
                                    }

                                    #endregion
                                }

                                #endregion

                                aseguradorasCobertura.Add(elementoPanel);
                            }

                            if (!string.IsNullOrEmpty(coberturaModel.SumaAseguradaDefault))
                            {
                                coberturaModel.FiltroValorRangoSuma = coberturaModel.SumaAseguradaDefault;
                            }

                            if (!string.IsNullOrEmpty(coberturaModel.DeducibleDefault))
                            {
                                coberturaModel.FiltroValorRangoDeducible = coberturaModel.DeducibleDefault;
                            }

                            if (!string.IsNullOrEmpty(coberturaModel.SumaAseguradaDefault) && !coberturaModel.PerfilSumaAsegurada
                                ||
                                !string.IsNullOrEmpty(coberturaModel.SumaAseguradaDefault) && coberturaModel.PerfilSumaAsegurada && usuarioPerfil.Count == 0)
                            {
                                rangoSumas.Clear();
                                rangoSumas.Add(coberturaModel.SumaAseguradaDefault);
                            }

                            if (!string.IsNullOrEmpty(coberturaModel.DeducibleDefault) && !coberturaModel.PerfilDeducible
                                ||
                                !string.IsNullOrEmpty(coberturaModel.DeducibleDefault) && coberturaModel.PerfilDeducible && usuarioPerfil.Count == 0)
                            {
                                rangoDeducibles.Clear();
                                rangoDeducibles.Add(coberturaModel.DeducibleDefault);
                            }

                            if (coberturaModel.IsEspecial)
                            {
                                rangoDeducibles.Clear();
                                rangoSumas.Clear();
                                coberturaModel.SumaAseguradaDefault = String.Empty;
                                coberturaModel.DeducibleDefault = String.Empty;
                                coberturaModel.FiltroValorRangoSuma = String.Empty;
                                coberturaModel.FiltroValorRangoDeducible = String.Empty;
                            }

                            rangosModel.RangosSumas = rangoSumas;
                            rangosModel.RangosDeducibles = rangoDeducibles;
                            coberturaModel.IsSeleccionada = true;
                            coberturaModel.RangosModel = rangosModel;
                            coberturaModel.AseguradorasCobertura = aseguradorasCobertura;

                            #endregion
                        }

                        panel.IdProducto = cotizadorModel.PanelCotizadorModel.IdProducto;
                        panel.IdTipoVehiculo = cotizadorModel.PanelCotizadorModel.IdTipoVehiculo;
                        panel.IdCondicionVehiculo = cotizadorModel.PanelCotizadorModel.IdCondicionVehiculo;
                        panel.IdTipoServicioVehiculo = cotizadorModel.PanelCotizadorModel.IdTipoServicioVehiculo;
                        panel.Coberturas = coberturaModels;
                        panel.Aseguradoras = aseguradorasModel;
                        panel.UDI = cotizadorModel.PanelCotizadorModel.UDI;
                        panel.UdiList = reglaUdi;

                        if (coberturasDependientes)
                        {
                            foreach (CoberturaModel coberturaFiltro in panel.Coberturas)
                            {
                                if (coberturaFiltro.IsSeleccionada)
                                {
                                    if (idCoberturaManiobrasCarDes == coberturaFiltro.IdCobertura)
                                    {
                                        coberturaFiltro.DeducibleDefault = valorCoberturaRcDeducible;
                                        coberturaFiltro.SumaAseguradaDefault = valorCoberturaRcSuma;
                                    }
                                    else if (idCoberturaRcComplementaria == coberturaFiltro.IdCobertura)
                                    {
                                        coberturaFiltro.DeducibleDefault = valorCoberturaRcDeducible;
                                    }
                                }
                            }
                        }

                        #endregion

                        return panel;
                    }
                    else
                    {
                        throw new DalException(CodesCotizador.ERR_01_02);
                    }
                }
                else
                {
                    throw new DalException(CodesCotizador.ERR_01_04);
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
        }

        public PanelCotizadorModel FiltraPanelCotizacionFlex(CotizadorModel cotizadorModel)
        {
            try
            {
                var valorCoberturaRcDeducible = "";
                var valorCoberturaRcSuma = "";
                int idCoberturaManiobrasCarDes = 0;
                int idCoberturaRcComplementaria = 0;
                bool coberturasDependientes = false;
                bool coberturaRespCiv = false;

                foreach (CoberturaModel coberturaFiltro in cotizadorModel.PanelCotizadorModel.Coberturas)
                {
                    foreach (ElementoPanelCotizadorModel aseguradoraFiltrada in coberturaFiltro.AseguradorasCobertura)
                    {
                        if (!coberturaRespCiv && !coberturasDependientes && coberturaFiltro.IsSeleccionada)
                        {
                            if (coberturaFiltro.Dependencia != "1" && coberturaFiltro.Dependencia != "0")
                            {
                                var dependencia = coberturaFiltro.Dependencia.Split('|');
                                idCoberturaManiobrasCarDes = Convert.ToInt32(dependencia[0]);
                                idCoberturaRcComplementaria = Convert.ToInt32(dependencia[1]);
                                valorCoberturaRcDeducible = coberturaFiltro.FiltroValorRangoDeducible;
                                valorCoberturaRcSuma = coberturaFiltro.FiltroValorRangoSuma;
                                coberturaRespCiv = true;
                            }
                            else if (coberturaFiltro.Dependencia == "1")
                            {
                                if (coberturaFiltro.NombreCobertura.IndexOf("Maniobras", StringComparison.Ordinal) != 1)
                                {
                                    idCoberturaManiobrasCarDes = coberturaFiltro.IdCobertura;
                                }
                                else
                                {
                                    idCoberturaRcComplementaria = coberturaFiltro.IdCobertura;
                                }

                                coberturasDependientes = true;
                            }
                        }
                        else if (coberturasDependientes && !coberturaRespCiv && coberturaFiltro.Dependencia != "1" && coberturaFiltro.Dependencia != "0" && coberturaFiltro.IsSeleccionada)
                        {
                            valorCoberturaRcDeducible = coberturaFiltro.FiltroValorRangoDeducible;
                            valorCoberturaRcSuma = coberturaFiltro.FiltroValorRangoSuma;
                        }

                        if (coberturaRespCiv && coberturaFiltro.IsSeleccionada)
                        {
                            if (idCoberturaManiobrasCarDes == coberturaFiltro.IdCobertura)
                            {
                                coberturaFiltro.FiltroValorRangoDeducible = valorCoberturaRcDeducible;
                                coberturaFiltro.FiltroValorRangoSuma = valorCoberturaRcSuma;
                            }
                            else if (idCoberturaRcComplementaria == coberturaFiltro.IdCobertura)
                            {
                                coberturaFiltro.FiltroValorRangoDeducible = valorCoberturaRcDeducible;
                            }
                        }

                        //Calculamos el indicador de la cobertura vs aseguradora
                        if (coberturaFiltro.IsFija)
                        {
                            aseguradoraFiltrada.IndicadorCobertura = CHECKED;

                            if (!aseguradoraFiltrada.ExisteRelacion)
                            {
                                aseguradoraFiltrada.IndicadorCobertura = NA;
                            }
                            else if (!coberturaFiltro.IsEspecial && ((!string.IsNullOrEmpty(coberturaFiltro.FiltroValorRangoSuma) && !aseguradoraFiltrada.RangosModel.RangosSumas.Contains(coberturaFiltro.FiltroValorRangoSuma))
                                                                     || (!string.IsNullOrEmpty(coberturaFiltro.FiltroValorRangoDeducible) && !aseguradoraFiltrada.RangosModel.RangosDeducibles.Contains(coberturaFiltro.FiltroValorRangoDeducible))))
                            {
                                aseguradoraFiltrada.IndicadorCobertura = NA;
                            }
                            else if (!coberturaFiltro.IsSeleccionada)
                            {
                                aseguradoraFiltrada.IndicadorCobertura = UNCHECKED;
                            }
                        }
                        else
                        {
                            if (!coberturaFiltro.IsSeleccionada || !aseguradoraFiltrada.ExisteRelacion)
                            {
                                aseguradoraFiltrada.IndicadorCobertura = UNCHECKED;
                            }
                            else if (!coberturaFiltro.IsEspecial && ((!string.IsNullOrEmpty(coberturaFiltro.FiltroValorRangoSuma) && !aseguradoraFiltrada.RangosModel.RangosSumas.Contains(coberturaFiltro.FiltroValorRangoSuma))
                                                                     || (!string.IsNullOrEmpty(coberturaFiltro.FiltroValorRangoDeducible) && !aseguradoraFiltrada.RangosModel.RangosDeducibles.Contains(coberturaFiltro.FiltroValorRangoDeducible))))
                            {
                                aseguradoraFiltrada.IndicadorCobertura = NA;
                            }
                            else if (coberturaFiltro.IsSeleccionada)
                            {
                                aseguradoraFiltrada.IndicadorCobertura = CHECKED;
                            }
                        }
                    }
                }

                if (coberturasDependientes)
                {
                    foreach (CoberturaModel coberturaFiltro in cotizadorModel.PanelCotizadorModel.Coberturas)
                    {
                        if (coberturaFiltro.IsSeleccionada)
                        {
                            if (idCoberturaManiobrasCarDes == coberturaFiltro.IdCobertura)
                            {
                                coberturaFiltro.FiltroValorRangoDeducible = valorCoberturaRcDeducible;
                                coberturaFiltro.FiltroValorRangoSuma = valorCoberturaRcSuma;
                            }
                            else if (idCoberturaRcComplementaria == coberturaFiltro.IdCobertura)
                            {
                                coberturaFiltro.FiltroValorRangoDeducible = valorCoberturaRcDeducible;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
            return cotizadorModel.PanelCotizadorModel;
        }

        public IList<VersionesModel> ConsultarVersiones(SolicitudVersionesModel solicitudVersiones)
        {
            IList<VersionesModel> vehiculosList;
            try
            {
                var sConsulta = new StringBuilder();
                var sFiltro = new StringBuilder();

                if (solicitudVersiones.Filtro == 1)
                {
                    sFiltro.Append("     AND Ve.VWBANK <> ''");
                    sFiltro.Append("     AND Ve.VWBANK  IS NOT NULL ");
                }
                else
                {
                    sFiltro.Append("");
                }

                sConsulta.Append("SELECT    Ve.VehiculoID ,Ve.Marca,Ve.Submarca,Ve.Descripcion,");
                sConsulta.Append("          Ve.ClaveInterna,Ve.Modelo,Ve.GNP,Ve.AIG,Ve.ABA,Ve.ING,");
                sConsulta.Append("          Ve.ATLAS,Ve.ROYAL,Ve.QUALITAS,Ve.Tipo,Ve.Servicio,");
                sConsulta.Append("          Ve.Pasajeros,Ve.JATOID,Ve.JATOIDUSADOS,Ve.ZURICH,");
                sConsulta.Append("          Ve.PNG,Ve.MAPFRE,Ve.BANORTE,Ve.CHARTIS,Ve.VWBANK,");
                sConsulta.Append("          Ve.ClaveMARSH,Ve.msrepl_tran_version,Ve.HDI,");
                sConsulta.Append("          Ve.BANCOMER,Tipo.ElementoID ");
                sConsulta.Append("FROM      dbo.Vehiculos Ve ");
                sConsulta.Append("     INNER JOIN dbo.Elementos Tipo ");
                sConsulta.Append("          ON(Ve.Tipo = Tipo.Comodin) ");
                sConsulta.Append("WHERE Tipo.ElementoID = " + solicitudVersiones.Tipo);
                sConsulta.Append("     AND Ve.Servicio = '" + solicitudVersiones.Servicio + "'");
                sConsulta.Append("     AND Ve.Modelo = " + solicitudVersiones.Modelo);
                sConsulta.Append("     AND Ve.Marca = '" + solicitudVersiones.Marca + "'");
                sConsulta.Append("     AND Ve.Submarca = '" + solicitudVersiones.Submarca + "'");
                sConsulta.Append(sFiltro);
                sConsulta.Append("ORDER BY Descripcion ASC");
                iGenericDataAccess.OpenConnection();

                IList<Vehiculos> vehiculos = iGenericDataAccess.ExecuteQuery<Vehiculos>(sConsulta.ToString());

                iGenericDataAccess.CloseConnection();
                vehiculosList = vehiculos.Select(
                                                 x => new VersionesModel()
                                                      {
                                                          VehiculoId = x.VehiculoId,
                                                          Descripcion = x.Descripcion.IndexOf("(", StringComparison.Ordinal) != -1 ? x.Descripcion.Split('(')[0] + x.Descripcion.Split(')')[1] : x.Descripcion,
                                                          ClaveInterna = x.ClaveInterna,
                                                          ClaveMarsh = x.ClaveMarsh
                                                      }).ToList();
                sConsulta.Clear();
                sFiltro.Clear();

                if (vehiculosList.Count == 0)
                {
                    throw new DomainException(CodesCotizador.INF_01_03);
                }
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
            return vehiculosList;
        }

        public IList<ElementoModel> CargaElementos(ElementoModel elementoModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<Elementos> elementos = iGenericDataAccess.Consultar(
                                                                          new Elementos()
                                                                          {
                                                                              CatalogoId = elementoModel.CatalogoId
                                                                          },
                                                                          new OptionsQueryZero()
                                                                          {
                                                                              ExcludeNumericsDefaults = true,
                                                                              ExcludeBool = true
                                                                          });
                iGenericDataAccess.CloseConnection();
                IList<ElementoModel> elementosList = elementos.Select(x => new ElementoModel()
                                                                           {
                                                                               CatalogoId = x.CatalogoId,
                                                                               ElementoId = x.ElementoId,
                                                                               Nombre = x.Nombre
                                                                           }).ToList();
                return elementosList;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        public IList<PasajerosModel> ConsultaPasajeros(SolicitudPasajerosModel solicitudPasajeros)
        {
            IList<PasajerosModel> pasajerosList = new List<PasajerosModel>();

            try
            {
                if (solicitudPasajeros.Producto.Contains("VANES") && solicitudPasajeros.Tipo == "AUTO" && solicitudPasajeros.EsFlexible ||
                    solicitudPasajeros.Producto.Contains("VANES") && solicitudPasajeros.Tipo == "PASAJE" && solicitudPasajeros.EsFlexible ||
                    !solicitudPasajeros.Producto.Contains("VANES") && solicitudPasajeros.Tipo == "AUTOBUS")
                {
                    string sConsulta = CQuerysCotizador.QryRangosPasajerosVehiculo.Replace("@ClaveVehiculo", solicitudPasajeros.Version.Split('|')[1].Trim());

                    iGenericDataAccess.OpenConnection();
                    IList<Vehiculos> vehiculos = iGenericDataAccess.ExecuteQuery<Vehiculos>(sConsulta);

                    iGenericDataAccess.CloseConnection();

                    if (vehiculos.Count == 0)
                    {
                        throw new DomainException(CodesCotizador.INF_01_04);
                    }

                    string[] rangos = vehiculos[0].RangoPasajeros.Split(',');
                    pasajerosList = rangos.Select(x => new PasajerosModel()
                                                       {
                                                           Pasajeros = Convert.ToInt32(x)
                                                       }).OrderBy(x => x.Pasajeros).ToList();
                }
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
            return pasajerosList;
        }

        public IList<AgenciasModel> ConsultaAgencias(ClientProdAgenAsegModel clientProdAgenAseg)
        {
            IList<AgenciasModel> agenciasList;

            try
            {
                iGenericDataAccess.OpenConnection();
                if (clientProdAgenAseg.GetIdPerfilUsuarioSesion() == ConstTipoPersonas.Agencia)
                {
                    List<AgenciasModel> listAg = new List<AgenciasModel>();
                    AgenciasModel agencia = new AgenciasModel();
                    agencia.IsSession = true;
                    agencia.IdAgencia = Convert.ToInt32(clientProdAgenAseg.GetIdUsuarioSesion());
                    agencia.Agencia = clientProdAgenAseg.GetNombreUsuarioSesion();
                    listAg.Add(agencia);
                    agenciasList = listAg;
                }
                else
                {
                    IList<VwCotSelClientProdAgenAseg> vwCotSelClientProdAgenAseg = iGenericDataAccess.Consultar(CQuerysCotizador.QryConsultarAgencias,
                                                                                                                new VwCotSelClientProdAgenAseg()
                                                                                                                {
                                                                                                                    PersonaIdOpcionA = clientProdAgenAseg.ClienteId,
                                                                                                                },
                                                                                                                new OptionsQueryZero()
                                                                                                                {
                                                                                                                    ExcludeNumericsDefaults = true,
                                                                                                                    ExcludeBool = true
                                                                                                                });
                    iGenericDataAccess.CloseConnection();
                    agenciasList = vwCotSelClientProdAgenAseg.Select(x => new AgenciasModel()
                                                                          {
                                                                              IdAgencia = x.ValorIdB,
                                                                              Agencia = x.NombreValorB,
                                                                              IsSession = false
                                                                          }).ToList();
                    if (agenciasList.Count == 0)
                    {
                        throw new DomainException(CodesCotizador.INF_01_02);
                    }
                }
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
            return agenciasList;
        }

        public IList<ValoresReglaModel> ConsultaReglaUdi(CotizadorModel cotizadorModel)
        {
            IList<ValoresReglaModel> reglaList = new List<ValoresReglaModel>();
            try
            {
                SqlCommand command = new SqlCommand
                                     {
                                         CommandText = ConstStoredProcedures.SpCotSelValoresReglas
                                     };
                command.Parameters.Add("@IdRegla", SqlDbType.Int, 80).Value = cotizadorModel.SolicitudRegla.IdRegla;
                command.Parameters.Add("@ProductoFlex", SqlDbType.Int, 80).Value = cotizadorModel.SolicitudRegla.ProductoFlex;
                command.Parameters.Add("@IdProducto", SqlDbType.NVarChar, 80).Value = cotizadorModel.SolicitudRegla.IdProducto;
                command.Parameters.Add("@IdCliente", SqlDbType.NVarChar, 80).Value = cotizadorModel.SolicitudRegla.IdCliente;
                command.Parameters.Add("@EstadoVehiculo", SqlDbType.NVarChar, 80).Value = cotizadorModel.SolicitudRegla.EstadoVehiculo;
                command.Parameters.Add("@Servicio", SqlDbType.NVarChar, 80).Value = cotizadorModel.SolicitudRegla.Servicio;
                command.Parameters.Add("@Perfil", SqlDbType.Int, 80).Value = cotizadorModel.GetIdPerfilUsuarioSesion();

                SqlDataReader reglasNegocio = iGenericDataAccess.StoredProcedure(command);
                if (reglasNegocio.HasRows)
                {
                    while (reglasNegocio.Read())
                    {
                        ValoresReglaModel valoresRegla = new ValoresReglaModel
                                                         {
                                                             ValorId = reglasNegocio["ValorId"].ToString(),
                                                             Valor = reglasNegocio["Valor"].ToString()
                                                         };
                        reglaList.Add(valoresRegla);
                    }
                }

                iGenericDataAccess.CloseConnection();

                if (reglaList.Count == 0)
                {
                    throw new DomainException(CodesCotizador.INF_01_07);
                }
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

            return reglaList.GroupBy(reglas => new
                                               {
                                                   reglas.ValorId,
                                                   reglas.Valor
                                               },
                                     (reglas, group) => new
                                                        {
                                                            ValorIdKey = reglas.ValorId,
                                                            ValorKey = reglas.Valor
                                                        }).Select(x => new ValoresReglaModel()
                                                                       {
                                                                           ValorId = x.ValorIdKey,
                                                                           Valor = x.ValorKey
                                                                       }).ToList();
        }

        public CabeceraCotizacionModel EjecutaGrabadoSolicitudCotizacion(CabeceraCotizacionModel cabeceraCotizacionModel)
        {
            try
            {
                SolicitudCotizacionModel solicitud = new SolicitudCotizacionModel();

                /*comparadorModel = {
                                    datosCotizacionModel:
                                    {
                                        SolicitudId: parseInt($stateParams.params.idSolicitud),
                                        FormaPagoId: FormaPago.FormaPagoId
                
                                    }*/
                SqlCommand command = new SqlCommand
                                     {
                                         CommandText = ConstStoredProcedures.GrabadoSolicitudCotizacion
                                     };

                #region ParametrosProcedimiento

                command.Parameters.Add("@ClienteID", SqlDbType.BigInt).Value = cabeceraCotizacionModel.Cliente.Cliente.ClienteId;
                command.Parameters.Add("@ProductoID", SqlDbType.BigInt).Value = cabeceraCotizacionModel.Cliente.Producto.ProductoId;
                command.Parameters.Add("@TipoVehiculoID", SqlDbType.BigInt).Value = cabeceraCotizacionModel.Vehiculo.Antiguedad.ValorId;
                command.Parameters.Add("@Renovacion", SqlDbType.Int).Value = 0;
                command.Parameters.Add("@ServicioID", SqlDbType.BigInt).Value = Convert.ToInt32(cabeceraCotizacionModel.Vehiculo.Servicio.ServicioId);
                command.Parameters.Add("@ValorFactura", SqlDbType.Decimal).Value = cabeceraCotizacionModel.Vehiculo.Valor;
                command.Parameters.Add("@UsoID", SqlDbType.BigInt).Value = cabeceraCotizacionModel.Vehiculo.TipoUnidad.ValorId;
                command.Parameters.Add("@Modelo", SqlDbType.VarChar, 5).Value = cabeceraCotizacionModel.Vehiculo.Modelo.Valor;
                command.Parameters.Add("@Marca", SqlDbType.VarChar, 100).Value = cabeceraCotizacionModel.Vehiculo.Armadora.Valor;
                command.Parameters.Add("@Submarca", SqlDbType.VarChar, 100).Value = cabeceraCotizacionModel.Vehiculo.SubMarca.Valor;
                command.Parameters.Add("@Descripcion", SqlDbType.VarChar, 100).Value = cabeceraCotizacionModel.Vehiculo.Version.Descripcion;
                command.Parameters.Add("@EstadoCirculacion", SqlDbType.VarChar, 100).Value = cabeceraCotizacionModel.Cotizacion.Estado != null ? cabeceraCotizacionModel.Cotizacion.Estado.Estado : cabeceraCotizacionModel.Cotizacion.CP.Estado;
                command.Parameters.Add("@EstadoCirculacionID", SqlDbType.Int).Value = cabeceraCotizacionModel.Cotizacion.Estado != null ? cabeceraCotizacionModel.Cotizacion.Estado.EstadoId : cabeceraCotizacionModel.Cotizacion.CP.EstadoId;
                command.Parameters.Add("@Plazo", SqlDbType.Int).Value = cabeceraCotizacionModel.Cotizacion.Plazo.Valor;
                command.Parameters.Add("@MonedaID", SqlDbType.VarChar).Value = "76";
                command.Parameters.Add("@LoJack", SqlDbType.Int).Value = cabeceraCotizacionModel.Vehiculo.LoJack;
                command.Parameters.Add("@AgenciaID", SqlDbType.Int).Value = cabeceraCotizacionModel.Cliente.Agencia != null ? cabeceraCotizacionModel.Cliente.Agencia.IdAgencia : 0;
                command.Parameters.Add("@UsuarioID", SqlDbType.Int).Value = cabeceraCotizacionModel.GetIdUsuarioSesion();
                command.Parameters.Add("@SumaEEspicial", SqlDbType.Decimal).Value = 0.0;
                command.Parameters.Add("@DescripcionEE", SqlDbType.VarChar).Value = string.Empty;
                command.Parameters.Add("@SumaAdaptaciones", SqlDbType.Decimal).Value = 0.0;
                command.Parameters.Add("@DescripcionAdaptaciones", SqlDbType.VarChar, 100).Value = string.Empty;
                command.Parameters.Add("@Deducible", SqlDbType.VarChar, 100).Value = string.Empty;
                command.Parameters.Add("@DeducibleOpcion", SqlDbType.VarChar, 100).Value = string.Empty;
                command.Parameters.Add("@InicioVigencia", SqlDbType.VarChar, 20).Value = cabeceraCotizacionModel.Cotizacion.InicioVigencia.ToString("yyyy-dd-MM 00:00:00"); //"2017-28-04 00:00:00"
                command.Parameters.Add("@FinVigencia", SqlDbType.VarChar, 20).Value = cabeceraCotizacionModel.Cotizacion.FinVigencia.ToString("yyyy-dd-MM 00:00:00"); //"2022-04-28 00:00:00"
                command.Parameters.Add("@PorcentajeUDI", SqlDbType.Int).Value = cabeceraCotizacionModel.Cotizacion.Udi != null ? Convert.ToInt32(cabeceraCotizacionModel.Cotizacion.Udi.Valor) : 0;
                command.Parameters.Add("@udis", SqlDbType.VarChar, 100).Value = cabeceraCotizacionModel.Cotizacion.Udi != null ? cabeceraCotizacionModel.Cotizacion.Udi.Valor : string.Empty;
                command.Parameters.Add("@pKM", SqlDbType.VarChar, 100).Value = "0";
                command.Parameters.Add("@TipoPago", SqlDbType.VarChar, 5).Value = ConsultaFormasPagoProducto(cabeceraCotizacionModel.Cliente.Producto.ProductoId)[0].FormaPagoId;

                #endregion

                SqlDataReader grabadoSolicitud = iGenericDataAccess.StoredProcedure(command);
                if (grabadoSolicitud.HasRows)
                {
                    while (grabadoSolicitud.Read())
                    {
                        cabeceraCotizacionModel.IdSolicitud = Convert.ToInt32(grabadoSolicitud[0].ToString());
                        DatosSolicitudModel dsolicitud = new DatosSolicitudModel
                                                         {
                                                             SolicitudId = cabeceraCotizacionModel.IdSolicitud
                                                         };

                        solicitud = ConsultarSolicitudCotizacion(dsolicitud)[0];
                        if (cabeceraCotizacionModel.IdSolicitud != 0 && cabeceraCotizacionModel.Cotizacion.CP != null)
                        {
                            IList<SolicitudCotizacionModel> solicitudCotizacion = ConsultarSolicitudCotizacion(dsolicitud);
                            if (cabeceraCotizacionModel.Cotizacion.CP != null)
                            {
                                solicitud = solicitudCotizacion[0];
                                EjecutaGrabadoSolicitudCotizacionCodigosPostales(solicitud, cabeceraCotizacionModel.Cotizacion.CP);
                            }
                        }

                        iGenericDataAccess.CloseConnection();
                        iGenericDataAccess.OpenConnection();
                        SolicitudCotizacion solicitudCotizacionEntity = iGenericDataAccess.Consultar(new SolicitudCotizacion()
                                                                                                     {
                                                                                                         SolicitudId = solicitud.SolicitudId
                                                                                                     }, new OptionsQueryZero()
                                                                                                        {
                                                                                                            ExcludeNumericsDefaults = true,
                                                                                                            ExcludeBool = true
                                                                                                        })[0];
                        solicitudCotizacionEntity.TipoCarga = cabeceraCotizacionModel.Vehiculo.Carga?.Valor;
                        solicitudCotizacionEntity.Ocupantes = cabeceraCotizacionModel.Vehiculo.Pasajero?.Pasajeros ?? 0;
                        solicitudCotizacionEntity.TipoArrendamiento = cabeceraCotizacionModel.Cliente.TipoArrendamiento?.ElementoId ?? 0;
                        solicitudCotizacionEntity.Remolques = cabeceraCotizacionModel.Vehiculo.Remolque?.Nombre;

                        iGenericDataAccess.Actualizar(solicitudCotizacionEntity);
                    }
                }
                if (cabeceraCotizacionModel.IdSolicitud != 0)
                {
                    GuardaDatosCotizanteCoberturas(cabeceraCotizacionModel, solicitud);
                }
                iGenericDataAccess.CloseConnection();
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

            return cabeceraCotizacionModel;
        }

        public IList<FormasPagoProductoModel> ConsultaFormasPagoProducto(int productoId)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<VwCotSelFormasPagoProducto> formasPagoProductos = iGenericDataAccess.Consultar(
                                                                                                     new VwCotSelFormasPagoProducto()
                                                                                                     {
                                                                                                         ProductoId = productoId,
                                                                                                         Predeterminado = true
                                                                                                     },
                                                                                                     new OptionsQueryZero()
                                                                                                     {
                                                                                                         ExcludeNumericsDefaults = true,
                                                                                                         ExcludeBool = true
                                                                                                     }
                                                                                                    );
                if (formasPagoProductos.Count == 0)
                {
                    formasPagoProductos = iGenericDataAccess.Consultar(
                                                                       new VwCotSelFormasPagoProducto()
                                                                       {
                                                                           ProductoId = productoId
                                                                       },
                                                                       new OptionsQueryZero()
                                                                       {
                                                                           ExcludeNumericsDefaults = true,
                                                                           ExcludeBool = true
                                                                       }
                                                                      );
                    if (formasPagoProductos.Count == 0)
                    {
                        throw new DalException(CodesCotizador.ERR_01_03);
                    }
                }
                iGenericDataAccess.CloseConnection();
                IList<FormasPagoProductoModel> listFormasPagoProducto = formasPagoProductos.Select(
                                                                                                   x => new FormasPagoProductoModel()
                                                                                                        {
                                                                                                            FormaPagoId = x.FormaPagoId,
                                                                                                            Nombre = x.Nombre
                                                                                                        }).ToList();
                return listFormasPagoProducto;
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
        }

        public static string SerializeObject(Object toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }

        private void GuardaDatosCotizanteCoberturas(CabeceraCotizacionModel cabeceraCotizacionModel,
                                                    SolicitudCotizacionModel solicitud)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                if (cabeceraCotizacionModel.IdSolicitud == 0)
                {
                    throw new DalException(CodesCalendario.INF_07_00);
                }
                StringBuilder xml = new StringBuilder();
                xml.Append("<CabeceraCotizacionModel>");
                xml.Append("<IdSolicitud>").Append(cabeceraCotizacionModel.IdSolicitud).Append("</IdSolicitud>");

                xml.Append(ReflectionUtil.ObjectToXml(cabeceraCotizacionModel.Cliente));
                xml.Append(ReflectionUtil.ObjectToXml(cabeceraCotizacionModel.Cotizacion));
                xml.Append(ReflectionUtil.ObjectToXml(cabeceraCotizacionModel.Vehiculo));

                xml.Append("<Panel>");
                if (solicitud.Flexible && null != cabeceraCotizacionModel.Panel)
                {
                    xml.Append("<IdProducto>").Append(cabeceraCotizacionModel.Panel.IdProducto).Append("</IdProducto>");
                    xml.Append("<IdTipoVehiculo>").Append(cabeceraCotizacionModel.Panel.IdTipoVehiculo).Append("</IdTipoVehiculo>");
                    xml.Append("<IdTipoServicioVehiculo>").Append(cabeceraCotizacionModel.Panel.IdTipoServicioVehiculo).Append("</IdTipoServicioVehiculo>");
                    xml.Append("<IdCondicionVehiculo>").Append(cabeceraCotizacionModel.Panel.IdCondicionVehiculo).Append("</IdCondicionVehiculo>");

                    xml.Append("<Aseguradoras>");
                    if (null != cabeceraCotizacionModel.Panel.Aseguradoras)
                    {
                        foreach (AseguradoraModel aseguradoraModel in cabeceraCotizacionModel.Panel.Aseguradoras)
                        {
                            xml.Append(ReflectionUtil.ObjectToXml(aseguradoraModel));
                        }
                    }
                    xml.Append("</Aseguradoras>");

                    xml.Append("<Coberturas>");
                    if (null != cabeceraCotizacionModel.Panel.Coberturas)
                    {
                        foreach (CoberturaModel cobertura in cabeceraCotizacionModel.Panel.Coberturas)
                        {
                            //if (cobertura.IsSeleccionada)
                            //{
                            xml.Append("<CoberturaModel>");
                            xml.Append("<IdCobertura>").Append(cobertura.IdCobertura).Append("</IdCobertura>");
                            xml.Append("<NombreCobertura>").Append(cobertura.NombreCobertura).Append("</NombreCobertura>");
                            xml.Append("<FiltroValorRangoSuma>").Append(cobertura.FiltroValorRangoSuma).Append("</FiltroValorRangoSuma>");
                            xml.Append("<FiltroValorRangoDeducible>").Append(cobertura.FiltroValorRangoDeducible).Append("</FiltroValorRangoDeducible>");
                            xml.Append("<IsSeleccionada>").Append(cobertura.IsSeleccionada).Append("</IsSeleccionada>");
                            xml.Append("<IsFija>").Append(cobertura.IsFija).Append("</IsFija>");
                            xml.Append("<IsEspecial>").Append(cobertura.IsEspecial).Append("</IsEspecial>");
                            xml.Append("</CoberturaModel>");
                            //}
                        }
                    }
                    xml.Append("</Coberturas>");
                }
                xml.Append("</Panel>");
                xml.Append("</CabeceraCotizacionModel>");

                SqlCommand command = new SqlCommand
                                     {
                                         CommandText = ConstStoredProcedures.SpCotInsertDatosCotizanteCobertCoti
                                     };
                command.Parameters.Add("@CabeceraCotizacionXML", SqlDbType.NVarChar, xml.Capacity).Value = xml.ToString();
                SqlDataReader cotizacioniDetalle = iGenericDataAccess.StoredProcedure(command);

                bool isError = false;
                string mensajeUsuario = string.Empty;
                string mensajeSistema = string.Empty;
                if (cotizacioniDetalle.HasRows)
                {
                    while (cotizacioniDetalle.Read())
                    {
                        isError = bool.Parse(cotizacioniDetalle["IsError"].ToString());
                        mensajeUsuario = cotizacioniDetalle["MensajeUsuario"].ToString();
                        mensajeSistema = cotizacioniDetalle["MensajeSistema"].ToString();
                    }
                }
                iGenericDataAccess.CloseConnection();
                if (isError)
                {
                    throw new DalException(mensajeUsuario, new Exception(mensajeSistema));
                }
            }
            catch (DalException)
            {
                throw;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesCalendario.ERR_07_03, e);
            }
        }

        public IList<UsuarioPerfilModel> ValidaUsuarioPerfil(CotizadorModel cotizadorModel)
        {
            try
            {
                var sWhere = new StringBuilder();

                sWhere.Append("PerfilId = " + cotizadorModel.GetIdPerfilUsuarioSesion());
                sWhere.Append(" OR ");
                sWhere.Append("(PerfilId = " + cotizadorModel.GetIdPerfilUsuarioSesion());
                sWhere.Append(" AND ");
                sWhere.Append("PersonaId = " + cotizadorModel.GetIdUsuarioSesion() + ")");

                iGenericDataAccess.OpenConnection();
                IList<PerfilesFlexible> perfilesFlexible = iGenericDataAccess.Consultar(
                                                                                        new PerfilesFlexible(),
                                                                                        new OptionsQueryZero()
                                                                                        {
                                                                                            ExcludeNumericsDefaults = true,
                                                                                            ExcludeBool = true,
                                                                                            WhereComplementary = sWhere.ToString()
                                                                                        });
                iGenericDataAccess.CloseConnection();
                IList<UsuarioPerfilModel> usuarioList = perfilesFlexible.Select(
                                                                                x => new UsuarioPerfilModel()
                                                                                     {
                                                                                         UsuarioId = x.PersonaId,
                                                                                         PerfilId = x.PerfilId
                                                                                     }).ToList();
                return usuarioList;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        /*********************************************************************
               * @description: Metodos para insertar en la tabla de CotizacionCodigoPostal
               * @params : Solicitud y regionPos
               */

        private void EjecutaGrabadoSolicitudCotizacionCodigosPostales(SolicitudCotizacionModel solicitudModel, RegionCodigoPostalModel regionModel)
        {
            try
            {
                SqlCommand command = new SqlCommand
                                     {
                                         CommandText = ConstStoredProcedures.SpCotMovimientosCodigosPostales
                                     };

                #region ParametrosProcedimiento

                command.Parameters.Add("@pTipo", SqlDbType.BigInt).Value = 1;
                command.Parameters.Add("@pIdSolicitudCotizacion", SqlDbType.BigInt).Value = solicitudModel.SolicitudId;
                command.Parameters.Add("@pCodigoPostal", SqlDbType.VarChar).Value = regionModel.CodigoPostal;
                command.Parameters.Add("@pPais", SqlDbType.VarChar).Value = regionModel.Pais;
                command.Parameters.Add("@pEstado", SqlDbType.VarChar).Value = regionModel.Estado;
                command.Parameters.Add("@pDelegacion", SqlDbType.VarChar).Value = regionModel.Delegacion;
                command.Parameters.Add("@pAsentamiento", SqlDbType.BigInt).Value = regionModel.Asentamiento;
                command.Parameters.Add("@pIdMunicipio", SqlDbType.BigInt).Value = regionModel.Mnpio;
                command.Parameters.Add("@pColonia", SqlDbType.VarChar).Value = regionModel.Colonia;
                command.Parameters.Add("@pUsuarioId", SqlDbType.BigInt).Value = solicitudModel.UsuarioId;
                command.Parameters.Add("@pAgenciaId", SqlDbType.BigInt).Value = solicitudModel.AgenciaId;

                #endregion

                iGenericDataAccess.StoredProcedure(command);
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
        }

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

        public List<FechaSistemaModel> ConsultaFechaSistema()
        {
            try
            {
                StringBuilder sConsultaFechaSistema = new StringBuilder();
                sConsultaFechaSistema.Append("SELECT GetDate() AS FechaSis");

                iGenericDataAccess.OpenConnection();
                IList<FechaSistema> fechaList = iGenericDataAccess.ExecuteQuery<FechaSistema>(sConsultaFechaSistema.ToString());

                iGenericDataAccess.CloseConnection();
                List<FechaSistemaModel> fecha = fechaList.Select(
                                                                 x => new FechaSistemaModel()
                                                                      {
                                                                          FechaSistema = x.FechaSis
                                                                      }).ToList();
                return fecha;
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

        public IList<CoberturaModel> ConsultaCoberturasCotizadas(SolicitudCotizacionModel solicitudCotizacion)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<CoberturasCotizacion> neCoberturasCotizacion = iGenericDataAccess.Consultar(
                                                                                                  new CoberturasCotizacion()
                                                                                                  {
                                                                                                      IdSolicitud = solicitudCotizacion.SolicitudId
                                                                                                  },
                                                                                                  new OptionsQueryZero()
                                                                                                  {
                                                                                                      ExcludeNumericsDefaults = true,
                                                                                                      ExcludeBool = true
                                                                                                  });
                iGenericDataAccess.CloseConnection();
                IList<CoberturaModel> coberturasList = neCoberturasCotizacion.Select(
                                                                                     x => new CoberturaModel()
                                                                                          {
                                                                                              IdCobertura = x.IdCobertura,
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

        public bool ExisteDaniosMaterialesCotizacionFlex(int idSolicitud)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<SpCotSelExisteDaniosMaterialesFlex> spCotSelExisteDm = iGenericDataAccess.ExecuteStoredProcedure(new SpCotSelExisteDaniosMaterialesFlex
                                                                                                                       {
                                                                                                                           SolicitduId = idSolicitud.ToString()
                                                                                                                       });
                iGenericDataAccess.CloseConnection();
                return spCotSelExisteDm[0].ExisteDm;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesCotizador.ERR_01_08, e);
            }
        }

        public IList<ClientesModel> ConsultarClientesDeAgencia(CotizadorModel cotizadorModel)
        {
            try
            {
                int noClientes = ConsultarCantidadCliente(cotizadorModel);
                //SELECT ne.PersonaID Id, ne.Nombre FROM dbo.nePersonas ne INNER JOIN (SELECT PerfilID, PersonaID, Valor FROM dbo.PerfilDatos WHERE opcion = @OpcionId AND PerfilID = @PerfilId AND PersonaID = @UsuarioId AND ISNUMERIC(Valor) = 1) pf ON ne.PersonaID = pf.Valor
                iGenericDataAccess.OpenConnection();
                string where = "";
                if (noClientes>20)
                {
                    where = " WHERE ISNULL(clientes.Nombre, '') + ISNULL(clientes.Paterno, '') LIKE '%" + cotizadorModel.ClienteModel.Cliente + "%'";
                }
                IList<VwCotSelClientesUsuario> vwCotSelClientes = iGenericDataAccess.ExecuteQuery<VwCotSelClientesUsuario>(CQuerysCotizador.QryClientesDeAgencia.Replace("@Where", where).Replace("@AgenciaId", cotizadorModel.GetIdUsuarioSesion().ToString()));

                iGenericDataAccess.CloseConnection();
                IList<ClientesModel> clientesList = vwCotSelClientes.Select(
                                                                            x => new ClientesModel
                                                                                 {
                                                                                     ClienteId = Convert.ToInt32(x.Id),
                                                                                     Cliente = x.Nombre
                                                                                 }).ToList().OrderBy(x => x.Cliente).ToList();
                return clientesList;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
        }
    }
}