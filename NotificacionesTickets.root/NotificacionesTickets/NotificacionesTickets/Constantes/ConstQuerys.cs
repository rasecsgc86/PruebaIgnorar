namespace NotificacionesTickets.Constantes
{
    public class ConstQuerys
    {
        public static readonly string GetStatus = "SELECT * FROM dbo.vwTICSelTicketCorreo WHERE IdEstatusTicket = 1 OR IdEstatusTicket = 2 OR IdEstatusTicket = 3";
        public static readonly string GetReporte = "SELECT IdEscalamientoTicket, TicketId, FechaEscalamiento, TipoEscalamiento FROM dbo.EscalamientosTickets WHERE TicketId = {r} ";
        public static readonly string SetReporte = "INSERT INTO dbo.EscalamientosTickets (TicketId, FechaEscalamiento, TipoEscalamiento) VALUES ( {TI}, {FE}, {TE} )";
        public static readonly string GetDiaInhabil = "SELECT IdDiaHabil, Dia, PersonaID, FechaRegistro FROM dbo.CatDiasHabiles";
        public static readonly string GetTotalInhabiles = "SELECT COUNT(Dia) totalInhabiles FROM CatDiasHabiles WHERE Dia >= {FechaRecepcion} AND Dia <= GETDATE()";
        public static readonly string GetMailsCc = "SELECT Correo FROM dbo.CorreosCopiaTickets WHERE TicketId = ";
        public static readonly string GetHoyDiaInhabil = "SELECT	[IdDiaHabil] ,[Dia] ,[PersonaID] ,[FechaRegistro] FROM [dbo].[CatDiasHabiles] WHERE[dia] = CONVERT(DATE, GETDATE())";
        public static readonly string GetTotalDiasInhabiles = "SELECT COUNT(Dia) totalInhabiles FROM CatDiasHabiles WHERE Dia >= {FechaRecepcion} AND Dia < GETDATE()";
    }
}