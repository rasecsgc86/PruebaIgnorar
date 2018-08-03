using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("CatEstatusTickets")]
    public class CatEstatusTickets : IEntity
    {
      [IdColumn(identity:true)]
      public int IdEstatusTicket { get; set; }
      public int CveEstatus { get; set; }
      public string Estatus { get; set; }
      public string Descripcion { get; set; }
    }
}
