using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Tickets
{
    [Table("vwTICSelEstatusTicket")]
    public class VwTicSelEstatusTicket : IEntity
    {
        public int TicketId { get; set; }
        public string Estatus { get; set; }
        public bool Cerrado { get; set; }
    }
}