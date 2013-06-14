using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nextgal.ECare.Controller;
using Nextgal.ECare.Model.Assisted.Dto;
using Nextgal.ECare.Model.Assisted.Facade;
using Nextgal.ECare.Model.Event.Dto;
using Nextgal.ECare.Model.Event.Facade;
using Nextgal.ECare.Model.Notification.Facade;
using PushSharp;
using PushSharp.Apple;

namespace Nextgal.ECare.WebTeleasistencia
{
    public partial class Schedule : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Page.Title = "Agenda";
                //if(Page.IsCallback)
                //{
                //    sch.Login(Convert.ToInt32(Request.QueryString["idActive"]));
                //}
                if (!IsPostBack)
                {
                    //sch.Login(Convert.ToInt32(Request.QueryString["idActive"]));

                    textoEvt.Attributes.Add("onkeydown",
                                            "javascript:textCounter(this, ctl00_MainContent_TabContainer1_TabPanel2_characters,148)");
                    textoEvt.Attributes.Add("onkeyup",
                       "javascript:textCounter(this,  ctl00_MainContent_TabContainer1_TabPanel2_characters,148)");
                    if (!String.IsNullOrEmpty(Request.QueryString["idActive"]))
                    {
                        string[] horas = { "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23" };
                        horaDDL.DataSource = horas;
                        horaDDL.DataBind();

                        string[] minutos = { "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59" };
                        minutoDDL.DataSource = minutos;
                        minutoDDL.DataBind();

                        AssistedDto assistedDto = AssistedFacade.GetInstance().FindAssistedByIdActive(
                            Convert.ToInt32(Request.QueryString["idActive"]));

                        //nombreLB.Text = assistedDto.Name;
                        //apellidosLB.Text = assistedDto.Surname;
                        //nacimientoLB.Text = assistedDto.BirthDate.ToShortDateString();
                        //identificadorLB.Text = assistedDto.Identifier;
                        //telefonoLB.Text = assistedDto.Phone;
                        //foto.ImageUrl = assistedDto.ImagePath;

                        ArrayList eventosFuturos = EventFacade.GetInstance().GetAllNoSentEvents(Convert.ToInt32(Request.QueryString["idActive"]));
                        //ArrayList eventos = new ArrayList();

                        //foreach (var eventoFuturo in eventosFuturos)
                        //{
                        //    eventos.Add(new EvtFut(((EventDto)eventoFuturo).Date,((EventDto)eventoFuturo).Text));
                        //}

                        gridEventos.RowDataBound += gridEventosDataBound;
                        gridEventos.DataSource = eventosFuturos;
                        //gridEventos.EmptyDataText = "No existen eventos futuros.";
                        gridEventos.EmptyDataText = Resources.Resources.notExistEvent;
                        gridEventos.DataBind();
                        gridEventos.Columns[2].ItemStyle.Width = 14;

                        //if ((assistedDto.ImagePath == null) || (assistedDto.ImagePath.Trim().Count() == 0))
                        //    foto.ImageUrl = ConfigurationManager.AppSettings["AssistedImageUnavailable"];
                        //else
                        //    foto.ImageUrl = assistedDto.ImagePath;

                        //foto.Height = 90;
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }

        protected void ProcessCommand(object sender, CommandEventArgs e)
        {
            msg.Visible = false;
            if (e.CommandName.Equals("delete"))
            {
                try
                {
                    int idEvent = Convert.ToInt32((string)e.CommandArgument);
                    EventFacade.GetInstance().DeleteEvent(idEvent);
                    LoadEvents();
                    //msg.CssClass = "labelOk";
                    //msg.Text = "Evento eliminado correctamente.";
                    //msg.Visible = true;
                }
                catch (Exception)
                {
                    msg.CssClass = "labelError";
                    //msg.Text = "Se ha producido un error al eliminar el evento.";
                    msg.Text = Resources.Resources.errorDeleteEvent;
                    msg.Visible = true;
                }
            }
        }       

        private void LoadEvents()
        {
            ArrayList eventosFuturos = EventFacade.GetInstance().GetAllNoSentEvents(Convert.ToInt32(Request.QueryString["idActive"]));

            gridEventos.RowDataBound += gridEventosDataBound;
            gridEventos.DataSource = eventosFuturos;
            gridEventos.EmptyDataText = Resources.Resources.notExistEvent;
            gridEventos.DataBind();
        }

        protected void nuevoEventoBT_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Request.QueryString["idActive"]))
                {

                    EventDto dto = new EventDto(-1, Convert.ToInt32(Request.QueryString["idActive"]),
                                                DateTime.Parse(fechaEvt.Text + " " + horaDDL.Text + ":" + minutoDDL.Text),
                                                textoEvt.Text, DateTime.MinValue);
                    dto = EventFacade.GetInstance().CreateEvent(dto);
                    horaDDL.SelectedIndex = 0;
                    minutoDDL.SelectedIndex = 0;
                    fechaEvt.Text = "";
                    textoEvt.Text = "";
                    //ArrayList eventosFuturos = EventFacade.GetInstance().GetAllNoSentEvents(Convert.ToInt32(Request.QueryString["idActive"]));
                    //ArrayList eventos = new ArrayList();
                    //foreach (var eventoFuturo in eventosFuturos)
                    //{
                    //    eventos.Add(new EvtFut(((EventDto)eventoFuturo).Date, ((EventDto)eventoFuturo).Text));
                    //}

                    //gridEventos.DataSource = eventos;
                    //gridEventos.DataBind();

                    //Notificamos por un mensaje Push especial al dispositivo que hay un evento nuevo
                    AssistedDto assistedDto = AssistedFacade.GetInstance().FindAssistedByIdActive(Convert.ToInt32(Request.QueryString["idActive"]));
                    if (assistedDto.DeviceToken.Length > 0)
                    {
                        //Configure and start Apple APNS
                        var appleCert =
                            File.ReadAllBytes(@"D:\WWW\familyecare.com\web\Resources\es.nextgal.familyecare.push.develop.p12");
                        PushService push = new PushService();
                        push.StartApplePushService(new ApplePushChannelSettings(false, appleCert, "n3xtg4l"));

                        push.QueueNotification(NotificationFactory.Apple()
                            .ForDeviceToken(assistedDto.DeviceToken)
                            .WithCustomItem("EVT", dto.IdEvent)); //notificamos al dispositivo de un nuevo evento y enviamos el id del evento
                    }

                    msg.CssClass = "labelOk";
                    msg.Text = Resources.Resources.createOkEvent;
                    msg.Visible = true;

                    LoadEvents();
                }
            } catch(Exception ex)
            {
                msg.CssClass = "labelError";
                msg.Text = Resources.Resources.errorCreateEvent;
                msg.Visible = true;
            }
        }

        protected void volverBT_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["returnUrl"]))
            {
                string s = Request.QueryString["returnUrl"];
                s += "?";
                if (!String.IsNullOrEmpty(Request.QueryString["idActive"]))
                {
                    s += "&idActive=" + Request.QueryString["idActive"];
                }
                if (!String.IsNullOrEmpty(Request.QueryString["endDate"]))
                {
                    s += "&endDate=" + Request.QueryString["endDate"];
                }
                if (!String.IsNullOrEmpty(Request.QueryString["startDate"]))
                {
                    s += "&startDate=" + Request.QueryString["startDate"];
                }
                if (!String.IsNullOrEmpty(Request.QueryString["startHour"]))
                {
                    s += "&startHour=" + Request.QueryString["startHour"];
                }
                if (!String.IsNullOrEmpty(Request.QueryString["endHour"]))
                {
                    s += "&endHour=" + Request.QueryString["endHour"];
                }
                s = s.Replace("?&", "?");
                Response.Redirect(s);
            }
        }

        protected void gridEventosDataBound(Object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Display the company name in italics.
                e.Row.Cells[0].Text = DateTime.Parse(e.Row.Cells[0].Text).ToString("dd/MM/yyyy HH:mm:ss");

            }

        }


        
    }

    class EvtFut
    {
        private DateTime m_Date;

        public DateTime Fecha
        {
            get { return m_Date; }
            set { m_Date = value; }
        }

        private string m_Text;

        public string Texto
        {
            get { return m_Text; }
            set { m_Text = value; }
        }

        public EvtFut(DateTime fecha, string texto)
        {
            m_Date = fecha;
            m_Text = texto;
        }
    }
}
