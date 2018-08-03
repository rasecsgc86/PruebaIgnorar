using System.Collections.Generic;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Cotizador;
using AM45Secure.Commons.Modelos.Emitir;
using Zero.Handlers.Response;
using AM45Secure.Commons.Modelos.Configurador;
using AM45Secure.Commons.Modelos.ConfigMultiple; 

namespace AM45Secure.Business.IBusiness.IEmitir
{
    public interface IEmitirBusiness
    {
        SingleResponse<EmitirModel> CargaInicial();
        SingleResponse<IList<ContratanteModel>> CrearEmision(ContratanteModel contratanteModel);
        SingleResponse<bool> ConsultaValoresPrima(SolicitudPrimaCotizacion solicitudPrima);
        SingleResponse<IList<NeIncisoEndoso>> ConsultaNeIncisosEndoso(NeIncisoEndoso neIncisosEndoso);
        SingleResponse<IList<InformacionClienteModel>> ConsultaInformacionCliente(CotizadorModel cotizador);
        SingleResponse<IList<VehiculoModel>> ConsultaSerie(VehiculoModel vehiculo);
        SingleResponse<IList<ContratanteModel>> CrearEmisionMultiple(ContratanteModel contratanteModel);
        SingleResponse<IList<VehiculoGrabModel>> ConsultaSerieGrab(VehiculoGrabModel vehiculo);
        SingleResponse<IList<DirectivasModel>> RecuperaInfoDirectivas(DirectivasModel directivasModel);
        SingleResponse<IList<DirectivasModel>> RecuperaListaCoberturas(DirectivasModel directivasModel);
        SingleResponse<IList<DirectivasModel>> AlmacenaInfoDirectivas(DirectivasModel directivasModel);
        /* INDRA FJQP Implementacion de config Emisión Multiple */
        SingleResponse<FiltroConfigMultiple> FiltrosConfigMultiple(requestFiltro request);
        /* INDRA FJQP Implementacion de config Emisión Multiple */
        SingleResponse<bool> UsuarioAdministrador(string tkn);
        // INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos
        SingleResponse<IList<configMultipleRegModel>> ConsultarConfigMultiple(ConfigRequestModel configRequestModel);
        // INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos
        SingleResponse<bool> GuardarDatosConfigMultiple(InsertConfigMultipleModel requestModel);
        // INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos
        SingleResponse<bool> EliminarDatosConfigMultiple(InsertConfigMultipleModel requestModel);
        // INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos
        SingleResponse<IList<configMultipleRegModel>> PermiteConfigMultiple(ConfigRequestModel configRequestModel);
    }
}