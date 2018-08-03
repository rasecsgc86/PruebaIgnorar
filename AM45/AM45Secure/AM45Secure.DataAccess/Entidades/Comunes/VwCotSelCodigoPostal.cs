using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("vwCOTSelCodigoPostal")]
    public class VwCotSelCodigoPostal : IEntity
    {
        [Column("PaisID")]
        public int PaisId { get; set; }
        public string Pais { get; set; }
        [Column("EstadoID")]
        public int EstadoId { get; set; }
        public string Estado { get; set; }
        [Column("DelegacionID")]
        public int DelegacionId { get; set; }
        public string Delegacion { get; set; }
        public string Colonia { get; set; }
        public string CodigoPostal { get; set; }
        public int Asentamiento { get; set; }
        public int Mnpio { get; set; }
        public int ColoniaId { get; set; }
    }
}