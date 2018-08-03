using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Cotizador
{
    [Table("vwCOTSelClientesUsuario")]
    public class VwCotSelClientesUsuario : IEntity
    {
        public int PerfilId { set; get; }
        public string Perfil { set; get; }
        public int UsuarioId { set; get; }
        public string Usuario { set; get; }
        public string Alias { set; get; }
        public int OpcionId { set; get; }
        public string Tipo { set; get; }
        public string Id { set; get; }
        public string Nombre { set; get; }
    }
}