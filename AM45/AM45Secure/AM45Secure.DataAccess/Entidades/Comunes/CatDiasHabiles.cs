using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("CatDiasHabiles")]
    public class CatDiasHabiles : IEntity
    {
        [IdColumn (Identity = true)]
        public int IdDiaHabil { get; set; }
        public DateTime Dia { get; set; }
        [Column("PersonaID")]
        public int PersonaId { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
