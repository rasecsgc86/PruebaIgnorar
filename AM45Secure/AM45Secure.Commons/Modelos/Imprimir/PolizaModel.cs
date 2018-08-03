using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Imprimir
{
    public class PolizaModel
    {
        [Required(FieldName = "Número de Póliza")]
        public string Poliza { get; set; }
    }
}