using System;
using System.IO;
using System.Text;
using System.Web.UI;
using Subgurim.Controles;
using System.Collections;
using System.Globalization;
using System.Configuration;
using Nextgal.ECare.Controller;
using Nextgal.ECare.Common.Util;
using System.Collections.Generic;
using Nextgal.ECare.Model.User.Dto;
using Nextgal.ECare.Model.User.Facade;
using Nextgal.ECare.Common.Exceptions;
using Nextgal.ECare.Model.Assisted.Dto;
using Nextgal.ECare.Model.Position.Dto;
using Nextgal.ECare.Model.Assisted.Facade;
using Nextgal.ECare.Model.Position.Facade;
using Nextgal.ECare.WebTeleasistencia.Util;
using Subgurim.Maps.V3;

namespace Nextgal.ECare.WebTeleasistencia
{
    public partial class MapLastPosition : Page
    {
        private bool changes; 

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Page.Title = Resources.Titles.mapLastPositionTitle;
                ScriptManager.RegisterStartupScript(Page, typeof(string), "resize", "resizeMap();", true);
                if (!IsPostBack)
                {
                    ArrayList zones = UserFacade.GetInstance().GetAllZoneByFamily(SessionManager.GetIdFamily(Context));
                    BindZones(zones);
                    TimerRefreshMap.Enabled = false;
                    TimerRefreshMap.Interval = 30000;
                    var actives = AssistedFacade.GetInstance().GetAllAssistedByFamily(SessionManager.GetIdFamily(Context));
                    AssistedDataBind(actives);
                    InicializarMapa();

                    if(!String.IsNullOrEmpty(Request.QueryString["idActive"]) && !changes)
                        ShowIdActive(Int32.Parse(Request.QueryString["idActive"]));
                    LoadData(true);
                } 
            }
            catch(Exception ex)
            {
            }
        }

        /// <summary>
        /// Carga el mapa y todos sus controles
        /// </summary>
        private void InicializarMapa()
        {
            GMap1.addControl(new GControl(GControl.preBuilt.LargeMapControl3D));
            GMap1.addMapType(GMapType.GTypes.Physical);
            GMap1.addControl(new GControl(GControl.preBuilt.NavLabelControl, new GControlPosition(GControlPosition.position.Bottom_Right)));
            GMap1.addControl(new GControl(GControl.preBuilt.ScaleControl));
            var latlon = new LatLng(Double.Parse(ConfigurationManager.AppSettings["mapCenterLat"].Replace(".", ","), new CultureInfo("es-ES")), Double.Parse(ConfigurationManager.AppSettings["mapCenterLng"].Replace(".", ","), new CultureInfo("es-ES")));
            var zoom = 6;
            try
            {
                zoom = Convert.ToInt32(ConfigurationManager.AppSettings["mapCenterZoom"]);
            }
            catch(Exception){}
            GLatLng gLatLng = new GLatLng(latlon.Lat, latlon.Lng);
            GMap1.setCenter(gLatLng, zoom);

            LoadMapScripts();
        }

        /// <summary>
        /// Loads map scripts
        /// </summary>
        protected void LoadMapScripts()
        {
            //var sb = new StringBuilder();
            //sb.Append("var control = new GMenuMapTypeControl();");            
            //sb.AppendFormat("{0}.addControl(control);", GMap1.GMap_Id);
            //sb.AppendFormat("GEvent.addListener({0}, \"infowindowopen\", function()", GMap1.GMap_Id);
            //sb.Append("{");
            //sb.AppendFormat("{0}.savePosition();", GMap1.GMap_Id);
            //sb.Append("});");
            //sb.AppendFormat("GEvent.addListener({0}, \"infowindowclose\", function()", GMap1.GMap_Id);
            //sb.Append("{");
            //sb.AppendFormat("{0}.returnToSavedPosition();", GMap1.GMap_Id);
            //sb.Append("});");
            //GMap1.addCustomInsideJavascript(sb.ToString());
        }

        /// <summary>
        /// Manages the diferent searches
        /// </summary>
        protected void LoadData(bool loadAll)
        {
            var positions = new ArrayList();
            try
            {
                if (activesCheckBoxList.SelectedIndex > -1)
                {
                    for (int i = 0; i < activesCheckBoxList.Items.Count; i++)
                    {
                        if (activesCheckBoxList.Items[i].Selected)
                        {
                            try
                            {
                                
                                PositionDto pos = PositionFacade.GetInstance().FindLastPositionActive(Int32.Parse(activesCheckBoxList.Items[i].Value), true);
                                positions.Add(pos);
                            }
                            catch (InstanceNotFoundException ex)
                            {
                            }
                        }
                    }
                }
                else if (loadAll)
                {
                    positions = PositionFacade.GetInstance().GetAllActiveLastKnownPosition(true);
                    CheckAll();
                }
                ConfigureMapItems(positions);

            }
            catch (InstanceNotFoundException)
            {
            }
        }

        /// <summary>
        /// Manage searches when when a get param exists
        /// </summary>
        /// <param name="idActive">active´s id</param>
        protected void ShowIdActive(int idActive)
        {
            try
            {
                var positions = new ArrayList();
                AssistedDto activeDto = AssistedFacade.GetInstance().FindAssistedByIdActive(idActive);
                if(activeDto != null)
                {
                    ArrayList actives = AssistedFacade.GetInstance().GetAllAssistedByFamily(SessionManager.GetIdFamily(Context));
                    AssistedDataBind(actives);
                    
                    for (int i = 0; i < activesCheckBoxList.Items.Count; i++)
                    {
                        activesCheckBoxList.Items[i].Selected = activesCheckBoxList.Items[i].Value == idActive.ToString();
                    }

                    PositionDto pos = PositionFacade.GetInstance().FindLastPositionActive(idActive, true);
                    positions.Add(pos);
                    ConfigureMapItems(positions);
                }
            }
            catch(NotAllowedException)
            {
                Response.Redirect("~/InternalError.aspx");
            }
        }

        /// <summary>
        /// Bind data into assisted DropDownList
        /// </summary>
        /// <param name="assisted">Assisted data</param>
        protected void AssistedDataBind(ArrayList assisted)
        {
            assisted.Sort();
            activesCheckBoxList.DataTextField = "name";
            activesCheckBoxList.DataValueField = "idActive";
            activesCheckBoxList.DataSource = assisted;
            activesCheckBoxList.DataBind();
        }

        /// <summary>
        /// Draw the diferent items in the map
        /// </summary>
        /// <param name="positions">positions where the items are added</param>
        protected void ConfigureMapItems(ArrayList positions)
        {
            var puntos = new List<GLatLng>();
            foreach (PositionDto posDto in positions)
            {
                var coord = new GLatLng(posDto.Latitude, posDto.Longitude);
                puntos.Add(coord);
                string status = PositionUtils.GetActiveStatus(posDto.IdActive, posDto.Date);
                //GInfoWindow window = new GInfoWindow("", );
                //var lastPosTabs = new List<GInfoWindow> { window };

                string html = FixMap.FixHtml(Context, changes,
                                             "<iframe FRAMEBORDER='0' width='390px' height='240px' src='Loading.aspx?PrincipalTab.aspx?idActive=" +
                                             posDto.IdActive + "&posDate=" + posDto.Date + "&source=lastPos&startDate=" +
                                             posDto.Date.ToShortDateString() + " 00:00" + "&endDate=" + posDto.Date +
                                             "'></iframe>");

                GMarker marker1 = MapUtils.DrawIconMarker(posDto, html, GMap1);
                MapUtils.DrawStatusMarker(posDto, html, GMap1, false);
                //var sb = new StringBuilder();
                //sb.AppendFormat("GEvent.addListener({0}, \"infowindowopen\", function()", marker1.ID);
                //sb.Append("{");
                //sb.AppendFormat("PanMap({0},{1},{2},{3});", GMap1.GMap_Id, 10, 100, marker1.ID);
                //sb.Append("});");
                //GMap1.addCustomInsideJavascript(sb.ToString());
                
                if (status.Equals("cellid"))
                {
                    MapUtils.DrawCellIdPolygon(posDto, marker1, GMap1);
                }
                DrawZones(ref puntos);
            }
            
            if (puntos.Count != 0)
            {
                //Center the map
                MapUtils.SetMapCenterAndZoom(puntos, GMap1);
            }
            else
            {
                var latlon = new GLatLng(Double.Parse(ConfigurationManager.AppSettings["mapCenterLat"].Replace(".", ","), new CultureInfo("es-ES")), Double.Parse(ConfigurationManager.AppSettings["mapCenterLng"].Replace(".", ","), new CultureInfo("es-ES")));
                var zoom = 6;
                try
                {
                    zoom = Convert.ToInt32(ConfigurationManager.AppSettings["mapCenterZoom"]);
                }
                catch(Exception){}
                GMap1.setCenter(latlon, zoom);
            }
        }

        /// <summary>
        /// Apply user selections to map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ApplyDevices(object sender, EventArgs e)
        {
            changes = true;
            GMap1.resetInfoWindows();
            GMap1.resetInfoWindowTabs();
            GMap1.resetMarkerManager();
            GMap1.resetMarkers();
            GMap1.resetPolygon();
            GMap1.resetPolylines();
            GMap1.resetCustomInsideJS();
            LoadMapScripts();
            LoadData(false);
        }

        /// <summary>
        /// Handles the Tick event of timerFresh timer
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance 
        /// containing the event data.</param>
        protected void TimerRefreshMap_Tick(object sender, EventArgs e)
        {
            changes = true;
            GMap1.resetInfoWindows();
            GMap1.resetInfoWindowTabs();
            GMap1.resetMarkerManager();
            GMap1.resetMarkers();
            GMap1.resetPolygon();
            GMap1.resetPolylines();
            GMap1.resetCustomInsideJS();
            LoadMapScripts();
            if (String.IsNullOrEmpty(Request.QueryString["idActive"]) || changes)
                LoadData(false);
            else
                ShowIdActive(Int32.Parse(Request.QueryString["idActive"]));
        }

        /// <summary>
        /// Handles the CheckedChanged event of chkAllowRefresh checkbox
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance 
        /// containing the event data.</param>
        protected void chkAllowRefresh_CheckedChanged(object sender, EventArgs e)
        {
            TimerRefreshMap.Enabled = chkAllowRefresh.Checked;
        }

        /// <summary>
        /// Checks all assisted checkBoxes
        /// </summary>
        private void CheckAll()
        {
            for (int i = 0; i < activesCheckBoxList.Items.Count; i++)
            {
                activesCheckBoxList.Items[i].Selected = true;
            }
        }

        /// <summary>
        /// Loads zones
        /// </summary>
        /// <param name="zones">zones list</param>
        private void BindZones(ArrayList zones)
        {
            if (zones.Count != 0)
            {
                zonesCheckBoxList.DataTextField = "name";
                zonesCheckBoxList.DataValueField = "idZone";
                zonesCheckBoxList.DataSource = zones;
                zonesCheckBoxList.DataBind();
            }
        }

        /// <summary>
        /// Draw selected zones over the map
        /// </summary>
        protected void DrawZones(ref List<GLatLng> puntos)
        {
            if (zonesCheckBoxList.SelectedIndex > -1)
            {
                for (int i = 0; i < zonesCheckBoxList.Items.Count; i++)
                {
                    if (zonesCheckBoxList.Items[i].Selected)
                    {
                        try
                        {
                            ZoneDto zone = UserFacade.GetInstance().FindZoneById(Int32.Parse(zonesCheckBoxList.Items[i].Value));
                            List<GLatLng> zonePoints = new List<GLatLng>();
                            char[] delim = {'#'};
                            char[] delim2 = {';'};
                            String[] coordinates = zone.Position.Split(delim);
                            if (coordinates.Length >= 2)
                            {
                                for (int j = 0; j < coordinates.Length - 1; j++)
                                {
                                    String[] latLng = coordinates[j].Split(delim2);
                                    var glatLng = new GLatLng(Double.Parse(latLng[0].Replace('.', ',')), Double.Parse(latLng[1].Replace('.', ',')));
                                    zonePoints.Add(glatLng);
                                    puntos.Add(glatLng);
                                }
                                GPolygon polygon = new GPolygon(zonePoints, "557799", 3, 0.2, "237464", 0.2);
                                polygon.close();
                                GMap1.addPolygon(polygon);
                            }
                        }
                        catch (InstanceNotFoundException)
                        {}
                    }
                }
            }
        }
    }
}
