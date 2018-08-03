using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Tickets
{
    [Table("vwTICKETSRepSelTicketsReporteExcel")]
    public class VwTicketsRepSelTicketsReporteExcel : IEntity
    {
        public int TicketId { get; set; }
        [Column("PersonaID")]
        public int PersonaId { get; set; }
        public int TipoId { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public string FechaCierre { set; get; }
        public string Cliente { get; set; }
        public string Caratula { get; set; }
        public string TipoTicket { get; set; }
        public string DescripcionTicket { get; set; }
        public string NombrePer { get; set; }
        public string PaternoPer { get; set; }
        public string MaternoPer { get; set; }
        public int HorasAtencion { set; get; }
        public string DescripcionEstatus { get; set; }
        public decimal TiempoAtencion { get; set; }
        public string EnTiempo { get; set; }
        public int CveEstatus { get; set; }
        public int DiasInhabiles { set; get; }
        public int AseguradoraId { get; set; }
        public string Nombre { get; set; }
        [Column("NumeroOT")]
        public string NumeroOt { set; get; }
        [Column("NumeroOTSICS")]
        public string NumeroOtSics { set; get; }
        public string Usuario { set; get; }
        public string Alias { set; get; }
        public string Perfil { set; get; }
        public string Comentario { set; get; }
    }
}