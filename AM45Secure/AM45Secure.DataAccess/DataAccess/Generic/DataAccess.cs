using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM45Secure.DataAccess.DataAccess.Generic
{
    public class DataAccess
    {
        //Nombre de la conexion a BD que se define en el web.config
        public static readonly string CONNECTION_DB = "conexion";

        /// <summary>
        /// Metodo que genera la conexion
        /// </summary>
        /// <returns></returns>
        public SqlConnection CreateConection()
        {
            string connetionString = ConfigurationManager.ConnectionStrings[DataAccess.CONNECTION_DB].ToString();
            SqlConnection connection = new SqlConnection(connetionString);
            connection.Open();
            return connection;
        }

        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[DataAccess.CONNECTION_DB].ToString();
        }
    }
}
