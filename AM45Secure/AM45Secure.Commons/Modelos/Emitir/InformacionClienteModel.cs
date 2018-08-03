using System;
using AM45Secure.Commons.Modelos.Comunes;

namespace AM45Secure.Commons.Modelos.Emitir
{
    public class InformacionClienteModel
    {
        public InfContratanteModel Contratante { get; set; }

        public DireccionModel Direccion { get; set; }
    }
}