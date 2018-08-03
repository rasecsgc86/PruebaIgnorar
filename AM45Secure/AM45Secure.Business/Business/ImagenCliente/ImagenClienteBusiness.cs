using AM45Secure.Business.IBusiness.IimagenCliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM45Secure.Commons.Modelos.ImagenCliente;
using Zero.Handlers.Response;
using AM45Secure.DataAccess.IDataAccess.IimagenCliente;
using Zero.Exceptions;

namespace AM45Secure.Business.Business.ImagenCliente
{
    public class ImagenClienteBusiness : IimagenClienteBusiness
    {

        private readonly IimagenClienteDataAccess iImagenClienteDataAccess;
       
        public ImagenClienteBusiness(IimagenClienteDataAccess iImagenClienteDataAccess)
        {
            this.iImagenClienteDataAccess = iImagenClienteDataAccess;
         

        }

        public SingleResponse<bool> ElimarDocumento(int Id)
        {
            SingleResponse<bool> response = new SingleResponse<bool>();
            try
            {


                var respon = iImagenClienteDataAccess.ElimarDocumento(Id);


                response.Done(respon, string.Empty);
            }

            catch (Exception e)
            {
                //Agregar menjase de error
                response.Error(new DomainException(e.Message));
            }
            return response;
        }

        public SingleResponse<bool> GuardarDatosIMagen(ImageClienteModel requestModel)
        {
            SingleResponse<bool> response = new SingleResponse<bool>();
            try
            {
            

                var respon = iImagenClienteDataAccess.GuardarDatosIMagen(requestModel);


                response.Done(respon, string.Empty);
            }

            catch (Exception e)
            {
                //Agregar menjase de error
                response.Error(new DomainException(e.Message));
            }
            return response;
        }

        public ImageClienteModel seleDatosImagen(ImageClienteModel requestModel)
        {
          
         

                var respon = iImagenClienteDataAccess.seleDatosImagen(requestModel);


           

         
            return respon;
        }
    }
}
