using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Tickets;
using AM45Secure.Commons.Recursos;
using AM45Secure.DataAccess.Entidades.Comunes;
using AM45Secure.DataAccess.Entidades.Tickets;
using AM45Secure.DataAccess.IDataAccess.IGeneric;
using AM45Secure.DataAccess.IDataAccess.Tickets;
using Zero.Ado.Models;
using Zero.Exceptions;

namespace AM45Secure.DataAccess.DataAccess.Tickets
{
    public class ConfigurarParametrosTicketsDataAccess : IConfigurarParametrosTicketsDataAccess
    {
        private readonly IGenericDataAccess iGenericDataAccess;


        public ConfigurarParametrosTicketsDataAccess(IGenericDataAccess iGenericDataAccess)
        {
            this.iGenericDataAccess = iGenericDataAccess;
        }

        public TiposTicketsClientesModel GuardarTiposTicketsClientes(TiposTicketsClientesModel tiposTicketsClientesModel)
        {
            TiposTicketsClientesModel model = new TiposTicketsClientesModel();
            try
            {
                iGenericDataAccess.OpenConnection();
                TiposTicketsClientes ticketsClientes = new TiposTicketsClientes
                                                       {
                                                           IdCliente = tiposTicketsClientesModel.IdCliente,
                                                           IdPersonaResponsable = tiposTicketsClientesModel.IdPersonaResponsable,
                                                           IdPersonaEscalamiento1 = tiposTicketsClientesModel.IdPersonaEscalamiento1,
                                                           IdPersonaEscalamiento2 = tiposTicketsClientesModel.IdPersonaEscalamiento2,
                                                           HorasAtencion = tiposTicketsClientesModel.HorasAtencion,
                                                           HorasSegundoEscalamiento = tiposTicketsClientesModel.HorasSegundoEscalamiento
                                                       };

                StringBuilder findSQL = new StringBuilder();
                findSQL.Append(" SELECT COUNT(tt.TipoId) TiempoAtencion ");
                findSQL.Append(" FROM dbo.TiposTicket tt ");
                findSQL.Append(" INNER JOIN dbo.TiposTicketsClientes ttc ");
                findSQL.Append(" ON ttc.TipoId = tt.TipoId ");
                findSQL.Append(" AND ttc.IdCliente = " + tiposTicketsClientesModel.IdCliente);
                findSQL.Append(" AND tt.Descripcion = '" + tiposTicketsClientesModel.TiposTicket.Descripcion + "'");

                TiposTicket findTiposTickets = iGenericDataAccess.ExecuteQuery<TiposTicket>(findSQL.ToString())[0];

                if (findTiposTickets.TiempoAtencion>0)
                {
                    throw new DomainException(CodesConfigParamTickets.INF_08_01);
                }

                TiposTicket tt = new TiposTicket
                                 {
                                     Descripcion = tiposTicketsClientesModel.TiposTicket.Descripcion,
                                     TiempoAtencion = tiposTicketsClientesModel.TiposTicket.TiempoAtencion,
                                     Activa = true
                                 };
                TiposTicket saveTicket = iGenericDataAccess.Guardar(tt);
                ticketsClientes.TipoId = saveTicket.TipoId;
                TiposTicketsClientes saveTiposTicketsClientes = iGenericDataAccess.Guardar(ticketsClientes);
                model.IdCliente = saveTiposTicketsClientes.TipoId;
                iGenericDataAccess.CloseConnection();
                return model;
            }
            catch (DomainException e)
            {
                throw new DomainException(e.Mensaje);
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesConfigParamTickets.ERR_08_03, e);
            }
        }

        public TiposTicketsClientesModel ActulizarTiposTicketsClientes(TiposTicketsClientesModel tiposTicketsClientesModel)
        {
            TiposTicketsClientesModel model = new TiposTicketsClientesModel();
            try
            {
                iGenericDataAccess.OpenConnection();

                TiposTicket findTiposTicket = iGenericDataAccess.BuscarUno(new TiposTicket()
                                                                           {
                                                                               TipoId = tiposTicketsClientesModel.TipoId
                                                                           },
                                                                           new OptionsQueryZero()
                                                                           {
                                                                               ExcludeNumericsDefaults = true
                                                                           });
                if (findTiposTicket != null)
                {
                    if (!findTiposTicket.Descripcion.Equals(tiposTicketsClientesModel.TiposTicket.Descripcion))
                    {
                        StringBuilder findSQL = new StringBuilder();
                        findSQL.Append(" SELECT COUNT(tt.TipoId) TiempoAtencion ");
                        findSQL.Append(" FROM dbo.TiposTicket tt ");
                        findSQL.Append(" INNER JOIN dbo.TiposTicketsClientes ttc ");
                        findSQL.Append(" ON ttc.TipoId = tt.TipoId ");
                        findSQL.Append(" AND ttc.IdCliente = " + tiposTicketsClientesModel.IdCliente);
                        findSQL.Append(" AND tt.Descripcion = '" + tiposTicketsClientesModel.TiposTicket.Descripcion + "'");

                        TiposTicket findTiposTicketsDos = iGenericDataAccess.ExecuteQuery<TiposTicket>(findSQL.ToString())[0];

                        if (findTiposTicketsDos.TiempoAtencion>0)
                        {
                            throw new DalException(CodesConfigParamTickets.INF_08_01);
                        }
                    }
                }

                TiposTicket tt = new TiposTicket
                                 {
                                     TipoId = tiposTicketsClientesModel.TipoId,
                                     Descripcion = tiposTicketsClientesModel.TiposTicket.Descripcion,
                                     TiempoAtencion = tiposTicketsClientesModel.TiposTicket.TiempoAtencion,
                                     Activa = true
                                 };
                TiposTicket saveTicket = iGenericDataAccess.Actualizar(tt);
                StringBuilder sqlUpdate = new StringBuilder();
                sqlUpdate.Append("UPDATE TiposTicketsClientes ");
                sqlUpdate.Append(" SET IdPersonaResponsable = " + tiposTicketsClientesModel.IdPersonaResponsable + ",");
                sqlUpdate.Append(" IdPersonaEscalamiento1 = " + tiposTicketsClientesModel.IdPersonaEscalamiento1 + ",");
                sqlUpdate.Append(" IdPersonaEscalamiento2 = " + tiposTicketsClientesModel.IdPersonaEscalamiento2 + ",");
                sqlUpdate.Append(" HorasAtencion = " + tiposTicketsClientesModel.HorasAtencion + ",");
                sqlUpdate.Append(" HorasSegundoEscalamiento = " + tiposTicketsClientesModel.HorasSegundoEscalamiento);
                sqlUpdate.Append(" WHERE TipoId = " + saveTicket.TipoId);
                sqlUpdate.Append(" AND IdCliente = " + tiposTicketsClientesModel.IdCliente);
                iGenericDataAccess.ExecuteSql(sqlUpdate.ToString());
                model.TipoId = saveTicket.TipoId;
                model.IdCliente = tiposTicketsClientesModel.IdCliente;
                iGenericDataAccess.CloseConnection();
                return model;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesConfigParamTickets.ERR_08_03, e);
            }
        }

        public IList<ConfigurarParametrosTicketsModelo> ConsultarConfigurarParametros(
            ConfigurarParametrosTicketsModelo configurarParametros)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<VwTicConsultarParametrosTicketsCliente> vwParametrosTickets =
                    iGenericDataAccess.Consultar(
                                                 new VwTicConsultarParametrosTicketsCliente()
                                                 {
                                                     IdCliente = configurarParametros.IdCliente
                                                 },
                                                 new OptionsQueryZero()
                                                 {
                                                     ExcludeNumericsDefaults = true,
                                                     ExcludeBool = true
                                                 });
                iGenericDataAccess.CloseConnection();
                IList<ConfigurarParametrosTicketsModelo> listConfParamTickets =
                    vwParametrosTickets.Select(
                                               x => new ConfigurarParametrosTicketsModelo()
                                                    {
                                                        TipoId = x.TipoId,
                                                        Descripcion = x.Descripcion,
                                                        PersonaResponsable = x.PersonaResponsable,
                                                        HorasAtencion = x.HorasAtencion,
                                                        HorasSegundoEscalamiento = x.HorasSegundoEscalamiento,
                                                        PersonaEscalamiento1 = x.PersonaEscalamiento1,
                                                        PersonaEscalamiento2 = x.PersonaEscalamiento2,
                                                        IdCliente = x.IdCliente,
                                                        IdPersonaResponsable = x.IdPersonaResponsable,
                                                        IdPersonaEscalamiento1 = x.IdPersonaEscalamiento1,
                                                        IdPersonaEscalamiento2 = x.IdPersonaEscalamiento2,
                                                        Mail = x.Mail,
                                                        MailEscalamiento1 = x.MailEscalamiento1,
                                                        MailEscalamiento2 = x.MailEscalamiento2
                                                    }
                                              ).ToList();
                return listConfParamTickets;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesConfigParamTickets.ERR_08_02, e);
            }
        }

        public bool EliminarTipoTicketsCliente(TiposTicketModel tiposTicket)
        {
            try
            {
                iGenericDataAccess.OpenConnection();

                TiposTicket tipos = iGenericDataAccess.BuscarUno(new TiposTicket
                                                                 {
                                                                     TipoId = tiposTicket.TipoId
                                                                 },
                                                                 new OptionsQueryZero
                                                                 {
                                                                     ExcludeNumericsDefaults = true,
                                                                     ExcludeBool = true
                                                                 });

                iGenericDataAccess.Actualizar(new TiposTicket
                                              {
                                                  TipoId = tipos.TipoId,
                                                  Descripcion = tipos.Descripcion,
                                                  TiempoAtencion = tipos.TiempoAtencion,
                                                  Activa = false
                                              });

                iGenericDataAccess.CloseConnection();
                return true;
            }
            catch (DomainException e)
            {
                throw new DalException(CodesConfigParamTickets.INF_08_03, e);
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesConfigParamTickets.ERR_08_04, e);
            }
        }

        public IList<ClienteProductoModel> ConsultarClientesConfigurarParametros(ClienteProductoModel clienteProductoModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<VwNueListaClientesProducto> lista = iGenericDataAccess.ExecuteQuery<VwNueListaClientesProducto>("SELECT " +
                                                                                                                      " total.Nombre as NombreCliente " +
                                                                                                                      " , total.idCliente as IdCliente " +
                                                                                                                      " FROM " +
                                                                                                                      " ( " +
                                                                                                                      " SELECT " +
                                                                                                                      "  p.Nombre " +
                                                                                                                      " , pD.idCliente " +
                                                                                                                      " , ROW_NUMBER() over(order by pD.idCliente) as RowNumber " +
                                                                                                                      " , RANK()over(order by pD.idCliente) as Rank " +
                                                                                                                      " FROM( " +
                                                                                                                      "        SELECT " +
                                                                                                                      "       p.Nombre " +
                                                                                                                      "      , p.PersonaID " +
                                                                                                                      "     , p.Tipo " +
                                                                                                                      " From dbo.nePersonas p " +
                                                                                                                      "    WHERE p.Tipo = 212 AND Nombre LIKE '%" + clienteProductoModel.NombreCliente + "%' " +
                                                                                                                      " ) as p " +
                                                                                                                      " INNER JOIN( " +
                                                                                                                      " SELECT " +
                                                                                                                      " pD.Valor as idCliente " +
                                                                                                                      " FROM " +
                                                                                                                      " dbo.PerfilDatos pD " +
                                                                                                                      " Where  ISNUMERIC(pD.Valor) = 1 " +
                                                                                                                      "  AND pD.Opcion = 1169 " +
                                                                                                                      " AND pD.PerfilID =  " + clienteProductoModel.GetIdPerfilUsuarioSesion() +
                                                                                                                      "     AND pD.PersonaID = " + clienteProductoModel.GetIdUsuarioSesion() + " " +
                                                                                                                      " ) as pD " +
                                                                                                                      " ON p.PersonaID = pD.idCliente " +
                                                                                                                      " ) as total " +
                                                                                                                      " where Rank = RowNumber ");

                iGenericDataAccess.CloseConnection();
                IList<ClienteProductoModel> listClienList = lista.Select(x => new ClienteProductoModel()
                                                                              {
                                                                                  IdCliente = x.IdCliente,
                                                                                  NombreCliente = x.NombreCliente
                                                                              }).ToList();
                return listClienList;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesConfigParamTickets.ERR_08_02, e);
            }
        }

        public IList<PersonaResponsableModel> BuscarUsuarioResponsable(PersonaResponsableModel personaResponsableModel)
        {
            try
            {
                StringBuilder filtro = new StringBuilder();
                filtro.Append(" Responsable LIKE '%" + personaResponsableModel.Nombre + "%'");

                iGenericDataAccess.OpenConnection();
                IList<VwTicConsultaResponsables> vwTicConsultaResponsableses = iGenericDataAccess.Consultar(new VwTicConsultaResponsables(),
                                                                                                            new OptionsQueryZero
                                                                                                            {
                                                                                                                ExcludeNumericsDefaults = true,
                                                                                                                ExcludeBool = true,
                                                                                                                WhereComplementary = filtro.ToString()
                                                                                                            });
                iGenericDataAccess.CloseConnection();

                IList<PersonaResponsableModel> listPersonaResponsable = vwTicConsultaResponsableses.Select(x => new PersonaResponsableModel()
                                                                                                                {
                                                                                                                    PersonaId = x.IdResponsable,
                                                                                                                    Nombre = x.Responsable,
                                                                                                                    Mail = x.Mail,
                                                                                                                    MailResponsable = x.Mail,
                                                                                                                    MailEscalemiento1 = x.Mail,
                                                                                                                    MailEscalamiento2 = x.Mail
                                                                                                                }).ToList();
                return listPersonaResponsable;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesConfigParamTickets.ERR_08_02, e);
            }
        }
    }
}