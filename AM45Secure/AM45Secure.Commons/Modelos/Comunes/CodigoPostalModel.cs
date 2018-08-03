using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Comunes
{
    public class CodigoPostalModel
    {
        [Required(FieldName = "CodigoPostal", Optional = true)]
        public string CodigoPostal { set; get; }

        [Required(FieldName = "Colonia", Optional = true)]
        public string Colonia { set; get; }
    }
}