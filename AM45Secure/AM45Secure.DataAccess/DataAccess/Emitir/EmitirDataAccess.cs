using System;
using System.Collections.Generic;
using System.Linq;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Emitir;
using AM45Secure.Commons.Recursos;
using AM45Secure.Commons.Modelos.ConfigMultiple;
using AM45Secure.DataAccess.Entidades.Comunes;
using AM45Secure.DataAccess.Entidades.Cotizador;
using AM45Secure.DataAccess.IDataAccess.IEmitir;
using AM45Secure.DataAccess.IDataAccess.IGeneric;
using Zero.Ado.Models;
using Zero.Exceptions;
using System.Data.SqlClient;
using AM45Secure.Commons.Constantes.Comunes;
using System.Data;

namespace AM45Secure.DataAccess.DataAccess.Emitir
{
    public class EmitirDataAccess : IEmitirDataAccess
    {
        private readonly IGenericDataAccess iGenericDataAccess;

        public EmitirDataAccess(IGenericDataAccess iGenericDataAccess)
        {
            this.iGenericDataAccess = iGenericDataAccess;
        }

        public IList<ElementoModel> ConsultaElementosPorCatalogoId(ElementoModel elementoModel)
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
                IList<ElementoModel> elementosList = elementos.Select(
                                                                      x => new ElementoModel()
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
                throw new DalException(CodesEmision.ERR_00_00, e);
            }
        }

        public DatosEmitirModel ConsultaDatosCotizacion(SolicitudPrimaCotizacion solicitudPrima)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<VwCotSelPrimasCotizacion> primas = iGenericDataAccess.Consultar(new VwCotSelPrimasCotizacion()
                                                                                      {
                                                                                          Numero = solicitudPrima.Numero,
                                                                                          CotizacionId = solicitudPrima.CotizacionId
                                                                                      }, new OptionsQueryZero()
                                                                                         {
                                                                                             ExcludeBool = true,
                                                                                             ExcludeNumericsDefaults = true
                                                                                         });
                iGenericDataAccess.CloseConnection();

                IList<DatosEmitirModel> primasList = primas.Select(x => new DatosEmitirModel()
                                                                        {
                                                                            SolicitudInt = x.SolicitudId,
                                                                            ServicioId = x.ServicioId,
                                                                            ClienteId = x.ClienteId,
                                                                            ProductoId = x.ProductoId,
                                                                            Numero = x.Numero,
                                                                            PaqueteId = x.PaqueteId,
                                                                            AseguradoraId = x.AseguradoraId,
                                                                            FormaPago = x.FormaPago,
                                                                            UsuarioId = x.UsuarioId,
                                                                            CotizacionId = x.CotizacionId,
                                                                            Cliente = x.Cliente
                                                                        }).ToList();

                if (primasList.Count>0)
                {
                    return primasList[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesEmision.ERR_00_01, e);
            }
        }

        public IList<NeIncisoEndoso> ConsultaNeIncisosEndoso(NeIncisoEndoso neIncisoEndoso)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<NeIncisosEndoso> neIncisosEndosos = iGenericDataAccess.Consultar(new NeIncisosEndoso()
                                                                                       {
                                                                                           Poliza = neIncisoEndoso.Poliza
                                                                                       }, new OptionsQueryZero()
                                                                                          {
                                                                                              ExcludeBool = true,
                                                                                              ExcludeNumericsDefaults = true
                                                                                          });
                iGenericDataAccess.CloseConnection();

                IList<NeIncisoEndoso> incisosEndosoList = neIncisosEndosos.Select(x => new NeIncisoEndoso()
                                                                                       {
                                                                                           Poliza = x.Poliza,
                                                                                           Inciso = x.Inciso,
                                                                                           Endoso = x.Endoso
                                                                                       }).ToList();

                if (incisosEndosoList.Count>0)
                {
                    return incisosEndosoList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesEmision.ERR_00_02, e);
            }
        }

        public IList<ElementoModel> ConsultaElementosPorElementoId(int elementoId)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<Elementos> elementos = iGenericDataAccess.Consultar(
                                                                          new Elementos()
                                                                          {
                                                                              ElementoId = elementoId
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
                                                                               Nombre = x.Nombre.ToUpper(),
                                                                               Comodin = x.Comodin
                                                                           }).ToList();
                return elementosList;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesEmision.ERR_00_00, e);
            }
        }

        public IList<VehiculoModel> ConsultaSerie(VehiculoModel vehiculo)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<VwCotSelSerie> serie = iGenericDataAccess.Consultar(
                                                                          new VwCotSelSerie()
                                                                          {
                                                                              Serie = vehiculo.Serie
                                                                          },
                                                                          new OptionsQueryZero()
                                                                          {
                                                                              ExcludeNumericsDefaults = true,
                                                                              ExcludeBool = true
                                                                          });
                iGenericDataAccess.CloseConnection();

                if (serie.Count>0)
                {
                    throw new DomainException(CodesEmision.INF_00_00 + serie[0].Poliza);
                }

                IList<VehiculoModel> vehiculoList = serie.Select(
                                                                 x => new VehiculoModel()
                                                                      {
                                                                          Serie = x.Serie
                                                                      }).ToList();

                return vehiculoList;
            }
            catch (DomainException e)
            {
                throw new DomainException(e.Mensaje);
            }
            catch
                (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesEmision.ERR_00_17, e);
            }
        }

        public ProductoModel ConsultaProductosFlex(ProductoModel producto)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<Productos> productoList = iGenericDataAccess.Consultar(new Productos()
                                                                             {
                                                                                 ProductoId = producto.ProductoId
                                                                             }, new OptionsQueryZero()
                                                                                {
                                                                                    ExcludeBool = true,
                                                                                    ExcludeNumericsDefaults = true
                                                                                });
                iGenericDataAccess.CloseConnection();

                IList<ProductoModel> productos = productoList.Select(x => new ProductoModel()
                                                                          {
                                                                              ProductoId = x.ProductoId,
                                                                              Flexible = Convert.ToDouble(x.Flexible)
                                                                          }).ToList();

                if (productos.Count>0)
                {
                    return productos[0];
                }

                return null;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesEmision.ERR_00_01, e);
            }
        }

        public IList<VehiculoGrabModel> ConsultaSerieGrab(VehiculoGrabModel vehiculo)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<VwCotSelSerie> serie = iGenericDataAccess.Consultar(
                                                                          new VwCotSelSerie()
                                                                          {
                                                                              Serie = vehiculo.Serie
                                                                          },
                                                                          new OptionsQueryZero()
                                                                          {
                                                                              ExcludeNumericsDefaults = true,
                                                                              ExcludeBool = true
                                                                          });
                iGenericDataAccess.CloseConnection();

                if (serie.Count > 0)
                {
                    throw new DomainException(CodesEmision.INF_00_00 + serie[0].Poliza);
                }

                IList<VehiculoGrabModel> vehiculoList = serie.Select(
                                                                 x => new VehiculoGrabModel()
                                                                 {
                                                                     Serie = x.Serie
                                                                 }).ToList();

                return vehiculoList;
            }
            catch (DomainException e)
            {
                throw new DomainException(e.Mensaje);
            }
            catch
                (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesEmision.ERR_00_17, e);
            }
        }

        /* INDRA FJQP Implementacion de config Emisión Multiple */
        public FiltroConfigMultiple FiltrosConfigMultiple(string IdUsuario)
        {
            //
            var Datos = new FiltroConfigMultiple();

            List<Commons.Modelos.ConfigMultiple.ClientesConfig> listClientes = new List<Commons.Modelos.ConfigMultiple.ClientesConfig>();
            List<Commons.Modelos.ConfigMultiple.ProductosConfig> listProdcutos = new List<Commons.Modelos.ConfigMultiple.ProductosConfig>();
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpSelAseguradorasConfigMultiple
                };
                command.Parameters.Add("@UsuarioId", SqlDbType.VarChar).Value = IdUsuario;

                SqlDataReader datosStored = iGenericDataAccess.StoredProcedure(command);
                if (datosStored.HasRows)
                {
                    while (datosStored.Read())
                    {
                        listClientes.Add(new Commons.Modelos.ConfigMultiple.ClientesConfig
                        {
                            Clave = Convert.ToString(datosStored["Clave"]),
                            Nombre = Convert.ToString(datosStored["Nombre"]),
                        });

                    }
                    iGenericDataAccess.CloseConnection();
                }


                SqlCommand command2 = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpSelProductosPorUsuarioFlex
                };
                command2.Parameters.Add("@UsuarioId", SqlDbType.VarChar).Value = IdUsuario;

                SqlDataReader datosStored2 = iGenericDataAccess.StoredProcedure(command2);
                if (datosStored2.HasRows)
                {
                    while (datosStored2.Read())
                    {
                        listProdcutos.Add(new Commons.Modelos.ConfigMultiple.ProductosConfig
                        {
                            Clave = Convert.ToString(datosStored2["Clave"]),
                            Nombre = Convert.ToString(datosStored2["Nombre"]),
                        });

                    }
                    iGenericDataAccess.CloseConnection();
                }

                Datos.ClientesList = listClientes;
                Datos.ProductosList = listProdcutos;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }

            return Datos;
        }
        // INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos
        public bool UsuarioAdministrador(string userID)
        {

            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpEsUsuarioAdministradorFlex
                };

                command.Parameters.Add("@UsuarioId", SqlDbType.VarChar).Value = userID;


                SqlDataReader datosStored = iGenericDataAccess.StoredProcedure(command);
                int valor = 0;
                if (datosStored.HasRows)
                {
                    while (datosStored.Read())
                    {

                        valor = Convert.ToInt32(datosStored["admin"]);

                    };

                }
                if (valor == 1)
                    return true;
            }
            catch (Exception e)
            {
                throw new DalException(Codes.ERR_00_01, e);
            }
            finally
            {
                iGenericDataAccess.CloseConnection();
            }

            return false;

        }
        // INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos
        public IList<configMultipleRegModel> ConsultarConfigMultiples(ConfigRequestModel configRequestModel)
        {
            List<configMultipleRegModel> listDatos = new List<configMultipleRegModel>();
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpObtieneDatosConfigMultiple
                };
                command.Parameters.Add("@IdAseguradora", SqlDbType.BigInt).Value = configRequestModel.Aseguradora;
                command.Parameters.Add("@IdProducto", SqlDbType.BigInt).Value = configRequestModel.Producto;
                command.Parameters.Add("@IdPerfil", SqlDbType.VarChar).Value = configRequestModel.Perfil;
                command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = configRequestModel.Usuario;

                SqlDataReader datosStored = iGenericDataAccess.StoredProcedure(command);
                if (datosStored.HasRows)
                {
                    while (datosStored.Read())
                    {
                        listDatos.Add(new configMultipleRegModel
                        {

                            idAseguradora = Convert.ToInt32(datosStored["idAseguradora"]),
                            Aseguradora = Convert.ToString(datosStored["Aseguradora"]),
                            idProducto = Convert.ToInt32(datosStored["idProducto"]),
                            Producto = Convert.ToString(datosStored["Producto"]),
                            idPerfil = Convert.ToInt32(datosStored["idPerfil"]),
                            Perfil = Convert.ToString(datosStored["Perfil"]),
                            idUsuario = Convert.ToInt32(datosStored["idUsuario"]),
                            Usuario = Convert.ToString(datosStored["Usuario"]),
                            iPermiteEmisionMultiple = Convert.ToInt32(datosStored["iPermiteEmisionMultiple"]),
                            iPermiteContrato = Convert.ToInt32(datosStored["iPermiteContrato"])

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
        // INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos
        public bool GuardarDatosConfigMultiple(InsertConfigMultipleModel requestModel, string usuario)
        {
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = (requestModel.IsUpdate == 0) ? ConstStoredProcedures.SpInsertConfigMultipleContratos : ConstStoredProcedures.SpUpdateConfigMultipleContratos
                };

                command.Parameters.Add("@idAseguradora", SqlDbType.Int).Value = requestModel.Aseguradora;
                command.Parameters.Add("@idProducto", SqlDbType.Int).Value = requestModel.Producto;
                command.Parameters.Add("@idPerfil", SqlDbType.Int).Value = requestModel.Perfil;
                command.Parameters.Add("@idUsuario", SqlDbType.Int).Value = requestModel.Usuario;
                command.Parameters.Add("@iPermiteEmisionMultiple", SqlDbType.Int).Value = requestModel.PermiteEmisionMultiple;
                command.Parameters.Add("@iPermiteContratos", SqlDbType.Int).Value = requestModel.PermiteCapContratos;
                SqlDataReader datosStored = iGenericDataAccess.StoredProcedure(command);
                iGenericDataAccess.CloseConnection();
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
            return true;
        }
        // INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos
        public bool EliminarDatosConfigMultiple(InsertConfigMultipleModel requestModel, string usuario)
        {
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpDeleteConfigMultipleContratos
                };

                command.Parameters.Add("@idAseguradora", SqlDbType.Int).Value = requestModel.Aseguradora;
                command.Parameters.Add("@idProducto", SqlDbType.Int).Value = requestModel.Producto;
                command.Parameters.Add("@idPerfil", SqlDbType.Int).Value = requestModel.Perfil;
                command.Parameters.Add("@idUsuario", SqlDbType.Int).Value = requestModel.Usuario;

                SqlDataReader datosStored = iGenericDataAccess.StoredProcedure(command);

                iGenericDataAccess.CloseConnection();

            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
            return true;
        }
        // INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos
        public IList<configMultipleRegModel> PermiteConfigMultiples(ConfigRequestModel configRequestModel)
        {
            List<configMultipleRegModel> listDatos = new List<configMultipleRegModel>();
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpPermiteEmisionMultiple
                };
                command.Parameters.Add("@IdAseguradora", SqlDbType.BigInt).Value = configRequestModel.Aseguradora;
                command.Parameters.Add("@IdProducto", SqlDbType.BigInt).Value = configRequestModel.Producto;
                command.Parameters.Add("@IdPerfil", SqlDbType.VarChar).Value = configRequestModel.Perfil;
                command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = configRequestModel.Usuario;

                SqlDataReader datosStored = iGenericDataAccess.StoredProcedure(command);
                if (datosStored.HasRows)
                {
                    while (datosStored.Read())
                    {
                        listDatos.Add(new configMultipleRegModel
                        {
                            iPermiteEmisionMultiple = Convert.ToInt32(datosStored["iPermiteEmisionMultiple"]),
                            iPermiteContrato = Convert.ToInt32(datosStored["iPermiteContrato"]),
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
    }
}