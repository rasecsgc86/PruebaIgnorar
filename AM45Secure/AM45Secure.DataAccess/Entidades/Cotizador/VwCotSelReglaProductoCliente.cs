using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Cotizador
{
    [Table("vwCOTSelReglaProductoCliente")]
    public class VwCotSelReglaProductoCliente : IEntity
    {
        [Column("ReglaID")]
        public decimal ReglaId { set; get; }
        public decimal Numero { set; get; }
        public string Valor { set; get; }
        public string Descripcion { set; get; }
        public string Producto { set; get; }
        public string Cliente { set; get; }
    }
}
