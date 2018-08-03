using System;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Tickets
{
    [Table("vwREGListaTicket")]
    public class VwRegListaTicket : IEntity
    {
        public int TicketId { get; set; }
        [Column("PersonaID")]
        public int PersonaId { get; set; }
        public string Tipo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public string DescripcionTicket { get; set; }
        public string NombreCompletoResponsable { get; set; }
        public string DescripcionEstatus { get; set; }
        public int ClaveEstatus { get; set; }
        public int UsuarioId { get; set; }
    }
}
