using System;
using Nextgal.ECare.WebTeleasistencia.App_Code.MessageManager;

namespace Nextgal.ECare.WebTeleasistencia
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Gsm.GetInstance().StartTimer();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
           Gsm.GetInstance().StopTimer();
        }
    }
}
