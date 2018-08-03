using System;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comparador
{
    [Table("neCoberturasCotizacion")]
    public class NeCoberturasCotizacion : IEntity
    {
        [Column("CotizacionID")]
        public int CotizacionId { get; set; }
        public int Numero { get; set; }
        [Column("CoberturaID")]
        public int CoberturaId { get; set; }    
        public string SumaAsegurada { get; set; }
        public decimal PrimaNeta { get; set; }
        public string Deducible { get; set; }
        public string SumaAseguradaInicial { get; set; }
    }
}
