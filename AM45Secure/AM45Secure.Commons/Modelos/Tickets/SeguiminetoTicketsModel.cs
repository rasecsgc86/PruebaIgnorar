using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM45Secure.Commons.Modelos.Comunes;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Tickets
{
    public class SeguiminetoTicketsModel : AbstractModel

    {
        public int TicketId { get; set; }
        public int PersonaId { get; set; }
        public int TipoId { get; set; }
        public string FechaRegistro { get; set; }
        public string FechaRecepcion { get; set; }
        public string DescripcionTicket { get; set; }
        public string NombrePer { get; set; }
        public string PaternoPer { get; set; }
        public string MaternoPer { get; set; }
        public string DescripcionEstatus { get; set; }
        public int NumTicket { get; set; }
        public string TiempoAtencion { get; set; }
        public bool EstatusAtencion { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public int CveEstatus { get; set; }
        public int IdEstatusTicket { get; set; }
        public int IdTicketEstatus { get; set; }
        public DateTime FechaRegistroDate { get; set; }
        public bool SiFlotilla { get; set; }
        public int IdCliente { get; set; }
        public string TipoUsuario { get; set; }
        public int PersonaIdTipoUsuarioTicket { get; set; }
        public int UsuarioSesion { get; set; }
        public string NumeroOt { get; set; }
        public string NumeroOtsics { get; set; }
        public bool IsCarga { get; set; }
        public string Duenio { get; set; }
        public string TipoTicket { get; set; }
        public int AseguradoraId { get; set; }
        public string Nombre { get; set; }
        public string NombreCliente { get; set; }
        public string Caratula { get; set; }
    }
}