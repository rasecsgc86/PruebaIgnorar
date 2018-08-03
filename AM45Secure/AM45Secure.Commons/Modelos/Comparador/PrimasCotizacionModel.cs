using System;

namespace AM45Secure.Commons.Modelos.Comparador
{
    public class PrimasCotizacionModel
    {
        public int CotizacionId { get; set; }
        public int Numero { get; set; }
        public int AseguradoraId { get; set; }
        public int ProductoId { get; set; }
        public int PaqueteId { get; set; }
        public string Tarifa { get; set; }
        public decimal PrimaNeta { get; set; }
        public decimal Recargo { get; set; }
        public decimal Derechos { get; set; }
        public decimal Iva { get; set; }
        public decimal PrimaTotal { get; set; }
        public decimal Comision { get; set; }
        public decimal PorcentajeRecargo { get; set; }
        public decimal PorcentajeIva { get; set; }
        public decimal PorcentajeComision { get; set; }
        public decimal Descuento { get; set; }
        public string FolioExterno { get; set; }
        public string Poliza { get; set; }
        public int Inciso { get; set; }
        public string Endoso { get; set; }
        public decimal PrimaAnualizada { get; set; }
        public int ComplementoTarifa { get; set; }
        public string ClaveCia { get; set; }
        public int Udi { get; set; }
        public decimal PrimaNetaMarkup { get; set; }
        public decimal PrimaTotalMarkup { get; set; }
        public int IvaMarkup { get; set; }
        public int PorcentajeMarkup { get; set; }
        public decimal PorcentajeDescuento { get; set; }
        public decimal Bonificacion { get; set; }
        public decimal PorcentajeBonificacion { get; set; }
        public int RecargoFraccionado { get; set; }
        public int TipoValorId { get; set; }
        public int PagoDeducible { get; set; }
        public decimal CesionComision { get; set; }
        public decimal PrimaDevolucion { get; set; }
        public decimal PrimaNetaDevolucion { get; set; }
        public DateTime InicioVigencia { get; set; }
        public DateTime FinVigencia { get; set; }
        public string Negocio { get; set; }
        public string IdTarifaBase { get; set; }
        public string IdTarifaMultianual { get; set; }
        public string IdTarifaRegional { get; set; }
        public int PrimaBase { get; set; }
        public int Cuota { get; set; }
        public int Constante { get; set; }
        public int PrimaNetaDesfase { get; set; }
        public int PrimaNetaAnual { get; set; }
        public string UsoCia { get; set; }
        public int Plazo { get; set; }
    }
}