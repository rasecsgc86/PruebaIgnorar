using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Comunes
{
    public class PlazosModel : AbstractModel
    {
        [Required(FieldName = "FechaIniVigencia")]
        public string FechaIniVigencia { set; get; }

        [Required(FieldName = "Plazos")]
        public int Plazos { set; get; }
    }
}