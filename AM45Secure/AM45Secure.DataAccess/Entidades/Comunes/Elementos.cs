using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("Elementos")]
    public class Elementos : IEntity
    {
        [Column ("CatalogoID")]
      public int CatalogoId { get; set; }

        [Column ("ElementoID")]
      public int ElementoId { get; set; }

        public string Nombre { get; set; }

        public int IdInterno { get; set; }

        public int IdPadre { get; set; }

        public string Comodin { get; set; }
    }
}
