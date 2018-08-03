using AM45Secure.Commons.Modelos.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Configurador
{
    public class CoberModel : AbstractModel
    {
        [Required(FieldName = "IdCobertura", Optional = true)]
        public int IdCobertura { set; get; }
        public int IdProducto { set; get; }
        public int IdTipoVehiculo { set; get; }
        public int IdTipoServicioVehiculo { set; get; }
        public int RangoInicial { set; get; }
        public int RangoFinal { set; get; }
        public int Saltos { set; get; }
        public string Homologacion { set; get; }
        public string Tooltip { set; get; }
        public IList<string> ListaDeducibles { set; get; }
        public int EliminarTodosDeducibles { set; get; }
        public int AgregarOeliminarElemento { set; get; }



    }
}
