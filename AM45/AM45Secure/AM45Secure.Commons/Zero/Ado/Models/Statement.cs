using System.Collections.Generic;

namespace Zero.Ado.Models
{
    /// <summary>
    /// Autor: wgrifaldo
    /// Creado el 23/10/2015 13:00
    /// Librearía propiedad de WM TI Soluciones(Walther Grifaldo Zúñiga) y Vision Consulting. Copyright (C) Vision Consulting All rights reserved. Todos los derechos reservados.
    /// 
    /// Clase que contiene información de la sentencia o consulta generada
    /// </summary>
    /// <remarks>
    /// Clase que contiene información de la sentencia o consulta generada
    /// </remarks>
    public class Statement
    {
        /// <summary>
        /// Bases de datos soportadas
        /// </summary>
        public enum DataBase { SqlServer, MySql }
        /// <summary>
        /// Almacena el valor del nombre del identificador de la tabla. Únicamente tendra valor para los insert
        /// </summary>
        public string ColumnId { get; set; }
        /// <summary>
        /// Determina si la columna de Identificador es autoincrementable
        /// </summary>
        public bool IdentityColumn { get; set; }
        /// <summary>
        /// Contiene la consulta o sentencia generada
        /// </summary>
        public string StatementQuery { get; set; }
        /// <summary>
        /// Valores o parametros de la consulta o sentencia
        /// </summary>
        public IDictionary<string,object> Values { get; set; }

        /// <summary>
        /// Contiene la relacion de propiedad y campo de bd que se usan en el select, unicamente disponible en getSelect
        /// </summary>
        public IDictionary<string, string> SelectColumns
        {
            get
            {
                if (selectColumns == null)
                {
                    selectColumns = new Dictionary<string, string>();
                }
                return selectColumns;
            }
            set
            {
                selectColumns = value;
            }
        }

        /// <summary>
        /// Contiene la relacion de propiedad y campo de bd que se usan en el select, unicamente disponible en getSelect
        /// </summary>
        private IDictionary<string, string> selectColumns;
        
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Statement()
        {
        }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="statement"> sentecnia o consulta formada</param>
        /// <param name="values">valores de los parametros de la consulta formada</param>
        public Statement(string statement, IDictionary<string, object> values)
        {
            StatementQuery = statement;
            Values = values;
        }

        /// <summary>
        /// Obtiene la consulta o sentencia para la base de datos indicada
        /// </summary>
        /// <param name="tipo">tipo de base de datos a la cual se aplicará la consulta, por default contiene el valor DataBase.SqlServer</param>
        /// <returns>cadena con la sentencia o consulta a ejecutar en una plataforma ADO</returns>
        public string GetQueryStatement(DataBase tipo = DataBase.SqlServer)
        {
            if (IdentityColumn)
            {
                switch (tipo)
                {
                    case DataBase.SqlServer:
                        return StatementQuery + "; SELECT SCOPE_IDENTITY();";
                    case DataBase.MySql:
                        return StatementQuery + "; select last_insert_id();";
                    default:
                        return StatementQuery + "; SELECT SCOPE_IDENTITY();";
                }
            }
            return StatementQuery;
        }


    }
}
