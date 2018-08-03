using System;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("AM_VersionSistema")]
    public class AmVersionSistema : IEntity
    {
        public int IdVersion { get; set; }
        public string Version { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string Ot { get; set; }
    }
}