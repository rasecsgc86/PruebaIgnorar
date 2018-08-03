using AM45Secure.Commons.Modelos.Comunes;

// INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos

namespace AM45Secure.Commons.Modelos.Emitir
{
    public class ConfigRequestModel : AbstractModel
    {
        public int Aseguradora { get; set; }

        public int Producto { get; set; }

        public int Perfil { get; set; }

        public int Usuario { get; set; }

    }
}


