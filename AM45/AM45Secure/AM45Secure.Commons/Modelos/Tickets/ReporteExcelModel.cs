namespace AM45Secure.Commons.Modelos.Tickets
{
    public class ReporteExcelModel
    {
        public int TicketId { get; set; }
        public int PersonaId { get; set; }
        public int TipoId { get; set; }
        public string FechaRegistro { get; set; }
        public string FechaRecepcion { get; set; }
        public string FechaCierre { set; get; }
        public string Cliente { get; set; }
        public string Caratula { get; set; }
        public string TipoTicket { get; set; }
        public string DescripcionTicket { get; set; }
        public string NombrePer { get; set; }
        public string PaternoPer { get; set; }
        public string MaternoPer { get; set; }
        public int HorasAtencion { get; set; }
        public string EnTiempo { get; set; }
        public string DescripcionEstatus { get; set; }
        public int NumTicket { get; set; }
        public int TiempoAtencion { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public int CveEstatus { get; set; }
        public int AseguradoraId { get; set; }
        public string Nombre { get; set; }
        public string CalTiempo { get; set; }
        public string NumeroOt { set; get; }
        public string NumeroOtSics { set; get; }
        public string Usuario { set; get; }
        public string Alias { set; get; }
        public string Perfil { set; get; }
        public string Comentario { set; get; }
    }
}