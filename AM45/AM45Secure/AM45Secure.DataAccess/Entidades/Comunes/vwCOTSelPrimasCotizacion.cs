using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("vwCOTSelPrimasCotizacion")]
    public class VwCotSelPrimasCotizacion : IEntity
    {
        public int SolicitudId { set; get; }
        public int ServicioId { set; get; }
        public int ClienteId { set; get; }
        public int ProductoId { set; get; }
        public int Numero { set; get; }

        [Column("PaqueteID")]
        public int PaqueteId { set; get; }
        public int AseguradoraId { set; get; }
        public int FormaPago { set; get; }
        public int UsuarioId { set; get; }
        public int CotizacionId { set; get; }
        public string Cliente { set; get; }
    }
}
