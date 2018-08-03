using AM45Secure.Commons.Constantes.Comunes;
using AM45Secure.Commons.Constantes.Querys;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Configurador;
using AM45Secure.Commons.Modelos.Cotizador;
using AM45Secure.Commons.Recursos;
using AM45Secure.DataAccess.Entidades.Comunes;
using AM45Secure.DataAccess.Entidades.Cotizador;
using AM45Secure.DataAccess.IDataAccess.IConfigurador;
using AM45Secure.DataAccess.IDataAccess.IGeneric;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado.Models;
using Zero.Exceptions;
using Zero.Handlers.Response;

namespace AM45Secure.DataAccess.DataAccess.Configurador
{
    public class ConfiguradorDataAccess : IConfiguradorDataAccess
    {
        private readonly IGenericDataAccess iGenericDataAccess;

        public SqlConnection Conexion { get; set; }

        private readonly string NA = "NA";

        private readonly string CHECKED = "CHECKED";

        private readonly string UNCHECKED = "UNCHECKED";

        public ConfiguradorDataAccess(IGenericDataAccess iGenericDataAccess)
        {
            this.iGenericDataAccess = iGenericDataAccess;
        }

        public IList<nePersonasModel> ConsultarClientesConfigurador()
        {
            try
            {
                string defaultName = "DAIMLER";
                iGenericDataAccess.OpenConnection();
                IList<nePersonasModel> SelAllClientes = iGenericDataAccess.ExecuteQuery<nePersonasModel>("select PersonaID, Nombre from nePersonas where Nombre Like '%" + defaultName + "%'");

                iGenericDataAccess.CloseConnection();

           
                    IList<nePersonasModel> listaClientes = SelAllClientes.Select(x => new nePersonasModel
                    {
                        PersonaID = x.PersonaID,
                        Nombre = x.Nombre

                    }).ToList();
              

                return listaClientes;
            }
            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }
        }

        public IList<ProductoModel> ConsultaProductosFlexibles()
        {

            try
            {
                iGenericDataAccess.OpenConnection();
                IList<ProductoModel> SelProductosFlexibles = iGenericDataAccess.ExecuteQuery<ProductoModel>("select ProductoID,Nombre as NombreProducto,Flexible,CP as Cp from Productos where Flexible = 1");
                iGenericDataAccess.CloseConnection();

                IList<ProductoModel> listaProductos = SelProductosFlexibles.Select(x => new ProductoModel
                {
                    ProductoId = x.ProductoId,
                    NombreProducto = x.NombreProducto,
                    Flexible = x.Flexible,
                    Cp = x.Cp
                }).ToList();

                return listaProductos;
            }
            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }

        }

        public IList<ElementoModel> ConsultaTipoAuto()
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<ElementoModel> SelTipoAutos = iGenericDataAccess.ExecuteQuery<ElementoModel>("select CatalogoID as CatalogoId, ElementoID as ElementoId, Nombre, IdInterno, Comodin from Elementos where CatalogoID = 18");
                iGenericDataAccess.CloseConnection();
                IList<ElementoModel> listaAutos = SelTipoAutos.Select(x => new ElementoModel
                {
                    CatalogoId = x.CatalogoId,
                    ElementoId = x.ElementoId,
                    Nombre = x.Nombre,
                    IdInterno = x.IdInterno,
                    Comodin = x.Comodin
                }).ToList();

                return listaAutos;
            }
            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }
        }

        public IList<ElementoModel> ConsultaTipoServicio()
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<ElementoModel> SelTipoServicios = iGenericDataAccess.ExecuteQuery<ElementoModel>("select CatalogoID as CatalogoId, ElementoID as ElementoId, Nombre, IdInterno, Comodin from Elementos where CatalogoID = 105");
                iGenericDataAccess.CloseConnection();

                IList<ElementoModel> listaServicios = SelTipoServicios.Select(x => new ElementoModel
                {
                    CatalogoId = x.CatalogoId,
                    ElementoId = x.ElementoId,
                    Nombre = x.Nombre,
                    IdInterno = x.IdInterno,
                    Comodin = x.Comodin

                }).ToList();

                return listaServicios;
            }

            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }
        }

        public IList<ElementoModel> ConsultaTipoSeguro()
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<ElementoModel> SelTipoSeguro = iGenericDataAccess.ExecuteQuery<ElementoModel>("select CatalogoID as CatalogoId, ElementoID as ElementoId, Nombre, IdInterno, Comodin from Elementos where CatalogoID = 109");
                iGenericDataAccess.CloseConnection();
                IList<ElementoModel> listaSeguros = SelTipoSeguro.Select(x => new ElementoModel
                {
                    CatalogoId = x.CatalogoId,
                    ElementoId = x.ElementoId,
                    Nombre = x.Nombre,
                    IdInterno = x.IdInterno,
                    Comodin = x.Comodin

                }).ToList();

                return listaSeguros;
            }
            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }
        }

        public IList<ElementoModel> EsNuevo()
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<ElementoModel> SelEsNuevo = iGenericDataAccess.ExecuteQuery<ElementoModel>("select CatalogoID as CatalogoId, ElementoID as ElementoId, Nombre, IdInterno, Comodin from Elementos where CatalogoID = 136");
                iGenericDataAccess.CloseConnection();
                IList<ElementoModel> listaNuevo = SelEsNuevo.Select(x => new ElementoModel
                {
                    CatalogoId = x.CatalogoId,
                    ElementoId = x.ElementoId,
                    Nombre = x.Nombre,
                    IdInterno = x.IdInterno,
                    Comodin = x.Comodin
                }).ToList();

                return listaNuevo;
            }

            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }
        }

        public IList<ElementoModel> CargoEnLinea()
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<ElementoModel> SelCargoLinea = iGenericDataAccess.ExecuteQuery<ElementoModel>("select CatalogoID as CatalogoId, ElementoID as ElementoId, Nombre, IdInterno, Comodin from Elementos where CatalogoID = 81");
                iGenericDataAccess.CloseConnection();
                IList<ElementoModel> listaCargoLinea = SelCargoLinea.Select(x => new ElementoModel {
                    CatalogoId = x.CatalogoId,
                    ElementoId = x.ElementoId,
                    Nombre = x.Nombre,
                    IdInterno = x.IdInterno,
                    Comodin = x.Comodin

                }).ToList();

                return listaCargoLinea;
            }

            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }
        }

        public IList<ElementoModel> ConsultaAseguradoras()
        {
            try
            {
                //int IdAseguradora = 2875;
                iGenericDataAccess.OpenConnection();
                IList<ElementoModel> selAseguradoras = iGenericDataAccess.ExecuteQuery<ElementoModel>("select CatalogoID as CatalogoId, ElementoID as ElementoId, Nombre, IdInterno, Comodin from Elementos where CatalogoID = 141");
                iGenericDataAccess.CloseConnection();
                IList<ElementoModel> listaAseguradoras = selAseguradoras.Select(x => new ElementoModel
                {
                    CatalogoId = x.CatalogoId,
                    ElementoId = x.ElementoId,
                    Nombre = x.Nombre,
                    IdInterno = x.IdInterno,
                    Comodin = x.Comodin


                }).ToList();

                return listaAseguradoras;
            }
            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }
        }

        public IList<ElementoModel> ConsultaEstatus()
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<ElementoModel> selEstatus = iGenericDataAccess.ExecuteQuery<ElementoModel>("select CatalogoID as CatalogoId, ElementoID as ElementoId, Nombre, IdInterno, Comodin from Elementos where CatalogoID =81");
                iGenericDataAccess.CloseConnection();

                IList<ElementoModel> listaEstatus = selEstatus.Select(x => new ElementoModel {
                    CatalogoId = x.CatalogoId,
                    ElementoId = x.ElementoId,
                    Nombre = x.Nombre,
                    IdInterno = x.IdInterno,
                    Comodin = x.Comodin

                }).ToList();

                return listaEstatus;
            }

            catch(Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }
        }
        // INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos
        public IList<PerfilesUsuarioModel> ConsultaPerfilesSistema()
        {

            try
            {

                iGenericDataAccess.OpenConnection();
                IList<PerfilesUsuarioModel> selPerfiles = iGenericDataAccess.ExecuteQuery<PerfilesUsuarioModel>("select PerfilUsuarioID, Upper(Nombre) as Nombre,	PerfilPadreID, Activo, OpcionAcceso, OpcionAccesoB from PerfilesUsuario");
                iGenericDataAccess.CloseConnection();

                IList<PerfilesUsuarioModel> listaPerfiles = selPerfiles.Select(x => new PerfilesUsuarioModel
                {

                    PerfilUsuarioID = x.PerfilUsuarioID,
                    Nombre = x.Nombre,
                    PerfilPadreID = x.PerfilPadreID,
                    Activo = x.Activo,
                    OpcionAcceso = x.OpcionAcceso,
                    OpcionAccesoB = x.OpcionAccesoB

                }).ToList();

                return listaPerfiles;
            }

            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }
        }

        public IList<UsuariosPerfil> ConsultaUsuarioPorPerfil(ConfiguradorModel usuarioPerfilModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                string query = @"select a.PersonaID,
                                 a.PerfilUsuarioID,
	                             ltrim(rtrim(ltrim(rtrim(Upper(b.Nombre))) + ' ' + rtrim(ltrim(Upper(b.Paterno))) + ' ' + ltrim(rtrim(Upper(b.Materno))))) as Nombre  
								 from Usuarios a
	                             inner join nePersonas b on b.PersonaID = a.PersonaID
                                 where a.PerfilUsuarioID = " + usuarioPerfilModel.UsuariosPerfilModel.PerfilUsuarioID;

                IList<UsuariosPerfil> selUsuarios = iGenericDataAccess.ExecuteQuery<UsuariosPerfil>(query);
                iGenericDataAccess.CloseConnection();
                IList<UsuariosPerfil> listaUsuarios = selUsuarios.Select(x => new UsuariosPerfil
                {
                    PersonaID = x.PersonaID,
                    PerfilUsuarioID = x.PerfilUsuarioID,
                    Nombre = x.Nombre

                }).ToList();

                return listaUsuarios;

            }

            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }
        }

        public IList<PerfilesFlexModel> ConsultarUsuariosFlexibles()
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                string query = @"
                                 select a.PerfilFlexibleId,
                                 b.Nombre as PerfilId,
		                         a.PersonaId,
		                         a.Comentario,
		                         a.maneja_udi

		                         from PerfilesFlexible a
		                         inner join PerfilesUsuario b on a.PerfilId = b.PerfilUsuarioID";
                IList<PerfilesFlexModel> selUsuariosFlex = iGenericDataAccess.ExecuteQuery<PerfilesFlexModel>(query);
                iGenericDataAccess.CloseConnection();
                IList<PerfilesFlexModel> listaPerfiles = selUsuariosFlex.Select(x => new PerfilesFlexModel
                {
                    PerfilFlexibleId = x.PerfilFlexibleId,
                    PerfilId = x.PerfilId,
                    PersonaId = x.PersonaId,
                    Comentario = x.Comentario,
                    maneja_udi = x.maneja_udi

                }).ToList();

                return listaPerfiles;

            }

            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }

        }

        public IList<FormaPagoModel> ConsultarFormasPagoLista()
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                string query = @"select a.FormaPagoID,
                                a.ProductoID,
                                c.Nombre as FormaPago,
                                b.Nombre as Producto,
		                        a.Predeterminado
		                        from FormasPagoProducto a
		                        inner join Productos b on a.ProductoID = b.ProductoID
		                        inner join Elementos c on a.FormaPagoID = c.ElementoID
                                where b.Nombre like '%DAIMLER%'";
                IList<FormaPagoModel> selFormasPago = iGenericDataAccess.ExecuteQuery<FormaPagoModel>(query);
                iGenericDataAccess.CloseConnection();
                IList<FormaPagoModel> listaFormasPago = selFormasPago.Select(x => new FormaPagoModel
                {
                    FormaPagoID = x.FormaPagoID,
                    ProductoID = x.ProductoID,
                    FormaPago = x.FormaPago,
                    Producto = x.Producto,
                    Predeterminado = x.Predeterminado
                }).ToList();

                return listaFormasPago;

            }
            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }
        }

        public ProductoFlexModel GuardaProductoFlexible(ConfiguradorModel productoFlexModel)
        {
            int idProductoFlex = 0;
            int idProductoFlexAseguradora = 0;
            int aseguradora = ObtenerIdAseguradora(productoFlexModel.ProductoFlexModel.AseguradoraId);

            var response = new SingleResponse<ProductoFlexModel>();
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpGrabarProductosFlexibles
                };
                command.Parameters.Add("@ProductoID", SqlDbType.Int).Value = productoFlexModel.ProductoFlexModel.ProductoID;
                command.Parameters.Add("@IdTipoVehiculo", SqlDbType.Int).Value = productoFlexModel.ProductoFlexModel.IdTipoVehiculo;
                command.Parameters.Add("@IdCondicionVehiculo", SqlDbType.Int).Value = productoFlexModel.ProductoFlexModel.IdCondicionVehiculo;
                command.Parameters.Add("@IdTipoServicioVehiculo", SqlDbType.Int).Value = productoFlexModel.ProductoFlexModel.IdTipoServicioVehiculo;
                command.Parameters.Add("@NotasImportantes", SqlDbType.NVarChar).Value = productoFlexModel.ProductoFlexModel.NotasImportantes;
                command.Parameters.Add("@Submarca", SqlDbType.NVarChar).Value = productoFlexModel.ProductoFlexModel.Submarca;
                SqlDataReader grabadoProductoFlex = iGenericDataAccess.StoredProcedure(command);
                if(grabadoProductoFlex.HasRows)
                {
                    while (grabadoProductoFlex.Read())
                    {
                       // productoFlexModel.IdProductoFlex = Convert.ToInt32(grabadoProductoFlex[0].ToString());
                        idProductoFlex = Convert.ToInt32(grabadoProductoFlex[0].ToString());
                        idProductoFlexAseguradora = grabarProductoFlexAseguradora(idProductoFlex, aseguradora);
                        var listaCoberturas = obtenerCoberturasAseguradora(aseguradora);
                        if (listaCoberturas.Count > 0)
                        {
                            grabarCoberturasFlexibles(listaCoberturas, idProductoFlexAseguradora, idProductoFlex);
                        }
                        iGenericDataAccess.CloseConnection();
                        
                    }
                }
            }

            catch(Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }

            return productoFlexModel.ProductoFlexModel;
        }

        public PerfilesFlexibleModel GuardaUsuarioFlexible(ConfiguradorModel perfilesFlexibleModel)
        {
            var response = new SingleResponse<PerfilesFlexibleModel>();
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpGrabarPerfilesFlexible
                };

                command.Parameters.Add("@PerfilId", SqlDbType.Int).Value = perfilesFlexibleModel.PerfilesFlexibleModel.PerfilId;
                command.Parameters.Add("@PersonaId", SqlDbType.Int).Value = perfilesFlexibleModel.PerfilesFlexibleModel.PersonaId;
                command.Parameters.Add("@Comentario", SqlDbType.VarChar).Value = perfilesFlexibleModel.PerfilesFlexibleModel.Comentario;
                command.Parameters.Add("@ManejaUdi", SqlDbType.Bit).Value = perfilesFlexibleModel.PerfilesFlexibleModel.manejaUdi;
                SqlDataReader grabadoPerfilFlexible = iGenericDataAccess.StoredProcedure(command);


            }

            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }
            return perfilesFlexibleModel.PerfilesFlexibleModel;
        }

        public FormasPagoProductoModel GrabarFormaPagoProducto(ConfiguradorModel formasPagoProductoModel)
        {
            var response = new SingleResponse<FormasPagoProductoModel>();
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpGrabarFormasPagoProducto
                };

                command.Parameters.Add("@FormaPagoId", SqlDbType.Int).Value = formasPagoProductoModel.FormasPagoProductoModel.FormaPagoID;
                command.Parameters.Add("@ProductoId", SqlDbType.Int).Value = formasPagoProductoModel.FormasPagoProductoModel.ProductoID;
                command.Parameters.Add("@Predeterminado", SqlDbType.Bit).Value = formasPagoProductoModel.FormasPagoProductoModel.Predeterminado;
                SqlDataReader grabadoFormasPago = iGenericDataAccess.StoredProcedure(command);
            }
            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }

            return formasPagoProductoModel.FormasPagoProductoModel;
        }

        public FormasPagoProductoAseguradora GrabarFormasPagoProductoAseguradora(ConfiguradorModel formasPagoProductoAseguradora)
        {
            var response = new SingleResponse<FormasPagoProductoAseguradora>();
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpGrabarFormasPagoProductoAseguradora
                };

                command.Parameters.Add("@AseguradoraId", SqlDbType.Int).Value = formasPagoProductoAseguradora.FormasPagoProductoAseguradora.AseguradoraID;
                command.Parameters.Add("@FormaPagoId", SqlDbType.Int).Value = formasPagoProductoAseguradora.FormasPagoProductoAseguradora.FormaPagoID;
                command.Parameters.Add("@Plazo", SqlDbType.Int).Value = formasPagoProductoAseguradora.FormasPagoProductoAseguradora.Plazo;
                command.Parameters.Add("@Codigo", SqlDbType.VarChar).Value = formasPagoProductoAseguradora.FormasPagoProductoAseguradora.Codigo;
                SqlDataReader grabarFormasPagoAseguradora = iGenericDataAccess.StoredProcedure(command);
            }
            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }

            return formasPagoProductoAseguradora.FormasPagoProductoAseguradora;
        }

        public IList<FormasPagoProductoAseguradoraModel> ConsultarFormasPagoAseguradoraLista()
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                string query = @"  select a.Id,
                                   b.Nombre as AseguradoraID,
		                           c.Nombre as FormaPagoID,
                                   a.Plazo,
		                           a.Codigo
                                   from FormasPagoProductoAseguradora a
                                   inner join nePersonas b on a.AseguradoraID = b.PersonaID
		                           inner join Elementos c on a.FormaPagoID = c.ElementoID";

                IList<FormasPagoProductoAseguradoraModel> selFormasPagoAseguradora = iGenericDataAccess.ExecuteQuery<FormasPagoProductoAseguradoraModel>(query);
                iGenericDataAccess.CloseConnection();
                IList<FormasPagoProductoAseguradoraModel> listaFormasAseguradora = selFormasPagoAseguradora.Select(x => new FormasPagoProductoAseguradoraModel
                {
                    Id = x.Id,
                    AseguradoraID = x.AseguradoraID,
                    FormaPagoID = x.FormaPagoID,
                    Plazo = x.Plazo,
                    Codigo = x.Codigo

                }).ToList();

                return listaFormasAseguradora;
            }
            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }
        }

        public PerfilesFlexibleModel ActualizaStatusUdi(ConfiguradorModel perfilesFlexibleModel)
        {
            var response = new SingleResponse<PerfilesFlexibleModel>();
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpActualizaUdiUsuario
                };

                command.Parameters.Add("@PerfilFlexibleId", SqlDbType.Int).Value = perfilesFlexibleModel.PerfilesFlexibleModel.PerfilFlexibleId;
                command.Parameters.Add("@manejaUdi", SqlDbType.Bit).Value = perfilesFlexibleModel.PerfilesFlexibleModel.manejaUdi;
                SqlDataReader actualizaUdi = iGenericDataAccess.StoredProcedure(command);
            }
            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }
            return perfilesFlexibleModel.PerfilesFlexibleModel;
        }

        public FormasPagoProductoModel ActualizaPredeterminadoPago(ConfiguradorModel formasPagoProductoModel)
        {
            var response = new SingleResponse<FormasPagoProductoModel>();
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpActualizaPredeterminadoPago
                };
                command.Parameters.Add("@FormaPagoId", SqlDbType.Int).Value = formasPagoProductoModel.FormasPagoProductoModel.FormaPagoID;
                command.Parameters.Add("@ProductoId", SqlDbType.Int).Value = formasPagoProductoModel.FormasPagoProductoModel.ProductoID;
                command.Parameters.Add("@Predeterminado", SqlDbType.Bit).Value = formasPagoProductoModel.FormasPagoProductoModel.Predeterminado;
                SqlDataReader actualizaPredeterminado = iGenericDataAccess.StoredProcedure(command);
            }
            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }

            return formasPagoProductoModel.FormasPagoProductoModel;
        }

        public PerfilesFlexibleModel EliminarUsuarioFlexible(ConfiguradorModel perfilesFlexibleModel)
        {

            var response = new SingleResponse<PerfilesFlexibleModel>();
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpEliminarUsuarioFlexible
                };
                command.Parameters.Add("@PerfilFlexibleId", SqlDbType.Int).Value = perfilesFlexibleModel.PerfilesFlexibleModel.PerfilFlexibleId;
                SqlDataReader eliminaUsuarioFlex = iGenericDataAccess.StoredProcedure(command);
            }
            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }
            return perfilesFlexibleModel.PerfilesFlexibleModel;
        }

        public FormasPagoProductoModel EliminarFormaDePago(ConfiguradorModel formasPagoProductoModel)
        {
            var response = new SingleResponse<FormasPagoProductoModel>();
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpEliminarFormaPago
                };
                command.Parameters.Add("@FormaPagoId", SqlDbType.Int).Value = formasPagoProductoModel.FormasPagoProductoModel.FormaPagoID;
                command.Parameters.Add("@ProductoId", SqlDbType.Int).Value = formasPagoProductoModel.FormasPagoProductoModel.ProductoID;
                SqlDataReader eliminarFormaPago = iGenericDataAccess.StoredProcedure(command);
            }
            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }

            return formasPagoProductoModel.FormasPagoProductoModel;
        }

        public FormasPagoProductoAseguradora EliminarFormaPagoProductoAseguradora(ConfiguradorModel formasPagoProductoAseguradora)
        {
            var response = new SingleResponse<FormasPagoProductoAseguradora>();
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpEliminarFormaPagoAseguradora
                };

                command.Parameters.Add("@Id", SqlDbType.Int).Value = formasPagoProductoAseguradora.FormasPagoProductoAseguradora.Id;
                SqlDataReader eliminarFormaPagoAseguradora = iGenericDataAccess.StoredProcedure(command);
            }

            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }

            return formasPagoProductoAseguradora.FormasPagoProductoAseguradora;
        }

        public IList<ElementoModel> ConsultaFormasPago()
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<ElementoModel> selEstatus = iGenericDataAccess.ExecuteQuery<ElementoModel>("select CatalogoID as CatalogoId, ElementoID as ElementoId, Nombre, IdInterno, Comodin from Elementos where CatalogoID =19");
                iGenericDataAccess.CloseConnection();

                IList<ElementoModel> listaEstatus = selEstatus.Select(x => new ElementoModel
                {
                    CatalogoId = x.CatalogoId,
                    ElementoId = x.ElementoId,
                    Nombre = x.Nombre,
                    IdInterno = x.IdInterno,
                    Comodin = x.Comodin

                }).ToList();

                return listaEstatus;
            }

            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }
        }

        private int grabarProductoFlexAseguradora(int idProductoFlex, int idAseguradora)
        {
            int idRetorno = 0;
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpGrabarProductosAseguradoraFlex
                };
                command.Parameters.Add("@IdProductoFlex", SqlDbType.Int).Value = idProductoFlex;
                command.Parameters.Add("@IdAseguradora", SqlDbType.Int).Value = idAseguradora;

                SqlDataReader grabadoProductoFlexAseguradora = iGenericDataAccess.StoredProcedure(command);
                //iGenericDataAccess.StoredProcedure(command);
                if (grabadoProductoFlexAseguradora.HasRows)
                {
                    while (grabadoProductoFlexAseguradora.Read()) {
                        idRetorno = Convert.ToInt32(grabadoProductoFlexAseguradora[0].ToString());
                    }
                  
                }

                return idRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void grabarCoberturasFlexibles(IList<neCoberturasPaqueteModel> listaCoberturas, int idProductoFlexAseguradora, int idProductoFlex)
        {
            try
            {
                //iGenericDataAccess.OpenConnection();
                //string query = @"select distinct IdCobertura from DistinctCoberturasProductos";
                //IList<DistinctCoberturasModel> selDistictCoberturas = iGenericDataAccess.ExecuteQuery<DistinctCoberturasModel>(query);
                //iGenericDataAccess.CloseConnection();

                

                //if (selDistictCoberturas.Count > 0)
                //{

                    //IList<DistinctCoberturasModel> distictCober = selDistictCoberturas.Select(x => new DistinctCoberturasModel
                    //{
                    //    IdCobertura = x.IdCobertura
                    //}).ToList();

                    //for (int i = 0; i < listaCoberturas.Count; i++)
                    //{
                    //    for (int j = 0; j < distictCober.Count; j++)
                    //    {
                    //        if (listaCoberturas[i].CoberturaID != distictCober[j].IdCobertura)
                    //        {
                    //            SqlCommand command = new SqlCommand
                    //            {
                    //                CommandText = ConstStoredProcedures.SpGuardarDistictCoberturas
                    //            };

                    //            command.Parameters.Add("@IdProductoFlex", SqlDbType.Int).Value = idProductoFlex;
                    //            command.Parameters.Add("@IdCobertura", SqlDbType.Int).Value = listaCoberturas[i].CoberturaID;
                    //            iGenericDataAccess.StoredProcedure(command);
                    //        }
                    //    }
                    //}

                    foreach (var item in listaCoberturas)
                    {
                       // var res = (from x in distictCober where x.IdCobertura == item.CoberturaID select x).FirstOrDefault();
                      //  if (res == null)
                      //  {
                            SqlCommand command = new SqlCommand
                            {
                                CommandText = ConstStoredProcedures.SpGuardarDistictCoberturas
                            };

                            command.Parameters.Add("@IdProductoFlex", SqlDbType.Int).Value = idProductoFlex;
                            command.Parameters.Add("@IdCobertura", SqlDbType.Int).Value = item.CoberturaID;
                            iGenericDataAccess.StoredProcedure(command);
                       // }
                    }

                    
              //  }

                foreach (var item in listaCoberturas)
                {
                    SqlCommand command = new SqlCommand
                    {
                        CommandText = ConstStoredProcedures.SpGuardarCoberturasAseguradora
                    };

                    command.Parameters.Add("@IdProductoFlexAseguradora", SqlDbType.Int).Value = idProductoFlexAseguradora;
                    command.Parameters.Add("@IdCobertura", SqlDbType.Int).Value = item.CoberturaID;
                    iGenericDataAccess.StoredProcedure(command);
                }




            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private IList<neCoberturasPaqueteModel> obtenerCoberturasAseguradora(int aseguradoraId)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                string query = @"select * from neCoberturasPaquete where PaqueteID = 2370 and AseguradoraID =" + aseguradoraId;
                IList<neCoberturasPaqueteModel> selCoberturasAseguradora = iGenericDataAccess.ExecuteQuery<neCoberturasPaqueteModel>(query);
                iGenericDataAccess.CloseConnection();

                IList<neCoberturasPaqueteModel> listaCoberturas = selCoberturasAseguradora.Select(x => new neCoberturasPaqueteModel
                {
                    AseguradoraID = x.AseguradoraID,
                    PaqueteID = x.PaqueteID,
                    CoberturaID = x.CoberturaID,
                    TipoID = x.TipoID
                }).ToList();

                return listaCoberturas;
            }

            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }
        }



        public PanelConfiguradorModel ConsultaPanelConfiguradorFlex(ConfiguradorModel configuradorModel)
        {
            try
            {
                var valorCoberturaRcDeducible = "";
                var valorCoberturaRcSuma = "";
                int idCoberturaManiobrasCarDes = 0;
                int idCoberturaRcComplementaria = 0;
                bool coberturasDependientes = false;
                bool coberturaRespCiv = false;

                var comodin = ComodinPorTipoVehiculo(configuradorModel);
                var aseguradoraId = ObtenerIdAseguradora(configuradorModel.PanelConfiguradorModel.AseguradoraId);

                IList<UsuarioPerfilModel> usuarioPerfil = ValidaUsuarioPerfil(configuradorModel);

                #region Consulta si el producto flexible contiene submarca
                iGenericDataAccess.OpenConnection();
                IList<ProductosFlex> existeSubarca = iGenericDataAccess.Consultar(CQuerysCotizador.QryExisteSubmarca,
                                                                                new ProductosFlex()
                                                                                {
                                                                                    ProductoId = configuradorModel.PanelConfiguradorModel.IdProducto,
                                                                                    IdTipoVehiculo = configuradorModel.PanelConfiguradorModel.IdTipoVehiculo,
                                                                                    Servicio = configuradorModel.PanelConfiguradorModel.IdTipoServicioVehiculo,
                                                                                    IdCondicionVehiculo = configuradorModel.PanelConfiguradorModel.IdCondicionVehiculo,
                                                                                    Submarca = configuradorModel.PanelConfiguradorModel.Submarca
                                                                                },
                                                                                new OptionsQueryZero()
                                                                                {
                                                                                    ExcludeNumericsDefaults = true,
                                                                                    ExcludeBool = true
                                                                                });
                iGenericDataAccess.CloseConnection();

                #endregion

                PanelConfiguradorModel panel = new PanelConfiguradorModel();
                iGenericDataAccess.OpenConnection();
                IList<VwCotSelProdsFlexDistinctCobertAseg> vwCotSelProdsFlexDistinctCobertAsegList = iGenericDataAccess.Consultar(new VwCotSelProdsFlexDistinctCobertAseg()
                {
                    IdProducto = configuradorModel.PanelConfiguradorModel.IdProducto,
                    IdTipoVehiculo = configuradorModel.PanelConfiguradorModel.IdTipoVehiculo,
                    IdCondicionVehiculo = configuradorModel.PanelConfiguradorModel.IdCondicionVehiculo,
                    IdTipoServicioVehiculo = comodin.Comodin,
                    IdAseguradora = aseguradoraId
                },
                                                                                                                          new OptionsQueryZero()
                                                                                                                          {
                                                                                                                              ExcludeBool = true,
                                                                                                                              ExcludeNumericsDefaults = true,
                                                                                                                              WhereComplementary = existeSubarca[0].ExisteSubmarca ? "Submarca = '" + configuradorModel.PanelConfiguradorModel.Submarca + "'" : "Submarca IS NULL OR  Submarca = ''"
                                                                                                                          });
                iGenericDataAccess.CloseConnection();

                if (vwCotSelProdsFlexDistinctCobertAsegList.Count > 0)
                {
                    if (string.IsNullOrEmpty(configuradorModel.PanelConfiguradorModel.TipoCarga) || !string.IsNullOrEmpty(configuradorModel.PanelConfiguradorModel.TipoCarga) && !configuradorModel.PanelConfiguradorModel.TipoCarga.Equals(ConstElementos.CargaMuyPeligrosa))
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
                       
                        aseguradorasModel.Add(aseguradoraModel);
                       
                    }

                    #endregion

                    if (aseguradorasModel.Count > 0)
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
                                                                                                  IsFijaPerfil = (x.PerfilCoberturaFijaKey && x.IsFijaKey && usuarioPerfil.Count > 0),
                                                                                                  IsEspecial = x.IsEspecialKey
                                                                                              }).ToList();

                        iGenericDataAccess.OpenConnection();
                        IList<VwCotSelProdsFlexCobertAseg> coberturasAseguradoras = iGenericDataAccess.Consultar(new VwCotSelProdsFlexCobertAseg()
                        {
                            IdProducto = configuradorModel.PanelConfiguradorModel.IdProducto,
                            IdTipoVehiculo = configuradorModel.PanelConfiguradorModel.IdTipoVehiculo,
                            IdCondicionVehiculo = configuradorModel.PanelConfiguradorModel.IdCondicionVehiculo,
                            IdTipoServicioVehiculo = comodin.Comodin,
                            IdAseguradora = aseguradoraId
                        },
                                                                                                                 new OptionsQueryZero()
                                                                                                                 {
                                                                                                                     ExcludeNumericsDefaults = true,
                                                                                                                     ExcludeBool = true,
                                                                                                                     WhereComplementary = existeSubarca[0].ExisteSubmarca ? "Submarca = '" + configuradorModel.PanelConfiguradorModel.Submarca + "'" : "Submarca IS NULL OR  Submarca = ''"
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

                        panel.IdProducto = configuradorModel.PanelConfiguradorModel.IdProducto;
                        panel.IdTipoVehiculo = configuradorModel.PanelConfiguradorModel.IdTipoVehiculo;
                        panel.IdCondicionVehiculo = configuradorModel.PanelConfiguradorModel.IdCondicionVehiculo;
                        panel.IdTipoServicioVehiculo = comodin.Comodin;
                        panel.Coberturas = coberturaModels;
                        panel.Aseguradoras = aseguradorasModel;
                   

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


        private ElementoModel ComodinPorTipoVehiculo(ConfiguradorModel configuradorModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                var SelTipoServicios = iGenericDataAccess.ExecuteQuery<ElementoModel>("select CatalogoID as CatalogoId, ElementoID as ElementoId, Nombre, IdInterno, Comodin from Elementos where CatalogoID = 105 and ElementoId =" + configuradorModel.PanelConfiguradorModel.IdTipoServicioVehiculo);
                iGenericDataAccess.CloseConnection();

                var listaServicios = SelTipoServicios.Select(x => new ElementoModel
                {
                    CatalogoId = x.CatalogoId,
                    ElementoId = x.ElementoId,
                    Nombre = x.Nombre,
                    IdInterno = x.IdInterno,
                    Comodin = x.Comodin

                }).FirstOrDefault();

                return listaServicios;
            }

            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }
        }

        private int ObtenerIdAseguradora(int valor)
        {
            var resultado = 0;
            switch (valor) {
                case 2872:
                    resultado = 14625;
                    break;
                case 2873:
                    resultado = 14623;
                    break;
                case 2874:
                    resultado = 252;
                    break;
                case 2875:
                    resultado = 222;
                    break;
                case 2876:
                    resultado = 14618;
                    break;
                case 2877:
                    resultado = 14621;
                    break;
                case 2878:
                    resultado = 63586;
                    break;
                case 3142:
                    resultado = 129597;
                    break;
                case 3156:
                    resultado = 66117;
                    break;
                case 5867:
                    resultado = 221604;
                    break;
            }

            return resultado;
        }

        public RangosModel ConsultaRangosSumasAseguradas(ConfiguradorModel configuradorModel)
        {
            try
            {
           
                iGenericDataAccess.OpenConnection();
                string query = @"SELECT a.[IdProductoFlexAseguradora]
                                       ,a.[IdCobertura]
                                       ,a.[Homologacion]
                                       ,a.[Tooltip]
                                       ,a.[RangosJSON]
                                       ,a.[Detalle]
                                       ,a.[PrimaNeta]
                                       ,a.[IndicadorXML]
                                       FROM [AMQA].[dbo].[CoberturasProductosFlexAseguradoras] a

                                       inner join ProductosFlexAseguradoras b on a.IdProductoFlexAseguradora = b.IdProductoFlexAseguradora
                                       inner join ProductosFlex c on b.IdProductoFlex = c.IdProductoFlex
                                       where c.ProductoID =" + configuradorModel.CoberModel.IdProducto + " and a.IdCobertura =" + configuradorModel.CoberModel.IdCobertura + "and c.IdTipoVehiculo =" + configuradorModel.CoberModel.IdTipoVehiculo + " and c.IdTipoServicioVehiculo =" + configuradorModel.CoberModel.IdTipoServicioVehiculo;

                RangosModel rangosModel = new RangosModel();
                IList<string> rangoSumas = new List<string>();
                IList<string> rangoDeducibles = new List<string>();
                var selRangosSumas = iGenericDataAccess.ExecuteQuery<CoberturasProductosFlexAseguradoras>(query);

                var rSumas = selRangosSumas.Select(x => new CoberturasProductosFlexAseguradoras
                {
                    RangosJson = x.RangosJson
                }).FirstOrDefault();

                if (!string.IsNullOrEmpty(rSumas.RangosJson))
                {
                    rangosModel = JsonConvert.DeserializeObject<RangosModel>(rSumas.RangosJson);
                }

                if (rangosModel.RangosSumas != null)
                {
                    foreach (string sumaAsegurada in rangosModel.RangosSumas)
                    {
                        if (!rangoSumas.Contains(sumaAsegurada))
                        {
                            rangoSumas.Add(sumaAsegurada);
                        }
                    }
                }

                if (rangosModel.RangosDeducibles != null)
                {
                    foreach (string rangoDeducible in rangosModel.RangosDeducibles)
                    {
                        if (!rangoDeducibles.Contains(rangoDeducible))
                        {
                            rangoDeducibles.Add(rangoDeducible);
                        }
                    }
                }

                return rangosModel;
            }

            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }
        }

        public IList<UsuarioPerfilModel> ValidaUsuarioPerfil(ConfiguradorModel configuradorModel)
        {
            try
            {
                var sWhere = new StringBuilder();

                sWhere.Append("PerfilId = " + configuradorModel.GetIdPerfilUsuarioSesion());
                sWhere.Append(" OR ");
                sWhere.Append("(PerfilId = " + configuradorModel.GetIdPerfilUsuarioSesion());
                sWhere.Append(" AND ");
                sWhere.Append("PersonaId = " + configuradorModel.GetIdUsuarioSesion() + ")");

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

        public CoberModel ActualizaRangosSumas(ConfiguradorModel configuradorModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                string jsonData = string.Empty;
                string query = @"SELECT a.[IdProductoFlexAseguradora]
                                       ,a.[IdCobertura]
                                       ,a.[Homologacion]
                                       ,a.[Tooltip]
                                       ,a.[RangosJSON]
                                       ,a.[Detalle]
                                       ,a.[PrimaNeta]
                                       ,a.[IndicadorXML]
                                       FROM [AMQA].[dbo].[CoberturasProductosFlexAseguradoras] a

                                       inner join ProductosFlexAseguradoras b on a.IdProductoFlexAseguradora = b.IdProductoFlexAseguradora
                                       inner join ProductosFlex c on b.IdProductoFlex = c.IdProductoFlex
                                       where c.ProductoID =" + configuradorModel.CoberModel.IdProducto + " and a.IdCobertura =" + configuradorModel.CoberModel.IdCobertura + "and c.IdTipoVehiculo =" + configuradorModel.CoberModel.IdTipoVehiculo + " and c.IdTipoServicioVehiculo =" + configuradorModel.CoberModel.IdTipoServicioVehiculo;

                RangosModel rangosModel = new RangosModel();
                IList<string> rangoSumas = new List<string>();
                IList<string> rangoDeducibles = new List<string>();
                var selRangosSumas = iGenericDataAccess.ExecuteQuery<CoberturasProductosFlexAseguradoras>(query);

                var rSumas = selRangosSumas.Select(x => new CoberturasProductosFlexAseguradoras
                {
                    RangosJson = x.RangosJson
                }).FirstOrDefault();

                if (!string.IsNullOrEmpty(rSumas.RangosJson))
                {
                    rangosModel = JsonConvert.DeserializeObject<RangosModel>(rSumas.RangosJson);

                    if (rangosModel.RangosSumas.Count > 0)
                    {

                        rangosModel.RangosSumas.Clear();

                    }
                    else
                    {
                        var listaSumas = CrearListaRangosSumas(configuradorModel);

                        if (listaSumas.Count > 0)
                        {
                            foreach (string rangoSum in listaSumas)
                            {
                                rangosModel.RangosSumas.Add(rangoSum);
                            }
                        }

                    }

                    if (rangosModel.RangosDeducibles != null)
                    {
                        foreach (string rangoDeducible in rangosModel.RangosDeducibles)
                        {


                            if (!rangoDeducible.Contains(rangoDeducible))
                            {
                                rangoDeducibles.Add(rangoDeducible);
                            }
                        }
                    }

                    jsonData = JsonConvert.SerializeObject(rangosModel);
                }

                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpEliminarSumasAseguradas
                };
                command.Parameters.Add("@IdProducto", SqlDbType.Int).Value = configuradorModel.CoberModel.IdProducto;
                command.Parameters.Add("@IdCobertura", SqlDbType.Int).Value = configuradorModel.CoberModel.IdCobertura;
                command.Parameters.Add("@IdTipoVehiculo", SqlDbType.Int).Value = configuradorModel.CoberModel.IdTipoVehiculo;
                command.Parameters.Add("@IdTipoSevicioVehiculo", SqlDbType.Int).Value = configuradorModel.CoberModel.IdTipoServicioVehiculo;
                command.Parameters.Add("@RangosJson", SqlDbType.VarChar).Value = jsonData;
                SqlDataReader actualizarRangosSumas = iGenericDataAccess.StoredProcedure(command);

            }
            catch (Exception e)
            {

            }

            return configuradorModel.CoberModel;
        }

        public CoberModel ActualizaRangosDeducibles(ConfiguradorModel configuradorModel)
        {

            try
            {
                iGenericDataAccess.OpenConnection();
                string jsonData = string.Empty;
                string query = @"SELECT a.[IdProductoFlexAseguradora]
                                       ,a.[IdCobertura]
                                       ,a.[Homologacion]
                                       ,a.[Tooltip]
                                       ,a.[RangosJSON]
                                       ,a.[Detalle]
                                       ,a.[PrimaNeta]
                                       ,a.[IndicadorXML]
                                       FROM [AMQA].[dbo].[CoberturasProductosFlexAseguradoras] a

                                       inner join ProductosFlexAseguradoras b on a.IdProductoFlexAseguradora = b.IdProductoFlexAseguradora
                                       inner join ProductosFlex c on b.IdProductoFlex = c.IdProductoFlex
                                       where c.ProductoID =" + configuradorModel.CoberModel.IdProducto + " and a.IdCobertura =" + configuradorModel.CoberModel.IdCobertura + "and c.IdTipoVehiculo =" + configuradorModel.CoberModel.IdTipoVehiculo + " and c.IdTipoServicioVehiculo =" + configuradorModel.CoberModel.IdTipoServicioVehiculo;

                RangosModel rangosModel = new RangosModel();
                IList<string> rangoSumas = new List<string>();
                IList<string> rangoDeducibles = new List<string>();
                IList<string> listaDeducibles = new List<string>();
                var selRangosSumas = iGenericDataAccess.ExecuteQuery<CoberturasProductosFlexAseguradoras>(query);

                var rSumas = selRangosSumas.Select(x => new CoberturasProductosFlexAseguradoras
                {
                    RangosJson = x.RangosJson
                }).FirstOrDefault();

                if (!string.IsNullOrEmpty(rSumas.RangosJson))
                {
                    rangosModel = JsonConvert.DeserializeObject<RangosModel>(rSumas.RangosJson);

                    if (rangosModel.RangosSumas != null)
                    {
                        foreach (string sumaAsegurada in rangosModel.RangosSumas)
                        {
                            if (!rangoSumas.Contains(sumaAsegurada))
                            {
                                rangoSumas.Add(sumaAsegurada);
                            }
                        }
                    }

                    if (rangosModel.RangosDeducibles != null)
                    {
                        foreach (string rangoDeducible in rangosModel.RangosDeducibles)
                        {
                            if (!rangoSumas.Contains(rangoDeducible))
                            {
                                rangoDeducibles.Add(rangoDeducible);
                            }
                        }


                    }

                    if (configuradorModel.CoberModel.EliminarTodosDeducibles == 1)
                    {
                        rangosModel.RangosDeducibles.Clear();
                    }
                    else
                    {
                        listaDeducibles = BorrarDatosDeducibles(configuradorModel.CoberModel.ListaDeducibles, rangoDeducibles, configuradorModel.CoberModel.AgregarOeliminarElemento);

                        rangosModel.RangosDeducibles = listaDeducibles;
                    }

                    jsonData = JsonConvert.SerializeObject(rangosModel);

                }
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpEliminarSumasAseguradas
                };
                command.Parameters.Add("@IdProducto", SqlDbType.Int).Value = configuradorModel.CoberModel.IdProducto;
                command.Parameters.Add("@IdCobertura", SqlDbType.Int).Value = configuradorModel.CoberModel.IdCobertura;
                command.Parameters.Add("@IdTipoVehiculo", SqlDbType.Int).Value = configuradorModel.CoberModel.IdTipoVehiculo;
                command.Parameters.Add("@IdTipoSevicioVehiculo", SqlDbType.Int).Value = configuradorModel.CoberModel.IdTipoServicioVehiculo;
                command.Parameters.Add("@RangosJson", SqlDbType.VarChar).Value = jsonData;
                SqlDataReader actualizarRangosDeducibles = iGenericDataAccess.StoredProcedure(command);


            }
            catch (Exception ex)
            {

            }
            return configuradorModel.CoberModel;

        }

        private IList<string> CrearListaRangosSumas(ConfiguradorModel configuradorModel)
        {
            IList<string> rangoSumas = new List<string>();
            try
            {

                var rangoInicial = configuradorModel.CoberModel.RangoInicial;
                var rangoFinal = configuradorModel.CoberModel.RangoFinal;
                var saltos = configuradorModel.CoberModel.Saltos;
                //var cadena = "{valor}";
                //var cadenaFinal = "";

                for (int i = rangoInicial; i <= rangoFinal; i++)
                {
                    int a = i;
                    //cadenaFinal += cadena.Replace("{valor}", a.ToString() + ",");

                    rangoSumas.Add(a.ToString());

                    i = (i + saltos) - 1;
                }




            }
            catch (Exception ex)
            {

            }
            return rangoSumas;

        }

        private IList<string> BorrarDatosDeducibles(IList<string> datosEliminar, IList<string> deducibles, int agregarOeliminarElemento)
        {
            IList<string> rangoDeducibles = new List<string>();
            try
            {
                if (agregarOeliminarElemento == 1)
                {
                    foreach (var item in datosEliminar)
                    {
                        deducibles.Add(item);
                    }
                }
                else
                {
                    foreach (var item2 in datosEliminar)
                    {
                        deducibles.Remove(item2);
                    }
                }


                rangoDeducibles = deducibles;

            }

            catch (Exception ex)
            {

            }

            return rangoDeducibles;
        }
        public CoberModel ActualizarHomologacionTooltip(ConfiguradorModel configuradorModel)
        {

            var response = new SingleResponse<CoberturasProductosFlexAseguradoras>();

            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpActualizarHomologacionTooltip
                };

               
                command.Parameters.Add("@IdProducto", SqlDbType.Int).Value = configuradorModel.CoberModel.IdProducto;
                command.Parameters.Add("@IdTipoVehiculo", SqlDbType.Int).Value = configuradorModel.CoberModel.IdTipoVehiculo;
                command.Parameters.Add("@IdTipoServicioVehiculo", SqlDbType.Int).Value = configuradorModel.CoberModel.IdTipoServicioVehiculo;
                command.Parameters.Add("@IdCobertura", SqlDbType.Int).Value = configuradorModel.CoberModel.IdCobertura;
                command.Parameters.Add("@Homologacion", SqlDbType.VarChar).Value = configuradorModel.CoberModel.Homologacion;
                command.Parameters.Add("@Tooltip", SqlDbType.VarChar).Value = configuradorModel.CoberModel.Tooltip;
                SqlDataReader actualizaHomoTootip = iGenericDataAccess.StoredProcedure(command);
            }
            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);

            }
            return configuradorModel.CoberModel;
        }

        public EnmascaradoDeduciblesModel ConsultarEnmascaradoDeducibles(ConfiguradorModel configuradorModel)
        {
            try
            {
                EnmascaradoDeduciblesModel cober = new EnmascaradoDeduciblesModel();
                iGenericDataAccess.OpenConnection();
                string query = @" select * from CoberturasEnmascaramientoDeducible where IdCliente =" + configuradorModel.CoberturaEnmascaramientoDeducible.IdCliente;
                var selEnmascarado = iGenericDataAccess.ExecuteQuery<CoberturasEnmascaramientoDeducible>(query);
                iGenericDataAccess.CloseConnection();

                IList<CoberturasEnmascaramientoDeducible> listaEnmascarado = selEnmascarado.Select(x => new CoberturasEnmascaramientoDeducible
                {
                    IdCoberturaEnmascaramientoDeducible = x.IdCoberturaEnmascaramientoDeducible,
                    IdCliente = x.IdCliente,
                    IdCobertura = x.IdCobertura,
                    DescripcionEnmascarado = x.DescripcionEnmascarado,
                    Enmascaramiento = x.Enmascaramiento

                }).ToList();

                cober.IdCliente = configuradorModel.CoberturaEnmascaramientoDeducible.IdCliente;
                cober.IdCobertura = configuradorModel.CoberturaEnmascaramientoDeducible.IdCobertura;
                cober.EnmascaradoDeducibles = listaEnmascarado;
                return cober;
            }

            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }
        }

        public DocumentosPorCoberturaModel GuardarDocumentoCobertura(ConfiguradorModel configuradorModel)
        {
            var response = new SingleResponse<DocumentosPorCoberturaModel>();
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpGuardaDocumentoCobertura
                };

                command.Parameters.Add("@IdProducto", SqlDbType.Int).Value = configuradorModel.DocumentoPorCobertura.IdProducto;
                command.Parameters.Add("@IdCobertura", SqlDbType.Int).Value = configuradorModel.DocumentoPorCobertura.IdCobertura;
                command.Parameters.Add("@UrlArchivoCobertura", SqlDbType.VarChar).Value = configuradorModel.DocumentoPorCobertura.UrlArchivoCobertura;
                SqlDataReader guardaDocumentoCobertura = iGenericDataAccess.StoredProcedure(command);
            }
            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }

            return configuradorModel.DocumentoPorCobertura;
        }

        public TextoAuxiliarUsoVehiculoModel GuardaTextoAuxiliarUso(ConfiguradorModel configuradorModel)
        {
            var response = new SingleResponse<TextoAuxiliarUsoVehiculoModel>();
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpGuardarTextoAuxiliarUso
                };

                command.Parameters.Add("@ClienteId", SqlDbType.Int).Value = configuradorModel.TextoAuxModel.ClienteID;
                command.Parameters.Add("@TipoVehiculoId", SqlDbType.Int).Value = configuradorModel.TextoAuxModel.TipoVehiculoID;
                command.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = configuradorModel.TextoAuxModel.Descripcion;
                SqlDataReader guardaTextoAux = iGenericDataAccess.StoredProcedure(command);
            }
            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }

            return configuradorModel.TextoAuxModel;
        }

        public IList<DocumentosCoberModel> ConsultaDocumentosPorCobertura(ConfiguradorModel configuradorModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                string query = @" select a.IdProducto,
                                   a.IdCobertura,
		                           c.Nombre,
		                           a.UrlArchivoCobertura
		                           from DocumentosPorCobertura a
		                           inner join neCoberturasCotizacion b on a.IdCobertura = b.CoberturaID
		                           inner join Elementos c on a.IdCobertura = c.ElementoID
		                           where b.CotizacionID =" + configuradorModel.CotizacionModel.CotizacionId;
                IList<DocumentosCoberModel> selDocumentos = iGenericDataAccess.ExecuteQuery<DocumentosCoberModel>(query);
                iGenericDataAccess.CloseConnection();
                IList<DocumentosCoberModel> listaDocumentos = selDocumentos.Select(x => new DocumentosCoberModel
                {
                    IdProducto = x.IdProducto,
                    IdCobertura = x.IdCobertura,
                    Nombre = x.Nombre,
                    UrlArchivoCobertura = x.UrlArchivoCobertura
                }).ToList();

                return listaDocumentos;
            }
            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }
        }

        public IList<DocumentosCoberModel> ConsultaDocumentosTodos()
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<DocumentosCoberModel> SelDocumentos = iGenericDataAccess.ExecuteQuery<DocumentosCoberModel>("select * from DocumentosPorCobertura");
                iGenericDataAccess.CloseConnection();

                IList<DocumentosCoberModel> listaProductos = SelDocumentos.Select(x => new DocumentosCoberModel
                {
                    IdProducto = x.IdProducto,
                    IdCobertura = x.IdCobertura,
                    Nombre = x.Nombre,
                    UrlArchivoCobertura = x.UrlArchivoCobertura
                }).ToList();

                return listaProductos;
            }
            catch (Exception ex)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, ex);
            }
        }

    }
}
