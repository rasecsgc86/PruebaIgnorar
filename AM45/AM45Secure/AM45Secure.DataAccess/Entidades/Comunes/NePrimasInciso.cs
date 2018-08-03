using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("nePrimasInciso")]
    public class NePrimasInciso : IEntity
    {
        public string Poliza { set; get; }
        public int Inciso { set; get; }
        public string Endoso { set; get; }
        public float Numero { set; get; }
        public decimal PrimaNeta { set; get; }
        public decimal PorcentajeDescuento { set; get; }
        public decimal Descuento { set; get; }
        public decimal PorcentajeRecargo { set; get; }
        public decimal Recargo { set; get; }
        public decimal Derechos { set; get; }
        public decimal PorcentajeIva { set; get; }
        public decimal IVA { set; get; }
        public decimal PrimaTotal { set; get; }
        public decimal PorcentajeComision { set; get; }
        public decimal Comision { set; get; }
        public decimal PorcentajeUdi { set; get; }
        public decimal UDI { set; get; }
        public int TableRecargosAxa { set; get; }

        [Column("primaNetaMarkup")]
        public decimal PrimaNetaMarkup { set; get; }

        [Column("primaTotalMarkup")]
        public decimal PrimaTotalMarkup { set; get; }

        [Column("ivaMarkup")]
        public decimal IvaMarkup { set; get; }

        [Column("porcentajeMarkup")]
        public decimal PorcentajeMarkup { set; get; }
        public decimal Bonificacion { set; get; }
        public decimal PorcentajeBonificacion { set; get; }
        public string IdTarifaMultianual { set; get; }
        public string IdTarifaRegional { set; get; }
        public decimal PrimaBase { set; get; }
        public decimal Cuota { set; get; }
        public decimal Constante { set; get; }
        public decimal PrimaNetaDesfase { set; get; }
        public decimal PrimaNetaAnual { set; get; }
        public string UsoCia { set; get; }
        public decimal RecargoFraccionado { set; get; }
    }
}