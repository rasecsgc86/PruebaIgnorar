using Zero.Ado;

namespace AM45Secure.Commons.Modelos.Comunes
{
    public class ElementoModel : IEntity
    {
        public int CatalogoId { set; get; }
        public int ElementoId { set; get; }
        public int IdInterno { set; get; }
        public string Nombre { set; get; }
        public string Comodin { set; get; }
    }
}