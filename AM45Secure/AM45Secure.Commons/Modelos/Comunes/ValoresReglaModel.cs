using System;
using System.Runtime.Serialization;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Comunes
{
    public class ValoresReglaModel
    {
        public string ValorId { set; get; }

        public string Valor { set; get; }

        [Required(FieldName = "ClienteId", Optional = true)]
        public string ClienteId { set; get; }

        public string Cliente { set; get; }

        [Required(FieldName = "ProductoId", Optional = true)]
        public string ProductoId { set; get; }

        public string Producto { set; get; }

        public string TipoVehiculoId { set; get; }

        public string TipoVehiculo { set; get; }

        public int EstadoId { set; get; }

        public string Estado { set; get; }

        public string Pais { set; get; }

        public string Delegacion { set; get; }

        public string Colonia { set; get; }

        public string CodigoPostal { set; get; }

        public string Domicilio { set; get; }
        public string NoExterior { set; get; }

        public int AseguradoraId { set; get; }

        public string Aseguradora { set; get; }

        public int HabilitaRemolques { set; get; }

        public string ServicioId { set; get; }

        public string Servicio { set; get; }

        [Column("AgenteUDI")]
        public string AgenteUdi { set; get; }

        public DateTime FechaRetro { set; get; }
    }
}