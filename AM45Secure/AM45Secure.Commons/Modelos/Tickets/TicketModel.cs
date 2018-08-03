
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM45Secure.Commons.Modelos.Comunes;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Tickets
{
    public class TicketModel: AbstractModel
    {
        public int TicketId { get; set; }
        [Required(FieldName = "Cliente")]
        public int PersonaId { get; set; }
        [Required(FieldName = "Tipo Ticket")]
        public int TipoId { get; set; }
        public string Tipo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaRecepcion { get; set; }
        [Required(FieldName = "Descripcion Ticket")]
        public string DescripcionTicket { get; set; }
        [Required(FieldName = "Estatus Ticket")]
        public int IdEstatusTicket { get; set; }
        public int ClaveEstatus { get; set; }
        public string DescripcionEstatus { get; set; }
        public int NumTicket { get; set; }
        public string CaratulaId { get; set; }
        public int ResponsableId { get; set; }
        public string NombreCompletoResponsable { get; set; }
        public string MailResponsable { get; set; }
        public string CopiarA { get; set; }
        public int? CatalogoOrigenId { get; set; }
        public int DatosContactoAgenciaId { get; set; }
        public string DatosContactoNombre { get; set; }
        public string DatosContactoApellidos { get; set; }
        public string DatosContactoTelefonos { get; set; }
        public string DatosContactoEmail { get; set; }
        public bool EsClienteFlotillas { get; set; }
        [Required(FieldName = "Archivos")]
        public List<ArchivoTicketsModel> Archivos { get; set; }
        public int IdTicketEstatus { get; set; }
        public int UsuarioId { get; set; }
        public int IdCliente { get; set; }
        public int UsuarioSesion { get; set; }
        public int AseguradoraId { get; set; }
        public string Nombre { get; set; }
        public int PersonaIdTipoUsuarioTicket { get; set; }
        public string NombreCliente { get; set; }
        public string Caratula { get; set; }

    }
}
