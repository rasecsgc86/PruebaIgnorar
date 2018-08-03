using AM45Secure.Commons.Modelos.Comunes;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Tickets
{
    public class TiposTicketModel : AbstractModel
    {
        public int TipoId { get; set; }

        [Required(FieldName = "Nombre del ticket")]
        public string Descripcion { get; set; }

        public int TiempoAtencion { get; set; }


    }
}