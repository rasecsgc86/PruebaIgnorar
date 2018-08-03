using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comparador
{
    [Table ("vwCotSelFormasPagoProducto")]
    public class VwCotSelFormasPagoProducto : IEntity
    {
        [Column("FormaPagoID")]
        public int FormaPagoId { get; set; }
        [Column("ProductoID")]
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public bool Predeterminado { get; set; }
    }
}
