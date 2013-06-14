using System;
using System.Text;
using System.Web.UI;
using Subgurim.Controles;
using Nextgal.ECare.Controller;
using System.Collections.Generic;
using Nextgal.ECare.Model.User.Dto;
using Nextgal.ECare.Common.Exceptions;
using Nextgal.ECare.Model.User.Facade;
using Nextgal.ECare.WebTeleasistencia.Util;
using Subgurim.Maps.V3;

namespace Nextgal.ECare.WebTeleasistencia
{
    public partial class UpdateZone : Page
    {
        protected static string decrypturl;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Page.Title = Resources.Titles.updateZoneTitle;
                if (!IsPostBack)
                {
                    FillHiddenField();
                    var sb = new StringBuilder();

                    sb.Append("var active;");
                    sb.Append("var zoneMarkers=[];");
                    sb.Append("var zonePolygons=[];");
                    sb.Append("function clearOverlays() {");
                    sb.Append("for (var x = 0; x < zoneMarkers.length; x++ ) { zoneMarkers[x].setMap(null); } zoneMarkers = [];");
                    sb.Append("for (var j = 0; j < zonePolygons.length; j++ ) { zonePolygons[j].setMap(null); } zonePolygons = [];");
                    sb.Append("}");
                    sb.Append("function subgurim_Remove()");
                    sb.Append("{");
                    sb.AppendFormat("clearOverlays();");
                    sb.Append("active = false;");
                    sb.Append("document.getElementById('ctl00_MainContent_hfPoints').value = '';");
                    sb.Append("}");

                    sb.Append("function searchClick()");
                    sb.Append("{var geocode = new google.maps.Geocoder();");
                    sb.Append("var address = document.getElementById('ctl00_MainContent_txtAddress').value;");
                    sb.Append("geocode.geocode({ 'address': address}, function(results, status){");
                    sb.Append("if (status != google.maps.GeocoderStatus.OK)");
                    sb.Append("{alert('" + Resources.Resources.G_GEO_UNKNOWN_ADDRESS + "');");
                    sb.Append("}else{");
                    sb.Append("var markerOptions = {draggable:false, clickable:false};");
                    sb.Append("var marker = new google.maps.Marker(markerOptions);");
                    sb.Append("marker.setPosition(results[0].geometry.location);");
                    sb.Append("marker.setMap(" + GMap1.GMap_Id + ");");
                    sb.Append(GMap1.GMap_Id + ".setCenter(results[0].geometry.location);");
                    sb.Append("}})}");

                    GMap1.Add(sb.ToString());

                    GMapUIOptions options = new GMapUIOptions();
                    options.controls_maptypecontrol = false;
                    options.controls_menumaptypecontrol = true;
                    options.maptypes_physical = true;

                    GMap1.addGMapUI(new GMapUI(options));

                    LabelPageTitle.Text = Resources.Titles.updateZoneTitle;
                    decrypturl = Request.Url.AbsoluteUri;

                    if (!String.IsNullOrEmpty(Request.QueryString["idZone"]) || ViewState["idZone"] != null)
                    {

                        if (!String.IsNullOrEmpty(Request.QueryString["idZone"]))
                        {
                            ViewState["idZone"] = Request.QueryString["idZone"];
                            ZoneDto zoneDto = UserFacade.GetInstance().FindZoneById(Int32.Parse(Request.QueryString["idZone"]));

                            txtName.Text = zoneDto.Name;
                            hfPoints.Value = zoneDto.Position;
                            List<GLatLng> puntos = new List<GLatLng>();
                            char[] delim = {'#'};
                            char[] delim2 = {';'};
                            String[] coordinates = zoneDto.Position.Split(delim);
                            for (int i = 0; i < coordinates.Length - 1; i++)
                            {
                                GMarkerOptions mOpts = new GMarkerOptions();
                                GIcon icon = new GIcon("../images/puntoRojo.png");
                                icon.iconAnchor = new GPoint(8, 8);
                                icon.shadowSize = new GSize(0, 0);
                                icon.iconSize = new GSize(16, 16);
                                mOpts.icon = icon;
                                String[] latLng = coordinates[i].Split(delim2);
                                GLatLng latLng2 = new GLatLng(Double.Parse(latLng[0].Replace('.', ',')),
                                                              Double.Parse(latLng[1].Replace('.', ',')));
                                GMarker marker = new GMarker(latLng2, mOpts);
                                GMap1.addGMarker(marker);
                                puntos.Add(latLng2);
                            }

                            GPolygon polygon = new GPolygon(puntos, "557799", 3, 0.2, "237464", 0.2);
                            polygon.close();
                            GMap1.Add(polygon);
                            MapUtils.SetMapCenterAndZoom(puntos, GMap1);
                        }
                    }
                }
            }
            catch (NotAllowedException ex)
            {
                Response.Redirect("~/InternalError.aspx?notavailabledata=true");
            }
        }

        /// <summary>
        /// Fills into hidden field coordenates of polygon points
        /// </summary>
        protected void FillHiddenField()
        {
            var sb = new StringBuilder();
            sb.Append("function(event) {");
            sb.Append("clearOverlays();");
            sb.Append("document.getElementById('ctl00_MainContent_hfPoints').value += event.latLng.lat() + ';' + event.latLng.lng()+ '#';");
            sb.Append("var puntos=new Array();");
            sb.Append("var coordinates = document.getElementById('ctl00_MainContent_hfPoints').value.split('#');");
            sb.Append("var j=0;");
            sb.Append("for(i=0;i<coordinates.length-1;i++){");
            sb.Append("var latLng = coordinates[i].split(';');");
            sb.Append("var point = new google.maps.LatLng(latLng[0], latLng[1]);");
            sb.Append("puntos[j] = point;");
            sb.Append("var icon = {url: \"../images/puntoRojo.png\" , anchor: new google.maps.Point(8,8), size:  new google.maps.Size(16,16) };");
            sb.Append("var markerOptions = {draggable:false, clickable:false, icon:icon};");
            sb.Append("var marker = new google.maps.Marker(markerOptions);");
            sb.Append("marker.setPosition(point);");
            sb.Append("marker.setMap(" + GMap1.GMap_Id + ");");
            sb.Append("zoneMarkers.push(marker);");
            sb.Append("j++;}");
            sb.Append("var polygon = new google.maps.Polygon({paths:puntos, strokeColor:'#557799', strokeWeight:3, strokeOpacity:0.2, fillColor:'#237464', fillOpacity:0.2, clickable:false});");
            sb.Append("polygon.setMap(" + GMap1.GMap_Id + ");");
            sb.Append("zonePolygons.push(polygon);");
            sb.Append("}");

            GListener listener2 = new GListener(GMap1.GMap_Id, GListener.Event.click, sb.ToString());
            GMap1.addListener(listener2);
        }

        /// <summary>
        /// Handles the Click event of the btnBack button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance 
        /// containing the event data.</param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Zones.aspx");
        }

        /// <summary>
        /// Handles the Click event of the btnUpdate button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance 
        /// containing the event data.</param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            lblSaveData.Visible = false;
            ZoneDto zoneDto;
            try
            {
                char[] delim = { '#' };
                String[] points = hfPoints.Value.Split(delim);
                if (points.Length - 1 >= 3)
                {
                    //Creates
                    if (ViewState["idZone"] == null)
                    {
                        zoneDto = new ZoneDto(-1, txtName.Text, hfPoints.Value, SessionManager.GetIdFamily(Context), DateTime.Now);
                        zoneDto = UserFacade.GetInstance().CreateZone(zoneDto);
                    }
                    //Updates
                    else
                    {
                        zoneDto = new ZoneDto(Int32.Parse(ViewState["idZone"].ToString()), txtName.Text, hfPoints.Value, SessionManager.GetIdFamily(Context), DateTime.Now);
                        zoneDto = UserFacade.GetInstance().UpdateZone(zoneDto);
                    }
                    
                    DrawOverlays();
                    lblSaveData.Visible = true;
                    lblSaveData.CssClass = "labelOk";
                    ViewState["idZone"] = zoneDto.IdZone;
                    lblSaveData.Text = Resources.Resources.saveDataOk;
                }
                else
                {
                    lblSaveData.CssClass = "labelError";
                    lblSaveData.Visible = true;
                    lblSaveData.Text = Resources.Resources.threePointsMap;
                }
            }
            catch (Exception ex)
            {
                lblSaveData.CssClass = "labelError";
                lblSaveData.Visible = true;
                lblSaveData.Text = Resources.Resources.errorData;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnAddress button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance 
        /// containing the event data.</param>
        protected void btnAddress_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtAddress.Text))
                {
                    string Key = System.Configuration.ConfigurationManager.AppSettings.Get("googlemaps.subgurim.net");
                    GeoCode geocode = GMap.geoCodeRequest(txtAddress.Text, Key);
                    StringBuilder sb = new StringBuilder();
                    if ((null != geocode) && geocode.valid)
                    {
                        var latLng = new GLatLng(geocode.Placemark.coordinates.lat, geocode.Placemark.coordinates.lng);
                        GMarker marker = new GMarker(latLng);
                        GMap1.addGMarker(marker);
                    }
                    else sb.Append(Resources.Resources.notFindPlace);
                    txtAddress.Text = sb.ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Draws markers and polygon
        /// </summary>
        protected void DrawOverlays()
        {
            if (!String.IsNullOrEmpty(hfPoints.Value))
            {
                GMap1.resetMarkers();
                GMap1.resetPolygon();
                List<GLatLng> puntos = new List<GLatLng>();
                char[] delim = { '#' };
                char[] delim2 = { ';' };
                String[] coordinates = hfPoints.Value.Split(delim);
                for (int i = 0; i < coordinates.Length - 1; i++)
                {
                    GMarkerOptions mOpts = new GMarkerOptions();
                    GIcon icon = new GIcon("../images/puntoRojo.png");
                    icon.iconAnchor = new GPoint(8, 8);
                    icon.shadowSize = new GSize(0, 0);
                    icon.iconSize = new GSize(16, 16);
                    mOpts.icon = icon;
                    String[] latLng = coordinates[i].Split(delim2);
                    var latLng2 = new GLatLng(Double.Parse(latLng[0].Replace('.', ',')), Double.Parse(latLng[1].Replace('.', ',')));
                    GMarker marker = new GMarker(latLng2, mOpts);
                    GMap1.addGMarker(marker);
                    puntos.Add(latLng2);
                }

                GPolygon polygon = new GPolygon(puntos, "557799", 3, 0.2, "237464", 0.2);
                polygon.close();
                GMap1.Add(polygon);
            }
        }
    }
}