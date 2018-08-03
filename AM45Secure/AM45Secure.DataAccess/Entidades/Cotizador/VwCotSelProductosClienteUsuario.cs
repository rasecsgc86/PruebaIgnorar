using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Cotizador
{
    [Table("vwCOTSelProductosClienteUsuario")]
    public class VwCotSelProductosClienteUsuario : IEntity
    {
        public string PerfilId { set; get; }
        public string Perfil { set; get; }
        public string UsuarioId { set; get; }
        public string Usuario { set; get; }
        public string Alias { set; get; }
        public string ClienteId { set; get; }
        public string Cliente { set; get; }
        [Column("ProductoID")]
        public string ProductoId { set; get; }
        public string Producto { set; get; }
        public float ProductoFlex { set; get; }
        public bool Cp { get; set; }

        public float Flexible { set; get; }
    }
}