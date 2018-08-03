using System;

namespace NotificacionesTickets.Models
{
    public class InhabilesModel
    {
        public int IdDiaHabil { set; get; }
        public DateTime Dia { set; get; } 
        public int PersonaId { set; get; } 
        public string FechaRegistro { set; get; }
    }
}
