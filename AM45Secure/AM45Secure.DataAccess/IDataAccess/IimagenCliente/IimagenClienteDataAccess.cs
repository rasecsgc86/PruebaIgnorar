using AM45Secure.Commons.Modelos.ImagenCliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM45Secure.DataAccess.IDataAccess.IimagenCliente
{
   public interface IimagenClienteDataAccess
    {
        bool GuardarDatosIMagen(ImageClienteModel requestModel);
        ImageClienteModel seleDatosImagen(ImageClienteModel requestModel);
        bool ElimarDocumento(int Id);
    }
}
