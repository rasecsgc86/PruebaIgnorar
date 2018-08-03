using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("TiposTicketsClientes")]
    public class TiposTicketsClientes : IEntity
    {
        [IdColumn(Identity = false)]
        public int TipoId { get; set; }
        public int IdCliente { get; set; }
        public int? IdPersonaResponsable { get; set; }
        public int IdPersonaEscalamiento1 { get; set; }
        public int IdPersonaEscalamiento2 { get; set; }
        public int HorasAtencion { get; set; }
        public int HorasSegundoEscalamiento { get; set; }
    }
}
