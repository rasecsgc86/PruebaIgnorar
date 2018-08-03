using Zero.Ado;

namespace AM45Secure.DataAccess.Entidades.Tickets
{
    public class VwNueListaClientesProducto : IEntity
    {
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public int IdIdentificadorFisicaMoral { get; set; }
        public int IdCategoriaCliente { get; set; }
        public string CategoriaCliente { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int IdCategoriaProducto { get; set; }
        public string CategoriaProducto { get; set; }
        public int IdResponsable { get; set; }
        public string NombreCompletoResponsable { get; set; }
        public string MailResponsable { get; set; }
        public int IdTipoTicket { get; set; }
        public string DescripcionTipoTicket { get; set; }
        public string PolizaCaratula { get; set; }
        public string FormaPago { get; set; }
        public int Tipo { get; set; }
        public int TipoCobranza { get; set; }
        public int Contador { set; get; }
        public string TipoCobranzaString { set; get; }
        public string TipoString { get; set; }
        public int HorasAtencion { set; get; }
    }
}