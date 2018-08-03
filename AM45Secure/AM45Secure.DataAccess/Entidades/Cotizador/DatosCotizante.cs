using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Cotizador
{
    [Table("DatosCotizante")]
    public class DatosCotizante : IEntity
    {
        [Column("IDSolicitud")]
        public int IdSolicitud { get; set; }
        public string RazonSocial { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string TipoPersona { get; set;}
    }
}
