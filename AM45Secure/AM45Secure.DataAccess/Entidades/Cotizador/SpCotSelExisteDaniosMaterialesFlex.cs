using System.Data;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Cotizador
{
    [Table("spCOTSelExisteDaniosMaterialesFlex")]
    public class SpCotSelExisteDaniosMaterialesFlex : IEntity
    {
        [Column("solicitduId", SqlDataType = SqlDbType.Int)]
        public string SolicitduId { set; get; }
        [Column("ExisteDM", true)]
        public bool ExisteDm { set; get; }
    }
}
