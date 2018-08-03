using AM45Secure.DataAccess.IDataAccess.IManuales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM45Secure.Commons.Modelos.Manuales;
using System.Data.SqlClient;
using AM45Secure.Commons.Constantes.Comunes;
using System.Data;
using AM45Secure.DataAccess.IDataAccess.IGeneric;
using Zero.Exceptions;
using AM45Secure.Commons.Recursos;

namespace AM45Secure.DataAccess.DataAccess.Manuales
{
    public class ManualesDataAccess : IManualesDataAccess
    {
        private readonly IGenericDataAccess iGenericDataAccess;


        public ManualesDataAccess(IGenericDataAccess iGenericDataAccess)
        {
            this.iGenericDataAccess = iGenericDataAccess;
        }

        public IList<CategoriaModel> ConsultarCategoria()
        {
            List<CategoriaModel> listDatos = new List<CategoriaModel>();
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpObtieneDatosCategoriaFlex
                };
            


                SqlDataReader datosStored = iGenericDataAccess.StoredProcedure(command);
                if (datosStored.HasRows)
                {
                    while (datosStored.Read())
                    {
                        listDatos.Add(new CategoriaModel
                        {

                            Id = Convert.ToInt32(datosStored["Id"]),
                            NombreCategoria = Convert.ToString(datosStored["Categoria"])

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

        public IList<ManualesModel> ConsultarManuales(ManualRequest manualModel)
        {
            List<ManualesModel> listDatos = new List<ManualesModel>();
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpObtieneDatosManualesFlex
                };
                command.Parameters.Add("@Cliente", SqlDbType.BigInt).Value = manualModel.Cliente;
                command.Parameters.Add("@Producto", SqlDbType.BigInt).Value = manualModel.Producto;
                command.Parameters.Add("@Categoria", SqlDbType.BigInt).Value = manualModel.Categoria;
                command.Parameters.Add("@Texto", SqlDbType.VarChar).Value = manualModel.Texto;
                command.Parameters.Add("@todo", SqlDbType.Int).Value = manualModel.Todo;


                SqlDataReader datosStored = iGenericDataAccess.StoredProcedure(command);
                if (datosStored.HasRows)
                {
                    while (datosStored.Read())
                    {
                        listDatos.Add(new ManualesModel
                        {

                            Id = Convert.ToInt32(datosStored["Id"]),
                            Nombre = Convert.ToString(datosStored["Nombre"]),
                            Url = Convert.ToString(datosStored["Url"]),

                            Descripcion = Convert.ToString(datosStored["Descripcion"]),
                            Usuario = Convert.ToString(datosStored["Usuario"]),

                            Fecha = Convert.ToDateTime(datosStored["Fecha"]),

                            ClienteId = Convert.ToInt32(datosStored["ClienteId"]),
                            ProductoID = Convert.ToInt32(datosStored["ProductoID"]),
                            IdCategoria = Convert.ToInt32(datosStored["IdCategoria"])
                            

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

  

        public bool GuardarDatosDocumento(InsertManualRequest requestModel,string usuario)
        {
            List<ManualesModel> listDatos = new List<ManualesModel>();
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = (requestModel.IsUpdate == 0) ? ConstStoredProcedures.SpInsertManualesFlex : ConstStoredProcedures.SpUpdateManualesFlex
                };
                if(requestModel.IsUpdate == 1)// Solo Para Updates
                command.Parameters.Add("@Id", SqlDbType.VarChar).Value = requestModel.Id;

                command.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = requestModel.Nombre;
                command.Parameters.Add("@URl", SqlDbType.VarChar).Value = requestModel.Url;
                command.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = requestModel.Descripcion;
                command.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = usuario;
                command.Parameters.Add("@Idcategoria", SqlDbType.Int).Value = requestModel.IdCategoria;
                command.Parameters.Add("@IdCliente", SqlDbType.Int).Value = requestModel.IdCliente;
                command.Parameters.Add("@idProducto", SqlDbType.Int).Value = requestModel.IdProducto;
                

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


        public bool ElimarDocumento(int Id)
        {
     
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpEliminaManualesFlex
                };
              
                command.Parameters.Add("@Id", SqlDbType.Int).Value = Id;


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

        public FiltroManuales FiltrosDocumentos(string IdUsuario)
        {
            //
            var Datos = new FiltroManuales();
          
            List<Clientes> listClientes = new List<Clientes>();
            List<Productos> listProdcutos = new List<Productos>();
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpSelClientesPorUsuarioFlex
                };
                command.Parameters.Add("@UsuarioId", SqlDbType.VarChar).Value = IdUsuario;
          


                SqlDataReader datosStored = iGenericDataAccess.StoredProcedure(command);
                if (datosStored.HasRows)
                {
                    while (datosStored.Read())
                    {
                        listClientes.Add(new Clientes
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
                        listProdcutos.Add(new Productos
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
    }
}
