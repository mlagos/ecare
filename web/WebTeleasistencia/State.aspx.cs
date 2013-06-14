using System;
using System.IO;
using System.Web;
using System.Web.UI;
using Nextgal.ECare.Controller;
using Nextgal.ECare.Model.Assisted.Dto;
using Nextgal.ECare.Model.Assisted.Facade;
using Nextgal.ECare.Model.Message.Dto;
using Nextgal.ECare.Model.Message.Facade;
using Nextgal.ECare.WebTeleasistencia.App_Code.MessageManager;
using PushSharp;
using PushSharp.Apple;

namespace Nextgal.ECare.WebTeleasistencia
{
    public partial class State :Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                txtMessageText.Attributes.Add("onfocus",
                   "javascript:textCounter(this, ctl00_MainContent_Window1_characters, 140)");
                txtMessageText.Attributes.Add("onkeydown",
                   "javascript:textCounter(this, ctl00_MainContent_Window1_characters, 140)");
                txtMessageText.Attributes.Add("onkeyup",
                   "javascript:textCounter(this, ctl00_MainContent_Window1_characters, 140)");
            }
        }

        protected void Send_Message(object sender, EventArgs e)
        {
            /*
            Gsm.GetInstance().EnviarMensaje(new CustomMessageDto(0, SessionManager.GetIdUser(Context), Convert.ToInt32(hfIdActive.Value), DateTime.MinValue, DateTime.MinValue,
                txtMessageText.Text.Trim(), hfPhone.Value));
            */

            AssistedDto assistedDto = AssistedFacade.GetInstance().FindAssistedByIdActive(Convert.ToInt32(hfIdActive.Value));

            //Almacenamos el mensaje en la BD
            MessageDto messageDto = new MessageDto(-1, SessionManager.GetIdUser(this.Context), Convert.ToInt32(hfIdActive.Value), DateTime.Now, DateTime.MinValue, txtMessageText.Text.Trim());
            MessageFacade.GetInstance().CreateMessage(messageDto);
            //Contamos los mensajes no leidos para enviarlo en la notificacion push
            int noLeidos = MessageFacade.GetInstance().CountMenssagesNotReaded(Convert.ToInt32(hfIdActive.Value));

            //Enviamos el mensaje por push al dispositivo
            if (assistedDto.DeviceToken.Length>0)
            {
                //Configure and start Apple APNS
                var appleCert =
                    File.ReadAllBytes(@"D:\WWW\familyecare.com\web\Resources\es.nextgal.familyecare.push.develop.p12");
                PushService push = new PushService();
                push.StartApplePushService(new ApplePushChannelSettings(false, appleCert, "n3xtg4l"));
                
                push.QueueNotification(NotificationFactory.Apple()
                    .ForDeviceToken(assistedDto.DeviceToken)
                    .WithAlert(txtMessageText.Text.Trim())
                    .WithSound("default")
                    .WithBadge(noLeidos));
                
                lblMessage.Text = Resources.Resources.sendMsgOk;
            }
            else
            {
                lblMessage.Text = Resources.Resources.NoPushDeviceToken;
            }

            
        }

        protected void New_Assisted(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx?returnUrl=State.aspx");
        }
    }
}
