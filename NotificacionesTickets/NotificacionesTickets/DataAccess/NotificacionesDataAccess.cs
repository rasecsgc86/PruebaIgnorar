using System;
using System.Collections.Generic;
using System.Data;
using NotificacionesTickets.Constantes;
using NotificacionesTickets.Exceptions;
using NotificacionesTickets.Models;

namespace NotificacionesTickets.DataAccess
{
    public class NotificacionesDataAccess
    {
        private static NotificacionesDataAccess Instance;
        private readonly DataAccess dataAccess;
        private DataSet dataSet;

        private NotificacionesDataAccess()
        {
            dataAccess = DataAccess.GetInstance();
        }

        public static NotificacionesDataAccess GetInstance()
        {
            return Instance ?? (Instance = new NotificacionesDataAccess());
        }

        public TicketModel RegistrosEscalamiento(int registro)
        {
            TicketModel ticket = new TicketModel();
            dataAccess.OpenConnection();
            var queryFinal = ConstQuerys.GetReporte.Replace("{r}", registro.ToString());
            dataAccess.ExecuteQuery(queryFinal);
            dataAccess.CloseConeccion();

            foreach (DataRow elemento in dataSet.Tables["EscalamientosTickets"].Rows)
            {
                ticket.IdEscalamientoTicket = int.Parse(elemento["IdEscalamientoTicket"].ToString());
                ticket.TicketId = int.Parse(elemento["TicketId"].ToString());
                ticket.FechaEscalamiento = elemento["FechaEscalamiento"].ToString();
                ticket.TipoEscalamiento = int.Parse(elemento["TipoEscalamiento"].ToString());
            }
            return ticket;
        }

        public List<TicketModel> GetData()
        {
            List<TicketModel> ticketList = new List<TicketModel>();
            try
            {
                dataAccess.OpenConnection();
                dataSet = dataAccess.ExecuteQuery(ConstQuerys.GetStatus);

                foreach (DataRow elemento in dataSet.Tables[0].Rows)
                {
                    TicketModel ticket = new TicketModel
                                         {
                                             TicketId = Int32.Parse(elemento["TicketId"].ToString()),
                                             PersonaId = Int32.Parse(elemento["PersonaID"].ToString()),
                                             Tipo = elemento["Tipo"].ToString(),
                                             FechaRegistro = DateTime.Parse(elemento["FechaRegistro"].ToString()),
                                             FechaRecepcion = DateTime.Parse(elemento["FechaRecepcion"].ToString()),
                                             DescripcionTicket = elemento["DescripcionTicket"].ToString(),
                                             NombreCompletoResponsable = elemento["NombreCompletoResponsable"].ToString(),
                                             DescripcionEstatus = elemento["DescripcionEstatus"].ToString(),
                                             MailResponsable = elemento["MailResponsable"].ToString(),
                                             HorasAtencion = Int32.Parse(elemento["HorasAtencion"].ToString()),
                                             HorasSegundoEscalamiento = Int32.Parse(elemento["HorasSegundoEscalamiento"].ToString()),
                                             IdPersonaEscalamiento1 = Int32.Parse(elemento["IdPersonaEscalamiento1"].ToString()),
                                             IdPersonaEscalamiento2 = Int32.Parse(elemento["IdPersonaEscalamiento2"].ToString()),
                                             NombreEscalamiento1 = elemento["NombreEscalamiento1"].ToString(),
                                             MailEscalamiento1 = elemento["MailEscalamiento1"].ToString(),
                                             NombreEscalamiento2 = elemento["NombreEscalamiento2"].ToString(),
                                             MailEscalamiento2 = elemento["MailEscalamiento2"].ToString(),
                                             UltimoEscalamiento = Int32.Parse(elemento["UltimoEscalamiento"].ToString()),
                                             MailUsuario = elemento["MailUsuario"].ToString(),
                                             MailDatosContacto = elemento["MailDatosContacto"].ToString()
                                         };
                    ticketList.Add(ticket);
                }
                dataAccess.CloseConeccion();

                return ticketList;
            }
            catch (DataAccessException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DataAccessException("Error:", e);
            }
        }


        public int ConsultarTotalDiasInhabiles(TicketModel ticket, DateTime actual)
        {
            int cantidad = 0;
            try
            {
                dataAccess.OpenConnection();
                string queryFinal = ConstQuerys.GetTotalInhabiles.Replace("{FechaRecepcion}", "'" + ticket.FechaRecepcion.ToString(("yyyy-MM-dd hh:mm:ss")) + "'");
                DataSet inhabiles = dataAccess.ExecuteQuery(queryFinal);

                foreach (DataRow elemento in inhabiles.Tables[0].Rows)
                {
                    cantidad = ticket.CantidadFinesSemana = Int32.Parse(elemento["totalInhabiles"].ToString());
                }
                dataAccess.CloseConeccion();

                return cantidad;
            }
            catch (Exception e)
            {
                throw new DataAccessException("Error:::", e);
            }
        }

        public void SetData(EscalamientoModel escalamiento)
        {
            try
            {
                dataAccess.OpenConnection();
                string queryFinal = ConstQuerys.SetReporte.Replace("{TI}, {FE}, {TE}", escalamiento.TicketId + " , " + "CONVERT(DATETIME,'" + escalamiento.FechaEscalamiento.ToString("yyyy/MM/dd HH:mm:mm.ss") + "', 120)" + " , " + escalamiento.TipoEscalamiento);
                //UPDATE DE ESCALAMIENTO
                dataAccess.ExecuteCommand(queryFinal);
            }
            catch (DataAccessException dal)
            {
                throw new DataAccessException("Error: ", dal);
            }
            catch (Exception e)
            {
                throw new DataAccessException("Error:::", e);
            }
        }

        public List<string> GetMailsCc(int ticketId)
        {
            try
            {
                dataAccess.OpenConnection();
                dataSet = dataAccess.ExecuteQuery(ConstQuerys.GetMailsCc + ticketId);

                List<string> mails = new List<string>();
                foreach (DataRow elemento in dataSet.Tables[0].Rows)
                {
                    mails.Add(elemento["Correo"].ToString());
                }
                dataAccess.CloseConeccion();
                return mails;
            }
            catch (DataAccessException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DataAccessException("Error:", e);
            }
        }

        public bool EsDiaInhabil()
        {
            try
            {
                dataAccess.OpenConnection();
                DataSet esInhabil = dataAccess.ExecuteQuery(ConstQuerys.GetHoyDiaInhabil);
                dataAccess.CloseConeccion();

                return esInhabil.Tables[0].Rows.Count>0;
            }
            catch (Exception e)
            {
                dataAccess.CloseConeccion();
                throw new Exception("Hubo un error al consultar dia inhabil", e);
            }
        }

        public int ConsultarTotalDiasInhabiles(DateTime fechaRecepcion)
        {
            try
            {
                CatDiasHabilesEntidad inhabiles = new CatDiasHabilesEntidad
                                                  {
                                                      TotalInhabiles = 0
                                                  };

                string queryFinal = ConstQuerys.GetTotalDiasInhabiles;
                dataAccess.OpenConnection();
                queryFinal = queryFinal.Replace("{FechaRecepcion}", "'" + fechaRecepcion.ToString(("yyyy-MM-dd hh:mm:ss")) + "'");
                DataSet diasInhabiles = dataAccess.ExecuteQuery(queryFinal);
                dataAccess.CloseConeccion();

                foreach (DataRow elemento in diasInhabiles.Tables[0].Rows)
                {
                    inhabiles.TotalInhabiles = int.Parse(elemento["totalInhabiles"].ToString());
                }

                return inhabiles.TotalInhabiles;
            }
            catch (Exception e)
            {
                throw new Exception("Error:::", e);
            }
        }
    }
}
