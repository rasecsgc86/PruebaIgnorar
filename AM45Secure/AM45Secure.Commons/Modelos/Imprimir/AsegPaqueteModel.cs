namespace AM45Secure.Commons.Modelos.Emitir
{
    public class AsegPaqueteModel
    {
        public string Aseguradora { get; set; }
        public string Paquete { get; set; }
        public int Numero { get; set; }
        public int AseguradoraId { get; set; }
        public int PaqueteId { get; set; }
        public int SolicitudId { get; set; }
    }
}
