using System;
using AM45Secure.Commons.Utils;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Emitir
{
    public class AgenciaModel
    {
        [DataColumn]
        public string Color { set; get; }

        [DataColumn]
        public string ClaveVendedor { set; get; }

        [DataColumn]
        [Required(FieldName = "Correo Electrónico", Optional = true)]
        public string Vendedor { set; get; }

        [DataColumn]
        public bool ClienteVip { set; get; }

        [DataColumn]
        [Required(FieldName = "No. de Contrato")]
        public string NumContrato { set; get; }

        [DataColumn]
        [Required(FieldName = "Inicio de Contrato")]
        public DateTime InicioContrato { set; get; }

        [DataColumn]
        public string CorreoAgencia { set; get; }
    }
}