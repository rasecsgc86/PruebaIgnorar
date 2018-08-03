using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Tickets
{
    [Table("vwTICConsultarDatosCorreo")]
    public class VwTicConsultarDatosCorreo : IEntity
    {
        public int TicketId { get; set; }
        public int TipoId { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionTicket { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public int IdPersonaResponsable { get; set; }
        public string PersonaResponsable { get; set; }
        public int IdPersonaResponsableInicial { get; set; }
        public string PersonaResponsableInicial { get; set; }
        public int IdPersonaEscalamiento1 { get; set; }
        public string PersonaEscalamiento1 { get; set; }
        public int IdPersonaEscalamiento2 { get; set; }
        public string PersonaEscalamiento2 { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public int IdCliente { get; set; }
        public int HorasAtencion { get; set; }
        public int HorasSegundoEscalamiento { get; set; }
        public string MailLevanto { get; set; }
        public string MailResponsable { get; set; }
        public string MailResponsableInicial { get; set; }
        public string UsuarioLevanto { get; set; }
        public string QuienCerroTicket { get; set; }
        public string MailQuienCerroTicket { get; set; }
        public string NombreReporta { get; set; }
        public string MailReporta { get; set; }
    }
}
