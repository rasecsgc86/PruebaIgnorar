using System;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("PerfilesFlexible")]
    public class PerfilesFlexible : IEntity
    {
        public int PerfilFlexibleId { set; get;}
        public int PerfilId { set; get;}
        public int PersonaId { set; get;}
        public DateTime Fecha { set; get;}
        public string Comentario { set; get; }
    }
}
