using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("vwCOTSelEstados")]
    public class VwCotSelEstados : IEntity
    {
        [Column("ReglaID")]
        public decimal ReglaId { set; get; }
        public decimal Numero { set; get; }
        public string Valor { set; get; }
        public string Descripcion { set; get; }
        public string ProductoId { set; get; }
        public string Producto { set; get; }
        public string ClienteId { set; get; }
        public string Cliente { set; get; }
        public string PaternoCliente { set; get; }
        public string MaternoCliente { set; get; }
        public decimal EstadoId { set; get; }
        public string Estado { set; get; }
        public string Pais { set; get; }
        public string Delegacion { set; get; }
        public string Colonia { set; get; }
    }
}