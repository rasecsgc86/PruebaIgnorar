using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Comunes
{
    public class LimiteValorFacturaModel
    {
        [Required(FieldName = "Limite Valor Factura", Optional = false)]
        public decimal LimiteValorFactura { get; set; }

        [Required(FieldName = "Valor Factura", Optional = false)]
        public string ValorFactura { get; set; }

        public bool IsOk { get; set; }
    }
}