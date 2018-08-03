using System;
using System.Collections.Generic;
using System.Reflection;

namespace Zero.Handlers.Messages
{
    /// <summary>
    /// Clase que permite configurar los codigos de error y mensajes
    /// </summary>
    public class Messages
    {
        private static readonly IDictionary<string, string> Mensajes = new Dictionary<string, string>();
        private static IDictionary<string, string> ConfigurationMessage;
        /// <summary>
        /// Agrega a la configuración un mensaje y un código de error
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public static void AddMessage(string code, string message)
        {
            if (Mensajes.ContainsKey(code))
            {
                Mensajes[code] = message;
            }
            Mensajes.Add(code,message);
        }

        public static string Message(string code)
        {
            if (!Mensajes.ContainsKey(code))
            {
                return code;
            }
            return Mensajes[code];
        }

        public static void LoadMessages<T>(T objetoRecursos)
        {
            Type tipo = objetoRecursos.GetType();
            IList<PropertyInfo> propiedades = tipo.GetProperties();
            foreach (PropertyInfo info in propiedades)
            {
                //si es de tipo string lo agregamos al Messages
                if (info.PropertyType == typeof(string))
                {
                    string value = (string) info.GetValue(objetoRecursos);
                    if (!string.IsNullOrEmpty(value))
                    {
                        AddMessage(info.Name,value);
                    }
                }
            }
        }

        public static void LoadMessages(Type tipo)
        {
            IList<PropertyInfo> propiedades = tipo.GetProperties();
            foreach (PropertyInfo info in propiedades)
            {
                //si es de tipo string lo agregamos al Messages
                if (info.PropertyType == typeof(string))
                {
                    string value = (string)info.GetValue(null);
                    if (!string.IsNullOrEmpty(value))
                    {
                        AddMessage(info.Name, value);
                    }
                }
            }
        }

        public static IDictionary<string, string> MessageList
        {
            get
            {
                return Mensajes;
            }
        }

        public static IDictionary<string, string> DefaultMessage
        {
            get
            {
                if (ConfigurationMessage == null)
                {
                    ConfigurationMessage = new Dictionary<string, string>();
                }
                return ConfigurationMessage;
            }
            set
            {
                ConfigurationMessage = value;
            }
        }
    }
}
