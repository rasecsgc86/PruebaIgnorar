using System;

namespace Zero.Attributes
{
    /// <summary>
    /// Anotación para definir un campo como un identificador de una tabla, 
    /// debe ser asociado con clases que extiendan de IEntity
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IdColumn : Attribute
    {
        /// <summary>
        /// Atributo que define si el identificador es una secuencia o autoincrementable
        /// </summary>
        public bool Identity { get; set; }
        /// <summary>
        /// Constructor para marcar un atributo como identificador como no autoincrementable o sin secuencia
        /// </summary>
        public IdColumn()
        {
            Identity = false;
        }

        /// <summary>
        /// Constructor para marcar un atributo como identificador autoincrementable
        /// </summary>
        /// <param name="identity"></param>
        public IdColumn(bool identity)
        {
            Identity = identity;
        }
    }
}
