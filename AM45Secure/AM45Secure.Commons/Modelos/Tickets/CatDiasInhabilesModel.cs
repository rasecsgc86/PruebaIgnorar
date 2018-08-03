using System;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Comunes
{
    public class CatDiasInhabilesModel
    {
        public int IdDiaHabil { get; set; }
        public DateTime Dia { get; set; }
        public int PersonaID { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}