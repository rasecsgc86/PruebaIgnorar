using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Comunes
{
    public class ClientesModel
    {
        [Required(FieldName = "Cliente", Optional = true)]
        public int ClienteId { set; get; }
        public string Cliente { set; get; }
        public float ProductoFlex { set; get; }
    }
}