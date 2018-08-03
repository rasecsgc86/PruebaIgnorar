using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Tickets
{
    [Table("CatDiasHabiles")]
    public class CatDiasHabilesEntidad : IEntity
    {
        public int TotalInhabiles { set; get; }
    }
}
