using Zero.Ado;

namespace AM45Secure.DataAccess.Entidades.Emitir
{
    public class AseguradoraPaquete : IEntity
    { 
        public string Aseguradora { get; set; }
        public string Paquete { get; set; }
        public int Numero { get; set; }
        public int AseguradoraId { get; set; }
        public int PaqueteId { get; set; }
        public int SolicitudId { get; set; }
    }
}
