using Zero.Ado;

namespace AM45Secure.DataAccess.Entidades.Tickets
{
    public class VwAsegModel : IEntity
    {
        public int AseguradoraId { get; set; }
        public string Nombre { get; set; }
    }
}