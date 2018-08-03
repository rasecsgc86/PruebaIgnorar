using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificacionesTickets.Models
{
    public class TiempoDeAtencionModel
    {
        public int Dias { get; set; }
        public int Horas { get; set; }
        public bool EnTiempo { get; set; }
    }
}