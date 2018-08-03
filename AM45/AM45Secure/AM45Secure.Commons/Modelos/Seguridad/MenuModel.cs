using AM45Secure.Commons.Modelos.Comunes;

namespace AM45Secure.Commons.Modelos.Seguridad
{
    public class MenuModel : AbstractModel
    {
        public int PerfilId { set; get; }
        public int PersonaId { set; get; }
        public string ClaveMenu { set; get; }
        public string NombreUsuario { set; get; }  /* INDRA FJQP Nombre en Pantalla de Inicio */

        public string ManejaUDI { set; get; } /* INDRA FJQP Manejo de UDI 14/017/2018 */
    }
}