using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using AM45Secure.Commons.Recursos;
using AM45Secure.DataAccess.IDataAccess.IGeneric;
using Zero.Ado;
using Zero.Ado.Models;
using Zero.Attributes;
using Zero.Exceptions;
using Zero.Utils;

namespace AM45Secure.DataAccess.DataAccess.Generic
{
    public class GenericDataAccess : IGenericDataAccess
    {
        public SqlConnection Connection { get; set; }
        public SqlTransaction Transaction { get; set; }
        private readonly DataAccess dataAccess;

        /// <summary>
        /// Constructor de la clase en donde se inyecta la conexion
        /// </summary>
        /// <param name="dataAccess"></param>
        public GenericDataAccess(DataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }


        /// <summary>
        /// Metodo que actualiza una entidad, se debe considerar que los datos para actualizar deben consultarse previamente con el método BuscarUno o Consultar
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns>Entidad actualizada</returns>
        public TEntity Actualizar<TEntity>(TEntity entity) where TEntity : IEntity
        {
            try
            {
                SqlCommand command = Connection.CreateCommand();
                command.Transaction = Transaction;
                Statement statement = QueryZero.GetUpdate(entity);

                foreach (KeyValuePair<string, object> entry in statement.Values)
                {
                    command.Parameters.AddWithValue(entry.Key, entry.Value ?? DBNull.Value);
                }

                command.CommandText = statement.GetQueryStatement();
                command.ExecuteNonQuery();

                return entity;
            }
            catch (Exception e)
            {
                Logger.Error(Codes.ERR_00_04, e);
                throw new DalException(Codes.ERR_00_04, e);
            }
        }

        /// <summary>
        /// Metodo que recibe una entidad para almacenar los datos contenidos en ella
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns>Regresa la entidad actualizada con el identificador en caso de ser autoincrementable</returns>
        public TEntity Guardar<TEntity>(TEntity entity) where TEntity : class, IEntity, new()
        {
            try
            {
                SqlCommand command = Connection.CreateCommand();
                command.Transaction = Transaction;
                Statement statement = QueryZero.GetInsert(entity);

                foreach (KeyValuePair<string, object> entry in statement.Values)
                {
                    command.Parameters.AddWithValue(entry.Key, entry.Value ?? DBNull.Value);
                }

                command.CommandText = statement.GetQueryStatement();
                object idSecuencia = command.ExecuteScalar();
                PropertyInfo idProperty = entity.GetType().GetProperty(statement.ColumnId);
                if (statement.IdentityColumn)
                {
                    idProperty.SetValue(entity, Zero.Utils.ZeroUtils.ChangeType(idSecuencia, idProperty.PropertyType));
                }
                
                return entity;
            }
            catch (Exception e)
            {
                Logger.Error(Codes.ERR_00_02, e);
                throw new DalException(Codes.ERR_00_02, e);
            }
        }

        /// <summary>
        /// Metodo que recibe una entidad y hace la eliminacion del registro
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void Eliminar<TEntity>(TEntity entity) where TEntity : IEntity
        {
            try
            {
                SqlCommand command = Connection.CreateCommand();
                command.Transaction = Transaction;
                Statement statement = QueryZero.GetDelete(entity);

                foreach (KeyValuePair<string, object> entry in statement.Values)
                {
                    command.Parameters.AddWithValue(entry.Key, entry.Value ?? DBNull.Value);
                }

                command.CommandText = statement.GetQueryStatement();
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Error(Codes.ERR_00_03, e);
                throw new DalException(Codes.ERR_00_03, e);
            }
        }

        /// <summary>
        /// Metodo que recibe una entidad para hacer una sonsulta y obtener los datos que necesita
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public IList<TEntity> Consultar<TEntity>(TEntity entity, OptionsQueryZero options = null) where TEntity : class, ISelect, new()
        {
            try
            {
                SqlCommand command = Connection.CreateCommand();
                command.Transaction = Transaction;
                Statement statement = QueryZero.GetSelect(entity, options);

                foreach (KeyValuePair<string, object> entry in statement.Values)
                {
                    command.Parameters.AddWithValue(entry.Key, entry.Value ?? DBNull.Value);
                }

                //Se crea un data set para regresarlo
                DataSet theDataSet = new DataSet();
                command.CommandText = statement.GetQueryStatement();
                SqlDataAdapter theDataAdapter = new SqlDataAdapter(command);
                //Se llena e dataset
                theDataAdapter.Fill(theDataSet);

                return Zero.Utils.ZeroUtils.DataSetToList<TEntity>(theDataSet);
            }
            catch (Exception e)
            {
                Logger.Error(Codes.ERR_00_03, e);
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        /// <summary>
        /// Método que busca el primer elemento de acuerdo a los parametros enviados en la entidad, y discrimina registros de acuerdo a las opciones configuradas.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity">Entidad con los parámetros para el where</param>
        /// <param name="options">Opciones de configuración para discriminar el where de la consulta</param>
        /// <returns></returns>
        public TEntity BuscarUno<TEntity>(TEntity entity, OptionsQueryZero options = null) where TEntity : class, ISelect, new()
        {
            try
            {
                return Consultar(entity, options).FirstOrDefault();
            }
            catch (Exception e)
            {
                Logger.Error(Codes.ERR_00_01, e);
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        /// <summary>
        /// Metodo que sirve para abrir la conexion y transaccion
        /// </summary>
        public void BeginTran()
        {
            try
            {
                Connection = dataAccess.CreateConection();
                Transaction = Connection.BeginTransaction();
            }
            catch (Exception e)
            {
                Logger.Error(Codes.ERR_00_05, e);
                throw new DalException(Codes.ERR_00_05, e);
            }
        }

        /// <summary>
        /// Metodo que sirve para hacer el commit a la transaccion y cerrar la conexion
        /// </summary>
        public void CommitTran()
        {
            try
            {
                Transaction.Commit();
                Connection.Close();
                Transaction = null;
                Connection = null;
            }
            catch (Exception e)
            {
                Logger.Error(Codes.ERR_00_05, e);
                throw new DalException(Codes.ERR_00_05, e);
            }
        }

        /// <summary>
        /// Metodo que sirve para realizar el rollback en caso que la transaccion truene
        /// </summary>
        public void RollbackTran()
        {
            try
            {
                if (Transaction != null && Connection != null)
                {
                    Transaction.Rollback();
                    Connection.Close();
                }
                Transaction = null;
                Connection = null;
            }
            catch (Exception e)
            {
                Logger.Error(Codes.ERR_00_05, e);
                throw new DalException(Codes.ERR_00_05, e);
            }
        }

        /// <summary>
        /// Metodo que sirve para abrir conexion sin trasaccion para consultas
        /// </summary>
        public void OpenConnection()
        {
            try
            {
                //if (Connection == null)
                //{
                Connection = dataAccess.CreateConection();
                //}
            }
            catch (Exception e)
            {
                Logger.Error(Codes.ERR_00_05, e);
                throw new DalException(Codes.ERR_00_05, e);
            }
        }

        /// <summary>
        /// Metodo que sirve para cerrar conexion sin trasaccion para consultas
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                if (Connection != null)
                {
                    Connection.Close();
                    Connection = null;
                }
            }
            catch (Exception e)
            {
                Logger.Error(Codes.ERR_00_05, e);
                throw new DalException(Codes.ERR_00_05, e);
            }
        }

        public SqlDataReader StoredProcedure(SqlCommand cmd)
        {
            try
            {
                // Indica el tipo "StoredProcedure" de comando a ejecuutar
                cmd.Connection = GetConnection();
                cmd.CommandType = CommandType.StoredProcedure;
                //Se declara un data set para regresarlo
                DataSet dataSet = new DataSet();
                // Ejecuta comando
                return (SqlDataReader) cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                Logger.Error(Codes.ERR_00_03, e);
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        /// <summary>
        /// Metodo que sirve para abrir conexion sin trasaccion para consultas
        /// </summary>
        public SqlConnection GetConnection()
        {
            try
            {
                //if (Connection == null)
                //{
                Connection = dataAccess.CreateConection();
                //}

                return Connection;
            }
            catch (Exception e)
            {
                Logger.Error(Codes.ERR_00_05, e);
                throw new DalException(Codes.ERR_00_05, e);
            }
        }

        /// <summary>
        /// Metodo que recibe una sql para ejecutar una consulta normal
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IList<TEntity> ExecuteQuery<TEntity>(string sql) where TEntity : class, ISelect, new()
        {
            try
            {
                SqlCommand command = Connection.CreateCommand();
                command.Transaction = Transaction;
                Statement statement = new Statement()
                                      {
                                          StatementQuery = sql
                                      };

                //Se crea un data set para regresarlo
                DataSet theDataSet = new DataSet();
                command.CommandText = statement.GetQueryStatement();
                SqlDataAdapter theDataAdapter = new SqlDataAdapter(command);
                //Se llena e dataset
                theDataAdapter.Fill(theDataSet);

                return Zero.Utils.ZeroUtils.DataSetToList<TEntity>(theDataSet);
            }
            catch (Exception e)
            {
                Logger.Error(Codes.ERR_00_03, e);
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        public IList<TEntity> ExecuteQuery<TEntity>(string sql, IDictionary<string, object> paramsQuery) where TEntity : class, ISelect, new()
        {
            try
            {
                SqlCommand command = Connection.CreateCommand();
                command.Transaction = Transaction;
                
                foreach (KeyValuePair<string, object> entry in paramsQuery)
                {
                    command.Parameters.AddWithValue(entry.Key, entry.Value ?? DBNull.Value);
                }

                //Se crea un data set para regresarlo
                DataSet theDataSet = new DataSet();
                command.CommandText = sql;
                SqlDataAdapter theDataAdapter = new SqlDataAdapter(command);
                //Se llena e dataset
                theDataAdapter.Fill(theDataSet);
                
                return Zero.Utils.ZeroUtils.DataSetToList<TEntity>(theDataSet);
            }
            catch (Exception e)
            {
                Logger.Error(Codes.ERR_00_03, e);
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        public IList<TEntity> Consultar<TEntity>(string sql, TEntity entity, OptionsQueryZero options = null) where TEntity : class, ISelect, new()
        {
            try
            {
                SqlCommand command = Connection.CreateCommand();
                command.Transaction = Transaction;
                Statement statement = QueryZero.GetSelect(entity, options);

                foreach (KeyValuePair<string, object> entry in statement.Values)
                {
                    command.Parameters.AddWithValue(entry.Key, entry.Value ?? DBNull.Value);
                }

                //Se crea un data set para regresarlo
                DataSet theDataSet = new DataSet();
                command.CommandText = sql;//se cambia por el personalizado en lugar del generado
                SqlDataAdapter theDataAdapter = new SqlDataAdapter(command);
                //Se llena e dataset
                theDataAdapter.Fill(theDataSet);

                return Zero.Utils.ZeroUtils.DataSetToList<TEntity>(theDataSet);
            }
            catch (Exception e)
            {
                Logger.Error(Codes.ERR_00_03, e);
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        public IList<TEntity> ExecuteStoredProcedure<TEntity>(TEntity entity, OptionsQueryZero options = null) where TEntity : class, ISelect, new()
        {
            try
            {
                SqlCommand command = QueryZero.GetStoredProcedureCommand(entity, options);
                command.Connection = GetConnection();
                command.Transaction = Transaction;

                //Se crea un data set para regresarlo
                DataSet theDataSet = new DataSet();
                SqlDataAdapter theDataAdapter = new SqlDataAdapter(command);
                //Se llena e dataset
                theDataAdapter.Fill(theDataSet);

                return Zero.Utils.ZeroUtils.DataSetToList<TEntity>(theDataSet);
            }
            catch (Exception e)
            {
                Logger.Error(Codes.ERR_00_03, e);
                throw new DalException(Codes.ERR_00_16, e);
            }
        }

        public void ExecuteSql(string sql)
        {
            try
            {
                SqlCommand command = Connection.CreateCommand();
                command.Transaction = Transaction;
                command.CommandText = sql;
                command.ExecuteScalar();
            }
            catch (Exception e)
            {
                Logger.Error(Codes.ERR_00_02, e);
                throw new DalException(Codes.ERR_00_02, e);
            }
        }
    }
}