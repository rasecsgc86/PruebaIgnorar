﻿namespace AM45Secure.Commons.Constantes.Querys
{
    public class CQuerysCotizador
    {
        public static readonly string QryProductosClienteUsuario = "SELECT  productos.Nombre Producto , productos.ProductoID ProductoId,productos.Flexible,productos.CP AS Cp FROM ( SELECT prod.Nombre , prod.ProductoID, COALESCE(prod.Flexible, 0) Flexible , ROW_NUMBER() over(order by prod.ProductoID )as RowNumber,RANK()over(order by prod.ProductoID )as Rank, prod.CP FROM dbo.Productos prod  INNER JOIN(SELECT pD.Valor FROM dbo.PerfilDatos pD  Where ISNUMERIC ( pD.Valor ) =1 AND pD.Opcion = 825 AND pd.PersonaID = @UsuarioId AND pd.PerfilID = @PerfilId  ) as pD  ON   prod.ProductoID = pD.Valor AND COALESCE(prod.Flexible,0) = @ProductoFlex INNER JOIN(SELECT rd.ValorB  FROM  dbo.RelacionDatos rD  WHERE rD.OpcionA = Cast(1169 as VARCHAR(12)) AND rd.OpcionB = Cast(825 as VARCHAR(12)) AND rd.ValorA = @ClienteId  ) as rD   ON  pd.Valor = rD.ValorB ) as productos where Rank=RowNumber";
        public static readonly string QryConsultarAgencias = "SELECT  agencia.PersonaID ValorIDB, ISNULL(agencia.Nombre, '') + ' ' + ISNULL(agencia.Paterno, '') AS NombreValorB FROM dbo.nePersonas agencia ( NOLOCK ) INNER JOIN dbo.RelacionDatos RD ( NOLOCK ) ON CAST(agencia.PersonaID AS VARCHAR(50)) = RD.ValorB AND OpcionB = '1215' AND RD.OpcionA = '1169' AND RD.ValorA = @PersonaidOpcionA INNER JOIN dbo.Usuarios usuario ( NOLOCK ) ON agencia.PersonaID = usuario.PersonaID AND usuario.StatusID = 1 AND usuario.PerfilUsuarioID IN ( 74 ) GROUP BY agencia.PersonaID, agencia.Nombre, agencia.Paterno ORDER BY agencia.Nombre ASC";
        public static readonly string QryProductosClienteUsuarioProducto = "SELECT	 productos.Nombre Producto ,productos.ProductoID ,productos.Flexible ,productos.CP FROM	(	SELECT	 prod.Nombre ,prod.ProductoID ,prod.Flexible ,prod.CP ,ROW_NUMBER() OVER(ORDER BY prod.ProductoID ) AS RowNumber ,RANK()OVER(ORDER BY prod.ProductoID ) AS RANK FROM	dbo.Productos prod  INNER JOIN (SELECT	pD.Valor FROM	dbo.PerfilDatos pD  WHERE ISNUMERIC ( pD.Valor ) = 1 AND pD.Opcion = 825 AND pd.PersonaID = @UsuarioId AND pd.PerfilID = @PerfilId  ) AS pD  ON  prod.ProductoID = pD.Valor AND prod.ProductoID = @ProductoId INNER JOIN (SELECT	rd.ValorB  FROM	dbo.RelacionDatos rD  WHERE	rD.OpcionA = CAST(1169 AS VARCHAR(12)) AND rd.OpcionB = CAST(825 AS VARCHAR(12)) AND rd.ValorA = @ClienteId  ) AS rD   ON  pd.Valor = rD.ValorB ) AS productos WHERE RANK = RowNumber";
        public static readonly string QryClientesUsuario = "SELECT ne.PersonaID Id,ne.Nombre FROM dbo.nePersonas ne INNER JOIN (SELECT PerfilID, PersonaID, Valor FROM dbo.PerfilDatos WHERE opcion = @OpcionId AND PerfilID = @PerfilId AND PersonaID = @UsuarioId AND ISNUMERIC(Valor)=1) pf ON ne.PersonaID = pf.Valor  ";
        public static readonly string QryCobPaqAsegProd = "SELECT	NPC.CotizacionID AS CotizacionId, NPC.Numero , NPC.AseguradoraID AS AseguradoraId , NPC.ProductoID AS ProductoId , NPC.PaqueteID AS PaqueteId,COB.Nombre,COB.Homologacion,	NPC.Tarifa ,NPC.PrimaNeta ,NPC.Recargo ,NPC.Derechos ,NPC.IVA ,NPC.PrimaTotal ,	NPC.Comision ,NPC.PorcentajeRecargo ,	NPC.PorcentajeIVA AS PorcentajeIva, NPC.PorcentajeComision , NPC.Descuento ,NPC.FolioExterno ,NPC.UDI AS Udi,NPC.RecargoFraccionado ,NPC.InicioVigencia ,NPC.FinVigencia ,NPC.Plazo ,NCC.SumaAsegurada AS SumaAseguradaC,NCC.PrimaNeta AS PrimaNetaC,NCC.Deducible AS DeducibleC,NCC.SumaAseguradaInicial AS SumaAseguradaInicialC FROM dbo.nePrimasCotizacion NPC INNER JOIN dbo.neCoberturasCotizacion NCC ON NPC.Numero = NCC.Numero AND NCC.CotizacionID = NPC.CotizacionID INNER JOIN( SELECT CobP.ProductoID AS ProductoId,COB.AseguradoraID AS AseguradoraId,  CP.PaqueteID AS PaqueteId , CP.CoberturaID AS CoberturaId,  COB.Nombre , COB.ClaveAseguradora AS Homologacion,  CP.TipoID AS TipoId FROM ( SELECT E.ElementoID , EA.ClaveAseguradora, E.Nombre, EA.AseguradoraID FROM dbo.ElementosAseguradora EA  INNER JOIN Elementos as E  ON  ISNUMERIC(EA.ClaveAseguradora)=0 AND E.ElementoID = EA.ElementoID AND E.CatalogoID = 40 AND EA.AseguradoraID= @AseguradoraId UNION  SELECT E.ElementoID , EA.ClaveAseguradora, E.Nombre , EA.AseguradoraID FROM dbo.ElementosAseguradora EA INNER JOIN Elementos as E ON  ISNUMERIC(EA.ClaveAseguradora)=1 AND E.ElementoID = EA.ClaveAseguradora AND E.CatalogoID = 40 AND EA.AseguradoraID= @AseguradoraId ) AS COB INNER JOIN dbo.neCoberturasPaquete CP ON CP.PaqueteID = @PaqueteId AND CP.AseguradoraID = @AseguradoraId AND CP.CoberturaID = COB.ElementoID AND CP.AseguradoraID = COB.AseguradoraID INNER JOIN dbo.neCoberturasProducto CobP  ON CobP.ProductoID = @ProductoId AND CobP.AseguradoraID = CP.AseguradoraID AND CobP.PaqueteID = CP.PaqueteID AND CobP.CoberturaID = CP.CoberturaID  AND CobP.TipoID = CP.TipoID)COB ON COB.AseguradoraId = NPC.AseguradoraID AND COB.PaqueteId = NPC.PaqueteID AND COB.CoberturaId = NCC.CoberturaID AND COB.ProductoId = NPC.ProductoID AND npc.CotizacionID = @CotizacionId";
        public static readonly string QryCobPaqAsegProdFlex = "	SELECT	NPC.CotizacionID AS CotizacionId, NPC.Numero ,NPC.AseguradoraID AS AseguradoraId ,NPC.ProductoID AS ProductoId , NPC.PaqueteID AS PaqueteId,COB.Nombre, COB.Homologacion,	COB.Tooltip,	NPC.Tarifa , NPC.PrimaNeta , NPC.Recargo ,NPC.Derechos ,NPC.IVA ,NPC.PrimaTotal , NPC.Comision , NPC.PorcentajeRecargo ,NPC.PorcentajeIVA AS PorcentajeIva, NPC.PorcentajeComision , NPC.Descuento ,NPC.FolioExterno ,NPC.UDI AS Udi,	NPC.RecargoFraccionado ,NPC.InicioVigencia ,NPC.FinVigencia ,NPC.Plazo ,NCC.SumaAsegurada AS SumaAseguradaC,NCC.PrimaNeta AS PrimaNetaC, NCC.Deducible AS DeducibleC, NCC.SumaAseguradaInicial AS SumaAseguradaInicialC FROM dbo.nePrimasCotizacion NPC INNER JOIN dbo.neCoberturasCotizacion NCC ON NPC.Numero = NCC.Numero AND NCC.CotizacionID = NPC.CotizacionID INNER JOIN( SELECT Pr.ProductoID AS ProductoId , PFA.IdAseguradora AS AseguradoraId , CP.PaqueteID AS PaqueteId, CP.TipoID TipoId , CPFA.IdCobertura AS CoberturaId, Cob.Nombre , CPFA.Homologacion , CPFA.Tooltip , CPFA.Detalle , PF.IdTipoVehiculo , PF.IdCondicionVehiculo AS IdAntiguedad, PF.IdTipoServicioVehiculo AS IdServicio FROM    [dbo].[ProductosFlexAseguradoras] PFA INNER JOIN dbo.ProductosFlex PF  ON (PFA.IdProductoFlex = PF.IdProductoFlex) INNER JOIN[dbo].[CoberturasProductosFlexAseguradoras]  CPFA  ON(PFA.IdProductoFlexAseguradora = CPFA.IdProductoFlexAseguradora)      INNER JOIN[dbo].[Elementos]  Cob    ON(CPFA.IdCobertura = Cob.ElementoID)  INNER JOIN [dbo].[neCoberturasPaquete]  CP ON(PFA.IdAseguradora = CP.AseguradoraID  AND CPFA.IdCobertura = CP.CoberturaID)  INNER JOIN[dbo].[Productos] Pr    ON(PF.ProductoID = Pr.ProductoID)  WHERE CP.AseguradoraID = @AseguradoraId AND Pr.ProductoID = @ProductoId      AND CP.PaqueteID = @PaqueteId   AND PF.IdTipoVehiculo = @IdTipoVehiculo   AND PF.IdCondicionVehiculo = @IdAntiguedad    AND PF.IdTipoServicioVehiculo = @IdServicio  )COB ON COB.AseguradoraId = NPC.AseguradoraID AND COB.PaqueteId = NPC.PaqueteID AND COB.CoberturaId = NCC.CoberturaID AND COB.ProductoId = NPC.ProductoID  AND NPC.CotizacionID = @CotizacionId	";
        public static readonly string QryCobPaqAsegPrdoCRep = "SELECT	COB.CoberturaId, NPC.CotizacionID AS CotizacionId,NPC.Numero ,NPC.AseguradoraID AS AseguradoraId , NPC.ProductoID AS ProductoId ,NPC.PaqueteID AS PaqueteId,EPkt.Nombre AS Paquete,COB.Nombre,COB.Homologacion,NPC.Tarifa ,NPC.PrimaNeta ,	NPC.Recargo ,NPC.Derechos ,	NPC.IVA ,	NPC.PrimaTotal ,	NPC.Comision ,NPC.PorcentajeRecargo ,NPC.PorcentajeIVA AS PorcentajeIva,NPC.PorcentajeComision ,NPC.Descuento ,NPC.FolioExterno ,NPC.UDI AS Udi,NPC.RecargoFraccionado ,NPC.InicioVigencia ,NPC.FinVigencia ,NPC.Plazo ,NCC.SumaAsegurada AS SumaAseguradaC,NCC.PrimaNeta AS PrimaNetaC,NCC.Deducible AS DeducibleC,NCC.SumaAseguradaInicial AS SumaAseguradaInicialC FROM dbo.nePrimasCotizacion NPC INNER JOIN dbo.neCoberturasCotizacion NCC ON NPC.Numero = NCC.Numero AND NCC.CotizacionID = NPC.CotizacionID INNER JOIN dbo.Elementos EPkt  ON EPkt.CatalogoID = 124 AND EPkt.ElementoID = NPC.PaqueteID INNER JOIN(  SELECT CobP.ProductoID AS ProductoId,  COB.AseguradoraID AS AseguradoraId,   CP.PaqueteID AS PaqueteId ,  CP.CoberturaID AS CoberturaId,  COB.Nombre , COB.ClaveAseguradora AS Homologacion,  CP.TipoID AS TipoId FROM ( SELECT E.ElementoID , EA.ClaveAseguradora, E.Nombre , EA.AseguradoraID FROM dbo.ElementosAseguradora EA  INNER JOIN Elementos as E  ON  ISNUMERIC(EA.ClaveAseguradora)=0 AND E.ElementoID = EA.ElementoID  AND E.CatalogoID = 40 UNION SELECT  E.ElementoID , EA.ClaveAseguradora , E.Nombre , EA.AseguradoraID FROM dbo.ElementosAseguradora EA INNER JOIN Elementos as E   ON  ISNUMERIC(EA.ClaveAseguradora)=1 AND E.ElementoID = EA.ClaveAseguradora  AND E.CatalogoID = 40  ) AS COB INNER JOIN dbo.neCoberturasPaquete CP  ON CP.PaqueteID = @PaqueteId  AND CP.CoberturaID = COB.ElementoID AND CP.AseguradoraID = COB.AseguradoraID INNER JOIN dbo.neCoberturasProducto CobP  ON CobP.AseguradoraID = CP.AseguradoraID AND CobP.PaqueteID = CP.PaqueteID AND CobP.CoberturaID = CP.CoberturaID  AND CobP.TipoID = CP.TipoID)COB ON COB.AseguradoraId = NPC.AseguradoraID AND COB.PaqueteId = NPC.PaqueteID  AND COB.CoberturaId = NCC.CoberturaID  AND COB.ProductoId = NPC.ProductoID AND NCC.CotizacionID = @CotizacionId AND NCC.Numero = @Numero";
        public static readonly string QrySolicitudReporte = "SELECT	VCS.ClaveVehiculoMarsh,NE.AseguradoraID AS AseguradoraId, VCS.Marca,VCS.EstadoCirculacion,VCS.MondaId,N.CotizacionID CotizacionId,VCS.ClaveVehiculoMarsh, N.FechaRegistro ,NE.RecargoFraccionado,ClaveAseg = CASE WHEN ne.AseguradoraID = 222 	THEN VCS.Qlt WHEN ne.AseguradoraID = 14625  THEN VCS.Gnp WHEN ne.AseguradoraID = 129597 THEN VCS.Mfr END, NE.PrimaNeta, NE.Derechos,NE.IVA, NE.PrimaTotal,  NE.Recargo,  NE.PaqueteID AS PaqueteId, FP.Nombre FROM dbo.vwCOTSelSolicitudCotizacionServTipUnidad VCS INNER JOIN dbo.neCotizacion N  ON N.FolioSolicitud = VCS.SolicitudId INNER JOIN (SELECT Nombre, ElementoID FROM dbo.Elementos WHERE CatalogoID = 19) FP  ON FP.ElementoID = N.FormaPagoID INNER JOIN dbo.nePrimasCotizacion NE ON NE.CotizacionID = N.CotizacionId  WHERE VCS.SolicitudId = @SolicitudId   AND N.CotizacionID = @CotizacionId AND NE.Numero = @Numero";
        public static readonly string QryCobPaqAsegPrdoFRep = "SELECT NPC.CotizacionID AS CotizacionId, NPC.Numero,EPkt.Nombre AS Paquete ,NPC.AseguradoraID AS AseguradoraId ,NPC.ProductoID AS ProductoId , NPC.PaqueteID AS PaqueteId,COB.Nombre, COB.Homologacion,	COB.Tooltip,	NPC.Tarifa , NPC.PrimaNeta , NPC.Recargo ,NPC.Derechos ,NPC.IVA ,NPC.PrimaTotal , NPC.Comision , NPC.PorcentajeRecargo ,NPC.PorcentajeIVA AS PorcentajeIva, NPC.PorcentajeComision , NPC.Descuento ,NPC.FolioExterno ,NPC.UDI AS Udi,	NPC.RecargoFraccionado ,NPC.InicioVigencia ,NPC.FinVigencia ,NPC.Plazo ,NCC.SumaAsegurada AS SumaAseguradaC,NCC.PrimaNeta AS PrimaNetaC, NCC.Deducible AS DeducibleC, NCC.SumaAseguradaInicial AS SumaAseguradaInicialC FROM dbo.nePrimasCotizacion NPC INNER JOIN dbo.neCoberturasCotizacion NCC ON NPC.Numero = NCC.Numero AND NCC.CotizacionID = NPC.CotizacionID INNER JOIN dbo.Elementos EPkt  ON EPkt.CatalogoID = 124 AND EPkt.ElementoID = NPC.PaqueteID  INNER JOIN( SELECT Pr.ProductoID AS ProductoId , PFA.IdAseguradora AS AseguradoraId , CP.PaqueteID AS PaqueteId, CP.TipoID TipoId , CPFA.IdCobertura AS CoberturaId, Cob.Nombre , CPFA.Homologacion , CPFA.Tooltip , CPFA.Detalle , PF.IdTipoVehiculo , PF.IdCondicionVehiculo AS IdAntiguedad, PF.IdTipoServicioVehiculo AS IdServicio FROM    [dbo].[ProductosFlexAseguradoras] PFA INNER JOIN dbo.ProductosFlex PF  ON (PFA.IdProductoFlex = PF.IdProductoFlex) INNER JOIN[dbo].[CoberturasProductosFlexAseguradoras]  CPFA  ON(PFA.IdProductoFlexAseguradora = CPFA.IdProductoFlexAseguradora)      INNER JOIN[dbo].[Elementos]  Cob    ON(CPFA.IdCobertura = Cob.ElementoID)  INNER JOIN [dbo].[neCoberturasPaquete]  CP ON(PFA.IdAseguradora = CP.AseguradoraID  AND CPFA.IdCobertura = CP.CoberturaID)  INNER JOIN[dbo].[Productos] Pr    ON(PF.ProductoID = Pr.ProductoID)  WHERE   Pr.ProductoID = @ProductoId      AND CP.PaqueteID = @PaqueteId   AND PF.IdTipoVehiculo = @IdTipoVehiculo   AND PF.IdCondicionVehiculo = @IdAntiguedad    AND PF.IdTipoServicioVehiculo = @IdServicio   )COB ON COB.AseguradoraId = NPC.AseguradoraID AND COB.PaqueteId = NPC.PaqueteID AND COB.CoberturaId = NCC.CoberturaID AND COB.ProductoId = NPC.ProductoID  AND NPC.CotizacionID = @CotizacionId AND NPC.Numero = @Numero";
        public static readonly string QryAsegPaquete = "SELECT NP.Nombre AS Aseguradora,El.Nombre AS Paquete,NE.AseguradoraID AS AseguradoraId,NE.PaqueteID AS PaqueteId FROM nePrimasCotizacion AS NE INNER JOIN nePersonas AS NP ON NP.PersonaID = Ne.AseguradoraID INNER JOIN Elementos AS El ON El.ElementoID = NE.PaqueteID WHERE Numero = @Numero";
        public static readonly string QryExisteSubmarca = "SELECT	CONVERT(BIT , COUNT(PF.Submarca)) AS ExisteSubmarca FROM	dbo.ProductosFlex PF	INNER JOIN (SELECT	 ElementoID	AS IdTipoServicio						,Comodin 				FROM	dbo.Elementos 				WHERE	CatalogoID = 105					AND Comodin = @Servicio) Serv		ON (Serv.Comodin = @Servicio)WHERE	ProductoID = @ProductoId		AND IdTipoVehiculo = @IdTipoVehiculo		AND IdCondicionVehiculo =@IdCondicionVehiculo		AND IdTipoServicioVehiculo = Serv.IdTipoServicio	AND Submarca = @Submarca";
        public static readonly string QryClientesDeAgencia = "SELECT clientes.PersonaID AS Id, ISNULL(clientes.Nombre, '') + ISNULL(clientes.Paterno, '') AS Nombre FROM dbo.nePersonas clientes INNER JOIN dbo.RelacionDatos RD ON rd.OpcionA= '1169' AND rd.OpcionB = '1215' AND ISNUMERIC(RD.ValorA)=1 AND RD.ValorA = clientes.PersonaID AND RD.ValorB = @AgenciaId @Where GROUP BY clientes.PersonaID, clientes.Nombre, clientes.Paterno ";
        public static readonly string QryRangosPasajerosVehiculo = "SELECT Rango AS RangoPasajeros FROM RangosPasajerosVehiculos WHERE ClaveVehiculo = '@ClaveVehiculo'";
        public static readonly string QryCoberturaRcEcologica = "SELECT ElementoID FROM dbo.Elementos WHERE CatalogoID = 40 AND IdInterno = '21'";
    }
}