using System;
using AM45Secure.Commons.Utils;

namespace AM45Secure.Commons.Modelos.Emitir
{
    public class DatosEmitirModel
    {
        [DataColumn]
        public int SolicitudInt { set; get; }

        [DataColumn]
        public int ServicioId { set; get; }

        [DataColumn]
        public int ClienteId { set; get; }

        [DataColumn]
        public int ProductoId { set; get; }

        [DataColumn]
        public bool ProductoFlex { set; get; }

        [DataColumn]
        public int Numero { set; get; }

        [DataColumn]
        public int PaqueteId { set; get; }

        [DataColumn]
        public int AseguradoraId { set; get; }

        [DataColumn]
        public int FormaPago { set; get; }

        [DataColumn]
        public int UsuarioId { set; get; }

        [DataColumn]
        public int PerfilId { set; get; }

        [DataColumn]
        public int CotizacionId { set; get; }

        [DataColumn]
        public string Cliente { set; get; }

        [DataColumn]
        public bool InfoComplementaria { set; get; }

        [DataColumn]
        public string TipoArrendamiento { set; get; }
    }
}