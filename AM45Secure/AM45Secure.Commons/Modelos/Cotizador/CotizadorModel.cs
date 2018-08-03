using System.Collections.Generic;
using AM45Secure.Commons.Modelos.Comunes;

namespace AM45Secure.Commons.Modelos.Cotizador
{
    public class CotizadorModel : AbstractModel
    {
        public ClientesModel ClienteModel { set; get; }

        public CodigoPostalModel CodigoPostalModel { set; get; }

        public SolicitudReglaNegocioModel SolicitudRegla { set; get; }

        public ProductoClienteModel ProductoCliente { set; get; }

        public SolicitudVersionesModel SolicitudVersiones { set; get; }

        public SolicitudPasajerosModel SolicitudPasajeros { set; get; }

        public ClientProdAgenAsegModel ClientProdAgenAseg { set; get; }

        public PlazosModel Plazos { set; get; }

        public FechaFinVigenciaModel FechaFinVigencia { set; get; }

        public PanelCotizadorModel PanelCotizadorModel { set; get; }

        public UsuarioPerfilModel UsuarioPerfil { set; get; }

        public LimiteValorFacturaModel LimiteValorFactura { set; get; }

        public IList<ProductoModel> Productos { set; get; }

        public IList<ClientesModel> Clientes { set; get; }

        public IList<RegionCodigoPostalModel> RegionCodigoPostal { set; get; }

        public IList<ValoresReglaModel> ValoresReglas { set; get; }

        public IList<ElementoModel> Arrendamiento { set; get; }

        public IList<ElementoModel> LoJack { set; get; }

        public IList<ElementoModel> Remolque { set; get; }

        public IList<VersionesModel> Versiones { set; get; }

        public IList<PasajerosModel> Pasajeros { set; get; }

        public IList<AgenciasModel> Agencias { set; get; }

        public IList<ValoresReglaModel> TipoArrendamiento { set; get; }

        public IList<ValoresReglaModel> TipoUnidad { set; get; }

        public IList<ValoresReglaModel> Antiguedad { set; get; }

        public IList<ValoresReglaModel> Servicio { set; get; }

        public IList<ValoresReglaModel> Modelos { set; get; }

        public IList<ValoresReglaModel> Armadoras { set; get; }

        public IList<ValoresReglaModel> Submarcas { set; get; }

        public IList<ValoresReglaModel> Cargas { set; get; }

        public IList<ValoresReglaModel> Plazo { set; get; }

        public IList<ValoresReglaModel> Estados { set; get; }

        public IList<ValoresReglaModel> Udi { set; get; }
    }
}