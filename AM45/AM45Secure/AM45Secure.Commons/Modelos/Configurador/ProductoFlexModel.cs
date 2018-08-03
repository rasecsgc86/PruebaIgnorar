using AM45Secure.Commons.Modelos.Comunes;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Configurador
{
    public class ProductoFlexModel : AbstractModel
    {
        
        [Required (FieldName="ProductoID", Optional = true)]
        public int ProductoID { set; get; }
        public int IdTipoVehiculo { set; get; }
        public int IdCondicionVehiculo { set; get; }
        public int IdTipoServicioVehiculo { set; get; }
        public string NotasImportantes { set; get; }
        public string Submarca { set; get; }

        public int AseguradoraId { set; get; }

    }
}
