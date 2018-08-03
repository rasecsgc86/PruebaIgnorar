using System;
using System.Collections.Generic;

namespace NotificacionesTickets.Models
{
    public class TicketModel
    {
        public int TicketId { get; set; }                           
        public int PersonaId { get; set; }
        public string Tipo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public string DescripcionTicket { get; set; }
        public int IdEstatusTicket { get; set; }
        public int ClaveEstatus { get; set; }
        public string DescripcionEstatus { get; set; }
        public int NumTicket { get; set; }
        public int ResponsableId { get; set; }
        public string NombreCompletoResponsable { get; set; }
        public string MailResponsable { get; set; }
        public int? IdEscalamientoTicket { set; get; }
        public string FechaEscalamiento { set; get; }
        public int? TipoEscalamiento { set; get; }
        public int HorasAtencion { set; get; }
        public int HorasSegundoEscalamiento { set; get; }
        public int? IdPersonaEscalamiento1 { set; get; }
        public int? IdPersonaEscalamiento2 { set; get; }
        public string NombreEscalamiento1 { set; get; }
        public string MailEscalamiento1 { set; get; }
        public string NombreEscalamiento2 { set; get; }
        public string MailEscalamiento2 { set; get; }
        public int? UltimoEscalamiento { set; get; }
        public int TiempoAtencion { set; get; } 
        public int CantidadFinesSemana { set; get; }
        public string MailUsuario { set; get; }
        public string MailDatosContacto { set; get; }
    }
}
