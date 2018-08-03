using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificacionesTickets.Models
{
    public class CatDiaInhabilesModel
    {
        public int IdDiaHabil { get; set; }
        public DateTime Dia { get; set; }
        public int PersonaId { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}