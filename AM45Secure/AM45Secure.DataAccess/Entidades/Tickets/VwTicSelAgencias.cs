using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Tickets
{
    [Table("vwTICSelAgencias")]
    public class VwTicSelAgencias : IEntity
    {
        public int IdCliente { get; set; }

        public int IdAgencia { get; set; }

        public string Agencia { get; set; }

        public int PerfilId { get; set; }
    }
}