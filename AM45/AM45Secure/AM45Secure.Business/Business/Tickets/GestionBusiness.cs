using AM45Secure.Business.IBusiness.Tickets;
using AM45Secure.Commons.Modelos.Tickets;
using AM45Secure.DataAccess.IDataAccess.Tickets;
using System;
using System.Collections.Generic;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Recursos;
using Zero.Exceptions;
using Zero.Handlers.Response;
using System.Linq;
using System.Text;
using AM45Secure.Commons.Utils;
using Zero.Utils;
using Zero.Utils.Models;

namespace AM45Secure.Business.Business.Tickets
{
    public class GestionBusiness : IGestionBussiness
    {
        private readonly IGestionDataAccess iGestionDataAccess;
        private readonly ISeguimientoTicketsDataAccess iSeguimientoTicketsDataAccess;
        private const int ESTATUS_REGISTRADO = 1;
        private const int ESTATUS_INCOMPLETO = 4;
        private const int ESTATUS_DOCUMENTACION = 5;

        public GestionBusiness(IGestionDataAccess iGestionDataAccess, ISeguimientoTicketsDataAccess iSeguimientoTicketsDataAccess)
        {
            this.iGestionDataAccess = iGestionDataAccess;
            this.iSeguimientoTicketsDataAccess = iSeguimientoTicketsDataAccess;
        }

        public SingleResponse<IList<TicketModel>> ConsultarTickets(TicketModel ticketModel)
        {
            SingleResponse<IList<TicketModel>> response = new SingleResponse<IList<TicketModel>>();
            try
            {
                IList<TicketModel> listTickest = iGestionDataAccess.ConsultarTickest(ticketModel);
                IList<TicketModel> tickest = new List<TicketModel>();
                int idUsuarioSession = ticketModel.GetIdUsuarioSesion();
                foreach (var ticket in listTickest)
                {
                    if (ticket.ClaveEstatus == ESTATUS_INCOMPLETO || ticket.ClaveEstatus == ESTATUS_DOCUMENTACION)
                    {
                        if (ticket.UsuarioId == idUsuarioSession)
                        {
                            tickest.Add(ticket);
                        }
                    }
                    else
                    {
                        tickest.Add(ticket);
                    }
                }
                List<TicketModel> ascListTickets = tickest.OrderBy(ticket => ticket.TicketId).ToList();
                response.Done(ascListTickets, string.Empty);
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
                //Agregar menjase de error
                response.Error(new DomainException(e.Message));
            }
            return response;
        }

        public SingleResponse<IList<ClienteProductoModel>> ConsultarClientes(ClienteProductoModel clienteProductoModel)
        {
            SingleResponse<IList<ClienteProductoModel>> response = new SingleResponse<IList<ClienteProductoModel>>();
            try
            {
                IList<ClienteProductoModel> listClientes = iGestionDataAccess.ConsultarClientes(clienteProductoModel);

                List<ClienteProductoModel> clientesAgrupados =
                    listClientes.GroupBy(cm => new
                                               {
                                                   cm.IdCliente,
                                                   cm.NombreCliente
                                               },
                                         (key, group) => new
                                                         {
                                                             key.IdCliente,
                                                             key.NombreCliente
                                                         }).Select(x => new ClienteProductoModel()
                                                                        {
                                                                            IdCliente = x.IdCliente,
                                                                            NombreCliente = x.NombreCliente
                                                                        }
                                                                  ).ToList();

                response.Done(clientesAgrupados, string.Empty);
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
                response.Error(new DomainException(CodesTickets.ERR_00_02, e));
            }
            return response;
        }

        public SingleResponse<IList<AgenciasModel>> ConsultarAgencias(AgenciasClienteModel agenciasCliente)
        {
            SingleResponse<IList<AgenciasModel>> response = new SingleResponse<IList<AgenciasModel>>();
            try
            {
                IList<AgenciasModel> listAgencias = iGestionDataAccess.ConsultarAgencias(agenciasCliente);

                response.Done(listAgencias, string.Empty);
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
                response.Error(new DomainException(CodesTickets.ERR_00_02, e));
            }
            return response;
        }

        public SingleResponse<bool> ConsultarSiEsClienteFlotillas(ClienteProductoModel clienteProductoModel)
        {
            SingleResponse<bool> response = new SingleResponse<bool>();
            try
            {
                bool isFlotillas = iGestionDataAccess.ConsultarSiEsClienteFlotillas(clienteProductoModel);
                response.Done(isFlotillas, string.Empty);
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
                response.Error(new DomainException(CodesTickets.ERR_00_02, e));
            }
            return response;
        }

        public SingleResponse<IList<ClienteProductoModel>> ConsultarCaratula(ClienteProductoModel clienteProductoModel)
        {
            SingleResponse<IList<ClienteProductoModel>> response = new SingleResponse<IList<ClienteProductoModel>>();
            try
            {
                IList<ClienteProductoModel> listClientes = iGestionDataAccess.ConsultarCaratula(clienteProductoModel);

                IList<ClienteProductoModel> clientesAgrupado = listClientes.GroupBy(cliente => new
                                                                                               {
                                                                                                   cliente.IdCliente,
                                                                                                   cliente.PolizaCaratula,
                                                                                                   cliente.FormaPago,
                                                                                                   cliente.TipoString,
                                                                                                   cliente.TipoCobranzaString
                                                                                               },
                                                                                    (key, group) => new
                                                                                                    {
                                                                                                        key.IdCliente,
                                                                                                        key.PolizaCaratula,
                                                                                                        key.FormaPago,
                                                                                                        key.TipoString,
                                                                                                        key.TipoCobranzaString
                                                                                                    }).Select(x => new ClienteProductoModel()
                                                                                                                   {
                                                                                                                       IdCliente = x.IdCliente,
                                                                                                                       PolizaCaratula = x.PolizaCaratula,
                                                                                                                       FormaPago = x.FormaPago,
                                                                                                                       TipoString = x.TipoString,
                                                                                                                       TipoCobranzaString = x.TipoCobranzaString
                                                                                                                   }
                                                                                                             ).ToList();

                response.Done(clientesAgrupado, string.Empty);
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
                response.Error(new DomainException(CodesTickets.ERR_00_02, e));
            }
            return response;
        }


        public SingleResponse<ClienteProductoModel> ConsultarResponsable(ClienteProductoModel clienteProductoModel)
        {
            SingleResponse<ClienteProductoModel> response = new SingleResponse<ClienteProductoModel>();
            try
            {
                IList<ClienteProductoModel> listClientes = iGestionDataAccess.ConsultarResponsable(clienteProductoModel);

                if (listClientes.Count>0)
                {
                    response.Done(listClientes[0], string.Empty);
                }
                else
                {
                    response.Error(new DomainException(CodesTickets.ERR_00_02));
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
                response.Error(new DomainException(CodesTickets.ERR_00_02, e));
            }
            return response;
        }

        public SingleResponse<IList<ClienteProductoModel>> ConsultarTiposTickets(
            ClienteProductoModel clienteProductoModel)
        {
            SingleResponse<IList<ClienteProductoModel>> response = new SingleResponse<IList<ClienteProductoModel>>();
            try
            {
                IList<ClienteProductoModel> listClientes = iGestionDataAccess.ConsultarTiposTickets(clienteProductoModel);

                IList<ClienteProductoModel> clientesAgrupado =
                    listClientes.GroupBy(
                                         cliente => new
                                                    {
                                                        cliente.IdCliente,
                                                        cliente.DescripcionTipoTicket,
                                                        cliente.IdTipoTicket,
                                                        cliente.HorasAtencion
                                                    },
                                         (key, group) => new
                                                         {
                                                             key.IdCliente,
                                                             key.DescripcionTipoTicket,
                                                             key.IdTipoTicket,
                                                             key.HorasAtencion
                                                         }).Select(x => new ClienteProductoModel()
                                                                        {
                                                                            IdCliente = x.IdCliente,
                                                                            DescripcionTipoTicket = x.DescripcionTipoTicket,
                                                                            IdTipoTicket = x.IdTipoTicket,
                                                                            HorasAtencion = x.HorasAtencion
                                                                        }
                                                                  ).ToList();

                response.Done(clientesAgrupado, string.Empty);
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
                response.Error(new DomainException(CodesTickets.ERR_00_02, e));
            }
            return response;
        }


        public SingleResponse<IList<CatOrigenTicketsModel>> ConsultarReportaA()
        {
            SingleResponse<IList<CatOrigenTicketsModel>> response = new SingleResponse<IList<CatOrigenTicketsModel>>();
            try
            {
                IList<CatOrigenTicketsModel> listOrigenTicketsModel = iGestionDataAccess.ConsultarReportaA();
                response.Done(listOrigenTicketsModel, string.Empty);
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
                response.Error(new DomainException(CodesTickets.ERR_00_02, e));
            }
            return response;
        }

        public SingleResponse<IList<CatEstatusTicketsModel>> ConsultaEstatusTickets()
        {
            SingleResponse<IList<CatEstatusTicketsModel>> response = new SingleResponse<IList<CatEstatusTicketsModel>>();
            try
            {
                IList<CatEstatusTicketsModel> listEstatusTicketsModel = iGestionDataAccess.ConsultaEstatusTickets();
                response.Done(listEstatusTicketsModel, string.Empty);
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
                response.Error(new DomainException(CodesTickets.ERR_00_02, e));
            }
            return response;
        }

        public SingleResponse<bool> GuardarTicket(TicketModel ticketModel)
        {
            SingleResponse<bool> response = new SingleResponse<bool>();
            try
            {
                if (ticketModel == null)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }
                IList<Validation> validations = ValidatorZero.Validate(ticketModel, new OptionsValidation
                                                                                    {
                                                                                        ValidateIntCero = true
                                                                                    });
                if (validations.Count>0)
                {
                    throw new DomainValidationsException(validations);
                }

                ticketModel.FechaRecepcion = CalculoFechaRecepcion();
                TicketModel registroCorrecto = iGestionDataAccess.GuardarTicket(ticketModel);
                try
                {
                    if (ticketModel.IdEstatusTicket == ESTATUS_REGISTRADO)
                    {
                        EnviarMail(registroCorrecto);
                    }
                }
                catch (Exception)
                {
                    response.Done(true, string.Empty);
                }

                response.Done(true, string.Empty);
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

        public DateTime CalculoFechaRecepcion()
        {
            DateTime fechaRecepcion = DateTime.Now;
            TimeSpan finDeHorario = TimeSpan.Parse("17:00:00");
            TimeSpan iniciaHorario = TimeSpan.Parse("07:59:00");
            TimeSpan inicioRecepcion = TimeSpan.Parse("08:00:00");

            if (fechaRecepcion.DayOfWeek == DayOfWeek.Saturday || fechaRecepcion.DayOfWeek == DayOfWeek.Sunday)
            {
                fechaRecepcion = fechaRecepcion.Date + inicioRecepcion;

                if (fechaRecepcion.DayOfWeek == DayOfWeek.Saturday)
                {
                    fechaRecepcion = fechaRecepcion.AddDays(1);
                }

                if (fechaRecepcion.DayOfWeek == DayOfWeek.Sunday)
                {
                    fechaRecepcion = fechaRecepcion.AddDays(1);
                }
            }

            if (fechaRecepcion.TimeOfDay<iniciaHorario)
            {
                fechaRecepcion = fechaRecepcion.Date + inicioRecepcion;
            }
            else if (fechaRecepcion.TimeOfDay>=finDeHorario)
            {
                fechaRecepcion = fechaRecepcion.AddDays(1);
                fechaRecepcion = fechaRecepcion.Date + inicioRecepcion;
            }

            IList<CatDiasInhabilesModel> listaDiasInhabiles = iGestionDataAccess.ConsultarDiasInhabiles(fechaRecepcion);

            if (listaDiasInhabiles.Count != 0)
            {
                foreach (CatDiasInhabilesModel catDiasInhabiles in listaDiasInhabiles)
                {
                    if (catDiasInhabiles.Dia == fechaRecepcion.Date)
                    {
                        fechaRecepcion = fechaRecepcion.AddDays(1);
                        fechaRecepcion = fechaRecepcion.Date + inicioRecepcion;

                        if (fechaRecepcion.DayOfWeek == DayOfWeek.Saturday)
                        {
                            fechaRecepcion = fechaRecepcion.AddDays(1);
                        }

                        if (fechaRecepcion.DayOfWeek == DayOfWeek.Sunday)
                        {
                            fechaRecepcion = fechaRecepcion.AddDays(1);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return fechaRecepcion;
        }

        private void EnviarMail(TicketModel ticketModel)
        {
            ConsultarDatosCorreoModel consultarDatosCorreoModel = iSeguimientoTicketsDataAccess.ObtenerDatosCorreo(ticketModel.TicketId);
            string bodyMail = BodyMail(ticketModel);

            MailModel mailModel = new MailModel
                                  {
                                      Body = bodyMail,
                                      Subject = ticketModel.Tipo + " - " + ticketModel.TicketId
                                  };

            if (!string.IsNullOrEmpty(ticketModel.MailResponsable))
            {
                List<string> mailsCcs = new List<string>();
                List<string> mailsTo = new List<string>
                                       {
                                           ticketModel.MailResponsable
                                       };

                mailModel.MailsTo = mailsTo;

                if (!string.IsNullOrEmpty(ticketModel.CopiarA))
                {
                    String[] correos = ticketModel.CopiarA.Split(';');
                    foreach (var copiarA in correos)
                    {
                        if (!string.IsNullOrEmpty(copiarA))
                        {
                            mailsCcs.Add(copiarA);
                        }
                    }
                }
                mailsCcs.Add(consultarDatosCorreoModel.MailLevanto);
                mailsCcs.Add(consultarDatosCorreoModel.MailReporta);
                mailModel.MailsCc = mailsCcs;
                List<string> attachementsPathsFiles = new List<string>();
                foreach (var archivo in ticketModel.Archivos)
                {
                    attachementsPathsFiles.Add(archivo.RutaArchivo + archivo.NombreArchivo);
                    mailModel.AttachmentName = archivo.NombreArchivo;
                }
                mailModel.AttachementsPathsFiles = attachementsPathsFiles;

                SendMailUtil.GetInstance().SendMailTickets(mailModel);
            }
        }

        private static string BodyMail(TicketModel ticketModel)
        {
            StringBuilder bodyMail = new StringBuilder();
            bodyMail.Append("<p style=\"font-family:Arial, Helvetica, sans-serif\"> Estimado(a): " + ticketModel.NombreCompletoResponsable + " <b></b><br/><br/> El usuario " + ticketModel.GetNombreUsuarioSesion() + " ha generado el ticket ");
            bodyMail.Append("#" + ticketModel.TicketId + " para su pronta atención:</p> ");
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
            bodyMail.Append("<td align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif; font-size: 13\">" + ticketModel.DescripcionTicket + "</td> ");
            bodyMail.Append("</tr> ");
            bodyMail.Append("</table> ");

            return bodyMail.ToString();
        }

        public SingleResponse<ArchivoTicketsModel> GuardarArchivo(ArchivoTicketsModel archivoTicketsModel)
        {
            SingleResponse<ArchivoTicketsModel> response = new SingleResponse<ArchivoTicketsModel>();
            try
            {
                ArchivoTicketsModel archivoModel = iGestionDataAccess.GuardarArchivo(archivoTicketsModel);
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

        public SingleResponse<bool> EliminarArchivo(ArchivoTicketsModel archivoTicketsModel)
        {
            SingleResponse<bool> response = new SingleResponse<bool>();
            try
            {
                bool archivoEliminado = iGestionDataAccess.EliminarArchivo(archivoTicketsModel);
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

        public SingleResponse<IList<AsegModel>> ConsultaAseguradoras(ClienteAsegModel clienteAseg)
        {
            SingleResponse<IList<AsegModel>> response = new SingleResponse<IList<AsegModel>>();
            try
            {
                IList<AsegModel> listAseg = iGestionDataAccess.ConsultaAseguradoras(clienteAseg);
                response.Done(listAseg, string.Empty);
                response.ThrowIfNotOk();
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
                response.Error(new DomainException(CodesTickets.ERR_00_17, e));
            }
            return response;
        }
    }
}