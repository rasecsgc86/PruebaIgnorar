using System;

namespace Zero.Attributes
{
    /// <summary>
    /// Atribto que se usa par ralizar el mapeo de una vista o entidad de base de datos a una entidad
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class Table : Attribute
    {
        public string Name { get; set; }

        public Table(string name)
        {
            Name = name;
        }
    }
}
