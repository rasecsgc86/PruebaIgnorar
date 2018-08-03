using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AM45Secure.Business.IBusiness.Tickets;
using AM45Secure.Commons.Modelos.Tickets;
using AM45Secure.Commons.Recursos;
using AM45Secure.DataAccess.IDataAccess.Tickets;
using Zero.Exceptions;
using Zero.Handlers.Response;
using AM45Secure.Commons.Modelos.Comunes;
using System.Text;
using System.Web.Configuration;
using AM45Secure.Commons.Utils;
using AM45Secure.DataAccess.Entidades.Comunes;

namespace AM45Secure.Business.Business.Tickets
{
    public class SeguimientoTicketsBusiness : ISeguimientoTicketsBusiness
    {
        private readonly ISeguimientoTicketsDataAccess iSeguimientoTicketsDataAccess;
        private readonly IGestionBussiness iGestionBussiness;
        private readonly int IMCOMPLETO = 4;
        private readonly int REGISTRADO = 1;

        public SeguimientoTicketsBusiness(ISeguimientoTicketsDataAccess iSeguimientoTicketsDataAccess, IGestionBussiness iGestionBussiness)
        {
            this.iSeguimientoTicketsDataAccess = iSeguimientoTicketsDataAccess;
            this.iGestionBussiness = iGestionBussiness;
        }

        public SingleResponse<SeguiminetoTicketsModel> ConsultarInformacionTicket(SeguiminetoTicketsModel seguiminetoTicketsModel)
        {
            SingleResponse<SeguiminetoTicketsModel> response = new SingleResponse<SeguiminetoTicketsModel>();
            try
            {
                SeguiminetoTicketsModel model = iSeguimientoTicketsDataAccess.ConsultarInformacionTicket(seguiminetoTicketsModel);
                response.Done(model, string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesConfigParamTickets.ERR_08_03, e));
            }
            return response;
        }

        public SingleResponse<SeguiminetoTicketsModel> ConsultarInformacionTicketLectura(SeguiminetoTicketsModel seguiminetoTicketsModel)
        {
            SingleResponse<SeguiminetoTicketsModel> response = new SingleResponse<SeguiminetoTicketsModel>();
            try
            {
                SeguiminetoTicketsModel model = iSeguimientoTicketsDataAccess.ConsultarInformacionTicketLectura(seguiminetoTicketsModel);
                response.Done(model, string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesConfigParamTickets.ERR_08_03, e));
            }
            return response;
        }

        public SingleResponse<IList<CatEstatusTicketsModel>> ObetnerEstatusByUsuario(SeguiminetoTicketsModel seguiminetoTicketsModel)
        {
            SingleResponse<IList<CatEstatusTicketsModel>> response = new SingleResponse<IList<CatEstatusTicketsModel>>();
            try
            {
                /**
            * 1	Registrado
            * 2	Proceso
            * 3	En trámite
            * 4	Incompleto
            * 5	Documentación
            * 6	Cerrado
            * 7	Cancelado
            * Si el usuario firmado es el mismo que levanto al ticket y el estatus actual es “Incompleto - 1 ” o “Documentación - 5”, 
            * la lista se deberá llenar con los Estatus: Registrado y Cancelado. 
            * Si el estatus actual es cualquier otro solo se deberá llenar la lista con la opción Cancelado.
            */
                IList<CatEstatusTicketsModel> model = iSeguimientoTicketsDataAccess.ObetnerEstatusByUsuario(seguiminetoTicketsModel);
                SeguiminetoTicketsModel res = ConsultarInformacionTicketLectura(seguiminetoTicketsModel).Response;
                if (res != null)
                {
                    if (res.TipoUsuario == "Duenio" &&
                        (res.CveEstatus == 4
                         || res.CveEstatus == 5))
                    {
                        CatEstatusTicketsModel obj2 = model.Single(r => r.CveEstatus == 2);
                        model.Remove(obj2);
                        CatEstatusTicketsModel obj3 = model.Single(r => r.CveEstatus == 3);
                        model.Remove(obj3);
                        CatEstatusTicketsModel obj4 = model.Single(r => r.CveEstatus == 4);
                        model.Remove(obj4);
                        CatEstatusTicketsModel obj5 = model.Single(r => r.CveEstatus == 5);
                        model.Remove(obj5);
                        CatEstatusTicketsModel obj6 = model.Single(r => r.CveEstatus == 6);
                        model.Remove(obj6);
                    }
                    else if (res.TipoUsuario == "Duenio" &&
                             (res.CveEstatus != 4
                              && res.CveEstatus != 5))
                    {
                        CatEstatusTicketsModel obj1 = model.Single(r => r.CveEstatus == 1);
                        model.Remove(obj1);
                        CatEstatusTicketsModel obj2 = model.Single(r => r.CveEstatus == 2);
                        model.Remove(obj2);
                        CatEstatusTicketsModel obj3 = model.Single(r => r.CveEstatus == 3);
                        model.Remove(obj3);
                        CatEstatusTicketsModel obj4 = model.Single(r => r.CveEstatus == 4);
                        model.Remove(obj4);
                        CatEstatusTicketsModel obj5 = model.Single(r => r.CveEstatus == 5);
                        model.Remove(obj5);
                        CatEstatusTicketsModel obj6 = model.Single(r => r.CveEstatus == 6);
                        model.Remove(obj6);
                    }

                    // Si el usuario firmado es el Responsable, 
                    //la lista se deberá llenar con los estatus: Incompleto, En Trámite y Cancelado, no importando el estatus actual del ticket.
                    /**
                * 1	Registrado
                * 2	Proceso
                * 3	En trámite
                * 4	Incompleto
                * 5	Documentación
                * 6	Cerrado
                * 7	Cancelado
                */
                    if (res.TipoUsuario == "Responsable" ||
                        res.TipoUsuario == "Escalamiento")
                    {
                        CatEstatusTicketsModel obj2 = model.Single(r => r.CveEstatus == 1);
                        model.Remove(obj2);
                        CatEstatusTicketsModel obj3 = model.Single(r => r.CveEstatus == 2);
                        model.Remove(obj3);
                        CatEstatusTicketsModel obj4 = model.Single(r => r.CveEstatus == 5);
                        model.Remove(obj4);
                        //CatEstatusTicketsModel obj5 = model.Single(r => r.CveEstatus == 6);
                        //model.Remove(obj5);
                    }
                }

                response.Done(model, string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesConfigParamTickets.ERR_08_03, e));
            }
            return response;
        }

        public SingleResponse<ComentariosTicketModel> GuardarComentariosTicket(ComentariosTicketModel comentariosTicketModel)
        {
            SingleResponse<ComentariosTicketModel> response = new SingleResponse<ComentariosTicketModel>();

            try
            {
                if (!comentariosTicketModel.Cerrado)
                {
                    TicketsEstatusModel ticketsEstatus = new TicketsEstatusModel
                                                         {
                                                             TicketId = comentariosTicketModel.TicketId
                                                         };

                    ticketsEstatus = ValidaEstatusTicket(ticketsEstatus)[0];

                    if (ticketsEstatus.Activo)
                    {
                        throw new DomainException(CodesSeguiminetorTickets.ERR_00_02 + ticketsEstatus.Estatus);
                    }
                }

                TicketModel registroCorrecto = iSeguimientoTicketsDataAccess.BuscarTicketSeguimiento(comentariosTicketModel);

                if (!comentariosTicketModel.Cerrado)
                {
                    if (comentariosTicketModel.CveEstatus == REGISTRADO)
                    {
                        registroCorrecto.FechaRegistro = DateTime.Now;
                        registroCorrecto.FechaRecepcion = iGestionBussiness.CalculoFechaRecepcion();
                        comentariosTicketModel.TicketModelUpdate = registroCorrecto;

                        if (comentariosTicketModel.CveEstatus == REGISTRADO)
                        {
                            ConsultarDatosCorreoModel consultarDatosCorreoModel = iSeguimientoTicketsDataAccess.ObtenerDatosCorreo(comentariosTicketModel.TicketId);
                            try
                            {
                                EnviarMailRegistro(registroCorrecto, consultarDatosCorreoModel);
                            }
                            catch (Exception e)
                            {
                                throw new DomainException(Codes.ERR_00_00, e);
                            }
                        }
                    }
                }

                comentariosTicketModel.TicketModelUpdate = registroCorrecto;
                ComentariosTicketModel model = iSeguimientoTicketsDataAccess.GuardarComentariosTicket(comentariosTicketModel);
                response.Done(model, string.Empty);

                if (!comentariosTicketModel.Cerrado)
                {
                    if (comentariosTicketModel.CveEstatus == IMCOMPLETO)
                    {
                        ConsultarDatosCorreoModel consultarDatosCorreoModel = iSeguimientoTicketsDataAccess.ObtenerDatosCorreo(comentariosTicketModel.TicketId);
                        if (model.ComentarioId>0)
                        {
                            try
                            {
                                EnviarMail(consultarDatosCorreoModel, comentariosTicketModel.Comentario);
                            }
                            catch (Exception)
                            {
                                throw new DomainException(CodesTickets.ERR_00_18);
                            }
                        }
                    }
                }

                if (comentariosTicketModel.Cerrado)
                {
                    ConsultarDatosCorreoModel consultarDatosCorreoModel = iSeguimientoTicketsDataAccess.ObtenerDatosCorreo(comentariosTicketModel.TicketId);
                    if (consultarDatosCorreoModel != null)
                    {
                        try
                        {
                            EnviarMailCerrar(consultarDatosCorreoModel, comentariosTicketModel);
                        }
                        catch (Exception)
                        {
                            throw new DomainException(CodesTickets.ERR_00_18);
                        }
                    }
                }
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesConfigParamTickets.ERR_08_03, e));
            }
            return response;
        }

        public SingleResponse<IList<ComentariosTicketModel>> ListarComentariosTicket(ComentariosTicketModel comentariosTicketModel)
        {
            SingleResponse<IList<ComentariosTicketModel>> response = new SingleResponse<IList<ComentariosTicketModel>>();
            try
            {
                IList<ComentariosTicketModel> lista = iSeguimientoTicketsDataAccess.ListarComentariosTicket(comentariosTicketModel);
                response.Done(lista, string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesConfigParamTickets.ERR_08_03, e));
            }
            return response;
        }

        public SingleResponse<IList<ArchivoTicketsModel>> ListaArchivosTickets(SeguiminetoTicketsModel seguiminetoTicketsModel)
        {
            SingleResponse<IList<ArchivoTicketsModel>> response = new SingleResponse<IList<ArchivoTicketsModel>>();
            try
            {
                IList<ArchivoTicketsModel> lista = iSeguimientoTicketsDataAccess.ListaArchivosTickets(seguiminetoTicketsModel);
                response.Done(lista, string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesConfigParamTickets.ERR_08_03, e));
            }
            return response;
        }

        public SingleResponse<bool> EliminarArchivo(ArchivoTicketsModel archivoTicketsModel)
        {
            SingleResponse<bool> response = new SingleResponse<bool>();
            try
            {
                TicketsEstatusModel ticketsEstatus = new TicketsEstatusModel
                                                     {
                                                         TicketId = archivoTicketsModel.TicketId
                                                     };

                ticketsEstatus = ValidaEstatusTicket(ticketsEstatus)[0];

                if (ticketsEstatus.Activo)
                {
                    throw new DomainException(CodesSeguiminetorTickets.ERR_00_02 + ticketsEstatus.Estatus);
                }

                bool archivoEliminado = iSeguimientoTicketsDataAccess.EliminarArchivo(archivoTicketsModel);
                response.Done(archivoEliminado, "Archivo Eliminado Correctamente");
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesCalendario.ERR_07_03, e));
            }
            return response;
        }

        public SingleResponse<ArchivoTicketsModel> GuardarArchivoSeguimiento(ArchivoTicketsModel archivoTicketsModel)
        {
            SingleResponse<ArchivoTicketsModel> response = new SingleResponse<ArchivoTicketsModel>();
            try
            {
                ArchivoTicketsModel archivoModel = iSeguimientoTicketsDataAccess.GuardarArchivoSeguimiento(archivoTicketsModel);
                response.Done(archivoModel, string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesCalendario.ERR_07_03, e));
            }
            return response;
        }

        private void EnviarMail(ConsultarDatosCorreoModel consultarDatosCorreoModel, string comentario)
        {
            string bodyMail = BodyMail(consultarDatosCorreoModel, comentario);

            MailModel mailModel = new MailModel
                                  {
                                      Body = bodyMail,
                                      Subject = consultarDatosCorreoModel.Descripcion + " - TICKET " + consultarDatosCorreoModel.TicketId + " - INCOMPLETO"
                                  };

            if (!string.IsNullOrEmpty(consultarDatosCorreoModel.MailLevanto))
            {
                List<string> mailsTo = new List<string>
                                       {
                                           consultarDatosCorreoModel.MailLevanto
                                       };
                mailModel.MailsTo = mailsTo;

                List<string> mailsCcs = new List<string>
                                        {
                                            consultarDatosCorreoModel.MailResponsable,
                                            consultarDatosCorreoModel.MailReporta
                                        };
                IList<CorreosCopiaTicketsModel> listaCorreos = iSeguimientoTicketsDataAccess.ObtenerCorreosCopiaTickets(consultarDatosCorreoModel.TicketId);

                foreach (CorreosCopiaTicketsModel obj in listaCorreos)
                {
                    mailsCcs.Add(obj.Correo);
                }
                mailModel.MailsCc = mailsCcs;

                SendMailUtil.GetInstance().SendMailTickets(mailModel);
            }
        }

        private static string BodyMail(ConsultarDatosCorreoModel consultarDatosCorreoModel, string comentario)
        {
            StringBuilder bodyMail = new StringBuilder();
            bodyMail.Append("<p style=\"font-family:Arial, Helvetica, sans-serif\"> Estimado(a): <b>" + consultarDatosCorreoModel.UsuarioLevanto + "</b><br/><br/>");
            bodyMail.Append("El usuario <b>");
            bodyMail.Append(consultarDatosCorreoModel.PersonaResponsable + "</b>");
            bodyMail.Append(" ha cambiado el estatus del ticket #" + consultarDatosCorreoModel.TicketId);
            bodyMail.Append(" a incompleto, ya que no se cuenta con");
            bodyMail.Append(" la información necesaria para atenderlo, favor de completarla dando seguimiento al ticket desde AutoMarsh</p><br>");
            bodyMail.Append("<table border='1' align=\"center\" style=\"width:100%;\"> ");
            bodyMail.Append("<tr> ");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">No</th> ");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">Fecha de registro</th> ");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">Fecha de recepción</th> ");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">Tipo Ticket</th> ");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">Descripción</th> ");
            bodyMail.Append("</tr> ");
            bodyMail.Append("<tr> ");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\">" + consultarDatosCorreoModel.TicketId + "</td> ");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\">" + consultarDatosCorreoModel.FechaRegistro + "</td> ");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\">" + consultarDatosCorreoModel.FechaRecepcion + "</td> ");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\">" + consultarDatosCorreoModel.Descripcion + "</td> ");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\"> <p style=\"text-align:justify\">" + consultarDatosCorreoModel.DescripcionTicket + "</p></td> ");
            bodyMail.Append("</tr> ");
            bodyMail.Append("</table><br>");
            bodyMail.Append("<table border='1' align=\"center\" style=\"width:100%;\"> ");
            bodyMail.Append("<tr> ");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">Fecha de registro</th> ");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">Usuario</th> ");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">Comentario</th> ");
            bodyMail.Append("</tr> ");
            bodyMail.Append("<tr> ");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\">" + DateTime.Now + "</td> ");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\">" + consultarDatosCorreoModel.PersonaResponsable + "</td> ");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\"> <p style=\"text-align:justify\">" + comentario + "</p></td> ");
            bodyMail.Append("</tr> ");
            bodyMail.Append("</table>");
            return bodyMail.ToString();
        }

        public SingleResponse<IList<PersonaResponsableModel>> BuscarUsuarioResponsable(PersonaResponsableModel personaResponsableModel)
        {
            SingleResponse<IList<PersonaResponsableModel>> response = new SingleResponse<IList<PersonaResponsableModel>>();
            try
            {
                IList<PersonaResponsableModel> listaPersonas = iSeguimientoTicketsDataAccess.BuscarUsuarioResponsable(personaResponsableModel);
                response.Done(listaPersonas, string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesConfigParamTickets.ERR_08_02, e));
            }
            return response;
        }

        public SingleResponse<RegistroTicketsModel> ReasignarResposnable(RegistroTicketsModel registroTicketsModel)
        {
            SingleResponse<RegistroTicketsModel> response = new SingleResponse<RegistroTicketsModel>();
            try
            {
                TicketsEstatusModel ticketsEstatus = new TicketsEstatusModel
                                                     {
                                                         TicketId = registroTicketsModel.TicketId
                                                     };

                ticketsEstatus = ValidaEstatusTicket(ticketsEstatus)[0];

                if (ticketsEstatus.Activo)
                {
                    throw new DomainException(CodesSeguiminetorTickets.ERR_00_02 + ticketsEstatus.Estatus);
                }

                RegistroTicketsModel registro = iSeguimientoTicketsDataAccess.ReasignarResposnable(registroTicketsModel);

                if (registro != null)
                {
                    try
                    {

                        PersonaResponsableModel persona = iSeguimientoTicketsDataAccess.BuscaPersonaResposnableTicket(registro);
                        string nombre = !string.IsNullOrEmpty(persona.Materno) ?persona.Nombre + " " + persona.Paterno + " " + persona.Materno : persona.Nombre + " " + persona.Paterno;
                        ConsultarDatosCorreoModel consultarDatosCorreoModel = iSeguimientoTicketsDataAccess.ObtenerDatosCorreo(registroTicketsModel.TicketId);
                        consultarDatosCorreoModel.IdPersonaResponsableInicial = persona.PersonaId;
                        consultarDatosCorreoModel.MailResponsableInicial = persona.MailResponsable;
                        consultarDatosCorreoModel.PersonaResponsableInicial = nombre;
                        EnviarMailReasignar(consultarDatosCorreoModel);
                    }
                    catch (Exception)
                    {
                        throw new DomainException(CodesTickets.ERR_00_18);
                    }
                }
                response.Done(registro, string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesConfigParamTickets.ERR_08_02, e));
            }
            return response;
        }

        private void EnviarMailReasignar(ConsultarDatosCorreoModel ticketModel)
        {
            string bodyMail = BodyMailReasignar(ticketModel);

            MailModel mailModel = new MailModel
                                  {
                                      Body = bodyMail,
                                      Subject = ticketModel.Descripcion + " - TICKET " + ticketModel.TicketId
                                  };

            if (!string.IsNullOrEmpty(ticketModel.MailLevanto))
            {
                List<string> mailsTo = new List<string>
                                       {
                                           ticketModel.MailLevanto,
                                           ticketModel.MailResponsable
                                       };
                mailModel.MailsTo = mailsTo;

                List<string> mailsCcs = new List<string>
                                        {
                                            ticketModel.MailResponsableInicial
                                        };
                mailModel.MailsCc = mailsCcs;

                SendMailUtil.GetInstance().SendMailTickets(mailModel);
            }
        }

        private static string BodyMailReasignar(ConsultarDatosCorreoModel ticketModel)
        {
            StringBuilder bodyMail = new StringBuilder();
            bodyMail.Append("<p style=\"font-family:Arial, Helvetica, sans-serif\"> Estimado(a): <b>" + ticketModel.UsuarioLevanto + " </b><br/><br/>");
            bodyMail.Append("El usuario <b>" + ticketModel.PersonaResponsableInicial + "</b> ha reasignado el ticket ");
            bodyMail.Append("#" + ticketModel.TicketId + " a <b>" + ticketModel.PersonaResponsable + "</b> para su pronta revisión.</p> ");
            bodyMail.Append("<table border='1' align=\"center\" style=\"width:100%;\"> ");
            bodyMail.Append("<tr> ");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">No</th> ");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">Fecha de registro</th> ");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">Fecha de recepci&oacute;n</th> ");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">Tipo Ticket</th> ");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">Descripci&oacute;n</th> ");
            bodyMail.Append("</tr> ");
            bodyMail.Append("<tr> ");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\">" + ticketModel.TicketId + "</td> ");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\">" + ticketModel.FechaRegistro + "</td> ");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\">" + ticketModel.FechaRecepcion + "</td> ");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\">" + ticketModel.Descripcion + "</td> ");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\"> <p style=\"text-align:justify\">" + ticketModel.DescripcionTicket + "</p></td> ");
            bodyMail.Append("</tr> ");
            bodyMail.Append("</table>");
            return bodyMail.ToString();
        }

        public SingleResponse<TicketsEstatusModel> GuardarSeguimientoCierreSinArchivo(TicketsEstatusModel archivoTicketsModel)
        {
            SingleResponse<TicketsEstatusModel> response = new SingleResponse<TicketsEstatusModel>();
            try
            {
                TicketsEstatusModel ticketsEstatus = new TicketsEstatusModel
                                                     {
                                                         TicketId = archivoTicketsModel.TicketId
                                                     };

                ticketsEstatus = ValidaEstatusTicket(ticketsEstatus)[0];

                if (ticketsEstatus.Activo)
                {
                    throw new DomainException(CodesSeguiminetorTickets.ERR_00_02 + ticketsEstatus.Estatus);
                }

                TicketsEstatusModel archivoModel = iSeguimientoTicketsDataAccess.GuardarSeguimientoCierreSinArchivo(archivoTicketsModel);
                response.Done(archivoModel, string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesCalendario.ERR_07_03, e));
            }
            return response;
        }

        private void EnviarMailCerrar(ConsultarDatosCorreoModel ticketModel, ComentariosTicketModel comentario)
        {
            string bodyMail = BodyMailCerrar(ticketModel, comentario.Comentario);

            MailModel mailModel = new MailModel
                                  {
                                      Body = bodyMail,
                                      Subject = ticketModel.Descripcion + " - TICKET " + ticketModel.TicketId + " - CERRADO"
                                  };

            if (!string.IsNullOrEmpty(ticketModel.MailLevanto))
            {
                List<string> mailsTo = new List<string>
                                       {
                                           ticketModel.MailLevanto
                                       };
                mailModel.MailsTo = mailsTo;

                List<string> mailsCcs = new List<string>
                                        {
                                            ticketModel.MailResponsable,
                                            ticketModel.MailReporta
                                        };

                IList<CorreosCopiaTicketsModel> listaCorreos = iSeguimientoTicketsDataAccess.ObtenerCorreosCopiaTickets(ticketModel.TicketId);

                foreach (CorreosCopiaTicketsModel obj in listaCorreos)
                {
                    mailsCcs.Add(obj.Correo);
                }
                mailModel.MailsCc = mailsCcs;
            }
            List<string> attachementsPathsFiles = new List<string>();
            if (comentario.ArchivoTickets != null)
            {
                attachementsPathsFiles.Add(comentario.ArchivoTickets.RutaArchivo + comentario.ArchivoTickets.NombreArchivo);
                mailModel.AttachementsPathsFiles = attachementsPathsFiles;
                mailModel.AttachmentName = comentario.ArchivoTickets.NombreArchivo;
            }
            SendMailUtil.GetInstance().SendMailTickets(mailModel);
        }

        private static string BodyMailCerrar(ConsultarDatosCorreoModel ticketModel, string comentario)
        {
            StringBuilder bodyMail = new StringBuilder();
            bodyMail.Append("<p style=\"font-family:Arial, Helvetica, sans-serif\"> Estimado(a):<b> " + ticketModel.UsuarioLevanto + " </b><br/><br/> El usuario <b> ");
            bodyMail.Append(ticketModel.QuienCerroTicket + " </b> ha cerrado el ticket #" + ticketModel.TicketId);
            bodyMail.Append("</b><br/>");
            bodyMail.Append("<table align=\"center\" border='1' width=\"650\">");
            bodyMail.Append("<tr>");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">No</th>");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">Fecha de registro</th>");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">Fecha de recepci&oacute;n</th>");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">Tipo Ticket</th>");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">Descripci&oacute;n</th></tr>");
            bodyMail.Append("<tr>");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\">");
            bodyMail.Append(ticketModel.TicketId);
            bodyMail.Append("</td>");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\">");
            bodyMail.Append(ticketModel.FechaRegistro);
            bodyMail.Append("</td>");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\">");
            bodyMail.Append(ticketModel.FechaRecepcion);
            bodyMail.Append("</td>");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\">");
            bodyMail.Append(ticketModel.Descripcion);
            bodyMail.Append("</td>");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\">");
            bodyMail.Append("<p style=\"text-align:justify\">" + ticketModel.DescripcionTicket + " </p>");
            bodyMail.Append("</td>");
            bodyMail.Append("</tr>");
            bodyMail.Append("</table><br>");

            bodyMail.Append("<table align=\"center\" border='1' width=\"650\">");
            bodyMail.Append("<tr>");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">Fecha de registro</th>");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">Usuario</th>");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">Comentario</th>");
            bodyMail.Append("</tr>");
            bodyMail.Append("<tr align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\">");
            bodyMail.Append("<td>");
            bodyMail.Append(DateTime.Now);
            bodyMail.Append("</td>");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\">");
            bodyMail.Append(ticketModel.QuienCerroTicket);
            bodyMail.Append("</td>");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\">");
            bodyMail.Append("<p style=\"text-align:justify\">" + comentario + " </p>");
            bodyMail.Append("</td>");
            bodyMail.Append("</tr>");
            bodyMail.Append("</table>");
            return bodyMail.ToString();
        }

        private void EnviarMailRegistro(TicketModel ticketModel, ConsultarDatosCorreoModel consultarDatosCorreoModel)
        {
            string bodyMail = BodyMailRegistro(ticketModel, consultarDatosCorreoModel);

            MailModel mailModel = new MailModel
                                  {
                                      Body = bodyMail,
                                      Subject = consultarDatosCorreoModel.Descripcion + " - " + ticketModel.TicketId
                                  };

            if (!string.IsNullOrEmpty(consultarDatosCorreoModel.MailResponsable))
            {
                List<string> mailsTo = new List<string>
                                       {
                                           consultarDatosCorreoModel.MailResponsable
                                       };
                mailModel.MailsTo = mailsTo;

                List<string> mailsCcs = new List<string>();

                IList<CorreosCopiaTicketsModel> listaCorreos = iSeguimientoTicketsDataAccess.ObtenerCorreosCopiaTickets(ticketModel.TicketId);

                foreach (CorreosCopiaTicketsModel obj in listaCorreos)
                {
                    mailsCcs.Add(obj.Correo);
                }
                mailsCcs.Add(consultarDatosCorreoModel.MailReporta);
                mailsCcs.Add(consultarDatosCorreoModel.MailLevanto);
                mailModel.MailsCc = mailsCcs;

                List<string> attachementsPathsFiles = new List<string>();

                IList<ArchivoTicketsModel> archivos = iSeguimientoTicketsDataAccess.ListaArchivosTickets(consultarDatosCorreoModel.TicketId);
                if (archivos != null)
                {
                    attachementsPathsFiles.Add(archivos[0].RutaArchivo + archivos[0].NombreArchivo);
                    mailModel.AttachementsPathsFiles = attachementsPathsFiles;
                    mailModel.AttachmentName = archivos[0].NombreArchivo;
                }
                mailModel.AttachementsPathsFiles = attachementsPathsFiles;

                SendMailUtil.GetInstance().SendMailTickets(mailModel);
            }
        }

        private string BodyMailRegistro(TicketModel ticketModel, ConsultarDatosCorreoModel consultarDatosCorreoModel)
        {
            StringBuilder bodyMail = new StringBuilder();
            bodyMail.Append("<p style=\"font-family:Arial, Helvetica, sans-serif\"> Estimado(a): " + consultarDatosCorreoModel.PersonaResponsable + " <b></b><br/><br/> Se ha registrado el ticket ");
            bodyMail.Append("#" + ticketModel.TicketId + " para su pronta revisión.</p> ");
            bodyMail.Append("<table border='1' align=\"center\" style=\"width:100%;\"> ");
            bodyMail.Append("<tr> ");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">No</th> ");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">Fecha de registro</th> ");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">Fecha de recepci&oacute;n</th> ");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">Tipo Ticket</th> ");
            bodyMail.Append("<th bgcolor='#2054DE' style=\"color: white; font-family:Arial, Helvetica, sans-serif; font-size: 13\">Descripci&oacute;n</th> ");
            bodyMail.Append("</tr> ");
            bodyMail.Append("<tr> ");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\">" + ticketModel.TicketId + "</td> ");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\">" + ticketModel.FechaRegistro + "</td> ");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\">" + ticketModel.FechaRecepcion + "</td> ");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\">" + ticketModel.Tipo + "</td> ");
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\"> <p style=\"text-align:justify\">" + ticketModel.DescripcionTicket + "</p></td> ");
            bodyMail.Append("</tr> ");
            bodyMail.Append("</table>");
            return bodyMail.ToString();
        }

        public SingleResponse<TicketsEstatusModel> GuardarArchivoSeguimientoCierre(GuardaSeguimientoTicketsModel guardaSeguimientoTickets)
        {
            SingleResponse<TicketsEstatusModel> response = new SingleResponse<TicketsEstatusModel>();
            try
            {
                TicketsEstatusModel ticketsEstatus = new TicketsEstatusModel
                                                     {
                                                         TicketId = Convert.ToInt32(guardaSeguimientoTickets.TicketId)
                                                     };

                ticketsEstatus = ValidaEstatusTicket(ticketsEstatus)[0];

                if (ticketsEstatus.Activo)
                {
                    throw new DomainException(CodesSeguiminetorTickets.ERR_00_02 + ticketsEstatus.Estatus);
                }

                TicketsEstatusModel ticketsEstatusModel = new TicketsEstatusModel();
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

                string sPath = WebConfigurationManager.AppSettings["TICKETS_FILES_PATH"] + WebConfigurationManager.AppSettings["TICKETS_FILES_SEGUIMIENTO"];
                string personaIdNuevo = guardaSeguimientoTickets.PersonaId.Contains("?") ? guardaSeguimientoTickets.PersonaId.Split('?')[0] : guardaSeguimientoTickets.PersonaId;

                ticketsEstatusModel.Tkn = guardaSeguimientoTickets.Tkn;

                for (int iCnt = 0; iCnt<=hfc.Count - 1; iCnt++)
                {
                    System.Web.HttpPostedFile hpf = hfc[iCnt];

                    if (hpf.ContentLength>0)
                    {
                        if (!Directory.Exists(sPath))
                        {
                            Directory.CreateDirectory(sPath);
                        }
                        if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                        {
                            hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                        }
                        else
                        {
                            StringBuilder archivoBuilder = new StringBuilder();
                            archivoBuilder.Append(sPath);
                            archivoBuilder.Append(Path.GetFileNameWithoutExtension(hpf.FileName));
                            archivoBuilder.Append("_");
                            archivoBuilder.Append(DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss"));
                            archivoBuilder.Append(Path.GetExtension(hpf.FileName));
                            var filename = archivoBuilder.ToString();
                            hpf.SaveAs(filename);
                        }
                        ticketsEstatusModel.TicketId = Int32.Parse(guardaSeguimientoTickets.TicketId);
                        ticketsEstatusModel.IdEstatusTicket = Int32.Parse(guardaSeguimientoTickets.IdEstatusTicket);
                        ticketsEstatusModel.NombreArchivoTicketCerrado = hpf.FileName;
                        ticketsEstatusModel.RutaArchivoTicketCerrado = sPath;
                        ticketsEstatusModel.IdTicketEstatus = Int32.Parse(guardaSeguimientoTickets.IdTicketEstatus);
                        ticketsEstatusModel.PersonaId = Int32.Parse(personaIdNuevo);
                    }
                }

                response.Done(iSeguimientoTicketsDataAccess.GuardarSeguimientoCierreSinArchivo(ticketsEstatusModel), string.Empty);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception)
            {
                response.Error(new DomainException(Codes.ERR_00_00));
            }

            return response;
        }

        public SingleResponse<ArchivoTicketsModel> CargarArchivoSeguimiento(GuardaSeguimientoTicketsModel guardaSeguimientoTickets)
        {
            SingleResponse<ArchivoTicketsModel> response = new SingleResponse<ArchivoTicketsModel>();
            try
            {
                TicketsEstatusModel ticketsEstatus = new TicketsEstatusModel
                                                     {
                                                         TicketId = Convert.ToInt32(guardaSeguimientoTickets.TicketId)
                                                     };

                ticketsEstatus = ValidaEstatusTicket(ticketsEstatus)[0];

                if (ticketsEstatus.Activo)
                {
                    throw new DomainException(CodesSeguiminetorTickets.ERR_00_02 + ticketsEstatus.Estatus);
                }

                ArchivoTicketsModel archivoTicketsModel = new ArchivoTicketsModel();
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                string idEstatusTicketNuevo = guardaSeguimientoTickets.IdEstatusTicket.Length>1 ? guardaSeguimientoTickets.IdEstatusTicket.Split('?')[0] : guardaSeguimientoTickets.IdEstatusTicket;
                string sPath = WebConfigurationManager.AppSettings["TICKETS_FILES_PATH"] + WebConfigurationManager.AppSettings["TICKETS_FILES_SEGUIMIENTO"];

                for (int iCnt = 0; iCnt<=hfc.Count - 1; iCnt++)
                {
                    System.Web.HttpPostedFile hpf = hfc[iCnt];

                    if (hpf.ContentLength>0)
                    {
                        sPath = sPath + guardaSeguimientoTickets.TicketId + "/";
                        if (!Directory.Exists(sPath))
                        {
                            Directory.CreateDirectory(sPath);
                        }
                        if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                        {
                            hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                        }
                        else
                        {
                            StringBuilder archivoBuilder = new StringBuilder();
                            archivoBuilder.Append(sPath);
                            archivoBuilder.Append(Path.GetFileNameWithoutExtension(hpf.FileName));
                            archivoBuilder.Append("_");
                            archivoBuilder.Append(DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss"));
                            archivoBuilder.Append(Path.GetExtension(hpf.FileName));
                            string filename = archivoBuilder.ToString();
                            hpf.SaveAs(filename);
                        }
                        archivoTicketsModel.TicketId = Int32.Parse(guardaSeguimientoTickets.TicketId);
                        archivoTicketsModel.IdEstatusTicket = Int32.Parse(idEstatusTicketNuevo);
                        archivoTicketsModel.NombreArchivo = hpf.FileName;
                        archivoTicketsModel.RutaArchivo = sPath;

                        response.Done(GuardarArchivoSeguimiento(archivoTicketsModel).Response, String.Empty);
                    }
                }
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception)
            {
                response.Error(new DomainException(Codes.ERR_00_00));
            }
            return response;
        }

        public IList<TicketsEstatusModel> ValidaEstatusTicket(TicketsEstatusModel ticketsEstatus)
        {
            return iSeguimientoTicketsDataAccess.ValidaEstatusTicket(ticketsEstatus);
        }

        public SingleResponse<ComentariosTicketModel> GuardaComentarioTicket(ComentariosTicketModel comentariosTicketModel)
        {
            SingleResponse<ComentariosTicketModel> response = new SingleResponse<ComentariosTicketModel>();

            try
            {
                if (!comentariosTicketModel.Cerrado)
                {
                    TicketsEstatusModel ticketsEstatus = new TicketsEstatusModel
                                                         {
                                                             TicketId = comentariosTicketModel.TicketId
                                                         };

                    ticketsEstatus = ValidaEstatusTicket(ticketsEstatus)[0];

                    if (ticketsEstatus.Activo)
                    {
                        throw new DomainException(CodesSeguiminetorTickets.ERR_00_02 + ticketsEstatus.Estatus);
                    }
                }

                comentariosTicketModel.TicketModelUpdate = iSeguimientoTicketsDataAccess.BuscarTicketSeguimiento(comentariosTicketModel);
                response.Done(iSeguimientoTicketsDataAccess.GuardarComentariosTicket(comentariosTicketModel), string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesConfigParamTickets.ERR_08_03, e));
            }
            return response;
        }
    }
}
