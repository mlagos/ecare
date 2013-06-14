using System;
using Obout.Grid;
using System.Web.UI;
using System.Collections;
using System.Configuration;
using Nextgal.ECare.Controller;
using System.Web.UI.WebControls;
using Nextgal.ECare.Common.Util;
using System.Collections.Generic;
using Nextgal.ECare.Model.Position.Dto;
using Nextgal.ECare.Model.Assisted.Dto;
using Nextgal.ECare.Model.Assisted.Facade;
using Nextgal.ECare.Model.Position.Facade;
using Nextgal.ECare.WebTeleasistencia.Util;

namespace Nextgal.ECare.WebTeleasistencia.UserControls
{
    public partial class MonitoringReport : UserControl
    {
        private Grid Grid1 = new Grid();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Grid1.ID = "grid1";
            Grid1.Width = new Unit("100%");
            Grid1.Serialize = true;
            Grid1.AutoGenerateColumns = false;
            Grid1.FolderStyle = "~/css/obout/gridstyles/grand_gray";
            Grid1.FolderLocalization = "~/css/obout/gridlocalizations/";
            string uiCulture = "";
            try
            {
                uiCulture = Request.UserLanguages[0];
                if (String.IsNullOrEmpty(uiCulture))
                {
                    uiCulture = "es";
                }
                else
                {
                    if (uiCulture.Contains("-"))
                    {
                        uiCulture = uiCulture.Split('-')[0];
                    }

                }
                if (uiCulture != "es" && uiCulture != "gl")
                    uiCulture = "es";
            }
            catch (Exception)
            {
                uiCulture = "es";
            }
            Grid1.Language = uiCulture;
            Grid1.AllowFiltering = true;
            Grid1.AllowAddingRecords = false;
            Grid1.AllowGrouping = false;
            Grid1.AllowRecordSelection = false;

            //Add events
            Grid1.RowDataBound += grid1_RowDataBound;

            //Creating the columns
            Column oCol1 = new Column();
            oCol1.DataField = "IdActive";
            oCol1.ReadOnly = true;
            oCol1.HeaderText = "ID";
            oCol1.Visible = false;

            Column oCol2 = new Column();
            oCol2.ParseHTML = true;
            oCol2.AllowFilter = false;
            oCol2.Align = "center";
            oCol2.AllowGroupBy = false;
            oCol2.Width = "8%";

            Column oCol3 = new Column();
            oCol3.DataField = "ActiveName";
            oCol3.HeaderText = Resources.Resources.name;
            oCol3.Width = "15%";
            oCol3.Wrap = true;

            Column oCol8 = new Column();
            oCol8.DataField = "Location";
            oCol8.ParseHTML = true;
            oCol8.AllowFilter = false;
            oCol8.AllowGroupBy = false;
            oCol8.Wrap = true;
            oCol8.HeaderText = Resources.Resources.address;
            oCol8.Width = "27%";

            Column oCol9 = new Column();
            oCol9.DataField = "LastPosDate";
            oCol9.HeaderText = Resources.Resources.lastPos;
            oCol9.Wrap = true;
            oCol9.ParseHTML = true;
            oCol9.AllowFilter = false;
            oCol9.AllowGroupBy = false;
            oCol9.Width = "20%";

            Column oCol10 = new Column();
            oCol10.DataField = "Status";
            oCol10.HeaderText = Resources.Resources.status;
            oCol10.ParseHTML = true;
            oCol10.AllowFilter = false;
            oCol10.HeaderAlign = "center";
            oCol10.Align = "center";
            oCol10.AllowGroupBy = false;
            oCol10.Width = "10%";

            Column oCol11 = new Column();
            oCol11.ParseHTML = true;
            oCol11.AllowFilter = false;
            oCol11.AllowGroupBy = false;
            oCol11.Align = "center";
            oCol11.Width = "20%";

            // Add columns to the Grid Column collection
            Grid1.Columns.Add(oCol1);
            Grid1.Columns.Add(oCol2);
            Grid1.Columns.Add(oCol3);
            Grid1.Columns.Add(oCol8);
            Grid1.Columns.Add(oCol9);
            Grid1.Columns.Add(oCol10);
            Grid1.Columns.Add(oCol11);

            phGrid1.Controls.Add(Grid1);

            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            List<MonitoringReportDto> lastPositions = PositionFacade.GetInstance().GetMonitoringReportDataByIdUser(Convert.ToInt32(SessionManager.GetIdFamily(Context)));

            hfPositions.Value = "";
            hfAddress.Value = "";
            hfAlready.Value = "False";
            if (lastPositions.Count > 0)
            {
                ArrayList copia = new ArrayList();
                ArrayList original = new ArrayList();
                foreach (MonitoringReportDto dto in lastPositions)
                {
                    copia.Add(dto);
                    original.Add(dto);
                }
                PositionFacade positionFacade = PositionFacade.GetInstance();
                int i = 0;
                foreach (MonitoringReportDto lastPosition in lastPositions)
                {
                    try
                    {
                        PositionDto lastPosDto = positionFacade.FindPositionByDate(DateTime.Parse(lastPosition.LastPosDate.ToString()), lastPosition.IdActive);
                        String lat = lastPosDto.Latitude.ToString();
                        lat = lat.Replace(",", ".");
                        String lon = lastPosDto.Longitude.ToString();
                        lon = lon.Replace(",", ".");
                        hfPositions.Value += i + "#" + lat + "@" + lon + ";";
                    }
                    catch (Exception)
                    {
                        hfPositions.Value += i + "#@;";
                    }
                    i++;
                }
                ViewState["copia"] = copia;
                ViewState["original"] = original;
            }

            MapUtils.FillActiveStatus(ref lastPositions);
            Grid1.DataSource = lastPositions;
            Grid1.DataBind();
            ScriptManager.RegisterStartupScript(Page, typeof (string), "getAddress", "initialize();theNext('" + hfPositions.Value + "','" + hfAddress.Value + "',0);", true);
        }

        public void grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
            AssistedDto activeDto = AssistedFacade.GetInstance().FindAssistedByIdActive(Int32.Parse(e.Row.Cells[0].Text));

            if(!String.IsNullOrEmpty(activeDto.ImagePath))
            {
                e.Row.Cells[1].Text = "<img src='" + activeDto.ImagePath + "'>";    
            }
            else
            {
                e.Row.Cells[1].Text = "<img src='" + ConfigurationManager.AppSettings["assistedImageUnavailable"] + "'>";
            }
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].VerticalAlign = VerticalAlign.Middle;

            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[6].VerticalAlign = VerticalAlign.Middle;

            if (String.IsNullOrEmpty(e.Row.Cells[4].Text))
            {
                e.Row.Cells[4].Text = "";
            }
            else
            {
                e.Row.Cells[4].Text = Utils.ConvertToLclTime(DateTime.Parse(e.Row.Cells[4].Text)).ToString("dd/MM/yyyy HH:mm:ss");
                e.Row.Cells[6].Text = "<a href='MapLastPosition.aspx?idActive=" + Convert.ToInt32(e.Row.Cells[0].Text) + "'><img src='images/icons/find.gif' alt='" + Resources.Resources.seeMap + "' title='" + Resources.Resources.seeMap + "'/></a>&nbsp;";
            }
            e.Row.Cells[6].Text = e.Row.Cells[6].Text + "<a href='#' onclick='Window1.Open();Window1.screenCenter();FillParameters(" + activeDto.IdActive + "," + activeDto.Phone + ");'><img src='images/icons/message.gif' alt='" + Resources.Resources.sendMsg + "' title='" + Resources.Resources.sendMsg + "'/></a>&nbsp;";
            e.Row.Cells[6].Text = e.Row.Cells[6].Text + "<a href='Schedule.aspx?idActive=" + Convert.ToInt32(e.Row.Cells[0].Text) + "&returnUrl=State.aspx'><img alt='" + Resources.Resources.programmingEvents + "' title='" + Resources.Resources.programmingEvents + "' tooltip='" + Resources.Resources.programmingEvents + "' src='images/icons/calendar.gif'/></a>&nbsp;";
            e.Row.Cells[6].Text = e.Row.Cells[6].Text + "<a href='UserProfile.aspx?idActive=" + Convert.ToInt32(e.Row.Cells[0].Text) + "&returnUrl=State.aspx'><img alt='" + Resources.Resources.updateProfile + "' title='" + Resources.Resources.updateProfile + "' tooltip='" + Resources.Resources.updateProfile + "' src='images/icons/user.gif'/></a>&nbsp;";
            e.Row.Cells[6].Text = e.Row.Cells[6].Text + "<a href='Messages.aspx?idActive=" + Convert.ToInt32(e.Row.Cells[0].Text) + "&returnUrl=State.aspx'><img alt='" + Resources.Resources.hMessages + "' title='" + Resources.Resources.hMessages + "' tooltip='" + Resources.Resources.hMessages + "' src='images/icons/mailbox.png'/></a>";
            e.Row.Cells[6].Text = e.Row.Cells[6].Text + "<a href='Alerts.aspx?idActive=" + Convert.ToInt32(e.Row.Cells[0].Text) + "&returnUrl=State.aspx'><img alt='" + Resources.Resources.alerts + "' title='" + Resources.Resources.alerts + "' tooltip='" + Resources.Resources.alerts + "' src='images/icons/alert.png'/></a>";
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void imputHide_Click(object sender, EventArgs e)
        {
            hfAlready.Value = "True";
            ArrayList direcciones = new ArrayList();
            if (hfAddress.Value.Length != 0)
            {
                //Separo las direcciones que hay en el hidden field
                string[] directions = hfAddress.Value.Split('#');
                for (int i = 0; i < directions.Length; i++)
                {
                    string[] separate = directions[i].Split('&');
                    if (separate.Length > 1)
                    {
                        if (separate[0].Equals("") /*|| separate[0].Equals("No disponible")*/ || separate[0].Equals("North Atlantic Ocean"))
                        {
                            direcciones.Add("");
                        }
                        else
                        {
                            direcciones.Add(separate[0]);
                        }
                    }
                }
            }

            ArrayList copia = (ArrayList)ViewState["copia"];
            for (int i = 0; i < direcciones.Count;i++ )
            {
                MonitoringReportDto dto = (MonitoringReportDto)copia[i];
                if (dto.LastPosDate.Equals(DateTime.MinValue))
                {
                    dto.LastPosDate = null;
                }
                dto.Location = direcciones[i].ToString();
            }
            
            MapUtils.FillActiveStatus(ref copia);
            Grid1.DataSource = copia;
            Grid1.DataBind();
        }
    }
}