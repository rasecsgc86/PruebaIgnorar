using System;
using System.Data;
using System.Data.SqlClient;
using NotificacionesTickets.Codes;
using NotificacionesTickets.Exceptions;
using NotificacionesTickets.Utils;

namespace NotificacionesTickets.DataAccess
{
    public class DataAccess
    {
        private static DataAccess Instance;
        private SqlConnection sqlConnection;
        private string strConectionString = string.Empty;

        private DataAccess()
        {
        }

        public static DataAccess GetInstance()
        {
            return Instance ?? (Instance = new DataAccess());
        }

        public void CloseConeccion()
        {
            try
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlConnection = null;
            }
            catch (Exception exception)
            {
                throw new DataAccessException(CodesNotificaciones.ERR_00_01, exception);
            }
        }

        public DataSet ExecuteQuery(string query)
        {
            DataSet dataSet = new DataSet();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataSet);
            }
            catch (Exception exception)
            {
                throw new DataAccessException(CodesNotificaciones.ERR_00_02, exception);
            }
            return dataSet;
        }

        public void ExecuteCommand(string query)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection)
                {
                    CommandTimeout = 600
                };
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
            }
            catch (Exception exception)
            {
                throw new DataAccessException(CodesNotificaciones.ERR_00_03, exception);
            }
        }

        public void OpenConnection()
        {
            try
            {
                if (strConectionString == string.Empty)
                {
                    strConectionString = ConfigXmlUtil.GetDataConfigXml(0);
                }
                sqlConnection = new SqlConnection(strConectionString);
                sqlConnection.Open();
            }
            catch (Exception exception)
            {
                throw new DataAccessException(CodesNotificaciones.ERR_00_00, exception);
            }
        }
    }
}
