using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Comunes
{
    public class ProductoClienteModel
    {
        [Required(FieldName = "Cliente")]
        public string Cliente { set; get; }

        [Required(FieldName = "Producto")]
        public string Producto { set; get; }
    }
}