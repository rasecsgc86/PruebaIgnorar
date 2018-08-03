using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM45Secure.Commons.Modelos.Comunes
{
    public class CalendarioModel
    {
        public int IdDiaHabil { get; set; }
        public DateTime Dia { get; set; }
        public int PersonaId { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string FechaDia { get; set; }
    }
}
