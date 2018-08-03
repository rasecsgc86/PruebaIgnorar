using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Tickets
{
    public class ConfigurarParametrosTicketsModelo
    {

        public int TipoId { get; set; }
        
        public string Descripcion { get; set; }
        public int IdPersonaResponsable { get; set; }
        public string PersonaResponsable { get; set; }
        public int IdPersonaEscalamiento1 { get; set; }
        public string PersonaEscalamiento1 { get; set; }
        public int IdPersonaEscalamiento2 { get; set; }
        public string PersonaEscalamiento2 { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        [Required(FieldName = "Cliente")]
        public int IdCliente { get; set; }
        public int HorasAtencion { get; set; }
        public int HorasSegundoEscalamiento { get; set; }
        public string Mail { get; set; }
        public string MailEscalamiento1 { get; set; }
        public string MailEscalamiento2 { get; set; }
    }
}
