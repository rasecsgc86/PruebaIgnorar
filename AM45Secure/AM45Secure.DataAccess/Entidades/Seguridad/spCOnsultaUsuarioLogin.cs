using System.Data;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Seguridad
{
    [Table("spConsultaUsuarioLogin")]
    public class spCOnsultaUsuarioLogin : IEntity
    {
        [Column("PerfilId", SqlDataType = SqlDbType.Int)]
        public string PerfilId { set; get; }

        [Column("PersonaId", SqlDataType = SqlDbType.Int)]
        public string PersonaId { set; get; }

        [Column("Persona", SqlDataType = SqlDbType.VarChar)]
        public string Persona { set; get; } /* INDRA FJQP Nombre de Usuario en pantalla de inicio*/
    }
}