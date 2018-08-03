namespace AM45Secure.Commons.Constantes.Querys
{
    public class CQuerysTickets
    {
        public static readonly string QryTiposTickets = "SELECT TTC.TipoId IdTipoTicket , TTC.IdCliente IdCliente, nP.Nombre + '  ' + ISNULL(nP.Paterno, '') + '  ' + ISNULL(nP.Materno, '') NombreCliente , TT.Descripcion DescripcionTipoTicket, TTC.HorasAtencion FROM    TiposTicketsClientes AS TTC INNER JOIN nePersonas nP ON TTC.IdCliente = {{IdCliente}} AND nP.PersonaID = TTC.IdPersonaResponsable INNER JOIN (SELECT TipoId, Descripcion FROM  TiposTicket WHERE  Activa = 1) TT ON TT.TipoId = TTC.TipoId;";
    }
}