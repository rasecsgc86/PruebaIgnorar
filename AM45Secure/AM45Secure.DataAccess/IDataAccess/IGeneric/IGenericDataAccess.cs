using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Zero.Ado;
using Zero.Ado.Models;

namespace AM45Secure.DataAccess.IDataAccess.IGeneric
{
    public interface IGenericDataAccess
    {
        /// <summary>
        /// Método que realiza el guardado de la entidad recibida, si la entidad contiene un id la actualiza de lo contrario la inserta
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Actualizar<TEntity>(TEntity entity) where TEntity : IEntity;

        /// <summary>
        /// Método que realiza el guardado de la entidad recibida, si la entidad contiene un id la actualiza de lo contrario la inserta
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Guardar<TEntity>(TEntity entity) where TEntity : class, IEntity, new();

        /// <summary>
        /// Método que elimina fisicamente registros de acuerdo a los filtros proporcionados en la entidad
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        void Eliminar<TEntity>(TEntity entity) where TEntity : IEntity;

        /// <summary>
        /// Método que realiza la busqueda de la entidad por su contenido de valores
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="options">Opciones que permiten ampliar la consulta where, eliminar de la consulta los enteros por default y boleanos</param>
        /// <returns></returns>
        IList<TEntity> Consultar<TEntity>(TEntity entity, OptionsQueryZero options = null) where TEntity : class, ISelect, new();

        /// <summary>
        /// Método que realiza la busqueda de la entidad por su contenido de valores
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="options">Opciones que permiten ampliar la consulta where, eliminar de la consulta los enteros por default y boleanos</param>
        /// <returns></returns>
        TEntity BuscarUno<TEntity>(TEntity entity, OptionsQueryZero options = null) where TEntity : class, ISelect, new();

        /// <summary>
        /// Se incia transaccion
        /// </summary>
        void BeginTran();

        /// <summary>
        /// Se hace commit
        /// </summary>
        void CommitTran();

        /// <summary>
        /// Se gace el rollback
        /// </summary>
        void RollbackTran();

        /// <summary>
        /// Metodo para abrir conexion sin trasaccion
        /// </summary>
        void OpenConnection();


        /// <summary>
        /// Metodo para cerrar conexion sin trasaccion
        /// </summary>
        void CloseConnection();

        SqlDataReader StoredProcedure(SqlCommand cmd);

        SqlConnection GetConnection();

        IList<TEntity> ExecuteQuery<TEntity>(string sql) where TEntity : class, ISelect, new();

        IList<TEntity> ExecuteQuery<TEntity>(string sql, IDictionary<string, object> paramsQuery) where TEntity : class, ISelect, new();

        IList<TEntity> Consultar<TEntity>(string sql,
                                                 TEntity entity,
                                                 OptionsQueryZero options = null) where TEntity : class, ISelect, new();

        IList<TEntity> ExecuteStoredProcedure<TEntity>(TEntity entity,
                                          OptionsQueryZero options = null) where TEntity : class, ISelect, new();

        void ExecuteSql(string sql);
    }
}