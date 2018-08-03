using System;

namespace Zero.Attributes
{
    /// <summary>
    /// Anotacion para obtener el valor string de la descripcion de una enumeración, se anota con descripciones especiales
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumString : Attribute
    {
        /// <summary>
        /// Mensaje de la anotación
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Valor que tomará la anotación
        /// </summary>
        public string Value { get; set; }
    }
}
