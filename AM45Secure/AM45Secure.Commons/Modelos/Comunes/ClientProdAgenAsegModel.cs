using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Comunes
{
    public class ClientProdAgenAsegModel:AbstractModel
    {
        [Required(FieldName = "ClienteId")]
        public int ClienteId { set; get; }
    }
}