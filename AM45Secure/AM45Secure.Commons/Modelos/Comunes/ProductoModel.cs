using Zero.Ado;
using Zero.Attributes;
namespace AM45Secure.Commons.Modelos.Comunes
{
    public class ProductoModel : IEntity
    {
        public int ProductoId { set; get; }
        public string NombreProducto { set; get; }
        public double Flexible { set; get; }
        public bool Cp { get; set; }
    }
}