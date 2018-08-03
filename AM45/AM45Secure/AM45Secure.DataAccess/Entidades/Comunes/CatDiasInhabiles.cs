using System;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("CatDiasHabiles")]
    public class CatDiasInhabiles : IEntity
    {
        [IdColumn(Identity = true)]
        public int IdDiaHabil { get; set; }
        public DateTime Dia { get; set; }
        public int PersonaID { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
