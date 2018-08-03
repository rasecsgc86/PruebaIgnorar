using System;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web.Configuration;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Recursos;
using Zero.Exceptions;
using System.IO;

namespace AM45Secure.Commons.Utils
{
    public class SendMailUtil
    {
        private static SendMailUtil Instance;
        private SmtpClient smtpClient;
        private MailMessage mailMessage;

        private SendMailUtil()
        {
            Config();
        }

        public static SendMailUtil GetInstance() => Instance != null ? Instance : (Instance = new SendMailUtil());

        private void Config()
        {
            smtpClient = new SmtpClient()
            {
                Port = int.Parse(GetDataConfigutation("MailConfigSmtpPort")),
                //DeliveryMethod = SmtpDeliveryMethod.Network,
                //UseDefaultCredentials = bool.Parse(GetDataConfigutation("MailConfigSmtpDefaultCredentials")),
                Host = GetDataConfigutation("MailConfigSmtpHost"),
                EnableSsl = bool.Parse(GetDataConfigutation("MailConfigSmtpSsl"))
            };
            if (bool.Parse(GetDataConfigutation("MailConfigSmtpUseCredentials")))
            {
                //smtpClient.UseDefaultCredentials = bool.Parse(GetDataConfigutation("MailConfigSmtpDefaultCredentials"));
                smtpClient.Credentials = new System.Net.NetworkCredential(GetDataConfigutation("MailConfigSmtpUser"), GetDataConfigutation("MailConfigSmtpPassword"));
            }
        }



        public void SendMailCotizacionPDF(MailModel mailModel, string numeroCotizacion, byte[] bytesMailPDF = null)
        {

            try
            {
                if (null != mailModel)
                {
                    mailMessage = new MailMessage()
                    {
                        From =  new MailAddress(GetDataConfigutation("MailConfigFrom")),
                        IsBodyHtml = true,
                        Subject = mailModel.Subject,
                        Body = mailModel.Body
                    };
                    if (null != mailModel.MailsTo)
                    {
                        foreach (string mailTo in mailModel.MailsTo)
                        {
                            if (!string.IsNullOrEmpty(mailTo))
                            {
                                mailMessage.To.Add(mailTo);
                            }

                        }
                    }
                    if (null != mailModel.MailsCc)
                    {
                        foreach (string mailCc in mailModel.MailsCc)
                        {
                            if (!string.IsNullOrEmpty(mailCc))
                            {
                                mailMessage.CC.Add(mailCc);
                            }

                        }
                    }


                    // if bytes
                    Attachment attachment = new Attachment(new MemoryStream(bytesMailPDF), "Cotizacion" + numeroCotizacion + ".pdf");
                    mailMessage.Attachments.Add(attachment);




                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception e)
            {
                throw new DomainException(Codes.ERR_00_11, e);
            }
        }
        public void SendMail(MailModel mailModel)
        {
            try
            {
                if (null != mailModel)
                {
                    mailMessage = new MailMessage()
                    {
                        From = new MailAddress(GetDataConfigutation("MailConfigFrom")),
                        IsBodyHtml = true,
                        Subject = mailModel.Subject,
                        Body = mailModel.Body
                    };
                    if (null != mailModel.MailsTo)
                    {
                        foreach (string mailTo in mailModel.MailsTo)
                        {
                            if (!string.IsNullOrEmpty(mailTo))
                            {
                                mailMessage.To.Add(mailTo);
                            }

                        }
                    }
                    if (null != mailModel.MailsCc)
                    {
                        foreach (string mailCc in mailModel.MailsCc)
                        {
                            if (!string.IsNullOrEmpty(mailCc))
                            {
                                mailMessage.CC.Add(mailCc);
                            }

                        }
                    }
                    if (null != mailModel.AttachementsPathsFiles)
                    {
                        foreach (string attachementPathFile in mailModel.AttachementsPathsFiles)
                        {
                            if (!string.IsNullOrEmpty(attachementPathFile))
                            {
                                Attachment attachment = new Attachment(attachementPathFile);
                                mailMessage.Attachments.Add(attachment);
                            }

                        }
                    }
                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception e)
            {
                throw new DomainException(Codes.ERR_00_11, e);
            }
        }

        public void SendMailTickets(MailModel mailModel)
        {
            try
            {
                if (null != mailModel)
                {
                    mailMessage = new MailMessage()
                    {
                        From = new MailAddress(GetDataConfigutation("MailConfigFrom")),
                        IsBodyHtml = true,
                        Subject = mailModel.Subject,
                        AlternateViews = { Body(mailModel.Body) }
                    };
                    mailMessage.IsBodyHtml = true;
                    mailMessage.BodyEncoding = Encoding.UTF8;
                    if (null != mailModel.MailsTo)
                    {
                        foreach (string mailTo in mailModel.MailsTo)
                        {
                            if (!string.IsNullOrEmpty(mailTo))
                            {
                                mailMessage.To.Add(mailTo);
                            }

                        }
                    }
                    if (null != mailModel.MailsCc)
                    {
                        foreach (string mailCc in mailModel.MailsCc)
                        {
                            if (!string.IsNullOrEmpty(mailCc))
                            {
                                mailMessage.CC.Add(mailCc);
                            }

                        }
                    }
                    if (null != mailModel.AttachementsPathsFiles)
                    {
                        foreach (string attachementPathFile in mailModel.AttachementsPathsFiles)
                        {
                            if (!string.IsNullOrEmpty(attachementPathFile))
                            {
                                Attachment attachment = new Attachment(attachementPathFile);
                                attachment.Name = mailModel.AttachmentName;
                                mailMessage.Attachments.Add(attachment);
                            }

                        }
                    }
                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception e)
            {
                throw new DomainException(Codes.ERR_00_11, e);
            }
        }

        private AlternateView Body(string body)
        {
            string pathHeader = GetDataConfigutation("HeaderMail");
            LinkedResource header = new LinkedResource(pathHeader, MediaTypeNames.Image.Jpeg)
            {
                ContentId = "Header"
            };
            string pathFooter = GetDataConfigutation("FooterMail");
            LinkedResource footer = new LinkedResource(pathFooter, MediaTypeNames.Image.Jpeg)
            {
                ContentId = "Footer"
            };
            StringBuilder bodyMail = new StringBuilder();
            bodyMail.Append("<body style=\"margin: 0; padding: 0;\"> ");
            bodyMail.Append("<table align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"650\" style=\"border-collapse: collapse;\"> ");
            bodyMail.Append("<tr> ");
            bodyMail.Append("<td align=\"center\"> ");
            bodyMail.Append("<img src=cid:Header align=\"left\" width=\"650\" style=\"display: block;\"/> ");
            bodyMail.Append("</td> ");
            bodyMail.Append("</tr> ");
            bodyMail.Append("<tr style=\"margin-top: 10px\"> ");
            bodyMail.Append("<td align=\"left\"> ");
            bodyMail.Append("<br/> ");
            bodyMail.Append(body);
            bodyMail.Append("<p style=\"font-family:Arial, Helvetica, sans-serif\">Para dar seguimiento a esta solicitud favor de ingresar a Automarsh:<br></p> ");
            bodyMail.Append("<p align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif\">" + GetDataConfigutation("URLMail") + "</p> ");
            bodyMail.Append("<p style=\"font-family:Arial, Helvetica, sans-serif\"> Por favor no responda a éste mensaje generado autom&aacute;ticamente.</p> ");
            bodyMail.Append("</td> ");
            bodyMail.Append("</tr> ");
            bodyMail.Append("<td align=\"center\"> ");
            bodyMail.Append("<img src=cid:Footer align=\"left\" width=\"650\" style=\"display: block;\" /> ");
            bodyMail.Append("</td> ");
            bodyMail.Append("</table> ");
            bodyMail.Append("</body> ");

            AlternateView av = AlternateView.CreateAlternateViewFromString(bodyMail.ToString(), null, MediaTypeNames.Text.Html);
            av.LinkedResources.Add(header);
            av.LinkedResources.Add(footer);
            return av;
        }
        public SmtpClient GetSmtpClient()
        {
            return smtpClient;
        }

        private string GetDataConfigutation(string key)
        {
            try
            {
                return WebConfigurationManager.AppSettings[key];
            }
            catch (Exception e)
            {
                throw new DomainException(Codes.ERR_00_11, e);
            }
        }
    }
}






