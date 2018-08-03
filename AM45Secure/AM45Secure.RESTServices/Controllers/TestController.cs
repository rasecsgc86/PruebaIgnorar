using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Cotizador;
using Zero.Exceptions;
using Zero.Handlers.Response;

namespace AM45Secure.RESTServices.Controllers
{
    //[Authorize]
    public class TestController : ApiController
    {
        [HttpPost]
        public SingleResponse<CotizadorModel> UploadFile(string parametroA, string parametroB)
        {
            SingleResponse<CotizadorModel> response = new SingleResponse<CotizadorModel>();
            try
            {
                CotizadorModel cotizadorModel = new CotizadorModel();
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                string sPath = WebConfigurationManager.AppSettings["TICKETS_FILES_PATH"] + WebConfigurationManager.AppSettings["TICKETS_FILES_REGISTRO"];
                int iUploadedCnt = 0;
                string filename = "";

                MailModel mailModel = new MailModel
                {
                    Body = "Mail mail mail...",
                    Subject = "Sbject subject subject..."
                };
                mailModel.Subject = "Sbject subject subject...";

                List<string> mailsTo = new List<string>();
                mailsTo.Add("josueramirezdavila@gmail.com");
                mailModel.MailsTo = mailsTo;

                List<string> mailsCcs = new List<string>();
                mailsCcs.Add("suejo.r15@gmail.com");
                mailsCcs.Add("victormgo2103@gmail.com");
                mailModel.MailsCc = mailsCcs;

                // CHECK THE FILE COUNT.
                for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                {
                    System.Web.HttpPostedFile hpf = hfc[iCnt];

                    if (hpf.ContentLength > 0)
                    {
                        sPath += hpf.FileName;
                        System.IO.Directory.CreateDirectory(sPath);
                        sPath += "/";
                        // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                        if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                        {
                            // SAVE THE FILES IN THE FOLDER.
                            hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                            filename = sPath + Path.GetFileName(hpf.FileName);

                            List<string> attachements = new List<string>();
                            attachements.Add(filename);
                            mailModel.AttachementsPathsFiles = attachements;

                            iUploadedCnt = iUploadedCnt + 1;
                        }
                    }
                }
                //SendMailUtil.GetInstance().SendMail(mailModel);

                // RETURN A MESSAGE (OPTIONAL).
                if (iUploadedCnt > 0)
                {
                    response.Done(cotizadorModel, "archivos recibidos " + iUploadedCnt + " nombre : " + filename + " parametroA: " + parametroA + " && parametroAB: " + parametroB);
                }
                else
                {
                    response.Done(cotizadorModel, " parametroA: " + parametroA + " && parametroAB: " + parametroB);
                }
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(e.Message));
            }
            return response;
        }

        //[HttpPost]
        //public string Decode(string tkn)
        //{
        //    byte[] data = Convert.FromBase64String(tkn);
        //    string decodedString = Encoding.UTF8.GetString(data);
        //    //Dictionary<string, string> values =  JsonConvert.DeserializeObject<Dictionary<string, string>>(decodedString);
        //    return values[ClaimTypes.UserData];
        //}
    }
}
