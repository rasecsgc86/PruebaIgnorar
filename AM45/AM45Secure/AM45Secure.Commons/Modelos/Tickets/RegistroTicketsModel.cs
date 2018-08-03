using AM45Secure.Commons.Modelos.Comunes;

namespace AM45Secure.Commons.Modelos.Tickets
{
    public class RegistroTicketsModel : AbstractModel
    {
        public int TicketId { get; set; }
        public string NumeroOt { get; set; }
        public string NumeroOtsics { get; set; }
        public int ResponsableId { get; set; }
    }
}
