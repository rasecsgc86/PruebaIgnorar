using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("vwCOTSelClientProdAgenAseg")]
    public class VwCotSelClientProdAgenAseg : IEntity
    {
        public int OpcionA { set; get; }
        public int OpcionB { set; get; }
        public string NombreOpcionA { set; get; }
        public string NombreOpcionB { set; get; }
        [Column("PersonaIDOpcionA")]
        public int PersonaIdOpcionA { set; get; }
        public string NombreValorA { set; get; }
        [Column("ValorIDB")]
        public int ValorIdB { set; get; }
        public string NombreValorB { set; get; }
    }
}