using AM45Secure.Commons.Utils;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Emitir
{
    public class VehiculoGrabModel
    {
        [DataColumn]
        [Required(FieldName = "Motor")]
        public string Motor { set; get; }

        [DataColumn]
        [Required(FieldName = "Serie")]
        public string Serie { set; get; }

        [DataColumn]
        public string Placas { set; get; }

        [DataColumn]
        public string BeneficiarioPreferente { set; get; }

        [DataColumn]
        public string RFCBeneficiario { set; get; }

        [DataColumn]
        public string AseguradoAdicional { set; get; }

        [DataColumn]
        public string Conductor { set; get; }

        [DataColumn]
        public string ContratoLoJack { set; get; }
        [DataColumn]
        public int IndiceSerie { set; get; }
    }
}

