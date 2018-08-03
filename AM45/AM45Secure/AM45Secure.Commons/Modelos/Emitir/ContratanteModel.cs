using System;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Cotizador;
using AM45Secure.Commons.Modelos.comunes;

namespace AM45Secure.Commons.Modelos.Emitir
{
    public class ContratanteModel : AbstractModel
    {
        public SolicitudPrimaCotizacion Solicitud { set; get; }

        public DatosContratanteModel Contratante { set; get; }

        public ComplementariaModel DatosComplementarios { set; get; }

        public DireccionModel Direccion { set; get; }

        public VehiculoModel Vehiculo { set; get; }

        public AgenciaModel Agencia { set; get; }

        public DatosEmitirModel DatosEmitir { set; get; }
        public NeIncisoEndoso NeIncisosEndoso { set; get; }
        public CabeceraCotizacionModel CabeceraCotHeredada { get; set; } /* INDRA FJQP Emisión Multiple */
        public CotizarModel CotizarModelHeredada { get; set; } /* INDRA FJQP Emisión Multiple */
        public int ConfirmedEncontrack { get; set; } /* INDRA FJQP Encontrack */
    }
}