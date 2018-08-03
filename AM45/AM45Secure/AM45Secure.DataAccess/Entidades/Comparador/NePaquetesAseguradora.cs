using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comparador
{
    [Table("nePaquetesAseguradora")]
    public class NePaquetesAseguradora : IEntity
    {
        [Column("AseguradoraID")]
        public int AseguradoraId { get; set; }
        [Column("PaqueteID")]
        public int PaqueteId { get; set; }
    }
}