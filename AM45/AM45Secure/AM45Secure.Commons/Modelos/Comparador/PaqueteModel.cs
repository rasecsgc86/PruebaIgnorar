using System.Collections.Generic;

namespace AM45Secure.Commons.Modelos.Comparador
{
    public class PaqueteModel
    {
        public int AseguradoraId { get; set; }
        public int Numero { get; set; }
        public int ElementoId { get; set; }
        public string Nombre { get; set; }
        public decimal Monto { get; set; }
        public int CotizacionId { get; set; }
        public bool Flexible { get; set; }
        public IList<CoberturacModel> ListaCoberturasModel { get; set; }
    }
}
