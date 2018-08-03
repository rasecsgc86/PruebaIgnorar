using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("ProductosFlex")]
    public class ProductosFlex : IEntity
    {
        public int IdProductoFlex { get; set; }
        [Column("ProductoID")]
        public int ProductoId { get; set; }
        public int IdTipoVehiculo { get; set; }
        public int IdCondicionVehiculo { get; set; }
        public int IdTipoServicioVehiculo { get; set; }
        public string NotasImportantes { get; set; }
        public string Submarca { get; set; }
        public string Servicio { get; set; }
        public bool ExisteSubmarca { get; set; }
    }
}