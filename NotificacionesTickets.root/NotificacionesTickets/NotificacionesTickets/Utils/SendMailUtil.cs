using System;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web.Configuration;
using NotificacionesTickets.Codes;
using NotificacionesTickets.Exceptions;
using NotificacionesTickets.Models;

namespace NotificacionesTickets.Utils
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
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Host = ConfigXmlUtil.GetDataConfigXml(1),
                Port = int.Parse(ConfigXmlUtil.GetDataConfigXml(2)),
                EnableSsl = bool.Parse(ConfigXmlUtil.GetDataConfigXml(3)),
                UseDefaultCredentials = bool.Parse(ConfigXmlUtil.GetDataConfigXml(4)),
                Credentials = new System.Net.NetworkCredential(ConfigXmlUtil.GetDataConfigXml(5), ConfigXmlUtil.GetDataConfigXml(6))
            };
        }

        public void SendMail(MailModel mailModel)
        {
            try
            {
                if (null != mailModel)
                {
                    mailMessage = new MailMessage()
                    {
                        From = new MailAddress(ConfigXmlUtil.GetDataConfigXml(7)),
                        IsBodyHtml = true,
                        Subject = mailModel.Subject,
                        Body = mailModel.Body
                    };
                    if (null != mailModel.MailsTo)
                    {
                        foreach (string mailTo in mailModel.MailsTo)
                        {
                            mailMessage.To.Add(mailTo);
                        }
                    }
                    if (null != mailModel.MailsCc)
                    {
                        foreach (string mailCc in mailModel.MailsCc)
                        {
                            mailMessage.CC.Add(mailCc);
                        }
                    }
                    if (null != mailModel.AttachementsPathsFiles)
                    {
                        foreach (string attachementPathFile in mailModel.AttachementsPathsFiles)
                        {
                            Attachment attachment = new Attachment(attachementPathFile);
                            mailMessage.Attachments.Add(attachment);
                        }
                    }
                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception e)
            {
                throw new MailException(CodesNotificaciones.ERR_00_04, e);
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
                        From = new MailAddress(ConfigXmlUtil.GetDataConfigXml(7)),
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
                                mailMessage.Attachments.Add(attachment);
                            }

                        }
                    }
                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception e)
            {
                throw new Exception(CodesNotificaciones.ERR_00_06, e);
            }
        }

        private AlternateView Body(string body)
        {
            string pathHeader = ConfigXmlUtil.GetDataConfigXml(8);
            LinkedResource header = new LinkedResource(pathHeader, MediaTypeNames.Image.Jpeg)
            {
                ContentId = "Header"
            };
            string pathFooter = ConfigXmlUtil.GetDataConfigXml(9);
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
            bodyMail.Append("<p style=\"font-family:Arial, Helvetica, sans-serif\">Para dar seguimiento a esta solicitud favor de ingresar a:<br></p> ");                        
            bodyMail.Append("<p align=\"center\" style=\"font-family:Arial, Helvetica, sans-serif\"> <a align=\"center\">" + ConfigXmlUtil.GetDataConfigXml(10) + "</p> ");
            bodyMail.Append("<p style=\"font-family:Arial, Helvetica, sans-serif\"> Por favor no responda a éste mensaje generado autom&aacute;ticamente. </p> ");            
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
    }
}
