using System.Collections.Generic;
using AM45Secure.Commons.Modelos.Comunes;

namespace AM45Secure.Commons.Modelos.Emitir
{
    public class EmitirModel: AbstractModel
    {
        public ContratanteModel ContratanteModel { set; get; }
        public IList<ElementoModel> NacionalidadList { set; get; }
        public IList<ElementoModel> GeneroList { set; get; }
        public IList<ElementoModel> PaisNacimientoList { set; get; }
        public IList<ElementoModel> EntidadNacimientoList { set; get; }
        public IList<ElementoModel> DoctoIdentifiacionList { set; get; }
        public IList<ElementoModel> ProfesionList { set; get; }
        public IList<ElementoModel> OcupacionList { set; get; }
        public IList<ElementoModel> GiroNegocioList { set; get; }
        public IList<ElementoModel> MandoEnGobiernoList { set; get; }
        public IList<ElementoModel> RegimenFiscalEmpresarialList { set; get; }
    }
}
