using System;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("RegistrosTicket")]
    public class RegistrosTicket : IEntity
    {
        [IdColumn(Identity = true)]
        public int TicketId { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int TipoId { get; set; }

        public int UsuarioId { get; set; }

        public string Descripcion { get; set; }

        public string IdCaratula { get; set; }

        public int IdCliente { get; set; }

        public int? IdOrigenTicket { get; set; }

        public DateTime FechaRecepcion { get; set; }

        public string NumeroOT { get; set; }

        public string NumeroOTSICS { get; set; }

        public int ResponsableId { get; set; }
        public int AseguradoraId { get; set; }
    }
}
