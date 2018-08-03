using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Tickets
{
    [Table("TiposTicket")]
    public class TiposTicket : IEntity
    {
        [IdColumn(Identity = true)]
        public int TipoId { get; set; }

        public string Descripcion { get; set; }

        public int TiempoAtencion { get; set; }

        public bool Activa { get; set; }
    }
}