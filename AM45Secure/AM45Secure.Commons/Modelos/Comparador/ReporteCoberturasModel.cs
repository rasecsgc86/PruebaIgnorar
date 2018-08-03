using System;

namespace AM45Secure.Commons.Modelos.Comparador
{
    public class ReporteCoberturasModel
    {
        public int CotizacionId { get; set; }
        public int PaqueteId { get; set; }
        public string PaqueteN { get; set; }
        public string Nombre { get; set; }
        public string Homologacion { get; set; }
        public decimal PrimaNeta { get; set; }
        public decimal Recargo { get; set; }
        public decimal Derechos { get; set; }
        public decimal Iva { get; set; }
        public decimal PrimaTotal { get; set; }
        public string SumaAsegurada { get; set; }
        public string Deducible { get; set; }
        public int RecargoFraccionado { get; set; }
        public DateTime InicioVigencia { get; set; }
        public DateTime FinVigencia { get; set; }
        public string SumaAseguradaC { get; set; }
        public decimal PrimaNetaC { get; set; }
        public string DeducibleC { get; set; }
        public string SumaAseguradaInicialC { get; set; }
    }
}