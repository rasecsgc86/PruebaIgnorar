using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Tickets
{
    [Table("vwTICConsultaResponsables")]
    public class VwTicConsultaResponsables :IEntity
    {
        public int IdResponsable { get; set; }
        public string Responsable { get; set; }
        public string Mail { get; set; }
    }
}
