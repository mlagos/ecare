using System;
using System.Web.UI;
using System.Collections;
using Nextgal.ECare.Controller;
using System.Web.UI.WebControls;
using Nextgal.ECare.Model.User.Facade;
using Nextgal.ECare.Model.Notification.Dto;
using Nextgal.ECare.Model.Notification.Facade;

namespace Nextgal.ECare.WebTeleasistencia
{
    public partial class Alerts : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["idActive"]))
                {
                    LoadZones();
                    LoadAlarms();    
                }
                else
                {
                    if (!String.IsNullOrEmpty(Request.QueryString["returnUrl"]))
                    {
                        Response.Redirect(Request.QueryString["returnUrl"]);
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
            }
        }

        private void LoadZones()
        {
            ArrayList zones = UserFacade.GetInstance().GetAllZoneByFamily(Convert.ToInt32(SessionManager.GetIdFamily(Context)));
            ddlZones.DataValueField = "IdZone";
            ddlZones.DataTextField = "Name";
            ddlZones.DataSource = zones;
            ddlZones.DataBind();
        }

        private void LoadAlarms()
        {
            ArrayList notifications = NotificationFacade.GetInstance().GetAllByIdActiveAndIdUser(Convert.ToInt32(Request.QueryString["idActive"]), Convert.ToInt32(SessionManager.GetIdUser(Context)), 1);
            gridAlarms.DataSource = notifications;
            gridAlarms.DataBind();
            ArrayList notifications2 = NotificationFacade.GetInstance().GetAllByIdActiveAndIdUser(Convert.ToInt32(Request.QueryString["idActive"]), Convert.ToInt32(SessionManager.GetIdUser(Context)), 2);
            batteryGrid.DataSource = notifications2;
            foreach (NotificationDto not in notifications2)
            {
                not.AlarmType = GetGlobalResourceObject("Resources", not.AlarmType).ToString();
            }
            batteryGrid.DataBind();
            if (NotificationFacade.GetInstance().ExistsBatteryNotification(Convert.ToInt32(Request.QueryString["idActive"]), SessionManager.GetIdUser(Context)))
            {
                //Button2.Visible = false;
                //cbxBatteryLevel.Visible = false;
                //cbxSms2.Visible = false;
                //cbxEmail2.Visible = false;
                alertLine.Visible = false;
            }
            else
            {
                alertLine.Visible = true;
                //Button2.Visible = true;
                //cbxBatteryLevel.Visible = true;
                //cbxSms2.Visible = true;
                //cbxEmail2.Visible = true;
            }
        }

        protected void guardar_Click(object sender, EventArgs e)
        {
            msg.Visible = false;
            try
            {
                if (!NotificationFacade.GetInstance().ExistsZoneNotification(Convert.ToInt32(Request.QueryString["idActive"]), Convert.ToInt32(ddlZones.SelectedValue)))
                {
                    NotificationFacade.GetInstance().CreateNotification(new NotificationDto(-1,Convert.ToInt32(Request.QueryString["idActive"]),Convert.ToInt32(ddlZones.SelectedValue),1,Convert.ToInt32(SessionManager.GetIdUser(Context)),cbxInAlarm.Checked,cbxOutAlarm.Checked,cbxSms.Checked,cbxMail.Checked));

                    LoadAlarms();
                    cbxInAlarm.Checked = false;
                    cbxOutAlarm.Checked = false;
                    cbxSms.Checked = false;
                    cbxMail.Checked = false;
                    msg.CssClass = "labelOk";
                    msg.Text = Resources.Resources.alertOK;
                    msg.Visible = true;
                }
                //Duplicate notification
                else
                {
                    msg.CssClass = "labelError";
                    msg.Text = Resources.Resources.alertZone;
                    msg.Visible = true;
                }
            }
            catch (Exception)
            {
                msg.CssClass = "labelError";
                msg.Text = Resources.Resources.alertError;
                msg.Visible = true;
            }
        }

        protected void guardar2_Click(object sender, EventArgs e)
        {
            msg.Visible = false;
            try
            {
                if (cbxBatteryLevel.Checked && !NotificationFacade.GetInstance().ExistsBatteryNotification(Convert.ToInt32(Request.QueryString["idActive"]), SessionManager.GetIdUser(Context)))
                {
                    NotificationFacade.GetInstance().CreateNotification(new NotificationDto(-1, Convert.ToInt32(Request.QueryString["idActive"]), null, 2, Convert.ToInt32(SessionManager.GetIdUser(Context)), false, false, cbxSms2.Checked, cbxEmail2.Checked));
                    LoadAlarms();
                    cbxSms2.Checked = false;
                    cbxEmail2.Checked = false;
                    msg.CssClass = "labelOk";
                    msg.Text = Resources.Resources.alertOK;
                    msg.Visible = true;
                }
                else
                {
                    if (!cbxBatteryLevel.Checked)
                    {
                        msg.Text = Resources.Resources.alertSelect;
                    }
                    //Duplicate notification
                    else
                    {
                        msg.Text = Resources.Resources.alertZone;
                    }
                    msg.CssClass = "labelError";
                    msg.Visible = true;
                }
            }
            catch (Exception)
            {
                msg.CssClass = "labelError";
                msg.Text = Resources.Resources.alertError;
                msg.Visible = true;
            }
        }

        protected void ProcessCommand(object sender, CommandEventArgs e)
        {
            msg.Visible = false;
            if(e.CommandName.Equals("delete"))
            {
                try
                {
                    int idNotification = Convert.ToInt32((string)e.CommandArgument);
                    NotificationFacade.GetInstance().DeleteNotification(idNotification);
                    LoadAlarms();
                    msg.CssClass = "labelOk";
                    msg.Text = Resources.Resources.alertDelete;
                    msg.Visible = true;
                }
                catch(Exception)
                {
                    msg.CssClass = "labelError";
                    msg.Text = Resources.Resources.alertError;
                    msg.Visible = true;
                }
            }
        }

        protected void volverBT_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["returnUrl"]))
            {
                Response.Redirect(Request.QueryString["returnUrl"]);
            }
            else
            {
                Response.Redirect("State.aspx");
            }
        }
    }
}
