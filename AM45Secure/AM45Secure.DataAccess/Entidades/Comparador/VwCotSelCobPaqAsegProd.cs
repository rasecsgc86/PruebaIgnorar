using System;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comparador
{
    [Table("vwCOTSelCobPaqAsegProd")]
    public class VwCotSelCobPaqAsegProd : IEntity
    {
        public int CotizacionId { get; set; }
        public int ProductoId { get; set; }
        public int AseguradoraId { get; set; }
        public int PaqueteId { get; set; }
        public string Paquete { get; set; }
        public int CoberturaId { get; set; }
        public int TipoId { get; set; }
        public string Nombre { get; set; }
        public string Homologacion { get; set; }
        public string Tarifa { get; set; }
        public decimal PrimaNeta { get; set; }
        public decimal Recargo { get; set; }
        public decimal Derechos { get; set; }
        [Column("IVA")]
        public decimal Iva { get; set; }
        public decimal PrimaTotal { get; set; }
        public decimal Comision { get; set; }
        public string SumaAsegurada { get; set; }
        public string Deducible { get; set; }
        public string SumaAseguradaInicial { get; set; }
        public int Numero { get; set; }
        public int Udi { get; set; }
        public int RecargoFraccionado { get; set; }
        public DateTime InicioVigencia { get; set; }
        public DateTime FinVigencia { get; set; }
        public string SumaAseguradaC { get; set; }
        public decimal PrimaNetaC { get; set; }
        public string DeducibleC { get; set; }
        public string SumaAseguradaInicialC { get; set; }
        public decimal Descuento { get; set; }
        public int IdTipoVehiculo { get; set; }
        public int IdAntiguedad { get; set; }
        public int IdServicio { get; set; }
        public string Detalle { get; set; }
        public string Tooltip { get; set; }

    }
}
