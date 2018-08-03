using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Comunes
{
    public class UsuarioPerfilModel
    {
        [Required (FieldName = "Usuario")]
        public int UsuarioId { set; get; }

        [Required (FieldName = "Perfil")]
        public int PerfilId { set; get; }
    }
}
