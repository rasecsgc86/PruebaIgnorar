using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Tickets;
using AM45Secure.Commons.Recursos;
using AM45Secure.DataAccess.Entidades.Comunes;
using AM45Secure.DataAccess.Entidades.Tickets;
using AM45Secure.DataAccess.IDataAccess.IGeneric;
using AM45Secure.DataAccess.IDataAccess.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AM45Secure.Commons.Constantes.Querys;
using Zero.Ado.Models;
using Zero.Exceptions;

namespace AM45Secure.DataAccess.DataAccess.Tickets
{
    public class GestionDataAccess : IGestionDataAccess
    {
        private readonly IGenericDataAccess iGenericDataAccess;
        private const int ESTATUS_CERRADO = 6;
        private const int ESTATUS_CANCELADO = 7;
        //private const int PERSONAS_MORALES = 212;
        //private const int PRODUCTO_FLOTILLAS = 118;
        private const int ESTATUS_REGISTRADO = 1;
        private const int ESTATUS_DOCUMENTACION = 5;
        //private const int PRODUCTO_AGENCIA = 1215;
        private const string FILTRO_ESTATUS = "CveEstatus != {0} AND CveEstatus != {1}";
        //private const string FILTRO_LISTA_CLIENTES = "IdIdentificadorFisicaMoral = {0}";
        //private const string FILTRO_AGENCIAS = "OpcionB = {0} AND NombreValorA LIKE '%{1}%'";
        //private const string FILTRO_SI_ES_FLOTILLA = "IdIdentificadorFisicaMoral = {0} AND IdCliente={1} AND IdProducto={2}";
        //private const string FILTRO_CLIENTE = "IdCliente = {0}";
        //private const string FILTRO_CARATULA = "IdCliente = {0} AND VigenciaCaratulaValida=1";
        //private const string FILTRO_TIPO_TICKET = "IdTipoTicket = {0}";
        private const string FILTRO_ESTATUS_TICKET = "CveEstatus = {0} OR CveEstatus = {1}";
        private const string FILTRO_DIAS_INHABILES = "Dia >= '{0}' order by Dia ASC";
        private readonly int DocumEstatus = 5; //Estatus Documentacion
        private readonly int IncompEstatus = 4; //Imcompleto

        public GestionDataAccess(IGenericDataAccess iGenericDataAccess)
        {
            this.iGenericDataAccess = iGenericDataAccess;
        }

        public IList<TicketModel> ConsultarTickest(TicketModel ticketModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<VwTicObtenerInformacionTickets> vwTickets = iGenericDataAccess.Consultar(
                                                                                               new VwTicObtenerInformacionTickets()
                                                                                               {
                                                                                                   PersonaIdTipoUsuarioTicket = ticketModel.GetIdUsuarioSesion()
                                                                                               },
                                                                                               new OptionsQueryZero()
                                                                                               {
                                                                                                   ExcludeNumericsDefaults = true,
                                                                                                   ExcludeBool = true,
                                                                                                   WhereComplementary = string.Format(FILTRO_ESTATUS,
                                                                                                                                      ESTATUS_CERRADO,
                                                                                                                                      ESTATUS_CANCELADO)
                                                                                               });
                iGenericDataAccess.CloseConnection();
                var cont = 1;
                IList<TicketModel> ticketsList = vwTickets.Select(
                                                                  x => new TicketModel()
                                                                       {
                                                                           TicketId = x.TicketId,
                                                                           PersonaId = x.PersonaId,
                                                                           Tipo = x.DescripcionTicket,
                                                                           FechaRecepcion = x.FechaRecepcion,
                                                                           FechaRegistro = x.FechaRegistro,
                                                                           DescripcionTicket = x.Descripcion,
                                                                           NombreCompletoResponsable = x.NombrePer + " " + x.PaternoPer + " " + x.MaternoPer,
                                                                           DescripcionEstatus = x.DescripcionEstatus,
                                                                           ClaveEstatus = x.CveEstatus,
                                                                           UsuarioId = x.UsuarioId,
                                                                           UsuarioSesion = ticketModel.GetIdUsuarioSesion(),
                                                                           //UsuarioId = ticketModel.UsuarioId,
                                                                           NumTicket = cont++,
                                                                           AseguradoraId = x.AseguradoraId,
                                                                           Nombre = (x.AseguradoraId == 0) ? "N/A" : x.Nombre,
                                                                           NombreCliente = x.NombreCliente,
                                                                           Caratula = x.Caratula ?? "N/A",
                                                                           PersonaIdTipoUsuarioTicket = x.PersonaIdTipoUsuarioTicket
                                                                       }).ToList().Where(where => (where.ClaveEstatus != DocumEstatus || where.ClaveEstatus != IncompEstatus && where.PersonaId != where.UsuarioSesion)
                                                                                                  || (where.ClaveEstatus != DocumEstatus || where.ClaveEstatus != IncompEstatus && where.PersonaIdTipoUsuarioTicket != where.UsuarioSesion)).ToList();

                return ticketsList;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_03, e);
            }
        }

        public IList<ClienteProductoModel> ConsultarClientes(ClienteProductoModel clienteProductoModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                //Se cambia para optimizar la consulta.

                IList<VwNueListaClientesProducto> lista = iGenericDataAccess
                    .ExecuteQuery<VwNueListaClientesProducto>(
                                                              "SELECT " +
                                                              "total.Nombre as NombreCliente" +
                                                              ", total.idCliente as IdCliente" +
                                                              " FROM" +
                                                              "(" +
                                                              "   SELECT " +
                                                              "   p.Nombre " +
                                                              "   , ttC.idCliente " +
                                                              "   , ROW_NUMBER() over(order by ttC.idCliente) as RowNumber " +
                                                              "   , RANK()over(order by ttC.idCliente) as Rank " +
                                                              "    FROM dbo.TiposTicketsClientes ttC " +
                                                              "   INNER JOIN( " +
                                                              "        SELECT " +
                                                              "        pD.Valor " +
                                                              "        FROM " +
                                                              "        dbo.PerfilDatos pD " +
                                                              "        Where  ISNUMERIC(pD.Valor) = 1 " +
                                                              "           AND pD.Opcion = 1169 " +
                                                              "           AND pd.PersonaID = " + clienteProductoModel.GetIdUsuarioSesion() + " AND pd.PerfilID =" + clienteProductoModel.GetIdPerfilUsuarioSesion() + " " +
                                                              "        ) as pD " +
                                                              "      ON " +
                                                              "      ttC.IdCliente = pD.Valor " +
                                                              "  INNER JOIN dbo.nePersonas p " +
                                                              "      ON p.PersonaID = pD.Valor and p.Tipo=212 " +
                                                              " ) as total" +
                                                              " where Rank = RowNumber AND total.Nombre LIKE \'%" + clienteProductoModel.NombreCliente + "%\'");

                iGenericDataAccess.CloseConnection();
                IList<ClienteProductoModel> clientesList = lista.Select(
                                                                        x => new ClienteProductoModel()
                                                                             {
                                                                                 IdCliente = x.IdCliente,
                                                                                 NombreCliente = x.NombreCliente
                                                                             }).ToList();
                return clientesList;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_04, e);
            }
        }

        public IList<AgenciasModel> ConsultarAgencias(AgenciasClienteModel agenciasCliente)
        {
            StringBuilder sWhere = new StringBuilder();
            sWhere.Append("Agencia LIKE '%" + agenciasCliente.Agencias.Agencia + "%' ");
            sWhere.Append("AND IdCliente = " + agenciasCliente.Clientes.ClienteId + " ");
            sWhere.Append("ORDER BY Agencia ASC");

            try
            {
                iGenericDataAccess.OpenConnection();
                IList<VwTicSelAgencias> lista = iGenericDataAccess.Consultar(new VwTicSelAgencias(), new OptionsQueryZero()
                                                                                                     {
                                                                                                         ExcludeNumericsDefaults = true,
                                                                                                         ExcludeBool = true,
                                                                                                         WhereComplementary = sWhere.ToString()
                                                                                                     });

                iGenericDataAccess.CloseConnection();
                IList<AgenciasModel> agenciasList = lista.Select(
                                                                 x => new AgenciasModel()
                                                                      {
                                                                          IdAgencia = x.IdAgencia,
                                                                          Agencia = x.Agencia
                                                                      }).ToList();
                return agenciasList;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_05, e);
            }
        }

        public bool ConsultarSiEsClienteFlotillas(ClienteProductoModel clienteProductoModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<VwNueListaClientesProducto> lista = iGenericDataAccess.ExecuteQuery<VwNueListaClientesProducto>("Select " +
                                                                                                                      " COUNT(valorB) Contador " +
                                                                                                                      " from " +
                                                                                                                      " RelacionDatos " +
                                                                                                                      " Where ISNUMERIC(ValorB) = 1 AND ValorB = 118 AND ValorA = " + clienteProductoModel.IdCliente);

                if (lista.Count>0)
                {
                    return lista.FirstOrDefault().Contador>0;
                }
                return false;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_06, e);
            }
        }


        public IList<ClienteProductoModel> ConsultarCaratula(ClienteProductoModel clienteProductoModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<VwTicSelCaratulas> lista = iGenericDataAccess.Consultar(new VwTicSelCaratulas()
                                                                              {
                                                                                  IdCliente = clienteProductoModel.IdCliente
                                                                              }, new OptionsQueryZero()
                                                                                 {
                                                                                     ExcludeNumericsDefaults = true,
                                                                                     ExcludeBool = true
                                                                                 });

                iGenericDataAccess.CloseConnection();
                IList<ClienteProductoModel> clientesList = lista.Select(
                                                                        x => new ClienteProductoModel()
                                                                             {
                                                                                 IdCliente = x.IdCliente,
                                                                                 PolizaCaratula = x.PolizaCaratula,
                                                                                 FormaPago = x.FormaPago,
                                                                                 TipoString = x.TipoVehiculo,
                                                                                 TipoCobranzaString = x.TipoCobranza
                                                                             }).ToList();
                return clientesList;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_07, e);
            }
        }

        public IList<ClienteProductoModel> ConsultarResponsable(ClienteProductoModel clienteProductoModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                //bool clienteFlotilla = ConsultarSiEsClienteFlotillas(clienteProductoModel);
                IList<VwNueListaClientesProducto> lista;

                lista = iGenericDataAccess
                    .ExecuteQuery<VwNueListaClientesProducto>(" SELECT " +
                                                              " TTC.IdCliente IdCliente" +
                                                              " , TTC.IdPersonaResponsable IdResponsable" +
                                                              " , nP.Nombre + '  ' + ISNULL(nP.Paterno, '') + '  ' + ISNULL(nP.Materno, '') NombreCompletoResponsable" +
                                                              " , nP.Mail MailResponsable" +
                                                              " FROM " +
                                                              " TiposTicketsClientes as TTC" +
                                                              " INNER JOIN nePersonas nP" +
                                                              " on  TTC.TipoId = " + clienteProductoModel.IdTipoTicket + " " +
                                                              " AND nP.PersonaID = TTC.IdPersonaResponsable " +
                                                              " AND  TTC.IdCliente = " + clienteProductoModel.IdCliente);

                iGenericDataAccess.CloseConnection();
                IList<ClienteProductoModel> clientesList = lista.Select(
                                                                        x => new ClienteProductoModel()
                                                                             {
                                                                                 IdCliente = x.IdCliente,
                                                                                 IdResponsable = x.IdResponsable,
                                                                                 NombreCompletoResponsable = x.NombreCompletoResponsable,
                                                                                 MailResponsable = x.MailResponsable
                                                                             }).ToList();
                return clientesList;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_08, e);
            }
        }

        public IList<ClienteProductoModel> ConsultarTiposTickets(ClienteProductoModel clienteProductoModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<VwNueListaClientesProducto> lista = iGenericDataAccess.ExecuteQuery<VwNueListaClientesProducto>(
                                                                                                                      CQuerysTickets.QryTiposTickets.Replace(
                                                                                                                                                             "{{IdCliente}}", clienteProductoModel.IdCliente.ToString()
                                                                                                                                                            )
                                                                                                                     );
                iGenericDataAccess.CloseConnection();
                IList<ClienteProductoModel> clientesList = lista.Select(
                                                                        x => new ClienteProductoModel()
                                                                             {
                                                                                 IdCliente = x.IdCliente,
                                                                                 NombreCliente = x.NombreCliente,
                                                                                 IdTipoTicket = x.IdTipoTicket,
                                                                                 DescripcionTipoTicket = x.DescripcionTipoTicket,
                                                                                 HorasAtencion = x.HorasAtencion
                                                                             }).ToList();
                return clientesList;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_09, e);
            }
        }

        public IList<CatOrigenTicketsModel> ConsultarReportaA()
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<CatOrigenTickets> listOrigenTicketsModel = iGenericDataAccess.Consultar(
                                                                                              new CatOrigenTickets(),
                                                                                              new OptionsQueryZero()
                                                                                              {
                                                                                                  ExcludeNumericsDefaults = true,
                                                                                                  ExcludeBool = true,
                                                                                                  ExcludeWhere = true
                                                                                              });
                iGenericDataAccess.CloseConnection();
                IList<CatOrigenTicketsModel> listOrigenTickets = listOrigenTicketsModel.Select(
                                                                                               x => new CatOrigenTicketsModel()
                                                                                                    {
                                                                                                        IdOrigenTicket = x.IdOrigenTicket,
                                                                                                        OrigenTicket = x.OrigenTicket
                                                                                                    }).ToList();
                return listOrigenTickets;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_10, e);
            }
        }

        public IList<CatEstatusTicketsModel> ConsultaEstatusTickets()
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<CatEstatusTickets> listEstatusTicketsModel = iGenericDataAccess.Consultar(
                                                                                                new CatEstatusTickets(),
                                                                                                new OptionsQueryZero()
                                                                                                {
                                                                                                    ExcludeNumericsDefaults = true,
                                                                                                    ExcludeBool = true,
                                                                                                    WhereComplementary = string.Format(FILTRO_ESTATUS_TICKET,
                                                                                                                                       ESTATUS_REGISTRADO,
                                                                                                                                       ESTATUS_DOCUMENTACION)
                                                                                                });
                iGenericDataAccess.CloseConnection();
                IList<CatEstatusTicketsModel> listEstatusTickets = listEstatusTicketsModel.Select(
                                                                                                  x => new CatEstatusTicketsModel()
                                                                                                       {
                                                                                                           IdEstatusTicket = x.IdEstatusTicket,
                                                                                                           CveEstatus = x.CveEstatus,
                                                                                                           Estatus = x.Estatus,
                                                                                                           Descripcion = x.Descripcion,
                                                                                                       }).ToList();
                return listEstatusTickets;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_11, e);
            }
        }

        public TicketModel GuardarTicket(TicketModel ticketModel)
        {
            DateTime fechaActual = DateTime.Now;
            try
            {
                iGenericDataAccess.OpenConnection();
                iGenericDataAccess.BeginTran();
                RegistrosTicket registrosTicket = iGenericDataAccess.Guardar(new RegistrosTicket()
                                                                             {
                                                                                 FechaRegistro = fechaActual,
                                                                                 TipoId = ticketModel.TipoId,
                                                                                 UsuarioId = ticketModel.GetIdUsuarioSesion(),
                                                                                 Descripcion = ticketModel.DescripcionTicket,
                                                                                 IdCaratula = ticketModel.CaratulaId,
                                                                                 IdCliente = ticketModel.PersonaId,
                                                                                 IdOrigenTicket = ticketModel.CatalogoOrigenId == 0 ? null : ticketModel.CatalogoOrigenId,
                                                                                 FechaRecepcion = ticketModel.FechaRecepcion,
                                                                                 NumeroOT = null, //investigar con que dato se llenara
                                                                                 NumeroOTSICS = null, //investigar con que dato se llenara
                                                                                 ResponsableId = ticketModel.ResponsableId,
                                                                                 AseguradoraId = ticketModel.AseguradoraId
                                                                             });

                iGenericDataAccess.Guardar(new TicketsEstatus()
                                           {
                                               TicketId = registrosTicket.TicketId,
                                               IdEstatusTicket = ticketModel.IdEstatusTicket, //Falta obtener el idUsuario de session
                                               PersonaId = ticketModel.PersonaId,
                                               FechaRegistro = fechaActual,
                                               NombreArchivoTicketCerrado = "",
                                               RutaArchivoTicketCerrado = "",
                                               Activo = true //Falta obtener fecha calculada
                                           });

                if (!ticketModel.EsClienteFlotillas)
                {
                    iGenericDataAccess.Guardar(new TicketsDatosContactos()
                                               {
                                                   TicketId = registrosTicket.TicketId,
                                                   IdAgencia = ticketModel.DatosContactoAgenciaId,
                                                   Nombre = ticketModel.DatosContactoNombre,
                                                   Apellidos = ticketModel.DatosContactoApellidos,
                                                   Telefono = ticketModel.DatosContactoTelefonos,
                                                   Email = ticketModel.DatosContactoEmail
                                               });
                }

                foreach (var archivo in ticketModel.Archivos)
                {
                    ArchivosTickets archivosTickets = iGenericDataAccess.BuscarUno(new ArchivosTickets
                                                                                   {
                                                                                       IdArchivoTicket = archivo.IdArchivoTicket,
                                                                                   }, new OptionsQueryZero
                                                                                      {
                                                                                          ExcludeNumericsDefaults = true,
                                                                                          ExcludeBool = true
                                                                                      });

                    iGenericDataAccess.Actualizar(new ArchivosTickets()
                                                  {
                                                      IdArchivoTicket = archivosTickets.IdArchivoTicket,
                                                      TicketId = registrosTicket.TicketId,
                                                      NombreArchivo = archivosTickets.NombreArchivo,
                                                      RutaArchivo = archivosTickets.RutaArchivo,
                                                      IdEstatusTicket = ticketModel.IdEstatusTicket
                                                  });
                }

                if (!string.IsNullOrEmpty(ticketModel.CopiarA))
                {
                    String[] correos = ticketModel.CopiarA.Split(';');
                    foreach (var copiarA in correos)
                    {
                        if (!string.IsNullOrEmpty(copiarA))
                        {
                            iGenericDataAccess.Guardar(new CorreosCopiaTickets()
                                                       {
                                                           TicketId = registrosTicket.TicketId,
                                                           Correo = copiarA
                                                       });
                        }
                    }
                }

                ticketModel.TicketId = registrosTicket.TicketId;
                ticketModel.FechaRegistro = fechaActual;
                ticketModel.FechaRecepcion = registrosTicket.FechaRecepcion;
                iGenericDataAccess.CommitTran();
            }
            catch (DomainException de)
            {
                iGenericDataAccess?.RollbackTran();
                throw new DalException(CodesTickets.ERR_00_12, de);
            }
            catch (Exception e)
            {
                iGenericDataAccess?.RollbackTran();
                iGenericDataAccess?.CloseConnection();
                throw new DalException(CodesCalendario.ERR_07_03, e);
            }
            return ticketModel;
        }

        public ArchivoTicketsModel GuardarArchivo(ArchivoTicketsModel archivoTicketsModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();

                ArchivosTickets archivoTickets = iGenericDataAccess.Guardar(new ArchivosTickets()
                                                                            {
                                                                                NombreArchivo = archivoTicketsModel.NombreArchivo,
                                                                                RutaArchivo = archivoTicketsModel.RutaArchivo
                                                                            });
                iGenericDataAccess.CloseConnection();
                archivoTicketsModel.IdArchivoTicket = archivoTickets.IdArchivoTicket;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_13, e);
            }
            return archivoTicketsModel;
        }

        public bool EliminarArchivo(ArchivoTicketsModel archivoTicketsModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();

                iGenericDataAccess.Eliminar(new ArchivosTickets()
                                            {
                                                IdArchivoTicket = archivoTicketsModel.IdArchivoTicket
                                            });

                iGenericDataAccess.CloseConnection();
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_14, e);
            }
            return true;
        }

        public IList<CatDiasInhabilesModel> ConsultarDiasInhabiles(DateTime fechaRecepcion)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<CatDiasInhabiles> listCatDiasInhabiles = iGenericDataAccess.Consultar(
                                                                                            new CatDiasInhabiles(),
                                                                                            new OptionsQueryZero()
                                                                                            {
                                                                                                ExcludeNumericsDefaults = true,
                                                                                                ExcludeBool = true,
                                                                                                WhereComplementary = string.Format(FILTRO_DIAS_INHABILES, fechaRecepcion.ToString("yyyy-MM-dd"))
                                                                                            });
                iGenericDataAccess.CloseConnection();
                IList<CatDiasInhabilesModel> listCatInhabiles = listCatDiasInhabiles.Select(
                                                                                            x => new CatDiasInhabilesModel()
                                                                                                 {
                                                                                                     IdDiaHabil = x.IdDiaHabil,
                                                                                                     Dia = x.Dia,
                                                                                                     PersonaID = x.PersonaID,
                                                                                                     FechaRegistro = x.FechaRegistro,
                                                                                                 }).ToList();
                return listCatInhabiles;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_15, e);
            }
        }

        public IList<AsegModel> ConsultaAseguradoras(ClienteAsegModel clienteAseg)
        {
            try
            {
                var consulta = new StringBuilder();
                consulta.Append("SELECT AC.AsegId  AS AseguradoraId ");
                consulta.Append(",AC.Aseguradora AS Nombre  ");
                consulta.Append("FROM dbo.nePersonas PPC  ");
                consulta.Append("INNER JOIN (SELECT NC.PersonaID AS ClienteId ");
                consulta.Append(", NC.Nombre AS Cliente    ");
                consulta.Append(", NA.PersonaID AS AsegId   ");
                consulta.Append(", NA.Nombre AS Aseguradora ");
                consulta.Append("FROM dbo.RelacionDatos RD  ");
                consulta.Append("INNER JOIN dbo.nePersonas NA  ");
                consulta.Append("ON RD.OpcionA = 1169 ");
                consulta.Append("AND RD.OpcionB = 2059  ");
                consulta.Append("AND RD.ValorB = CAST(NA.PersonaID  AS VARCHAR(15))  ");
                consulta.Append("INNER JOIN dbo.nePersonas NC ");
                consulta.Append("ON  RD.ValorA = CAST(NC.PersonaID  AS VARCHAR(15))) AC  ");
                consulta.Append("ON AC.ClienteId = PPC.PersonaID ");
                consulta.Append("WHERE AC.ClienteId =  " + clienteAseg.ClienteId);
                iGenericDataAccess.OpenConnection();
                IList<VwAsegModel> lista = iGenericDataAccess.ExecuteQuery<VwAsegModel>(consulta.ToString());
                iGenericDataAccess.CloseConnection();
                IList<AsegModel> listAseg = lista.Select(
                                                         x => new AsegModel()
                                                              {
                                                                  AseguradoraId = x.AseguradoraId,
                                                                  Nombre = x.Nombre
                                                              }).ToList();
                listAseg.Add(new AsegModel()
                             {
                                 AseguradoraId = 0,
                                 Nombre = "NA"
                             });
                return listAseg.OrderBy(x => x.AseguradoraId).ToList();
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_16, e);
            }
        }
    }
}