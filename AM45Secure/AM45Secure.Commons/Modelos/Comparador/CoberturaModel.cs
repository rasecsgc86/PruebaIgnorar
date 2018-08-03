namespace AM45Secure.Commons.Modelos.Comparador
{
    public class CoberturacModel
    {
        public int CotizacionId { get; set; }
        public int AseguradoraId { get; set; }
        public int PaqueteId { get; set; }
        public int CoberturaId { get; set; }
        public int TipoId { get; set; }
        public string Nombre { get; set; }
        public string SumaAsegurada { get; set; }
        public decimal PrimaNeta { get; set; }
        public string Deducible { get; set; }
        public string SumaAseguradaInicial { get; set; }
        public string Homologacion { get; set; }
        public string Detalle { get; set; }
        public int isfija { get; set; } /* INDRA FJQP Notas Importantes, Tooltips, Directivas*/
    }
}