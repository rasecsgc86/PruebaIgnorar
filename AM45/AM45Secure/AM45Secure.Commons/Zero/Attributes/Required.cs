using System;
using AM45Secure.Commons.Zero.Exceptions.Codes;
using Zero.Exceptions;
using Zero.Handlers.Messages;

namespace Zero.Attributes
{
    /// <summary>
    /// Anotación para definir campos requeridos en un modelo de datos(DTO)
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class Required : Attribute
    {

        

        private string fieldName;
        /// <summary>
        /// Nombre del campo al cual se debe validar, debe especificar la leyenda final
        /// ó Clave ingresada en Messages para cargar los mensajes
        /// <example>Apellido Paterno</example>
        /// </summary>
        public string FieldName {
            get
            {
                if (string.IsNullOrEmpty(fieldName))
                {
                    fieldName = string.Empty;
                }
                string nombreCampo = Messages.Message(fieldName);
                if (!string.IsNullOrEmpty(nombreCampo))
                {
                    fieldName = nombreCampo;
                }
                return fieldName;
            }
            set
            {
                fieldName = value;
            }
        }

        public bool Optional { get; set; }

        private string messageRequired;
        /// <summary>
        /// Mensaje que debe contener al validar si es requerido el campo.
        /// ó clave agregada al Message
        /// Se puede agregar como parametro unicamente el nombre del campo.
        /// <example>El campo {0} es requerido.</example>
        /// </summary>
        public string MessageRequired
        {
            get
            {
                if (string.IsNullOrEmpty(messageRequired))
                {
                    if (Messages.DefaultMessage.ContainsKey(ZeroCodes.MSJ_00_01))
                    {
                        string keyMessageDefault = Messages.DefaultMessage[ZeroCodes.MSJ_00_01];
                        if (string.IsNullOrEmpty(Messages.Message(keyMessageDefault)) || Messages.Message(keyMessageDefault) == keyMessageDefault)
                        {
                            throw new ZeroException(string.Format(ZeroCodes.ERR_00_05, ZeroCodes.MSJ_00_01));
                        }
                        messageRequired = Messages.Message(keyMessageDefault);
                    }
                    else
                    {
                        throw new ZeroException(string.Format(ZeroCodes.ERR_00_05, ZeroCodes.MSJ_00_01));
                    }
                    
                }

                string mensajeRequerido = Messages.Message(messageRequired);
                if (!string.IsNullOrEmpty(mensajeRequerido))
                {
                    messageRequired = string.Format(mensajeRequerido, FieldName);
                }
                return messageRequired;
            }
            set { messageRequired = value; } 
        }

        
        
    }
}
