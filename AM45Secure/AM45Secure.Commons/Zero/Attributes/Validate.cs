using System;
using AM45Secure.Commons.Zero.Exceptions.Codes;
using Zero.Exceptions;
using Zero.Handlers.Messages;

namespace Zero.Attributes
{
    /// <summary>
    /// Anotación para validar con expresiones regulares algunos campos de un modelo específico
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class Validate : Attribute
    {        
        /// <summary>
        /// Nombre del campo al cual se debe validar, debe especificar la leyenda final
        /// <example>Apellido Paterno</example>
        /// </summary>
        private string field;
        public string Field {
            get
            {
                if (string.IsNullOrEmpty(field))
                {
                    field = string.Empty;
                }
                string nombreCampo = Messages.Message(field);
                if (!string.IsNullOrEmpty(nombreCampo))
                {
                    field = nombreCampo;
                }
                return field;
            }
            set
            {
                field = value;
            }
        }

        /// <summary>
        /// Expresión regular que debe validar con respecto al campo
        /// </summary>
        public string RegularExpression { get; set; }

        
        private string message;
        /// <summary>
        /// Mensaje que deberá desplegar en caso de no cumplir con la expresion regular
        /// </summary>
        public string Message
        {
            get
            {
                
                if (Messages.DefaultMessage.ContainsKey(ZeroCodes.MSJ_00_02))
                {
                    string keyMessageDefault = Messages.DefaultMessage[ZeroCodes.MSJ_00_02];
                    if (string.IsNullOrEmpty(Messages.Message(keyMessageDefault)) || Messages.Message(keyMessageDefault) == keyMessageDefault)
                    {
                        throw new ZeroException(string.Format(ZeroCodes.ERR_00_05, ZeroCodes.MSJ_00_02));
                    }
                    message = Messages.Message(keyMessageDefault);
                }
                else
                {
                    throw new ZeroException(string.Format(ZeroCodes.ERR_00_05, ZeroCodes.MSJ_00_02));
                }

                string mensaje = Messages.Message(message);
                if (!string.IsNullOrEmpty(mensaje))
                {
                    message = string.Format(mensaje, Field);
                }
                
                return message;
            }
            set { message = value; }
        }
    }
}
