using System.Collections.Generic;
using System.Web.Http;
using AM45Secure.Business.IBusiness.IEmitir;
using AM45Secure.Business.IBusiness.IComparador;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Cotizador;
using AM45Secure.Commons.Modelos.Emitir;
using Zero.Handlers.Response;
using AM45Secure.Commons.Modelos.Configurador;
using AM45Secure.Commons.Modelos.ConfigMultiple; 

namespace AM45Secure.RESTServices.Controllers.Emitir
{
    public class EmitirController : ApiController
    {
        private readonly IEmitirBusiness iEmitirBusiness;
        private readonly IComparadorBusiness iComparadorBusiness;

        public EmitirController(IEmitirBusiness iEmitirBusiness, IComparadorBusiness iComparadorBusiness)
        {
            this.iEmitirBusiness = iEmitirBusiness;
            this.iComparadorBusiness = iComparadorBusiness;
        }

        [HttpPost]
        public SingleResponse<EmitirModel> CargaInicial()
        {
            return iEmitirBusiness.CargaInicial();
        }
        /* INDRA FJQP -- Modificaciones Emision Multiple */
        [HttpPost]
        public SingleResponse<IList<ContratanteModel>> CrearEmision(ContratanteModel contratanteModel)
        {
            return iEmitirBusiness.CrearEmision(contratanteModel);
        }
        /* INDRA FJQP -- Modificaciones Emision Multiple */
        public SingleResponse<IList<ContratanteModel>> CrearEmisionMultiple(ContratanteModel contratanteModel)
        {
            return iEmitirBusiness.CrearEmisionMultiple(contratanteModel);
        }
        [HttpPost]
        public SingleResponse<bool> ConsultaDatosCotizacion(SolicitudPrimaCotizacion solicitudPrima)
        {
            return iEmitirBusiness.ConsultaValoresPrima(solicitudPrima);
        }

        [HttpPost]
        public SingleResponse<IList<NeIncisoEndoso>> ConsultaNeIncisosEndoso(NeIncisoEndoso neIncisosEndoso)
        {
            return iEmitirBusiness.ConsultaNeIncisosEndoso(neIncisosEndoso);
        }

        [HttpPost]
        public SingleResponse<IList<InformacionClienteModel>> ConsultaInformacionCliente(CotizadorModel cotizador)
        {
            return iEmitirBusiness.ConsultaInformacionCliente(cotizador);
        }
        /* INDRA FJQP -- Modificaciones Emision Multiple */
        [HttpPost]
        public SingleResponse<IList<VehiculoModel>> ConsultaSerie(VehiculoModel vehiculo)
        {
            return iEmitirBusiness.ConsultaSerie(vehiculo);
        }
        /* INDRA FJQP -- Modificaciones Emision Multiple */
        [HttpPost]
        public SingleResponse<IList<VehiculoGrabModel>> ConsultaSerieGrab(VehiculoGrabModel vehiculo)
        {
            return iEmitirBusiness.ConsultaSerieGrab(vehiculo);
        }

        [HttpPost]
        public SingleResponse<IList<DirectivasModel>> RecuperaListaCoberturas(DirectivasModel directivasModel)
        {
            return iEmitirBusiness.RecuperaListaCoberturas(directivasModel);
        }
        /* INDRA FJQP -- Modificaciones Emision Multiple */
        [HttpPost]
        public SingleResponse<IList<DirectivasModel>> RecuperaInfoCoberturas(DirectivasModel directivasModel)
        {
            return iEmitirBusiness.RecuperaInfoDirectivas(directivasModel);
        }
        /* INDRA FJQP -- Modificaciones Emision Multiple */
        [HttpPost]
        public SingleResponse<IList<DirectivasModel>> AlmacenaInfoCoberturas(DirectivasModel directivasModel)
        {
            return iEmitirBusiness.AlmacenaInfoDirectivas(directivasModel);
        }
        //* INDRA FJQP -- Modificaciones Emision Multiple */
        [HttpPost]
        public SingleResponse<FiltroConfigMultiple> FiltrosConfigMultiple(requestFiltro request)
        {
            return iEmitirBusiness.FiltrosConfigMultiple(request);
        }
        //* INDRA FJQP -- Modificaciones Emision Multiple */
        [HttpPost]
        public SingleResponse<bool> usuarioAdministador(requestFiltro request)
        {
            return iEmitirBusiness.UsuarioAdministrador(request.Tkn);
        }
        //* INDRA FJQP -- Modificaciones Emision Multiple */
        [HttpPost]
        public SingleResponse<IList<configMultipleRegModel>> ConsultarConfigMultiples(ConfigRequestModel configRequestModel)
        {
           return iEmitirBusiness.ConsultarConfigMultiple(configRequestModel);
        }
        //* INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos
        public SingleResponse<bool> guardarDatosConfigMultiple(InsertConfigMultipleModel requestModel)
        {
            return iEmitirBusiness.GuardarDatosConfigMultiple(requestModel);
        }
        // INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos
        public SingleResponse<bool> EliminarDatosConfigMultiple(InsertConfigMultipleModel requestModel)
        {
            return iEmitirBusiness.EliminarDatosConfigMultiple(requestModel);
        }
        // INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos
        [HttpPost]
        public SingleResponse<IList<configMultipleRegModel>> PermiteConfigMultiples(ConfigRequestModel configRequestModel)
        {
            return iEmitirBusiness.PermiteConfigMultiple(configRequestModel);
        }
    }
}