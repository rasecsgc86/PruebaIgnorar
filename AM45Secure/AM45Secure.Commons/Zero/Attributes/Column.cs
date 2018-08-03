using System;
using System.Data;

namespace Zero.Attributes
{
    /// <summary>
    /// Anotación para definir el nombre de un atributo de un IEntity o ISlect 
    /// cuando el nombre no corresponde al de la base de datos, o bien, cuando
    /// se tiene un propiedad en un Entity o Model y que se requiere excluir de
    /// la asignación del mapeo. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Column : Attribute
    {
        /// <summary>
        /// Atributo que almacena el nombre real de la base de datos que se requiere mapear
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Atributo que define si se exluye del mapeo el campo en la entidad o modelo
        /// </summary>
        public bool Exclude { get; }
        /// <summary>
        /// Atributo que define el tipo de dato para la columna en un stored procedure
        /// </summary>
        public SqlDbType SqlDataType { set; get; }

        public Column(string name)
        {
            Name = name;
            Exclude = false;
            SqlDataType = SqlDbType.VarChar;
        }

        public Column(string name, bool exclude)
        {
            Name = name;
            Exclude = exclude;
            SqlDataType = SqlDbType.VarChar;
        }

        public Column(bool exclude)
        {
            Exclude = exclude;
            SqlDataType = SqlDbType.VarChar;
        }

        public Column(SqlDbType sqlDbType, bool exclude)
        {
            Exclude = exclude;
            SqlDataType = sqlDbType;
        }
    }
}
