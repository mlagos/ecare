using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Nextgal.ECare.Model.Message.Dto;
using Nextgal.ECare.WebTeleasistencia.App_Code.MessageManager;

namespace Nextgal.ECare.WebTeleasistencia.Services
{
    /// <summary>
    /// Summary description for SendMessage
    /// </summary>
    [WebService(Namespace = "http://ecare.nextgal.es/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SendMessage : System.Web.Services.WebService
    {

        [WebMethod]
        public bool SendMsg(string key,string telNumber, string msg)
        {
            try
            {
                if(key.Equals("Axj45TrVj"))
                {
                    //Gsm.GetInstance().EnviarMensaje(telNumber, msg);        
                }
                else
                {
                    return false;
                }
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }
    }
}
