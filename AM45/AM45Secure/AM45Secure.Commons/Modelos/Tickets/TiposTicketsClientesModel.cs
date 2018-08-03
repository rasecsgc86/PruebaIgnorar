using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Tickets
{
    public class TiposTicketsClientesModel
    {
        public int TipoId { get; set; }
        public int IdCliente { get; set; }
        [Required(FieldName = "Responsable Atención")]
        public int IdPersonaResponsable { get; set; }
        [Required(FieldName = "Nombre Escalamiento 1")]
        public int IdPersonaEscalamiento1 { get; set; }
        [Required(FieldName = "Nombre Escalamiento 2")]
        public int IdPersonaEscalamiento2 { get; set; }
        [Required(FieldName = "Horas Atención")]
        public int HorasAtencion { get; set; }
        [Required(FieldName = "Horas")]
        public int HorasSegundoEscalamiento { get; set; }
        public TiposTicketModel TiposTicket { get; set; }
    }
}
