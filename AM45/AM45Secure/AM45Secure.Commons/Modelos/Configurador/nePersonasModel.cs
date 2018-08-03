using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Configurador
{
  
    public class nePersonasModel : IEntity
    {
        public int PersonaID { get; set; }
        public string Nombre { get; set; }
    }
}
