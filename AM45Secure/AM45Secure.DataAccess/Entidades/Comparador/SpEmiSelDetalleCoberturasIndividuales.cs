using System.Data;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comparador
{
    [Table("spCOTSelDetalleCob")]
    public class SpEmiSelDetalleCoberturasIndividuales : IEntity
    {
        [Column("pCotDetalleId", SqlDataType = SqlDbType.VarChar)]
        public string PCotizacionId { set; get; }
        [Column("pNumDetalle", SqlDataType = SqlDbType.VarChar)]
        public string PNumero { set; get; }
        [Column("CotizacionID", true)]
        public int CotizacionId { set; get; }
        [Column("CoberturaID", true)]
        public int CoberturaId { set; get; }
        [Column("Cobertura", true)]
        public string Cobertura { set; get; }
        [Column("SumaAsegurada", true)]
        public string SumaAsegurada { set; get; }
        [Column("Deducible", true)]
        public string Deducible { set; get; }
        [Column("PrimaNeta", true)]
        public string PrimaNeta { set; get; }
        [Column("ElementoID", true)]
        public int ElementoId { set; get; }
        [Column("Numero", true)]
        public int Numero { set; get; }
        [Column("QLT", true)]
        public int Qlt { set; get; }
        [Column("Detalle", true)]
        public string Detalle { set; get; }
        [Column("isfija", true)]
        public int isfija { get; set; }
    }
}
