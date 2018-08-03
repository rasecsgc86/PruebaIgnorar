using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Zero.Attributes;
using Zero.Exceptions;
using Zero.Utils.Models;

namespace Zero.Utils
{
    /// <summary>
    /// Autor: wgrifaldo
    /// Creado el 23/10/2015 13:00
    /// Librearía propiedad de WM TI Soluciones. Copyright (C) Vision Consulting All rights reserved. Todos los derechos reservados.
    /// </summary>
    public class ZeroUtils
    {

        public static IList<T> DataSetToList<T>(DataSet dataSet, int indexTable = 0) where T : class, new()
        {
            IList<T> result = new List<T>();
            foreach (DataRow row in dataSet.Tables[indexTable].Rows)
            {
                T rowEntity = new T();
                foreach (PropertyInfo propiedad in rowEntity.GetType().GetProperties())
                {
                    Column columna = propiedad.GetCustomAttribute<Column>();

                    string nameKey = columna != null ? columna.Name : propiedad.Name;

                    if (dataSet.Tables[indexTable].Columns.Contains(nameKey))
                    {
                        propiedad.SetValue(rowEntity, ChangeType(row[nameKey], propiedad.PropertyType), null);
                    }
                }
                result.Add(rowEntity);
            }
            return result;
        }

        public static T DataSetToModel<T>(DataSet dataSet, int indexTable = 0) where T : class, new()
        {
            foreach (DataRow row in dataSet.Tables[indexTable].Rows)
            {
                T rowEntity = new T();
                foreach (PropertyInfo propiedad in rowEntity.GetType().GetProperties())
                {
                    Column columna = propiedad.GetCustomAttribute<Column>();

                    string nameKey = columna != null ? columna.Name : propiedad.Name;

                    if (dataSet.Tables[indexTable].Columns.Contains(nameKey))
                    {
                        propiedad.SetValue(rowEntity, ChangeType(row[nameKey], propiedad.PropertyType), null);
                    }
                }
                return rowEntity;
            }
            return null;
        }

        public static T DataSetToValue<T>(DataSet dataSet, int indexTable = 0)
        {

            foreach (DataColumn columna in dataSet.Tables[indexTable].Columns)
            {
                foreach (DataRow row in dataSet.Tables[indexTable].Rows)
                {
                    return (T) ChangeType(row[columna.ColumnName], typeof(T));
                }
            }
            return default(T);
        }

        public static object ChangeType(object value, Type conversion)
        {
            try
            {
                Type t = conversion;

                if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    if (value == null || value is DBNull)
                    {
                        return null;
                    }

                    t = Nullable.GetUnderlyingType(t);
                }

                if (value is DBNull)
                {

                    if (t.IsValueType)
                    {
                        return Activator.CreateInstance(t);
                    }

                    return null;
                }

                return Convert.ChangeType(value, t);
            }
            catch (Exception e)
            {
                Logger.Error("Ocurrio un error al obtener la informacion del data set::: ", e);
                if (value != null)
                {
                    throw new ZeroException(string.Format("Error al convertir el tipo de dato {} a {}.", value.GetType().FullName, conversion.FullName), e);
                }
                throw;
            }
            
        }

        /// <summary>
        /// Redondea un numero al siguiente numero si es que tiene decimales
        /// </summary>
        /// <param name="amount">cantidad a redondear</param>
        /// <returns></returns>
        public static int RoundUp(decimal amount)
        {
            int round = Convert.ToInt32(Math.Round(amount));
            if (amount > round)
            {
                round++;
            }
            return round;
        }
        
    }
}
