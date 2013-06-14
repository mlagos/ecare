using System;
using System.Web;
using System.Text;
using System.Web.UI;
using Subgurim.Controles;
using System.Collections;
using System.Configuration;
using System.Globalization;
using Nextgal.ECare.Controller;
using Nextgal.ECare.Common.Util;
using System.Collections.Generic;
using Nextgal.ECare.Model.User.Dto;
using Nextgal.ECare.Common.Exceptions;
using Nextgal.ECare.Model.User.Facade;
using Nextgal.ECare.Model.Position.Dto;
using Nextgal.ECare.Model.Assisted.Dto;
using Nextgal.ECare.Model.Assisted.Facade;
using Nextgal.ECare.Model.Position.Facade;
using Nextgal.ECare.WebTeleasistencia.Util;
using Subgurim.Maps.V3;

namespace Nextgal.ECare.WebTeleasistencia
{
    public partial class MapTraceRoute : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Page.Title = Resources.Titles.mapTraceRutaTitle;
                if (!IsPostBack)
                {
                    ArrayList zones = UserFacade.GetInstance().GetAllZoneByFamily(SessionManager.GetIdFamily(Context));
                    BindZones(zones);
                    ScriptManager.RegisterStartupScript(Page, typeof(string), "resize", "resizeMap('" + GMap1.GMap_Id + "', 'no');", true);
                    LoadDropDownLists();
                    txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    
                    endHour.SelectedValue = DateTime.Now.Hour.ToString().PadLeft(2, '0');
                    endMinutes.SelectedValue = DateTime.Now.Minute.ToString().PadLeft(2, '0');
                    
                    AssistedDto assistedDto = null;
                    ArrayList assisteds = AssistedFacade.GetInstance().GetAllAssistedByFamily(SessionManager.GetIdFamily(Context));
                    AssistedDataBind(assisteds);

                    //Inicializamos componentes del mapa siempre que !IsPostback
                    InitializeMap();
                    if ((!String.IsNullOrEmpty(Request.QueryString["idActive"])) && (!String.IsNullOrEmpty(Request.QueryString["startDate"])))
                    {
                        int idActive = Int32.Parse(Request.QueryString["idActive"]);
                        assistedDto = AssistedFacade.GetInstance().FindAssistedByIdActive(idActive);
                    }
                    if (!String.IsNullOrEmpty(Request.QueryString["startHour"]))
                    {
                        string s = Request.QueryString["startHour"];
                        startHour.SelectedValue = s.Substring(0,2);
                        startMinutes.SelectedValue = s.Substring(3, 2);
                    }
                    if (!String.IsNullOrEmpty(Request.QueryString["endHour"]))
                    {
                        string s = Request.QueryString["endHour"];
                        DateTime _endHour = Utils.ConvertToLclTime(DateTime.Parse(txtEndDate.Text + " " + s));
                        s = _endHour.ToShortTimeString();
                        if (_endHour.Hour < 10)
                            s = "0" + s;
                        endHour.SelectedValue = s.Substring(0, 2);
                        endMinutes.SelectedValue = s.Substring(3, 2);
                    }
                    else
                    {
                        endHour.SelectedValue = DateTime.Now.ToLongTimeString().Substring(0,2);
                        endMinutes.SelectedValue = DateTime.Now.ToLongTimeString().Substring(3,2);
                    }
                    if (assistedDto != null)
                    {
                        assistedsCheckBoxList.SelectedValue = Request.QueryString["idActive"];
                        txtStartDate.Text = DateTime.Parse(Request.QueryString["startDate"]).ToString("dd/MM/yyyy");
                        if (!String.IsNullOrEmpty(Request.QueryString["endDate"]))
                            txtEndDate.Text = DateTime.Parse(Request.QueryString["endDate"]).ToString("dd/MM/yyyy");
                        else
                            txtEndDate.Text = DateTime.Parse(Request.QueryString["startDate"]).ToString("dd/MM/yyyy");
                        var activesArray = new ArrayList { assistedDto };
                        CreateTrace(activesArray);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Load map controls 
        /// </summary>
        protected void InitializeMap()
        {
            GMap1.addMapType(GMapType.GTypes.Physical);
            GMap1.addControl(new GControl(GControl.preBuilt.ScaleControl));
            GMap1.addControl(new GControl(GControl.preBuilt.LargeMapControl3D));
            GMap1.addControl(new GControl(GControl.preBuilt.MenuMapTypeControl, new GControlPosition(GControlPosition.position.Top_Right)));
            GMap1.addControl(new GControl(GControl.preBuilt.NavLabelControl, new GControlPosition(GControlPosition.position.Bottom_Right)));
            
            var latlon = new GLatLng(Double.Parse(ConfigurationManager.AppSettings["mapCenterLat"].Replace(".", ","), new CultureInfo("es-ES")), Double.Parse(ConfigurationManager.AppSettings["mapCenterLng"].Replace(".", ","), new CultureInfo("es-ES")));
            var zoom = 6;
            try
            {
                zoom = Convert.ToInt32(ConfigurationManager.AppSettings["mapCenterZoom"]);
            }
            catch (Exception){}
            GMap1.setCenter(latlon, zoom);
            LoadMapScripts();
        }

        /// <summary>
        /// Load Pan Map scripts
        /// </summary>
        protected void LoadMapScripts()
        {
            //var sb = new StringBuilder();
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
        /// Binds assisteds into dropdownlist of actives
        /// </summary>
        /// <param name="assisteds">Assisteds list</param>
        protected void AssistedDataBind(ArrayList assisteds)
        {
            assisteds.Sort();
            assistedsCheckBoxList.DataTextField = "name";
            assistedsCheckBoxList.DataValueField = "idActive";
            assistedsCheckBoxList.DataSource = assisteds;
            assistedsCheckBoxList.DataBind();
        }

        /// <summary>
        /// Obtains parked positions 
        /// </summary>
        /// <param name="points">points of one day routes</param>
        /// <returns>parked positions</returns>
        protected static ArrayList GetParkedPositions(ArrayList points)
        {
            ArrayList parkedPos = new ArrayList();
            foreach (MonitoringRoutesRowDto pos in points)
            {
                if(pos.Status.Equals("parked"))
                {
                    parkedPos.Add(pos);
                }
            }
            return parkedPos;
        }

        /// <summary>
        /// Last grouping after applying restrictions
        /// </summary>
        /// <param name="data">report data</param>
        /// <param name="idActive">active´s identifier</param>
        /// <returns>data to bind</returns>
        protected static ArrayList LastGrouping(ArrayList data, int idActive)
        {
            try
            {
                for (int i = 1; i < data.Count; i++)
                {
                    MonitoringRoutesRowDto startRow = (MonitoringRoutesRowDto)data[0];
                    MonitoringRoutesRowDto prevRow = (MonitoringRoutesRowDto)data[i - 1];
                    string prevStatus = prevRow.Status;
                    MonitoringRoutesRowDto row = (MonitoringRoutesRowDto)data[i];
                    string status = row.Status;
                    PositionDto pos1 = PositionFacade.GetInstance().FindPositionByDate(prevRow.Date, idActive);
                    PositionDto pos2 = PositionFacade.GetInstance().FindPositionByDate(row.Date, idActive);
                    GLatLng prevPoint = new GLatLng(pos1.Latitude, pos1.Longitude);
                    GLatLng point = new GLatLng(pos2.Latitude, pos2.Longitude);
                    PositionDto startPos = PositionFacade.GetInstance().FindPositionByDate(startRow.Date, startRow.IdActive);
                    double mileage = MapUtils.CalculateDistanceInMeters(idActive, startPos, pos1, pos2);
                    if (status.Equals("moving") && mileage == 0.0)
                    {
                        GLatLng ini = new GLatLng(pos1.Latitude, pos1.Longitude);
                        GLatLng fin = new GLatLng(pos2.Latitude, pos2.Longitude);
                        mileage = ini.distanceFrom(fin);
                    }
                    prevRow.Mileage = mileage.ToString();

                    if (prevStatus.Equals(status) && prevStatus.Equals("parked"))
                    {
                        if (!(prevPoint.distanceFrom(point) > 50))
                        {
                            prevRow.EndTime = row.EndTime;
                            DateTime lclEndTime = Utils.ConvertToLclTime(DateTime.Parse(prevRow.Date.ToShortDateString() + " " + prevRow.EndTime));
                            DateTime lclStartTime = Utils.ConvertToLclTime(DateTime.Parse(prevRow.Date.ToShortDateString() + " " + prevRow.StartTime));
                            string time = DateTime.Parse(lclEndTime.ToLongTimeString()).Subtract(DateTime.Parse(lclStartTime.ToLongTimeString())).ToString();
                            if (time.Substring(0, 1).Equals("-"))
                            {
                                prevRow.DiffTime = time.Substring(1, time.Length - 1);
                            }
                            else
                            {
                                prevRow.DiffTime = time;
                            }
                            data.RemoveAt(i);
                            i--;
                        }
                    }
                }
                for (int j = 0; j < data.Count - 1; j++)
                {
                    MonitoringRoutesRowDto row = (MonitoringRoutesRowDto)data[j];
                    string status = row.Status;
                    MonitoringRoutesRowDto nextRow = (MonitoringRoutesRowDto)data[j + 1];
                    if (status.Equals("stopped") || status.Equals("parked"))
                    {
                        double mil = Double.Parse(row.Mileage);
                        if (!String.IsNullOrEmpty(nextRow.Mileage))
                        {
                            double nextMil = Double.Parse(nextRow.Mileage);
                            mil = mil + nextMil;
                        }
                        row.Mileage = "";
                        nextRow.Mileage = mil.ToString();
                    }

                }
            }
            catch (Exception e)
            {}
            return data;
        }

        /// <summary>
        /// Show intermediate positions
        /// </summary>
        protected void ShowPositionsOverZoom(ArrayList validPos, int idActive, ref MarkerManager mk)
        {
            GInfoWindow window;
            MonitoringRoutesRowDto firstPos = (MonitoringRoutesRowDto)validPos[0];
            MonitoringRoutesRowDto lastPos = (MonitoringRoutesRowDto)validPos[validPos.Count - 1];
            if (validPos.Count > 0)
            {
                int i = 0;
                foreach (MonitoringRoutesRowDto pos in validPos)
                {
                    if (!String.IsNullOrEmpty(pos.EndTime))
                    {
                        if (!firstPos.Date.Equals(pos.Date) && !lastPos.Date.Equals(pos.Date))
                        {
                            MonitoringRoutesRowDto next = (MonitoringRoutesRowDto) validPos[i+1];

                            if (!pos.Latitude.Equals(next.Latitude) || !pos.Longitude.Equals(next.Longitude))
                            {
                                GMarker marker = DrawStatusMarker(pos, true, firstPos, lastPos, idActive);
                                //var sb = new StringBuilder();
                                //sb.AppendFormat("GEvent.addListener({0}, \"infowindowopen\",function()", marker.ID);
                                //sb.Append("{");
                                //sb.AppendFormat("PanMap({0},{1},{2},{3});", GMap1.GMap_Id, 10, 50, marker.ID);
                                //sb.Append("});");
                                //GMap1.addCustomInsideJavascript(sb.ToString());
                                if (pos.Status.Equals("cellid"))
                                {
                                    MapUtils.DrawCellIdPolygon(pos, marker, GMap1);
                                }
                                DateTime startDate = Utils.ConvertToGMTTime(DateTime.Parse(txtStartDate.Text + " " + startHour.SelectedValue + ":" + startMinutes.SelectedValue));
                                DateTime endDate = Utils.ConvertToGMTTime(DateTime.Parse(txtEndDate.Text + " " + endHour.SelectedValue + ":" + endMinutes.SelectedValue));
                                window = new GInfoWindow(marker, FixMap.FixHtml(Context, Page.IsPostBack, "<iframe FRAMEBORDER='0' width='475px' height='250px' src='Loading.aspx?PrincipalTab.aspx?idActive=" + pos.IdActive + "&posDate=" + pos.Date + "&source=routes&startDate=" + startDate + "&endDate=" + endDate + "'></iframe>"), GListener.Event.mouseover);
                                GInfoWindowOptions options = new GInfoWindowOptions();
                                options.maxWidth = 300;
                                window.options = options;
                                mk.Add(window, 14, 17);
                            }
                        }
                    }
                    i++;
                }
            }
        }

        /// <summary>
        /// Finds and draws first and last positions of a route
        /// </summary>
        protected void SetFirstAndLastPositions(ArrayList validPos, AssistedDto assistedDto)
        {
            try
            {
                MonitoringRoutesRowDto firstPos = (MonitoringRoutesRowDto) validPos[0];
                MonitoringRoutesRowDto lastPos = (MonitoringRoutesRowDto) validPos[validPos.Count - 1];
                PositionDto first = PositionFacade.GetInstance().FindPositionByDate(firstPos.Date, firstPos.IdActive);
                first.Latitude = firstPos.Latitude;
                first.Longitude = firstPos.Longitude;
                DateTime lastDate = Utils.ConvertToGMTTime(DateTime.Parse(lastPos.EndTime));
                PositionDto last = PositionFacade.GetInstance().FindPositionByDate(lastDate, lastPos.IdActive);
                last.Latitude = lastPos.Latitude;
                last.Longitude = lastPos.Longitude;

                //List<GInfoWindowTab> firstPosTabs = new List<GInfoWindowTab>();
                //List<GInfoWindowTab> lastPosTabs = new List<GInfoWindowTab>();
                DateTime startDate = Utils.ConvertToGMTTime(DateTime.Parse(txtStartDate.Text + " " + startHour.SelectedValue + ":" + startMinutes.SelectedValue));
                DateTime endDate = Utils.ConvertToGMTTime(DateTime.Parse(txtEndDate.Text + " " + endHour.SelectedValue + ":" + endMinutes.SelectedValue));
                //GInfoWindowTab firstPosTab = new GInfoWindowTab("", );
                //GInfoWindowTab lastPosTab = new GInfoWindowTab("", );

                string firstHtml =
                    String.Format(
                        "<iframe FRAMEBORDER='0' width='475px' height='250px' src='Loading.aspx?PrincipalTab.aspx?idActive=" +
                        firstPos.IdActive + "&posDate=" + firstPos.Date + "&source=routes&startDate=" + startDate +
                        "&endDate=" + endDate + "'></iframe>");

                string secondHtml =
                    String.Format(
                        "<iframe FRAMEBORDER='0' width='475px' height='250px' src='Loading.aspx?PrincipalTab.aspx?idActive=" +
                        lastPos.IdActive + "&posDate=" + lastPos.Date + "&source=routes&startDate=" + startDate +
                        "&endDate=" + endDate + "'></iframe>");
                
                //firstPosTabs.Add(firstPosTab);
                //lastPosTabs.Add(lastPosTab);
                MapUtils.DrawStatusMarker(first, firstHtml, GMap1, true);
                MapUtils.DrawStatusMarker(last, secondHtml, GMap1, false);
                GMarker marker2 = MapUtils.DrawIconMarker(last, secondHtml, GMap1);
                GMarker marker1 = MapUtils.DrawHouseMarker(first, firstHtml, GMap1);
                
                if (firstPos.Status.Equals("cellid"))
                {
                    MapUtils.DrawCellIdPolygon(firstPos, marker1, GMap1);
                }
                if (lastPos.Status.Equals("cellid"))
                {
                    MapUtils.DrawCellIdPolygon(lastPos, marker2, GMap1);
                }

                GInfoWindowOptions options = new GInfoWindowOptions(0, 400);
                GInfoWindow firstTabs = new GInfoWindow(marker1, firstHtml, false, options, GListener.Event.mouseover);
                GMap1.addInfoWindow(firstTabs);
                GInfoWindow lastTabs = new GInfoWindow(marker2, secondHtml, false, options, GListener.Event.mouseover);
                GMap1.addInfoWindow(lastTabs);

                //var sb = new StringBuilder();
                //sb.AppendFormat("GEvent.addListener({0}, \"infowindowopen\",function()", marker1.ID);
                //sb.Append("{");
                //sb.AppendFormat("PanMap({0},{1},{2},{3});", GMap1.GMap_Id, 10, 50, marker1.ID);
                //sb.Append("});");
                //sb.AppendFormat("GEvent.addListener({0}, \"infowindowopen\",function()", marker2.ID);
                //sb.Append("{");
                //sb.AppendFormat("PanMap({0},{1},{2},{3});", GMap1.GMap_Id, 10, 50, marker2.ID);
                //sb.Append("});");
                //GMap1.addCustomInsideJavascript(sb.ToString());
                
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Draw status marker in the point indicated
        /// </summary>
        /// <param name="point">point to draw marker</param>
        /// <param name="interPos">true if intermediate position</param>
        /// <param name="firstPos">first position</param>
        /// <param name="lastPos">last position</param>
        /// <param name="idActive">active´s identifier</param>
        protected static GMarker DrawStatusMarker(MonitoringRoutesRowDto point, bool interPos, MonitoringRoutesRowDto firstPos, MonitoringRoutesRowDto lastPos, int idActive)
        {
            bool firstCellID = false;
            bool lastCellId = false;

            if(lastPos.Status.Equals("cellid"))
                lastCellId = true;
            if (firstPos.Status.Equals("cellid"))
                firstCellID = true;

            GLatLng firstPoint = new GLatLng(firstPos.Latitude, firstPos.Longitude);
            GLatLng lastPoint = new GLatLng(lastPos.Latitude, lastPos.Longitude);
            GLatLng pointMarker = new GLatLng(point.Latitude, point.Longitude);
            GIcon icon = null;
            if (interPos && PositionFacade.GetInstance().IsCellIdPosition(point.Date, idActive))
            {
                icon = new GIcon("images/icons/32x32/antenna.png");
                icon.iconSize = new GSize(32, 32);
                icon.shadowSize = new GSize(0, 0);
                icon.iconAnchor = new GPoint(16, 28);
                icon.infoWindowAnchor = new GPoint(16, 0);
            }
            if (interPos && !PositionFacade.GetInstance().IsCellIdPosition(point.Date, idActive))
            {
                if (!(firstCellID && firstPoint.Equals(new GLatLng(point.Latitude, point.Longitude)) || lastCellId && lastPoint.Equals(new GLatLng(point.Latitude, point.Longitude))))
                {
                    icon = new GIcon(point.StatusPath);
                    icon.iconSize = new GSize(16, 16);
                    icon.shadowSize = new GSize(0, 0);
                    icon.iconAnchor = new GPoint(8, 8);
                    icon.infoWindowAnchor = new GPoint(8, 0);
                }
            }
            if (!interPos && PositionFacade.GetInstance().IsCellIdPosition(point.Date, idActive))
            {
                icon = new GIcon("images/icons/16x16/antenna.png");
                icon.iconSize = new GSize(16, 16);
                icon.shadowSize = new GSize(0, 0);
                icon.iconAnchor = new GPoint(-4, -1);
                icon.infoWindowAnchor = new GPoint(16, 0);
            }

            if (!interPos && !PositionFacade.GetInstance().IsCellIdPosition(point.Date, idActive))
            {
                icon = new GIcon(point.StatusPath);
                icon.iconSize = new GSize(16, 16);
                icon.shadowSize = new GSize(0, 0);
                icon.iconAnchor = new GPoint(-4, -1);
                icon.infoWindowAnchor = new GPoint(16, 0);
            }
           
            GMarkerOptions mOpts = new GMarkerOptions(icon);
            GMarker marker = new GMarker(pointMarker, mOpts);
            return marker;
        }

        /// <summary>
        /// Loads data into time dropDownLists
        /// </summary>
        protected void LoadDropDownLists()
        {
            ArrayList hours = new ArrayList();
            ArrayList minutes = new ArrayList();
            for (int i = 0; i < 24; i++)
            {
                if (i < 10)
                {
                    hours.Add("0" + i);
                }
                else
                {
                    hours.Add(i);
                }
            }
            for (int j = 0; j < 60; j++)
            {
                if (j < 10)
                {
                    minutes.Add("0" + j);
                }
                else
                {
                    minutes.Add(j);
                }
            }
            startHour.DataSource = hours;
            startHour.DataBind();
            endHour.DataSource = hours;
            endHour.DataBind();
            endHour.SelectedValue = "23";
            startMinutes.DataSource = minutes;
            startMinutes.DataBind();
            endMinutes.DataSource = minutes;
            endMinutes.DataBind();
            endMinutes.SelectedValue = "59";
        }

        /// <summary>
        /// Groupes cellId positions to show only the latest one
        /// </summary>
        /// <param name="cellIdPos">cellId positions</param>
        /// <returns>cellId positions grouped</returns>
        protected static ArrayList GetGroupedCellIdPositions(ArrayList cellIdPos)
        {
            ArrayList grouped = new ArrayList();
            ArrayList points = new ArrayList();
            for (int i = cellIdPos.Count - 1; i >= 0; i--)
            {
                MonitoringRoutesRowDto pos = (MonitoringRoutesRowDto) cellIdPos[i];
                if (PositionFacade.GetInstance().IsCellIdPosition(Utils.ConvertToGMTTime(pos.Date), pos.IdActive))
                {
                    double lat = pos.Latitude;
                    double lon = pos.Longitude;
                    GLatLng point = new GLatLng(lat, lon);
                    if (!points.Contains(point))
                    {
                        points.Add(point);
                        if (grouped.Count == 0)
                        {
                            grouped.Insert(0, pos);
                        }
                        else
                        {
                            grouped.Insert(0, pos);
                        }
                    }
                }
                else
                {
                    grouped.Insert(0,pos);
                }
            }
            return grouped;
        }

        /// <summary>
        /// Applies filters to route positions
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance 
        /// containing the event data.</param>
        protected void ApplyFilters(object sender, EventArgs e)
        {
            hfPositions.Value = "";
            hfAddress.Value = "";
            lblMsg.Visible = false;

            if (assistedsCheckBoxList.SelectedIndex > -1)
            {
                if (!String.IsNullOrEmpty(txtStartDate.Text) && !String.IsNullOrEmpty(txtEndDate.Text))
                {
                    ArrayList actives = new ArrayList();
                    //Selected actives count

                    for (int i = 0; i < assistedsCheckBoxList.Items.Count; i++)
                    {
                        if (assistedsCheckBoxList.Items[i].Selected)
                        {
                            try
                            {
                                actives.Add(AssistedFacade.GetInstance().FindAssistedByIdActive(Int32.Parse(assistedsCheckBoxList.Items[i].Value)));
                            }
                            catch (Exception ex)
                            {}
                        }
                    }
                    CreateTrace(actives);
                }
                else
                {
                    lblMsg.CssClass = "labelError";
                    lblMsg.Visible = true;
                    lblMsg.Text = Resources.Resources.mustSelectDate;
                }
            }
            else
            {
                lblMsg.CssClass = "labelError";
                lblMsg.Visible = true;
                lblMsg.Text = Resources.Resources.mustSelectActive;
            }
        }

        /// <summary>
        /// Creates assisteds routes
        /// </summary>
        /// <param name="assisteds"></param>
        private void CreateTrace(ArrayList assisteds)
        {
            GMap1.resetInfoWindows();
            GMap1.resetInfoWindowTabs();
            GMap1.resetMarkerManager();
            GMap1.resetMarkers();
            GMap1.resetPolylines();
            GMap1.resetCustomInsideJS();
            LoadMapScripts();
            List<GLatLng> globalMapPoints = new List<GLatLng>();
            MarkerManager mk = new MarkerManager();
            try
            {
                foreach (AssistedDto assistedDto in assisteds)
                {
                    //Vamos añadiendo todos los puntos al array para luego centrar mapa
                    globalMapPoints.AddRange(DrawActiveRoute(assistedDto, ref mk));
                }
                if(globalMapPoints.Count > 0)
                {
                    //Añadimos markers (Solo puede haber un markermanager por mapa -> pasamos por ref)
                    GMap1.markerManager = mk;
                    //Set map center and the best zoom to see the route
                    MapUtils.SetMapCenterAndZoom(globalMapPoints, GMap1);
                }
                else
                {
                    var latlon = new GLatLng(Double.Parse(ConfigurationManager.AppSettings["mapCenterLat"].Replace(".", ","), new CultureInfo("es-ES")), Double.Parse(ConfigurationManager.AppSettings["mapCenterLng"].Replace(".", ","), new CultureInfo("es-ES")));
                    var zoom = 6;
                    try
                    {
                        zoom = Convert.ToInt32(ConfigurationManager.AppSettings["mapCenterZoom"]);
                    }
                    catch (Exception) { }
                    GMap1.setCenter(latlon, zoom);
                }
                DrawZones(ref globalMapPoints);
            }
            catch(Exception ex)
            {
            }
        }

        /// <summary>
        /// Draws in the map assisted selected route
        /// </summary>
        /// <param name="assistedDto">Assisted</param>
        /// <param name="manager">Marker manager</param>
        /// <returns>List of route google.maps.LatLng points </returns>
        private List<GLatLng> DrawActiveRoute(AssistedDto assistedDto, ref MarkerManager manager)
        {
            List<GLatLng> points = new List<GLatLng>();
            ArrayList validPos = new ArrayList();
            ArrayList data;
            
            try
            {
                CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(HttpContext.Current.Request.UserLanguages[0]);
                DateTime startDate = DateTime.Parse(txtStartDate.Text + " " + startHour.SelectedValue + ":" + startMinutes.SelectedValue, cultureInfo);
                DateTime endDate = DateTime.Parse(txtEndDate.Text + " " + endHour.SelectedValue + ":" + endMinutes.SelectedValue, cultureInfo);

                data = PositionFacade.GetInstance().GetMonitoringRoutesReportData(assistedDto.IdActive, Utils.ConvertToGMTTime(startDate), Utils.ConvertToGMTTime(endDate), true);
                           
                if(data.Count > 0)
                {
                    data = MapUtils.GroupPositions(Context, data, assistedDto.IdActive);
                    data = MapUtils.ApplyRestrictions(data);
                    data = LastGrouping(data, assistedDto.IdActive);

                    for (int i = 0; i < data.Count; i++)
                    {
                        MonitoringRoutesRowDto dto = (MonitoringRoutesRowDto)data[i];
                        dto.StartTime = Utils.ConvertToLclTime(DateTime.Parse(dto.StartTime)).ToString();
                        dto.EndTime = Utils.ConvertToLclTime(DateTime.Parse(dto.EndTime)).ToString();
                        dto.DiffTime = DateTime.Parse(dto.EndTime).Subtract(DateTime.Parse(dto.StartTime)).ToString();
                        GLatLng point = new GLatLng(dto.Latitude, dto.Longitude);
                        points.Add(point);
                        validPos.Add(dto);
                    }
                    
                    //Pintamos las lineas
                    DrawPolilynes(validPos);
                    
                    //Show information about intermediate points since a minimal zoom
                    ShowPositionsOverZoom(validPos, assistedDto.IdActive, ref manager);
                    
                    //Sets and draws first and last positions in the map
                    SetFirstAndLastPositions(validPos, assistedDto);
                }
            }
            catch(Exception ex)
            {
            }
            
            return points;
        }

        /// <summary>
        /// Draws a different polyline for each route
        /// </summary>
        /// <param name="validPos">Routes positions</param>
        private void DrawPolilynes(ArrayList validPos)
        {
            ArrayList temp = new ArrayList();
            List<GLatLng> tempPoints = new List<GLatLng>();
            new ArrayList();
            int j = 1;
            foreach (MonitoringRoutesRowDto pos in validPos)
            {
                if (!pos.Status.Equals("cellid"))
                    temp.Add(pos);
                if (pos.Status.Equals("parked") || j == validPos.Count)
                {
                    GPolyline linea;
                    foreach (MonitoringRoutesRowDto pos2 in temp)
                    {
                        tempPoints.Add(new GLatLng(pos2.Latitude, pos2.Longitude));
                    }
                    if (tempPoints.Count > 1)
                    {
                        linea = new GPolyline(tempPoints, "#" + MapUtils.GenerarColores(j), 3);
                        GMap1.addPolyline(linea);
                    }
                    temp = new ArrayList();
                    tempPoints = new List<GLatLng>();
                    temp.Add(pos);
                    tempPoints.Add(new GLatLng(pos.Latitude, pos.Longitude));
                }
                j++;
            }
        }

        /// <summary>
        /// Handles the Click event of minButton1 button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance 
        /// containing the event data.</param>
        protected void minButton1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime endDate = DateTime.Parse(txtEndDate.Text + " " + endHour.SelectedValue + ":" + endMinutes.SelectedValue);
                txtEndDate.Text = endDate.ToString("dd/MM/yyyy");
                endHour.SelectedValue = endDate.Hour.ToString().PadLeft(2, '0');
                endMinutes.SelectedValue = endDate.Minute.ToString().PadLeft(2, '0');
                DateTime startDate = endDate.Subtract(new TimeSpan(0, 0, 15, 0));
                txtStartDate.Text = startDate.ToString("dd/MM/yyyy");
                startHour.SelectedValue = startDate.Hour.ToString().PadLeft(2, '0');
                startMinutes.SelectedValue = startDate.Minute.ToString().PadLeft(2, '0');
            }
            catch(Exception ex)
            {}
        }

        /// <summary>
        /// Handles the Click event of minButton2 button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance 
        /// containing the event data.</param>
        protected void minButton2_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime endDate = DateTime.Parse(txtEndDate.Text + " " + endHour.SelectedValue + ":" + endMinutes.SelectedValue);
                txtEndDate.Text = endDate.ToString("dd/MM/yyyy");
                endHour.SelectedValue = endDate.Hour.ToString().PadLeft(2, '0');
                endMinutes.SelectedValue = endDate.Minute.ToString().PadLeft(2, '0');
                DateTime startDate = endDate.Subtract(new TimeSpan(0, 0, 30, 0));
                txtStartDate.Text = startDate.ToString("dd/MM/yyyy");
                startHour.SelectedValue = startDate.Hour.ToString().PadLeft(2, '0');
                startMinutes.SelectedValue = startDate.Minute.ToString().PadLeft(2, '0');
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// Handles the Click event of minButton3 button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance 
        /// containing the event data.</param>
        protected void minButton3_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime endDate = DateTime.Parse(txtEndDate.Text + " " + endHour.SelectedValue + ":" + endMinutes.SelectedValue);
                txtEndDate.Text = endDate.ToString("dd/MM/yyyy");
                endHour.SelectedValue = endDate.Hour.ToString().PadLeft(2, '0');
                endMinutes.SelectedValue = endDate.Minute.ToString().PadLeft(2, '0');
                DateTime startDate = endDate.Subtract(new TimeSpan(0, 0, 60, 0));
                txtStartDate.Text = startDate.ToString("dd/MM/yyyy");
                startHour.SelectedValue = startDate.Hour.ToString().PadLeft(2, '0');
                startMinutes.SelectedValue = startDate.Minute.ToString().PadLeft(2, '0');
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// Handles the Click event of minButton4 button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance 
        /// containing the event data.</param>
        protected void minButton4_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime endDate = DateTime.Parse(txtEndDate.Text + " " + endHour.SelectedValue + ":" + endMinutes.SelectedValue);
                txtEndDate.Text = endDate.ToString("dd/MM/yyyy");
                endHour.SelectedValue = endDate.Hour.ToString().PadLeft(2, '0');
                endMinutes.SelectedValue = endDate.Minute.ToString().PadLeft(2, '0');
                DateTime startDate = endDate.Subtract(new TimeSpan(0, 0, 90, 0));
                txtStartDate.Text = startDate.ToString("dd/MM/yyyy");
                startHour.SelectedValue = startDate.Hour.ToString().PadLeft(2, '0');
                startMinutes.SelectedValue = startDate.Minute.ToString().PadLeft(2, '0');
            }
            catch (Exception ex) { }
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
                            char[] delim = { '#' };
                            char[] delim2 = { ';' };
                            String[] coordinates = zone.Position.Split(delim);
                            if (coordinates.Length >= 2)
                            {
                                for (int j = 0; j < coordinates.Length - 1; j++)
                                {
                                    String[] latLng = coordinates[j].Split(delim2);
                                    var latLng2 = new GLatLng(Double.Parse(latLng[0].Replace('.', ',')), Double.Parse(latLng[1].Replace('.', ',')));
                                    zonePoints.Add(latLng2);
                                    puntos.Add(latLng2);
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