using System.Collections.Generic;
using AM45Secure.Commons.Modelos.Cotizador;

namespace AM45Secure.Commons.Modelos.Comunes
{
    public class CoberturaModel
    {
        public int IdCobertura { set; get; }

        public string NombreCobertura { set; get; }

        public string Dependencia { get; set; }

        public bool Enmascaramiento { get; set; }

        public RangosModel RangosModel { set; get; }

        public bool IsFija { set; get; }

        public IList<ElementoPanelCotizadorModel> AseguradorasCobertura { set; get; }

        public string FiltroValorRangoSuma { set; get; }

        public string FiltroValorRangoDeducible { set; get; }

        public bool IsSeleccionada { set; get; }

        public string SumaAseguradaDefault { set; get; }

        public string DeducibleDefault { set; get; }

        public bool IsEspecial { set; get; }
        public bool IsAdaptacion { set; get; }
        public bool PerfilCoberturaFija { set; get; }
        public bool PerfilSumaAsegurada { set; get; }
        public bool PerfilDeducible { set; get; }
        public bool IsFijaPerfil { get; set; }
    }
}