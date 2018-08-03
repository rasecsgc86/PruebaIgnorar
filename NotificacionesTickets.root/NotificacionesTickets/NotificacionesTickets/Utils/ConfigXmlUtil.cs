using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotificacionesTickets.Codes;
using NotificacionesTickets.Exceptions;

namespace NotificacionesTickets.Utils
{
    public class ConfigXmlUtil
    {
        private static readonly string __ConfigFileXML = "NotificacionesTicketsConfig.xml";
        private static readonly char __SLASH = '\\';

        public static string GetDataConfigXml(int nodeIndex)
        {
            try
            {
                System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
                string programPath = System.Windows.Forms.Application.ExecutablePath;
                programPath = programPath.Replace(programPath.Split(__SLASH)[programPath.Split(__SLASH).Length - 1], string.Empty);
                xmlDocument.Load(programPath + __ConfigFileXML);
                if (xmlDocument.DocumentElement != null)
                {
                    return xmlDocument.DocumentElement.ChildNodes[nodeIndex].InnerText;
                }
                throw new MailException(CodesNotificaciones.ERR_00_06);
            }
            catch (Exception e)
            {
                throw new MailException(CodesNotificaciones.ERR_00_05, e);
            }
        }
    }
}
