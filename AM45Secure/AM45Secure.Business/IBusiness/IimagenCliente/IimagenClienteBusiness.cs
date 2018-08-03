using AM45Secure.Commons.Modelos.ImagenCliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Handlers.Response;

namespace AM45Secure.Business.IBusiness.IimagenCliente
{
  public  interface IimagenClienteBusiness
    {
        SingleResponse<bool> GuardarDatosIMagen(ImageClienteModel requestModel);
        ImageClienteModel seleDatosImagen(ImageClienteModel requestModel);
        SingleResponse<bool> ElimarDocumento(int Id);
    }
}
