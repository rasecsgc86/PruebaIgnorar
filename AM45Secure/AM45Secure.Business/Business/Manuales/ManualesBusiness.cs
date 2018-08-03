using AM45Secure.Business.IBusiness.IManuales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM45Secure.Commons.Modelos.Manuales;
using Zero.Handlers.Response;
using AM45Secure.DataAccess.IDataAccess.IManuales;
using Zero.Exceptions;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.DataAccess.IDataAccess.IComparador;

namespace AM45Secure.Business.Business.Manuales
{
    public class ManualesBusiness : IManualesBussines
    {

        private readonly IManualesDataAccess iManualDataAccess;
        private readonly IComparadorDataAccess iComparadorDataAccess;
        public ManualesBusiness(IManualesDataAccess iManualDataAccess, IComparadorDataAccess iComparadorDataAccess)
        {
            this.iManualDataAccess = iManualDataAccess;
            this.iComparadorDataAccess = iComparadorDataAccess;

        }

        public SingleResponse<IList<CategoriaModel>> ConsultarCategoria()
        {
            SingleResponse<IList<CategoriaModel>> response = new SingleResponse<IList<CategoriaModel>>();
            try
            {
                IList<CategoriaModel> listaCategoria = iManualDataAccess.ConsultarCategoria();


                response.Done(listaCategoria, string.Empty);
            }

            catch (Exception e)
            {
                //Agregar menjase de error
                response.Error(new DomainException(e.Message));
            }
            return response;
        }

        public SingleResponse<IList<ManualesModel>> ConsultarManuale(ManualRequest manualModel)
        {
            SingleResponse<IList<ManualesModel>> response = new SingleResponse<IList<ManualesModel>>();
            try
            {
                IList<ManualesModel> listManuales = iManualDataAccess.ConsultarManuales(manualModel);
           
               
                response.Done(listManuales, string.Empty);
            }
         
            catch (Exception e)
            {
                //Agregar menjase de error
                response.Error(new DomainException(e.Message));
            }
            return response;
        }

        public SingleResponse<bool> GuardarDatosDocumento(InsertManualRequest requestModel)
        {
            SingleResponse<bool> response = new SingleResponse<bool>();
            try
            {
                var DatosUsuarioCotizante = new PersonaEmail();
                DatosUsuarioCotizante.Tkn = requestModel.Tkn;
                DatosUsuarioCotizante.PersonaId = DatosUsuarioCotizante.GetIdUsuarioSesion();
                obtenerDatosEmail(DatosUsuarioCotizante);

                var respon = iManualDataAccess.GuardarDatosDocumento(requestModel, DatosUsuarioCotizante.Nombre);


                response.Done(respon, string.Empty);
            }

            catch (Exception e)
            {
                //Agregar menjase de error
                response.Error(new DomainException(e.Message));
            }
            return response;
        }

        public void obtenerDatosEmail(PersonaEmail DatosUsuarioCotizante)
        {
            var pm = iComparadorDataAccess.obtenerDatosPersonaEmail(DatosUsuarioCotizante);
        }

        public SingleResponse<bool> ElimarDocumento(int Id)
        {
            SingleResponse<bool> response = new SingleResponse<bool>();
            try
            {
             

                var respon = iManualDataAccess.ElimarDocumento(Id);


                response.Done(respon, string.Empty);
            }

            catch (Exception e)
            {
                //Agregar menjase de error
                response.Error(new DomainException(e.Message));
            }
            return response;
        }

        public SingleResponse<FiltroManuales> FiltrosDocumentos(requestFiltro request)
        {
            SingleResponse<FiltroManuales> response = new SingleResponse<FiltroManuales>();
            try
            {
                var DatosUsuarioCotizante = new PersonaEmail();
                DatosUsuarioCotizante.Tkn = request.Tkn;
                DatosUsuarioCotizante.PersonaId = DatosUsuarioCotizante.GetIdUsuarioSesion();

                var respon = iManualDataAccess.FiltrosDocumentos(DatosUsuarioCotizante.PersonaId.ToString());


                response.Done(respon, string.Empty);
            }

            catch (Exception e)
            {
                //Agregar menjase de error
                response.Error(new DomainException(e.Message));
            }
            return response;
        }

        public SingleResponse<bool> UsuarioAdministrador(string tkn)
        {
            SingleResponse<bool> response = new SingleResponse<bool>();
            try
            {

                var DatosUsuarioCotizante = new PersonaEmail();
                DatosUsuarioCotizante.Tkn = tkn;
                DatosUsuarioCotizante.PersonaId = DatosUsuarioCotizante.GetIdUsuarioSesion();
                var respon = iManualDataAccess.UsuarioAdministrador(DatosUsuarioCotizante.PersonaId.ToString());


                response.Done(respon, string.Empty);
            }

            catch (Exception e)
            {
                //Agregar menjase de error
                response.Error(new DomainException(e.Message));
            }
            return response;
        }
    }
}
