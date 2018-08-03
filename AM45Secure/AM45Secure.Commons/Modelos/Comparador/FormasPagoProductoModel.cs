namespace AM45Secure.Commons.Modelos.Comparador
{
    public class FormasPagoProductoModel
    {
        public int FormaPagoId { get; set; }
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public bool Predeterminado { get; set; }
    }
}
