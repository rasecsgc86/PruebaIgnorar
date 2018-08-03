using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("neCoberturasInciso")]
    public class NeCoberturasInciso : IEntity
    {
        public string Poliza { set; get; }
        public int Inciso { set; get; }
        public string Endoso { set; get; }

        [Column("CoberturaID")]
        public float CoberturaId { set; get; }
        public string SumaAsegurada { set; get; }
        public decimal PrimaNeta { set; get; }
        public decimal Deducible { set; get; }
        public decimal RecargoDescuento { set; get; }
    }
}
