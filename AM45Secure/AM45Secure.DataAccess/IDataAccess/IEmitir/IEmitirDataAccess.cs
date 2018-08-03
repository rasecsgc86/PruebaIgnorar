using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Emitir;
using Zero.Handlers.Response;
using AM45Secure.Commons.Modelos.ConfigMultiple;

namespace AM45Secure.DataAccess.IDataAccess.IEmitir
{
    public interface IEmitirDataAccess
    {
        IList<ElementoModel> ConsultaElementosPorCatalogoId(ElementoModel elementoModel);
        DatosEmitirModel ConsultaDatosCotizacion(SolicitudPrimaCotizacion solicitudPrima);
        IList<NeIncisoEndoso> ConsultaNeIncisosEndoso(NeIncisoEndoso neIncisosEndoso);
        IList<ElementoModel> ConsultaElementosPorElementoId(int elementoId);
        IList<VehiculoModel> ConsultaSerie(VehiculoModel vehiculo);
        ProductoModel ConsultaProductosFlex(ProductoModel producto);
        IList<VehiculoGrabModel> ConsultaSerieGrab(VehiculoGrabModel vehiculo); //* INDRA FJAQP Emisión Multiple*/
         FiltroConfigMultiple FiltrosConfigMultiple(string IdUsuario); //* INDRA FJQP Implementacion de config Emisión Multiple *//
         bool UsuarioAdministrador(string userID); //* INDRA FJQP Implementacion de config Emisión Multiple *//
         IList<configMultipleRegModel> ConsultarConfigMultiples(ConfigRequestModel configRequestModel); //* INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos*//
         bool GuardarDatosConfigMultiple(InsertConfigMultipleModel requestModel, string usuario);  //* INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos *//
         bool EliminarDatosConfigMultiple(InsertConfigMultipleModel requestModel, string usuario); //* INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos *//
        IList<configMultipleRegModel> PermiteConfigMultiples(ConfigRequestModel configRequestModel); //* INDRA : FJQP CHANGE REQUEST ConfigMultiple y Capt. Contratos *//
    }
}