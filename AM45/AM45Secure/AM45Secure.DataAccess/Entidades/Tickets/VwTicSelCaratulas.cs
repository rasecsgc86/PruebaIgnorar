using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Tickets
{
    [Table("vwTICSelCaratulas")]
    public class VwTicSelCaratulas : IEntity
    {
        public int IdCliente { set; get; }
        public string PolizaCaratula { set; get; }
        public string FormaPago { set; get; }
        public string TipoVehiculo { set; get; }
        public string TipoCobranza { set; get; }
    }
}