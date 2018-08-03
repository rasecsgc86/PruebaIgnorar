using System;
using System.Collections.Generic;
using System.Text;
using NotificacionesTickets.Constantes;
using NotificacionesTickets.DataAccess;
using NotificacionesTickets.Exceptions;
using NotificacionesTickets.Models;
using NotificacionesTickets.Utils;

namespace NotificacionesTickets.Business
{
    public class NotificacionesBusiness
    {
        private static NotificacionesBusiness Instance;
        private readonly NotificacionesDataAccess notificacionesDataAccess;

        private NotificacionesBusiness()
        {
            notificacionesDataAccess = NotificacionesDataAccess.GetInstance();
        }

        public static NotificacionesBusiness GetInstance()
        {
            return Instance ?? (Instance = new NotificacionesBusiness());
        }

        public void EjecutarNotificaciones()
        {
            try
            {
                EscalamientoModel escalamiento = new EscalamientoModel();
                List<TicketModel> ticket = notificacionesDataAccess.GetData();

                foreach (TicketModel i in ticket)
                {
                    TicketModel elemento = i;

                    if (elemento.UltimoEscalamiento == 0)
                    {
                        TiempoDeAtencionModel tiempoDeAtencion = CalculaTiempoAtencion(elemento.FechaRecepcion, elemento.HorasAtencion);

                        if (!tiempoDeAtencion.EnTiempo)
                        {
                            PrimerEscalamiento(elemento);
                            escalamiento.TicketId = elemento.TicketId;
                            escalamiento.FechaEscalamiento = DateTime.Now;
                            escalamiento.TipoEscalamiento = 1;
                            notificacionesDataAccess.SetData(escalamiento);
                        }
                    }

                    if (elemento.UltimoEscalamiento == 1)
                    {
                        TiempoDeAtencionModel tiempoDeAtencion = CalculaTiempoAtencion(elemento.FechaRecepcion, elemento.HorasAtencion + elemento.HorasSegundoEscalamiento);

                        if (!tiempoDeAtencion.EnTiempo) // Se verifica que la fecha actual sea mayor a la fecha maxima de entrega de correo
                        {
                            SegundoEscalamiento(elemento);
                            escalamiento.TicketId = elemento.TicketId;
                            escalamiento.FechaEscalamiento = DateTime.Now;
                            escalamiento.TipoEscalamiento = 2;
                            notificacionesDataAccess.SetData(escalamiento);
                        }
                    }
                }
            }
            catch (DataAccessException)
            {
                //throw new DataAccessException(e.ToString());
            }
            catch (MailException)
            {
                //throw new MailException(e.ToString());
            }
            catch (Exception)
            {
                //throw new Exception(e.ToString());
            }
        }

        private void PrimerEscalamiento(TicketModel ticket) // Genera el correo electrónico para el primer escalamiento
        {
            MailModel mail = new MailModel();
            List<string> destinatarios = new List<string>();
            List<string> copiados = new List<string>();
            destinatarios.Add(ticket.MailResponsable);
            copiados.Add(ticket.MailEscalamiento1);
            copiados.Add(ticket.MailUsuario);
            if (ticket.MailDatosContacto != null && !ticket.MailDatosContacto.Equals(string.Empty))
            {
                copiados.Add(ticket.MailDatosContacto);
            }
            copiados.AddRange(notificacionesDataAccess.GetMailsCc(ticket.TicketId));
            mail.MailsCc = copiados;
            mail.MailsTo = destinatarios;
            mail.Subject = ticket.Tipo + " - " + "TICKET " + ticket.TicketId + ConstMail.MailSubjectPrimerEscalamiento;
            mail.Body = BuildPlantilla(false, ticket);
            SendMailUtil.GetInstance().SendMailTickets(mail);
        }

        private void SegundoEscalamiento(TicketModel ticket) // Genera el correo electrónico para el segundo` escalamiento
        {
            MailModel mail = new MailModel();
            List<string> destinatarios = new List<string>();
            List<string> copiados = new List<string>();
            destinatarios.Add(ticket.MailEscalamiento2);
            copiados.Add(ticket.MailEscalamiento1);
            copiados.Add(ticket.MailResponsable);
            copiados.Add(ticket.MailUsuario);
            if (ticket.MailDatosContacto != null && !ticket.MailDatosContacto.Equals(string.Empty))
            {
                copiados.Add(ticket.MailDatosContacto);
            }
            copiados.AddRange(notificacionesDataAccess.GetMailsCc(ticket.TicketId));
            mail.MailsTo = destinatarios;
            mail.MailsCc = copiados;
            mail.Subject = ticket.Tipo + " - " + "TICKET " + ticket.TicketId + ConstMail.MailSubjectSegundoEscalamiento;
            mail.Body = BuildPlantilla(true, ticket);
            SendMailUtil.GetInstance().SendMailTickets(mail);
        }

        private string BuildPlantilla(bool escalamiento, TicketModel ticket) // Genera plantilla HTML para el cuerpo del Correo.
        {
            StringBuilder body = new StringBuilder();
            body.Append("<p style=\"font-family:Arial, Helvetica, sans-serif\"> Estimado(a): <b>{{Usuario}}</b><br/>");
            body.Append("El ticket #{{NoTicket}} ha alcanzado su máximo de atención y requiere de la acción correspondiente</p>");

            body.Append("<table align=\"center\" border=\'1\' width=\"650\"><tr>");
            body.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\"> No.</th>");
            body.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\"> Fecha de Registro </th>");
            body.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\"> Fecha de Recepción </th>");
            body.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\"> Tipo Ticket </th>");
            body.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\"> Descripción </th>");
            body.Append("</tr> <tr>");
            body.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\"> {{NoTicket}} </td>");
            body.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\"> {{FechaRegistro}} </td>");
            body.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\"> {{FechaRecepcion}} </td>");
            body.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\"> {{TipoTicket}} </td>");
            body.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\"> {{Descripción}} </td>");
            body.Append("</tr></table> ");

            if (escalamiento)
                body.Replace("requiere de la acción correspondiente", "no ha sido resuelto en el tiempo asignado al 1er escalamiento . Se requiere tomar acciones para cerrar el ticket.");

            body.Replace("{{Usuario}}", escalamiento ? ticket.NombreEscalamiento2 : ticket.NombreCompletoResponsable);
            body.Replace("{{NoTicket}}", ticket.TicketId.ToString());
            body.Replace("{{FechaRegistro}}", ticket.FechaRegistro.ToString("dd/MM/yy h:mm:ss tt"));
            body.Replace("{{FechaRecepcion}}", ticket.FechaRecepcion.ToString("dd/MM/yy h:mm:ss tt"));
            body.Replace("{{TipoTicket}}", ticket.Tipo);
            body.Replace("{{Descripción}}", ticket.DescripcionTicket);

            return body.ToString();
        }

        public TiempoDeAtencionModel CalculaTiempoAtencion(DateTime fechaRecepcion, int horasAtencion)
        {
            try
            {
                TiempoDeAtencionModel tiempoAtencion = new TiempoDeAtencionModel();

                int totalHoras = 0;
                var diasLaborables = 0;
                var fechaActual = DateTime.Now;
                var horaFechaActual = DateTime.Now.Hour;
                var minutoFechaActual = DateTime.Now.Minute;
                var horaFechaRecepcion = fechaRecepcion.Hour;
                var minutosFechaRecepcion = (fechaRecepcion.Minute == 0) ? 60 : fechaRecepcion.Minute;

                if (fechaRecepcion.Date<=fechaActual.Date)
                {
                    TiempoDeAtencionModel tiempoActualTranscurrido = new TiempoDeAtencionModel();

                    var fechaRecepcionEspejo = fechaRecepcion.AddDays(1);
                    var diasInhabiles = notificacionesDataAccess.ConsultarTotalDiasInhabiles(fechaRecepcion);
                    var diaFechaRecepcion = fechaRecepcion.Date<fechaActual.Date && (horaFechaRecepcion == 8 && (minutosFechaRecepcion == 0 || minutosFechaRecepcion == 60)) ? 1 : 0;
                    var horasFaltantesDeHorario = 0;

                    if (fechaRecepcion.Date != fechaActual.Date)
                    {
                        if (horaFechaRecepcion == 8 && (minutosFechaRecepcion == 0 || minutosFechaRecepcion == 60))
                        {
                            horasFaltantesDeHorario = 0;
                        }
                        else if (horaFechaRecepcion == 8 && minutosFechaRecepcion>0 && minutosFechaRecepcion<60 || horaFechaRecepcion>=8)
                        {
                            horasFaltantesDeHorario = 17 - fechaRecepcion.Hour;
                            horasFaltantesDeHorario = (fechaRecepcion.Minute>0) ? horasFaltantesDeHorario - 1 : horasFaltantesDeHorario;
                        }
                    }

                    while (fechaRecepcionEspejo.Date<fechaActual.Date)
                    {
                        if (fechaRecepcionEspejo.DayOfWeek != DayOfWeek.Saturday && fechaRecepcionEspejo.DayOfWeek != DayOfWeek.Sunday)
                        {
                            diasLaborables++;
                        }
                        fechaRecepcionEspejo = fechaRecepcionEspejo.AddDays(1);
                    }

                    if (notificacionesDataAccess.EsDiaInhabil())
                    {
                        diasLaborables++;
                        diasLaborables = (diasLaborables - diasInhabiles) + diaFechaRecepcion;
                        totalHoras = (fechaRecepcion.Date == fechaActual.Date) ? 0 : horasFaltantesDeHorario;
                    }
                    else
                    {
                        if (fechaActual.DayOfWeek != DayOfWeek.Saturday && fechaActual.DayOfWeek != DayOfWeek.Sunday)
                        {
                            if (fechaRecepcion.Date == fechaActual.Date)
                            {
                                tiempoActualTranscurrido.Dias = (fechaRecepcion.Hour == 8 && fechaRecepcion.Minute == 0 && horaFechaActual>=17) ? 1 : 0;
                            }
                            else
                            {
                                tiempoActualTranscurrido.Dias = horaFechaActual<17 ? 0 : 1;
                            }

                            if ((fechaRecepcion.Date == fechaActual.Date || fechaRecepcion.Date != fechaActual.Date) && horaFechaActual<8)
                            {
                                tiempoActualTranscurrido.Horas = 0;
                            }
                            else if (horaFechaActual == 8 && horaFechaRecepcion == 8 && fechaRecepcion.Minute<=minutoFechaActual)
                            {
                                tiempoActualTranscurrido.Horas = fechaRecepcion.Date != fechaActual.Date ? 1 : 0;
                            }
                            else if ((fechaRecepcion.Date == fechaActual.Date || fechaRecepcion.Date != fechaActual.Date) && horaFechaRecepcion == 8 && minutosFechaRecepcion == 0)
                            {
                                tiempoActualTranscurrido.Horas = (horaFechaActual>=17) ? 0 : horaFechaActual - 8;
                                tiempoActualTranscurrido.Horas = (fechaRecepcion.Date != fechaActual.Date) ? tiempoActualTranscurrido.Horas + 1 : tiempoActualTranscurrido.Horas;
                            }
                            else if (fechaRecepcion.Date == fechaActual.Date && (horaFechaRecepcion == 8 && minutosFechaRecepcion>=0 && minutosFechaRecepcion<60 || horaFechaRecepcion>8))
                            {
                                if (horaFechaActual>17)
                                {
                                    tiempoActualTranscurrido.Horas = (minutosFechaRecepcion>0 && minutosFechaRecepcion<60) ? 17 - horaFechaRecepcion - 1 : 17 - horaFechaRecepcion;
                                }
                                else
                                {
                                    tiempoActualTranscurrido.Horas = (fechaRecepcion.Minute<=minutoFechaActual) ? horaFechaActual - horaFechaRecepcion : horaFechaActual - horaFechaRecepcion - 1;
                                }
                            }
                            else if (fechaRecepcion.Date != fechaActual.Date && (horaFechaRecepcion == 8 && minutosFechaRecepcion>0 && minutosFechaRecepcion<60 || horaFechaRecepcion>8))
                            {
                                if (horaFechaActual>=17)
                                {
                                    tiempoActualTranscurrido.Horas = 0;
                                }
                                else
                                {
                                    tiempoActualTranscurrido.Horas = (minutosFechaRecepcion<=minutoFechaActual) ? horaFechaActual - 7 : horaFechaActual - 8;
                                }
                            }
                            else if (fechaRecepcion.Date != fechaActual.Date && horaFechaActual>=8)
                            {
                                if (horaFechaActual == 8 && minutoFechaActual == 0 || horaFechaActual>17)
                                {
                                    tiempoActualTranscurrido.Horas = 0;
                                }
                                else if (horaFechaActual == 8 && minutoFechaActual != 0)
                                {
                                    tiempoActualTranscurrido.Horas = fechaRecepcion.Minute<=minutoFechaActual ? 1 : 0;
                                }
                                else if (horaFechaActual>=8 && horaFechaActual<=17)
                                {
                                    tiempoActualTranscurrido.Horas = fechaRecepcion.Minute<=minutoFechaActual ? horaFechaActual - 8 : horaFechaActual - 9;
                                }
                            }
                            else if (fechaRecepcion.Date == fechaActual.Date && fechaActual.Hour<17)
                            {
                                tiempoActualTranscurrido.Horas = horaFechaActual - 8;
                            }
                        }
                        else
                        {
                            tiempoActualTranscurrido.Horas = 0;
                            tiempoActualTranscurrido.Dias = 0;
                        }

                        diasLaborables = (tiempoActualTranscurrido.Horas + horasFaltantesDeHorario>=9) ? diasLaborables + 1 : diasLaborables;
                        diasLaborables = (diasLaborables - diasInhabiles) + tiempoActualTranscurrido.Dias + diaFechaRecepcion;
                        totalHoras = (tiempoActualTranscurrido.Horas + horasFaltantesDeHorario>=9) ? (tiempoActualTranscurrido.Horas + horasFaltantesDeHorario) - 9 : tiempoActualTranscurrido.Horas + horasFaltantesDeHorario;
                    }
                }

                var minutos = (horaFechaActual>=17) ? 60 : minutoFechaActual;

                tiempoAtencion.Dias = diasLaborables;
                tiempoAtencion.Horas = totalHoras;
                tiempoAtencion.EnTiempo = horasAtencion>(diasLaborables * 9) + totalHoras || ((horasAtencion == (diasLaborables * 9) + totalHoras) && minutosFechaRecepcion == minutos);

                return tiempoAtencion;
            }
            catch (Exception e)
            {
                throw new Exception("Hubo un error al realizar el conteo de horas", e);
            }
        }
    }
}