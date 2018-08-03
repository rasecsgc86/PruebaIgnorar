using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("nePrimasCotizacion")]
    public class NePrimasCotizacion : IEntity
    {
        [Column("CotizacionID")]
        public int CotizacionId { set; get; }
        public int Numero { set; get; }
        [Column("AseguradoraID")]
        public int AseguradoraId { set; get; }
        [Column("ProductoID")]
        public int ProductoId { set; get; }
        [Column("PaqueteID")]
        public int PaqueteId { set; get; }
        public decimal PrimaTotal { set; get; }
    }
}
