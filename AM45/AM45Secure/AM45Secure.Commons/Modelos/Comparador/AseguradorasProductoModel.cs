using System.Collections.Generic;

namespace AM45Secure.Commons.Modelos.Comparador
{
    public class AseguradorasProductoModel
    {

        public int AseguradoraId { set; get; }
        public int ProductoId { set; get; }
        public int Numero { set; get; }
        public int CatalogoId { set; get; }
        public int ElementoId { set; get; }
        public string Nombre { set; get; }
        public int IdInterno { set; get; }
        public string Img { set; get; }
        public List<PaqueteModel> ListaPaquetes { set; get; }


    }
}
