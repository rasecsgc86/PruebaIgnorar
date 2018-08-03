using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Net.Http;
using System.Web;
using Zero.Attributes;

namespace AM45Secure.Commons.Utils
{
    /// <summary>
    /// Clase de utileria en uso de Reflection
    /// </summary>
    public class ReflectionUtil
    {
        /// <summary>
        /// Verifica si un tipo está dentro de los base como primitivos, decimales y fechas
        /// </summary>
        /// <param name="tipo">Tipo que se debe verificar</param>
        /// <returns>Verdadero si esta dentro de los tipos primitivos, Decimal o Datetime</returns>
        public static bool IsBaseType(Type tipo)
        {
            Type[] tipos = new Type[]
            {
                typeof (bool), typeof (byte), typeof (sbyte), typeof (short),
                typeof (ushort), typeof (int), typeof (uint), typeof (long),
                typeof (ulong), typeof (IntPtr), typeof (UIntPtr), typeof (char),
                typeof (double), typeof (float), typeof (decimal), typeof (string),
                typeof (DateTime),
                typeof (bool?), typeof (byte?), typeof (sbyte?), typeof (short?),
                typeof (ushort?), typeof (int?), typeof (uint?), typeof (long?),
                typeof (ulong?), typeof (IntPtr?), typeof (UIntPtr?), typeof (char?),
                typeof (double?), typeof (float?), typeof (decimal?), typeof (DateTime?)
            };

            if (tipo.IsPrimitive)
            {
                return true;
            }

            return tipos.Any(x => x == tipo);
        }
        /// <summary>
        /// Lee las anotaciones de una enumeracion y obtiene su valor string de la anotación
        /// </summary>
        /// <param name="value">objeto del cual se quiere obtener la descripcion</param>
        /// <returns></returns>
        public static string GetStringValue(object value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            EnumString[] attribs = fieldInfo.GetCustomAttributes(
                typeof(EnumString), false) as EnumString[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].Value : "";
        }
        /// <summary>
        /// Copia un objeto logico a un objeto xml
        /// </summary>
        /// <param name="objeto">objeto a convertir a xml</param>
        /// <returns></returns>
        public static string ObjectToXml(object objeto)
        {
            if (null != objeto)
            {
                IList<PropertyInfo> propiedades = objeto.GetType().GetProperties();
                string xml = string.Format("<{0}>", objeto.GetType().Name);

                foreach (PropertyInfo info in propiedades)
                {
                    xml += string.Format("<{0}>", info.Name);
                    if (typeof(IList).IsInstanceOfType(info.GetValue(objeto)))
                    {
                        IList lista = (IList)info.GetValue(objeto);
                        if (lista.Count > 0)
                        {
                            Type tipoInterno = lista[0].GetType();
                            foreach (var item in lista)
                            {
                                if (IsBaseType(tipoInterno))
                                {
                                    //verificamos los caracteres especiales que tenga
                                    Object valueXml;
                                    if (typeof(string) == tipoInterno)
                                    {
                                        valueXml = HttpUtility.HtmlAttributeEncode(((string)item));
                                    }
                                    else
                                    {
                                        valueXml = item;
                                    }


                                    xml += string.Format("<item>{0}</item>", valueXml);
                                }
                                else
                                {
                                    xml += ObjectToXml(item);
                                }
                            }
                        }
                    }
                    else if (IsBaseType(info.PropertyType))
                    {
                        Object valueXml;
                        if (typeof(string) == info.PropertyType)
                        {
                            valueXml = HttpUtility.HtmlAttributeEncode((string)info.GetValue(objeto));
                        }
                        else
                        {
                            valueXml = info.GetValue(objeto);
                        }
                        xml += valueXml;
                    }
                    else
                    {
                        xml += ObjectToXml(info.GetValue(objeto));
                    }
                    xml += string.Format("</{0}>", info.Name);
                }
                xml += string.Format("</{0}>", objeto.GetType().Name);
                return xml;
            }
            return String.Empty;
        }

    }
}
