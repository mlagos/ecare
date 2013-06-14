using System;
using Nextgal.ECare.Model.Notification.Facade;
using Obout.Grid;
using System.Web.UI;
using System.Collections;
using System.Configuration;
using Nextgal.ECare.Controller;
using System.Web.UI.WebControls;
using Nextgal.ECare.Model.User.Facade;
using Nextgal.ECare.Model.Event.Facade;

namespace Nextgal.ECare.WebTeleasistencia
{
    public partial class Zones : Page
    {
        private Grid zonesGrid = new Grid();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = Resources.Titles.zonesTitle;
            InitializeZonesTab();

            if (!IsPostBack)
            {
                LabelPageTitle.Text = Resources.Titles.zonesTitle;
            }
        }

        private void InitializeZonesTab()
        {
            zonesGrid.ID = "zonesGrid";
            zonesGrid.Width = new Unit("100%");
            zonesGrid.Serialize = true;
            zonesGrid.AutoGenerateColumns = false;
            zonesGrid.AutoPostBackOnSelect = true;
            zonesGrid.CallbackMode = false;
            zonesGrid.PageSize = 50;
            zonesGrid.FolderStyle = "~/css/obout/gridstyles/grand_gray";
            zonesGrid.FolderLocalization = "~/css/obout/gridlocalizations/";
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
            zonesGrid.Language = uiCulture;
            zonesGrid.AllowFiltering = true;
            zonesGrid.AllowAddingRecords = false;
            
            // Events
            zonesGrid.Select += new Grid.EventHandler(SelectRecord);
            zonesGrid.DeleteCommand += new Grid.EventHandler(DeleteRecord);

            // Creating the columns
            Column oCol1 = new Column();
            oCol1.DataField = "IdZone";
            oCol1.ReadOnly = true;
            oCol1.HeaderText = "ID";
            oCol1.Visible = false;

            Column oCol2 = new Column();
            oCol2.DataField = "Name";
            oCol2.HeaderText = Resources.Resources.name;
            oCol2.Width = "90%";

            Column oCol3 = new Column();
            oCol3.HeaderText = "";
            oCol3.Width = "10%";
            oCol3.AllowDelete = true;

           // Add the columns to the Columns collection of the grid
            zonesGrid.Columns.Add(oCol1);
            zonesGrid.Columns.Add(oCol2);
            zonesGrid.Columns.Add(oCol3);

            // Add the grid to the controls collection of the PlaceHolder
            phGrid1.Controls.Add(zonesGrid);

            if (!Page.IsPostBack)
            {
                LoadZones();
            }
        }

        private void LoadZones()
        {
            try
            {
                ArrayList zones = UserFacade.GetInstance().GetAllZoneByFamily(SessionManager.GetIdFamily(Context));
                zones.Sort();
                zonesGrid.DataSource = zones;
                zonesGrid.DataBind();
            }
            catch(Exception ex)
            {
            }
        }

        private void SelectRecord(object sender, GridRecordEventArgs e)
        {
            Hashtable records = (Hashtable)e.RecordsCollection[0];
            Response.Redirect("UpdateZone.aspx?idZone=" + records["IdZone"]);
        }

        private void DeleteRecord(object sender, GridRecordEventArgs e)
        {
            try
            {
                lblSaveData.Visible = false;
                int idZone = Convert.ToInt32(e.Record["IdZone"]);
                if (NotificationFacade.GetInstance().ExistsZoneNotification(idZone))
                {
                    lblSaveData.Visible = true;
                    lblSaveData.Text = Resources.Resources.existsEventsInZone;
                    lblSaveData.CssClass = "labelError";
                }
                else
                {
                    UserFacade.GetInstance().DeleteZone(idZone);
                    LoadZones();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
