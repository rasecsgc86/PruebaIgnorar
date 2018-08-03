using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using AM45Secure.Commons.Constantes.Comunes;
using AM45Secure.Commons.Zero.Exceptions.Codes;
using Zero.Ado.Models;
using Zero.Attributes;
using Zero.Exceptions;

namespace Zero.Ado
{
    public class QueryZero
    {
        /// <summary>
        /// Se debe colocar el tipo de dato que lleva un wrapper, en el caso de string lleva '
        /// </summary>
        private static readonly IDictionary<Type, string> TiposWrapper = new Dictionary<Type, string>()
                                                            {
                                                                {typeof(string),"'" },
                                                                {typeof(DateTime),"'" },
                                                                {typeof(DateTime?),"'" }
                                                            };

        private static object GetValueForDataBase(object value, bool isWhere = false, OptionsQueryZero options = null)
        {
            if (options == null)
            {
                options = new OptionsQueryZero();
            }

            if (value == null)
            {
                return isWhere ? "is null" : "null";
            }

            object valor = value;
            string prefix = string.Empty;
            Type propertyType = value.GetType();
            //se asigna el prefijo solo si contiene valor
            if (TiposWrapper.ContainsKey(propertyType))
            {
                prefix = TiposWrapper[propertyType];
            }

            if (value is bool)
            {
                valor = (bool)value ? "1" : "0";
            }

            if (value is string)
            {
                if (options.UseLikeForString)
                {
                    valor = "%" + valor + "%";
                }
            }

            if (value is DateTime)
            {
                if ((DateTime) value == DateTime.MinValue || (DateTime) value == DateTime.MaxValue)
                {
                    return isWhere ? "is null" : "null";
                }
                valor = ((DateTime)value).ToString("dd/MM/yyyy HH:mm:ss");
            }

            return prefix + valor + prefix;
        }

        public static Statement GetUpdate(IEntity entity)
        {
            if (entity == null)
            {
                throw new ZeroException(ZeroCodes.ERR_00_01);
            }

            StringBuilder statement = new StringBuilder();
            StringBuilder statementWhere = new StringBuilder();
            IDictionary<string, object> values = new Dictionary<string, object>();

            //obtenemos el nombre de la tabla
            Table table = entity.GetType().GetCustomAttribute<Table>();
            string nameTable = table == null ? entity.GetType().Name : table.Name;

            //se deben obtener todas las propiedades, incluidas las de la super clase
            PropertyInfo[] properties = entity.GetType().GetProperties();

            statement.Append("Update ");
            statement.Append(nameTable);
            statement.Append(" SET ");

            bool isFirst = true;
            object valueId = null;
            foreach (PropertyInfo property in properties)
            {
                Column columna = property.GetCustomAttribute<Column>();
                IdColumn idColumn = property.GetCustomAttribute<IdColumn>();

                if (columna != null && columna.Exclude)
                {
                    continue;
                }

                string nameColumn = columna == null ? property.Name : columna.Name;
                //obtenemos el valor del id y la columna para el where del update
                if (idColumn != null)
                {
                    statementWhere.Append(" Where ");
                    statementWhere.Append(nameColumn);
                    statementWhere.Append(" = @");
                    statementWhere.Append(nameColumn);

                    valueId = GetValue(property.GetValue(entity, null));
                }
                else //si no es id
                {
                    if (isFirst)
                    {
                        isFirst = false;
                    }
                    else
                    {
                        statement.Append(", ");
                    }

                    statement.Append(nameColumn);
                    statement.Append(" = @");
                    statement.Append(nameColumn);

                }
                values.Add("@"+nameColumn, GetValue(property.GetValue(entity, null)));
            }

            if (valueId == null)
            {
                throw new ZeroException(ZeroCodes.ERR_00_02);
            }
            statement.Append(statementWhere);
            Statement statementComplete = new Statement(statement.ToString(), values);
            return statementComplete;
        }

        /// <summary>
        /// Método que genera la cadena de un Insert a partir de una entidad
        /// </summary>
        /// <param name="entity">entidad de la cual se queire obtener un insert</param>
        /// <returns>Statement</returns>
        public static Statement GetInsert(IEntity entity)
        {
           
            if (entity == null)
            {
                throw new ZeroException(ZeroCodes.ERR_00_01);
            }

            StringBuilder statement = new StringBuilder();
            StringBuilder statementParams = new StringBuilder();
            IDictionary<string, object> values = new Dictionary<string, object>();

            //obtenemos el nombre de la tabla
            Table table = entity.GetType().GetCustomAttribute<Table>();
            string nameTable = table == null ? entity.GetType().Name : table.Name;

            //se deben obtener todas las propiedades, incluidas las de la super clase
            PropertyInfo[] properties = entity.GetType().GetProperties();

            //generamos la base de la sentencia de insert
            statement.Append("Insert into ");
            statement.Append(nameTable);
            statement.Append(" (");

            bool isFirst = true;
            string columnaId = null;
            bool isIdSequence = false;
            
            foreach (PropertyInfo property in properties)
            {
                Column columna = property.GetCustomAttribute<Column>();
                IdColumn idColumn = property.GetCustomAttribute<IdColumn>();

                if (columna != null && columna.Exclude)
                {
                    continue;
                }

                if (idColumn != null)
                {
                    columnaId = property.Name;
                }

                bool sequence = idColumn != null && idColumn.Identity;
                if (sequence)
                {
                    isIdSequence = true;
                }
                string nameColumn = columna == null ? property.Name : columna.Name;
                //si no es secuencia hacemos el proceso
                if (!sequence)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        
                    }
                    else
                    {
                        statement.Append(", ");
                        statementParams.Append(",");
                    }
                    statementParams.Append("@");
                    statementParams.Append(nameColumn);
                    //si no hay anotacion de columna ponemos el nombre de la propiedad de la entidad
                    statement.Append(nameColumn);

                    object value = GetValue(property.GetValue(entity, null));
                    
                    values.Add("@"+nameColumn,value);
                }
            }

            statement.Append(" ) Values (");
            statement.Append(statementParams);
            statement.Append(")");
            Statement statementComplete = new Statement(statement.ToString(), values)
            {
                ColumnId = columnaId,
                IdentityColumn = isIdSequence
            };

            return statementComplete;
        }


        public static Statement GetDelete(IEntity entity)
        {
            if (entity == null)
            {
                throw new ZeroException(ZeroCodes.ERR_00_01);
            }

            StringBuilder statement = new StringBuilder();
            IDictionary<string, object> values = new Dictionary<string, object>();

            //obtenemos el nombre de la tabla
            Table table = entity.GetType().GetCustomAttribute<Table>();
            string nameTable = table == null ? entity.GetType().Name : table.Name;

            //se deben obtener todas las propiedades, incluidas las de la super clase
            PropertyInfo[] properties = entity.GetType().GetProperties();

            //generamos la base de la sentencia de insert
            statement.Append("Delete From ");
            statement.Append(nameTable);
            statement.Append(" Where ");

            PropertyInfo property = properties.FirstOrDefault(x => x.GetCustomAttribute<IdColumn>() != null);
            if (property == null || property.GetValue(entity, null) == null)
            {
                throw new ZeroException(ZeroCodes.ERR_00_02);
            }

            Column columna = property.GetCustomAttribute<Column>();
            object value = GetValue(property.GetValue(entity, null));
            string nameColumn = columna == null ? property.Name : columna.Name;
            statement.Append(nameColumn);
            statement.Append(" = @");
            statement.Append(nameColumn);
            values.Add("@" + nameColumn, value);

            Statement statementComplete = new Statement(statement.ToString(), values);


            return statementComplete;
        }

        public static Statement GetSelect(ISelect entity, OptionsQueryZero options = null)
        {
            if (options == null)
            {
                options = new OptionsQueryZero();
            }
            Statement statementComplete = new Statement();
            if (entity == null)
            {
                throw new ZeroException(ZeroCodes.ERR_00_01);
            }


            StringBuilder selectFields = new StringBuilder();
            StringBuilder selecttWhere = new StringBuilder();
            IDictionary<string, object> values = new Dictionary<string, object>();

            //obtenemos valores de las anotaciones
            Table table = entity.GetType().GetCustomAttribute<Table>();
            string nameTable = table == null ? entity.GetType().Name : table.Name;

            //se deben obtener todas las propiedades, incluidas las de la super clase
            PropertyInfo[] properties = entity.GetType().GetProperties();

            bool isFirst = true;
            bool isFirstFilter = true;
            foreach (PropertyInfo property in properties)
            {
                Column columna = property.GetCustomAttribute<Column>();
                if (columna != null && columna.Exclude)
                {
                    continue;
                }
                string nameColumn = columna == null ? property.Name : columna.Name;

                statementComplete.SelectColumns.Add(property.Name, nameColumn);
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    selectFields.Append(", ");
                }
                selectFields.Append(nameColumn);

                object value = property.GetValue(entity, null);
                bool useEqual = true;

                if (value is DateTime && ((DateTime)value == DateTime.MinValue || (DateTime)value == DateTime.MaxValue))
                {
                    value = null;
                }

                if (value is string)
                {
                    if (string.IsNullOrEmpty((string)value))
                    {
                        value = null;
                    }

                    if (options.UseLikeForString)
                    {
                        useEqual = false;
                    }
                }

                if (value is bool && options.ExcludeBool)
                {
                    value = null;
                }

                if (value is int && (int)value == default(int) && options.ExcludeNumericsDefaults)
                {
                    value = null;
                }

                if (value is long && (long)value == default(long) && options.ExcludeNumericsDefaults)
                {
                    value = null;
                }

                if (value is decimal && (decimal)value == default(decimal) && options.ExcludeNumericsDefaults)
                {
                    value = null;
                }

                if (value is double && Math.Abs((double)value - default(double)) < 0.01 && options.ExcludeNumericsDefaults)
                {
                    value = null;
                }

                if (value != null)
                {
                    if (isFirstFilter)
                    {
                        isFirstFilter = false;
                    }
                    else
                    {
                        selecttWhere.Append(" and ");
                    }
                    selecttWhere.Append(nameColumn);
                    selecttWhere.Append(useEqual ? " = " : " like ");
                    selecttWhere.Append("@");
                    selecttWhere.Append(nameColumn);


                    values.Add("@" + nameColumn, GetValue(property.GetValue(entity, null)));
                }


            }
            StringBuilder select = new StringBuilder();
            select.Append("Select ");
            select.Append(selectFields);
            select.Append(" From ");
            select.Append(nameTable);

            if (!isFirstFilter && !options.ExcludeWhere)
            {
                select.Append(" Where ");
                select.Append(selecttWhere);
            }

            statementComplete.StatementQuery = select.ToString();
            statementComplete.Values = values;

            if (!string.IsNullOrEmpty(options.WhereComplementary) && !options.ExcludeWhere)
            {
                if (options.WhereComplementary.Trim().ToUpper().StartsWith("AND "))
                {
                    options.WhereComplementary = options.WhereComplementary.Trim().Substring(3);
                }
                if (!isFirstFilter)
                {
                    //si empieza con and
                    statementComplete.StatementQuery += " and ";
                }
                else
                {
                    statementComplete.StatementQuery += " Where ";
                }
                statementComplete.StatementQuery += options.WhereComplementary;
            }

            return statementComplete;
        }



        public static Statement GetMax<TEntity>()
        {
            Statement statementComplete = new Statement();

            //obtenemos valores de las anotaciones
            Table table = typeof(TEntity).GetCustomAttribute<Table>();
            string nameTable = table == null ? typeof(TEntity).Name : table.Name;

            //se deben obtener todas las propiedades, incluidas las de la super clase
            PropertyInfo[] properties = typeof(TEntity).GetProperties();


            //buscamos el id para verificar el max
            PropertyInfo idProperty = properties.FirstOrDefault(x => x.GetCustomAttribute<IdColumn>() != null);
            if (idProperty == null)
            {
                throw new ZeroException(ZeroCodes.ERR_00_04);
            }
            Column idColumna = idProperty.GetCustomAttribute<Column>();

            StringBuilder select = new StringBuilder();
            select.Append("Select ");
            select.Append(" max(");
            select.Append(idColumna == null ? idProperty.Name : idColumna.Name);
            select.Append(") as max");
            select.Append(" From ");
            select.Append(nameTable);

            statementComplete.StatementQuery = select.ToString();
            statementComplete.Values = null;

            return statementComplete;
        }

        private static object GetValue(object value)
        {
            if (value is DateTime)
            {
                if ((DateTime)value == DateTime.MinValue || (DateTime)value == DateTime.MaxValue)
                {
                    return null;
                }
            }
            return value;
        }

        public static SqlCommand GetStoredProcedureCommand(ISelect entity, OptionsQueryZero options = null)
        {
            Table table = entity.GetType().GetCustomAttribute<Table>();
            string nameTable = table == null ? entity.GetType().Name : table.Name;

            SqlCommand command = new SqlCommand
            {
                CommandText = nameTable,
                CommandType =  CommandType.StoredProcedure
            };

            PropertyInfo[] properties = entity.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                Column columna = property.GetCustomAttribute<Column>();
                if (columna != null && columna.Exclude)
                {
                    continue;
                }

                string nameColumn = columna == null ? property.Name : columna.Name;
                object value = property.GetValue(entity, null);
                
                if (value is DateTime && ((DateTime)value == DateTime.MinValue || (DateTime)value == DateTime.MaxValue))
                {
                    value = null;
                }

                if (value is string)
                {
                    if (string.IsNullOrEmpty((string)value))
                    {
                        value = null;
                    }
                }

                if (value is bool && options.ExcludeBool)
                {
                    value = null;
                }

                if (value is int && (int)value == default(int) && options.ExcludeNumericsDefaults)
                {
                    value = null;
                }

                if (value is long && (long)value == default(long) && options.ExcludeNumericsDefaults)
                {
                    value = null;
                }

                if (value is decimal && (decimal)value == default(decimal) && options.ExcludeNumericsDefaults)
                {
                    value = null;
                }

                if (value is double && Math.Abs((double)value - default(double)) < 0.01 && options.ExcludeNumericsDefaults)
                {
                    value = null;
                }

                if (value != null)
                {
                    command.Parameters.Add("@" + nameColumn, columna.SqlDataType, GetValue(property.GetValue(entity, null)).ToString().Length).Value = (string) GetValue(property.GetValue(entity, null));
                }
            }
            return command;
        }
    }
}
