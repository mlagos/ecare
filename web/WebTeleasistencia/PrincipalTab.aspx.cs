using System;
using System.IO;
using System.Web.UI;
using System.Configuration;
using Nextgal.ECare.Controller;
using Nextgal.ECare.Common.Util;
using System.Collections.Specialized;
using Nextgal.ECare.Model.Message.Dto;
using Nextgal.ECare.Model.Assisted.Dto;
using Nextgal.ECare.Model.Message.Facade;
using Nextgal.ECare.Model.Position.Dto;
using Nextgal.ECare.Model.Assisted.Facade;
using Nextgal.ECare.Model.Position.Facade;
using Nextgal.ECare.WebTeleasistencia.Util;
using Nextgal.ECare.WebTeleasistencia.App_Code.MessageManager;
using System.Globalization;
using System.Web;
using PushSharp;
using PushSharp.Apple;

namespace Nextgal.ECare.WebTeleasistencia
{
    public partial class PrincipalTab : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtMessageText.Attributes.Add("onkeydown", "javascript:textCounter(this, Window1_characters, 140)");
                    txtMessageText.Attributes.Add("onkeyup", "javascript:textCounter(this, Window1_characters, 140)");
                    lblMessage.Attributes.Add("onprerender", "javascript:ClearSms(Window1_lblMessage)");
                    //CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(HttpContext.Current.Request.UserLanguages[0]);

                    if (!String.IsNullOrEmpty(Request.QueryString["idActive"]) && !String.IsNullOrEmpty(Request.QueryString["posDate"]))
                    {
                        #region Assisted data

                        DateTime date = DateTime.Parse(Request.QueryString["posDate"]);
                        int idActive = Int32.Parse(Request.QueryString["idActive"]);
                        string status = PositionUtils.GetActiveStatus(idActive, date);
                        hfIdActive.Value = idActive.ToString();
                        AssistedDto assistedDto = AssistedFacade.GetInstance().FindAssistedByIdActive(idActive);
                        PositionDto positionDto = PositionFacade.GetInstance().FindPositionByDate(date, idActive);
                        lblPosDate.Text = Utils.ConvertToLclTime(date).ToString("dd/MM/yyyy");
                        //if (status.Equals("parked"))
                        //{
                        //    PositionDto next = PositionFacade.GetInstance().GetNextPosition(idActive, date, false);
                        //    lblPosTime.Text = Utils.ConvertToLclTime(date).ToLongTimeString() + " - " + Utils.ConvertToLclTime(next.Date).ToLongTimeString(); ;
                        //}
                        //else
                        //{
                            lblPosTime.Text = Utils.ConvertToLclTime(date).ToLongTimeString();
                        //}
                        lblAge.Text = Utils.CalculateAge(assistedDto.BirthDate) + " " + Resources.Resources.years;
                        lblPhone.Text = assistedDto.Phone.Trim();
                        lblAssistedName.Text = assistedDto.Name.Trim() + " " + assistedDto.Surname.Trim();

                        #endregion

                        #region Battery level

                        string batLvl = PositionUtils.GetBatteryLevel(Int32.Parse(Request.QueryString["idActive"]), date);

                        if (!String.IsNullOrEmpty(batLvl))
                        {
                            batLvl = (Double.Parse(batLvl)).ToString();
                            batLevel.Visible = true;
                            string path = PositionUtils.GetBatteryLevelIcon(batLvl);
                            batLevel.ImageUrl = path.Trim();
                            batLevel.AlternateText = batLvl + "%";
                            batLevel.ToolTip = batLvl + "%";
                        }

                        #endregion

                        #region Assisted image

                        if (!String.IsNullOrEmpty(assistedDto.ImagePath))
                        {
                            string image = assistedDto.ImagePath;
                            assistedImage.ImageUrl = image;
                            assistedImage.AlternateText = assistedDto.Name + " " + assistedDto.Surname;
                        }
                        else
                        {
                            assistedImage.ImageUrl = ConfigurationManager.AppSettings["AssistedImageUnavailable"];
                        }

                        #endregion

                        #region Status icon

                        
                        lblStatus.Text = (string)GetGlobalResourceObject("Resources", status);
                        imgStatusIcon.ImageUrl = MapUtils.GetReportStatusIcon32x32Path(positionDto, status);
                        imgStatusIcon.AlternateText = (string)GetGlobalResourceObject("Resources", status);
                        imgStatusIcon.ToolTip = (string)GetGlobalResourceObject("Resources", status);
                        if (status.Equals("moving"))
                        {
                            OrderedDictionary addInfo = PositionFacade.GetInstance().GetAllAdditionalInformationByDate(date, idActive);
                            if (addInfo.Contains("speed"))
                            {
                                lclSpeed.Visible = true;
                                lblSpeed.Text = addInfo["speed"]+"km/h";
                            }
                        }

                        #endregion

                        #region Buttons

                        if(!String.IsNullOrEmpty(Request.QueryString["startDate"]) && !String.IsNullOrEmpty(Request.QueryString["endDate"]))
                        {
                            DateTime startDate = DateTime.Parse(Request.QueryString["startDate"]);
                            DateTime endDate = DateTime.Parse(Request.QueryString["endDate"]);
                            string sDate = startDate.ToShortDateString().Replace('/', '-');
                            string sHour = startDate.ToShortTimeString();
                            if (startDate.Hour<10)
                                sHour = "0" + sHour;
                            string eDate = endDate.ToShortDateString().Replace('/', '-');
                            string eHour = endDate.ToShortTimeString();
                            if (endDate.Hour < 10)
                                eHour = "0" + eHour;

                            hfFollow.Value =   idActive + "," + sDate + "," + sHour + "," + eDate + "," + eHour;
                            hfSchedule.Value = idActive + "," + sDate + "," + sHour + "," + eDate + "," + eHour;
                            hfAssisted.Value = idActive + "," + sDate + "," + sHour + "," + eDate + "," + eHour;
                            hfMessages.Value = idActive + "," + sDate + "," + sHour + "," + eDate + "," + eHour;
                        }
                        
                        txtMessageNumber.Text = assistedDto.Phone;
                        btnSMS.OnClientClick = "Window1.Open();Window1.screenCenter();FillParameters("+assistedDto.Phone+");";
                        if (!String.IsNullOrEmpty(Request.QueryString["source"]))
                        {
                            string source;
                            if (Request.QueryString["source"].Equals("routes"))
                            {
                                btnRoutes.Style["display"] = "none";
                                source = "MapTraceRoute.aspx?";
                            }
                            else
                            {
                                btnLastPos.Style["display"] = "none";
                                source = "MapLastPosition.aspx?";
                            }
                            hfLocation.Value = source;
                        }
                        else
                        {
                            btnLastPos.Style["display"] = "none";
                        }

                        #endregion

                        #region Inverse geocoding

                        //Paso la posicion a un hidden field para calcular el geocoding inverso
                        String latitud = positionDto.Latitude.ToString();
                        latitud = latitud.Replace(",", ".");
                        String longitud = positionDto.Longitude.ToString();
                        longitud = longitud.Replace(",", ".");
                        hfPosition.Value += latitud + "," + longitud;

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void Send_Message(object sender, EventArgs e)
        {
            //Gsm.GetInstance().EnviarMensaje(new CustomMessageDto(0, SessionManager.GetIdUser(Context), Convert.ToInt32(hfIdActive.Value), DateTime.MinValue, DateTime.MinValue,
            //                                      txtMessageText.Text.Trim(), txtMessageNumber.Text.Trim()));
            //lblMessage.Text = "El mensaje ha sido enviado";
            //txtMessageText.Text = "";

            AssistedDto assistedDto = AssistedFacade.GetInstance().FindAssistedByIdActive(Convert.ToInt32(hfIdActive.Value));

            //Almacenamos el mensaje en la BD
            MessageDto messageDto = new MessageDto(-1, SessionManager.GetIdUser(this.Context), Convert.ToInt32(hfIdActive.Value), DateTime.Now, DateTime.MinValue, txtMessageText.Text.Trim());
            MessageFacade.GetInstance().CreateMessage(messageDto);
            //Contamos los mensajes no leidos para enviarlo en la notificacion push
            int noLeidos = MessageFacade.GetInstance().CountMenssagesNotReaded(Convert.ToInt32(hfIdActive.Value));

            //Enviamos el mensaje por push al dispositivo
            if (assistedDto.DeviceToken.Length > 0)
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
    }
}
