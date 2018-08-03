using AM45Secure.Commons.Modelos.Comunes;

namespace AM45Secure.Commons.Modelos.Cotizador
{
    public class DatosVehiculoModel
    {
        public ClaveValorModel TipoUnidad { get; set; }
        public ClaveValorModel Antiguedad { get; set; }
        public string ClaveMarsh { get; set; }
        public ClaveValorModel Servicio { get; set; }
        public string Valor { get; set; }
        public ClaveValorModel Modelo { get; set; }
        public ClaveValorModel SubMarca { get; set; }
        public ClaveValorModel Armadora { get; set; }
        public ClaveValorModel Carga { get; set; }
        public PasajerosModel Pasajero { get; set; }
        public VersionesModel Version { get; set; }
        public ElementoModel Remolque { get; set; }
        public ElementoModel LoJackModel { get; set; }
        public int RemolqueInt { set; get; }
        public int LoJack { get; set; }
        public bool ShowCargas { get; set; }
        public bool ShowRemolques { get; set; }
        public ElementoModel ServicioInterno { get; set; }
        public ElementoModel ElAntiguedad { get; set; }
    }
}