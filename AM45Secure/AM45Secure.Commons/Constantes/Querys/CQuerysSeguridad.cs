namespace AM45Secure.Commons.Constantes.Querys
{
    public class CQuerysSeguridad
    {
        public static readonly string QryVersionSistema = "SELECT	TOP 1 	[IdVersion] ,  [Version] ,  [Descripcion] ,  [Fecha] ,  [OT] FROM	dbo.AM_VersionSistema ORDER BY IdVersion DESC";
    }
}