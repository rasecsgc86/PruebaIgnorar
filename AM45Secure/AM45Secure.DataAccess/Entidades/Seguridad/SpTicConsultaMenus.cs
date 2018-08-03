using System.Data;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Seguridad
{
    [Table("spTICConsultaMenus")]
    public class SpTicConsultaMenus : IEntity
    {
        [Column("PerfilId", SqlDataType = SqlDbType.Int)]
        public string PerfilId { set; get; }

        [Column("PersonaId", SqlDataType = SqlDbType.Int)]
        public string PersonaId { set; get; }

        [Column("CveMenu", SqlDataType = SqlDbType.VarChar)]
        public string ClaveMenu { set; get; }

        [Column("ManejaUDI", SqlDataType = SqlDbType.VarChar)]
        public string ManejaUDI { get; set; } /* INDRA FJQP ManejaUDI*/
        public string NombreUsuario { set; get; } /* INDRA FJQP Nombre de Usuario en pantalla de inicio*/
    }
}
