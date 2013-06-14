using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Collections;
using Nextgal.ECare.Common.Exceptions;
using Nextgal.ECare.Common.Util;
using Nextgal.ECare.Model.Assisted.Dto;
using Nextgal.ECare.Model.Assisted.Facade;
using Nextgal.ECare.Model.Position.Dto;
using Subgurim.Controles;
using System.Configuration;
using System.Globalization;
using System.Collections.Generic;
using System.Collections.Specialized;
using Nextgal.ECare.Model.Position.Facade;
using Subgurim.Maps.V3;

namespace Nextgal.ECare.WebTeleasistencia.Util
{
    public class MapUtils :Page
    {
        public static string CalculatePolygonRadius(DateTime dateTime, int idActive)
        {
            try
            {
                string radius = "";
                OrderedDictionary addInfo = PositionFacade.GetInstance().GetAllAdditionalInformationByDate(dateTime, idActive);
                if (addInfo.Contains("coverage"))
                {
                    double coverage = Double.Parse(addInfo["coverage"].ToString());

                    if (coverage >= 0 && coverage < 10)
                    {
                        radius = "0.05";
                    }
                    else if (coverage <= 10 && coverage < 20)
                    {
                        radius = "0.5";
                    }
                    else if (coverage >= 20 && coverage < 30)
                    {
                        radius = "0.45";
                    }
                    else if (coverage >= 30 && coverage < 40)
                    {
                        radius = "0.40";
                    }
                    else if (coverage <= 40 && coverage < 50)
                    {
                        radius = "0.35";
                    }
                    else if (coverage >= 50 && coverage < 60)
                    {
                        radius = "0.30";
                    }
                    else if (coverage >= 60 && coverage < 70)
                    {
                        radius = "0.20";
                    }
                    else if (coverage <= 70 && coverage < 80)
                    {
                        radius = "0.15";
                    }
                    else if (coverage >= 80)
                    {
                        radius = "0.1";
                    }
                    //else if (coverage >= 90)
                    //{
                    //    radius = "0.05";
                    //}
                    return radius;
                }
                return "0.5";
            }
            catch (Exception ex)
            {
                return "0.5";
            }
        }

        /// <summary>
        /// Creates a html string to show assisted photo into infowindows
        /// </summary>
        /// <param name="pos">PositionDto</param>
        /// <returns>html string with assisted picture</returns>
        public static string ConfigureActivePicture(PositionDto pos)
        {
            AssistedDto assistedDto = AssistedFacade.GetInstance().FindAssistedByIdActive(pos.IdActive);
            string activePhoto = !String.IsNullOrEmpty(assistedDto.ImagePath) ? assistedDto.ImagePath.Substring(1, assistedDto.ImagePath.Length - 1) : ConfigurationManager.AppSettings["ActiveImageUnavailable"].Substring(1, ConfigurationManager.AppSettings["ActiveImageUnavailable"].Length - 1);
            string html = "<div style='float:right'><img alt='" + assistedDto.Name + "' height='100px' width='140px' src='" + activePhoto + "'/></div>";
            return html;
        }

        /// <summary>
        /// Creates html string to get status icon
        /// </summary>
        /// <param name="pos">PositionDto</param>
        /// <returns>html string with status icon</returns>
        public static string GetStatusIcon(PositionDto pos)
        {
            OrderedDictionary addInfo = PositionFacade.GetInstance().GetAllAdditionalInformationByDate(pos.Date, pos.IdActive);
            string status = PositionUtils.GetActiveStatus(pos.IdActive, pos.Date);
            string html = "";
            if (!pos.IsCellID)
            {
                if (status.Equals("parked"))
                {
                    html = "<img alt='parked' align='middle' src='..images/icons/32x32/parking.png'/>";
                }
                if (status.Equals("stopped"))
                {
                    html = "<img alt='stopped' src='..images/icons/32x32/stopped.png'/>";
                }
                if (status.Equals("moving"))
                {
                    if (addInfo.Contains("direction"))
                    {
                        double direction = Double.Parse(addInfo["direction"].ToString().Replace('.', ','));
                        if (direction <= 22)
                        {
                            html = "<img alt='moving' src='..images/icons/32x32/moving_000.png'/>";
                        }
                        if (direction > 22 && direction <= 67)
                        {
                            html = "<img alt='moving' src='..images/icons/32x32/moving_045.png'/>";
                        }
                        if (direction > 67 && direction <= 112)
                        {
                            html = "<img alt='moving' src='..images/icons/32x32/moving_090.png'/>";
                        }
                        if (direction > 112 && direction <= 157)
                        {
                            html = "<img alt='moving' src='..images/icons/32x32/moving_135.png'/>";
                        }
                        if (direction > 157 && direction <= 202)
                        {
                            html = "<img alt='moving' src='..images/icons/32x32/moving_180.png'/>";
                        }
                        if (direction > 202 && direction <= 247)
                        {
                            html = "<img alt='moving' src='..images/icons/32x32/moving_225.png'/>";
                        }
                        if (direction > 247 && direction <= 292)
                        {
                            html = "<img alt='moving' src='..images/icons/32x32/moving_270.png'/>";
                        }
                        if (direction > 292)
                        {
                            html = "<img alt='moving' src='..images/icons/32x32/moving_315.png'/>";
                        }
                    }
                    else
                    {
                        html = "<img alt='parking' src='..images/icons/32x32/moving_000.png'/>";
                    }
                }
            }
            else
            {
                html = "<img alt='Cellid' src='..images/icons/32x32/antenna.png'/>";
            }
            return html;
        }

        /// <summary>
        /// Creates html string to get status icon
        /// </summary>
        /// <param name="pos">PositionDto</param>
        /// <returns>html string with status icon</returns>
        public static string GetStatusIcon16x16(PositionDto pos)
        {
            OrderedDictionary addInfo = PositionFacade.GetInstance().GetAllAdditionalInformationByDate(pos.Date, pos.IdActive);
            string status = PositionUtils.GetActiveStatus(pos.IdActive, pos.Date);
            string html = "";
            if (!pos.IsCellID)
            {
                if (status.Equals("parked"))
                {
                    html = "<img alt='Detenido' src='images/icons/16x16/parking.png'/>";
                }
                if (status.Equals("stopped"))
                {
                    html = "<img alt='Parado' src='images/icons/16x16/stopped.png'/>";
                }
                if (status.Equals("moving"))
                {
                    if (addInfo.Contains("direction"))
                    {
                        double direction = Double.Parse(addInfo["direction"].ToString().Replace('.', ','));
                        if (direction <= 22)
                        {
                            html = "<img alt='Movimiento' src='images/icons/16x16/moving_000.png'/>";
                        }
                        if (direction > 22 && direction <= 67)
                        {
                            html = "<img alt='Movimiento' src='images/icons/16x16/moving_045.png'/>";
                        }
                        if (direction > 67 && direction <= 112)
                        {
                            html = "<img alt='Movimiento' src='images/icons/16x16/moving_090.png'/>";
                        }
                        if (direction > 112 && direction <= 157)
                        {
                            html = "<img alt='Movimiento' src='images/icons/16x16/moving_135.png'/>";
                        }
                        if (direction > 157 && direction <= 202)
                        {
                            html = "<img alt='Movimiento' src='images/icons/16x16/moving_180.png'/>";
                        }
                        if (direction > 202 && direction <= 247)
                        {
                            html = "<img alt='Movimiento' src='images/icons/16x16/moving_225.png'/>";
                        }
                        if (direction > 247 && direction <= 292)
                        {
                            html = "<img alt='Movimiento' src='images/icons/16x16/moving_270.png'/>";
                        }
                        if (direction > 292)
                        {
                            html = "<img alt='Movimiento' src='images/icons/16x16/moving_315.png'/>";
                        }
                    }
                    else
                    {
                        html = "<img alt='Desconocido' src='images/icons/16x16/moving_000.png'/>";
                    }
                }
                if (!status.Equals("parked") & !status.Equals("stopped") & !status.Equals("moving") & !status.Equals("crane"))
                {
                    html = "<img alt='Desconocido' src='images/icons/16x16/questionmark.png'>";
                }
            }
            else
            {
                html = "<img alt='Sin datos' src='images/icons/16x16/antenna.png'>";
            }
            return html;
        }

        ///// <summary>
        ///// Gets status icon to show into monitoring reports
        ///// </summary>
        ///// <param name="pos">position dto</param>
        ///// <param name="isLastPos">true if is last position of route</param>
        ///// <returns>status icon</returns>
        public string GetReportStatusIcon(PositionDto pos, bool isLastPos)
        {
            string status = PositionUtils.GetActiveStatus(pos.IdActive, pos.Date);
            string html = "";
            if (!pos.IsCellID)
            {
                if (status.Equals("parked"))
                {
                    html = "<img tooltip='Aparcado' alt='Aparcado' src='images/icons/16x16/parking.png'/>";
                }
                if (status.Equals("stopped"))
                {
                    html = "<img tooltip='Parado' alt='Parado' src='images/icons/16x16/stopped.png'/>";
                }
                if (status.Equals("crane"))
                {
                    html = "<img tooltip='Grua' alt='Grua' src='images/icons/16x16/alert.png'/>";
                }
                if (status.Equals("moving"))
                {
                    html = "<img tooltip='Movimiento' alt='Movimiento' src='images/icons/16x16/moving_000.png'/>";
                }
                //if (PositionUtils.isAlarmPosition(pos.IdActive, pos.Date) && isLastPos)
                //{
                //    ArrayList alerts = PositionUtils.ShowInfoWindowAlarm(pos.IdActive, pos.Date);
                //    html += "<img tooltip='";
                //    int i = 0;
                //    string tooltip = "";
                //    foreach (string text in alerts)
                //    {
                //        if (alerts.Count - 1 - i == 0)
                //            tooltip += GetGlobalResourceObject("InfoWindow", text);
                //        else
                //            tooltip += GetGlobalResourceObject("InfoWindow", text) + ", ";
                //        i++;
                //    }
                //    html += tooltip + "' alt='" + tooltip + "' src='..images/icons/16x16/alert.png'>";
                //}
                if (!status.Equals("parked") & !status.Equals("stopped") & !status.Equals("moving") & !status.Equals("crane"))
                {
                    html = "<img tooltip='Desconocido' alt='Desconocido' src='images/icons/16x16/questionmark.png'>";
                }
            }
            else
            {
                html = "<img tooltip='CellId' alt='CellId' src='images/icons/16x16/antenna.png'>";
            }
            return html;
        }

        /// <summary>
        /// Gets status icon path to show into monitoring reports
        /// </summary>
        /// <param name="pos">position</param>
        /// <param name="status">the status</param>
        /// <returns>status icon</returns>
        public static string GetReportStatusIcon32x32Path(PositionDto pos, string status)
        {
            string path = "";
            if (status.Equals("parked"))
            {
                path = "images/icons/32x32/parking.png";
            }
            if (status.Equals("stopped"))
            {
                path = "images/icons/32x32/stopped.png";
            }
            if (status.Equals("crane"))
            {
                path = "images/icons/32x32/crane.jpg";
            }
            if (status.Equals("moving"))
            {
                OrderedDictionary addInfo = PositionFacade.GetInstance().GetAllAdditionalInformationByDate(pos.Date,
                                                                                                           pos.IdActive);
                if (addInfo.Contains("direction"))
                {
                    double direction = Double.Parse(addInfo["direction"].ToString().Replace('.', ','),
                                                    new CultureInfo("es-ES"));
                    if (direction <= 22)
                    {
                        path = "images/icons/32x32/moving_000.png";
                    }
                    if (direction > 22 && direction <= 67)
                    {
                        path = "images/icons/32x32/moving_045.png";
                    }
                    if (direction > 67 && direction <= 112)
                    {
                        path = "images/icons/32x32/moving_090.png";
                    }
                    if (direction > 112 && direction <= 157)
                    {
                        path = "images/icons/32x32/moving_135.png";
                    }
                    if (direction > 157 && direction <= 202)
                    {
                        path = "images/icons/32x32/moving_180.png";
                    }
                    if (direction > 202 && direction <= 247)
                    {
                        path = "images/icons/32x32/moving_225.png";
                    }
                    if (direction > 247 && direction <= 292)
                    {
                        path = "images/icons/32x32/moving_270.png";
                    }
                    if (direction > 292)
                    {
                        path = "images/icons/32x32/moving_315.png";
                    }
                }
                else
                {
                    path = "images/icons/32x32/moving_000.png";
                }
            }
            if (status.Equals("cellid"))
            {
                path = "images/icons/32x32/antenna.png";
            }
            if (!status.Equals("parked") & !status.Equals("stopped") & !status.Equals("moving") & !status.Equals("cellid") & !status.Equals("crane"))
            {
                path = "images/icons/32x32/questionmark.png";
            }
            return path;
        }

        /// <summary>
        /// Creates html string to get status icon path 16x16 
        /// </summary>
        /// <param name="pos">PositionDto</param>
        /// <returns>html string with status icon</returns>
        public static string GetStatusIcon16x16Path(PositionDto pos)
        {
            OrderedDictionary addInfo = PositionFacade.GetInstance().GetAllAdditionalInformationByDate(pos.Date, pos.IdActive);
            string status = PositionUtils.GetActiveStatus(pos.IdActive, pos.Date);
            string path = "";

            if (!pos.IsCellID)
            {
                if (status.Equals("parked"))
                {
                    path = "images/icons/16x16/parking.png";
                }
                if (status.Equals("stopped"))
                {
                    path = "images/icons/16x16/stopped.png";
                }
                if (status.Equals("moving"))
                {
                    if (addInfo.Contains("direction"))
                    {
                        double direction = Double.Parse(addInfo["direction"].ToString().Replace('.', ','));
                        if (direction <= 22)
                        {
                            path = "images/icons/16x16/moving_000.png";
                        }
                        if (direction > 22 && direction <= 67)
                        {
                            path = "images/icons/16x16/moving_045.png";
                        }
                        if (direction > 67 && direction <= 112)
                        {
                            path = "images/icons/16x16/moving_090.png";
                        }
                        if (direction > 112 && direction <= 157)
                        {
                            path = "images/icons/16x16/moving_135.png";
                        }
                        if (direction > 157 && direction <= 202)
                        {
                            path = "images/icons/16x16/moving_180.png";
                        }
                        if (direction > 202 && direction <= 247)
                        {
                            path = "images/icons/16x16/moving_225.png";
                        }
                        if (direction > 247 && direction <= 292)
                        {
                            path = "images/icons/16x16/moving_270.png";
                        }
                        if (direction > 292)
                        {
                            path = "images/icons/16x16/moving_315.png";
                        }
                    }
                    else
                    {
                        path = "images/icons/16x16/moving_000.png";
                    }
                }
                if (!status.Equals("parked") & !status.Equals("stopped") & !status.Equals("moving") & !status.Equals("crane"))
                {
                    path = "images/icons/16x16/questionmark.png";
                }
            }
            else
            {
                path = "images/icons/16x16/antenna.png";
            }
            return path;
        }

        /// <summary>
        /// Calculates distance in meters between two coordenates.
        /// </summary>
        /// <param name="idActive">Active´s identifier</param>
        /// <param name="startPos">Start position</param>
        /// <param name="pos1">First position</param>
        /// <param name="pos2">Second position</param>
        /// <returns>Distance in kilometers</returns>
        public static double CalculateDistanceInMeters(int idActive, PositionDto startPos, PositionDto pos1, PositionDto pos2)
        {
            double meters = 0;
            ArrayList positions = PositionFacade.GetInstance().GetPositionsByDate(idActive, pos1.Date, pos2.Date, false);
            foreach (PositionDto pos in positions)
            {
                string ms = PositionFacade.GetInstance().GetMileageByDate(pos.Date, idActive).Replace('.', ',');
                if (string.IsNullOrEmpty(ms) || ms.Equals("NeuN") || ms.Equals("NaN"))
                    meters += 0;
                else
                    meters += double.Parse(ms, new CultureInfo("es-ES"));

            }
            return meters;
        }

        /// <summary>
        /// Calculates distance in meters between two dates.
        /// </summary>
        /// <param name="idActive">Active´s identifier</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Distance in kilometers</returns>
        public static double CalculateDistanceInMeters(int idActive, DateTime startDate, DateTime endDate)
        {
            double meters = 0;
            try
            {
                ArrayList positions = PositionFacade.GetInstance().GetPositionsByDate(idActive, startDate, endDate.AddSeconds(1), false);
                foreach (PositionDto pos in positions)
                {
                    string ms = PositionFacade.GetInstance().GetMileageByDate(pos.Date, idActive).Replace('.', ',');
                    if (string.IsNullOrEmpty(ms) || ms.Equals("NeuN") || ms.Equals("NaN"))
                        meters += 0;
                    else
                        meters += double.Parse(ms, new CultureInfo("es-ES"));
                }
                return meters;
            }
            catch(Exception ex)
            {
                return meters;
            }
        }

        /// <summary>
        /// Calculates total distance in kilometers meters between two coordenates.
        /// </summary>
        /// <param name="lastPosition">Active's last position</param>
        /// <returns>Active's total distance in kilometers</returns>
        public static double CalculateOdometer(PositionDto lastPosition)
        {
            try
            {
                return PositionFacade.GetInstance().GetTotalMileage(lastPosition.IdActive);
            }
            catch(Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// Draw status marker in the point indicated
        /// </summary>
        /// <param name="point">point to draw marker</param>
        /// <param name="GMap1">Google map</param>
        public static void DrawStatusMarker(PositionDto point, GMap GMap1)
        {
            var latLng = new GLatLng(point.Latitude, point.Longitude);
            GIcon icon = new GIcon(GetStatusIcon16x16Path(point));
            icon.iconSize = new GSize(16, 16);
            icon.shadowSize = new GSize(0, 0);
            icon.iconAnchor = new GPoint(-4, -1);
            icon.infoWindowAnchor = new GPoint(16,0);
            GMarkerOptions mOpts = new GMarkerOptions(icon);
            GMarker marker = new GMarker(latLng, mOpts);
            GMap1.addGMarker(marker);
        }

        /// <summary>
        /// Draw status marker in the point indicated with tabs asociated
        /// </summary>
        /// <param name="point">Point to draw marker</param>
        /// <param name="tabs">List of GInfoWindowTabs</param>
        /// <param name="GMap1">Google map</param>
        /// <param name="first">True if first position</param>
        public static GMarker DrawStatusMarker(PositionDto point, string html, GMap GMap1, bool first)
        {
            var latLng = new GLatLng(point.Latitude, point.Longitude);
            GIcon icon = new GIcon(GetStatusIcon16x16Path(point));
            icon.iconSize = new GSize(16, 16);
            icon.shadowSize = new GSize(0, 0);
            icon.shadow = null;
            if (first)
                icon.iconAnchor = new GPoint(-10, -1);
            else 
                icon.iconAnchor = new GPoint(-4, -1);
            icon.infoWindowAnchor = new GPoint(16, 0);
            GMarkerOptions mOpts = new GMarkerOptions(icon);
            GMarker marker = new GMarker(latLng, mOpts);
            GMap1.addGMarker(marker);
            return marker;
        }

        /// <summary>
        /// Draw status marker in the point indicated with tabs asociated
        /// </summary>
        /// <param name="point">Point to draw marker</param>
        /// <param name="tabs">List of GInfoWindowTabs</param>
        /// <param name="GMap1">Google map</param>
        public static GMarker DrawIconMarker(PositionDto point, string html, GMap GMap1)
        {
            var latLng = new GLatLng(point.Latitude, point.Longitude);
            GIcon icon = new GIcon(ConfigurationManager.AppSettings["AssistedIcon"]);
            icon.shadowSize = new GSize(0, 0);
            icon.iconSize = new GSize(32, 32);
            icon.infoWindowAnchor = new GPoint(16, 0);
            icon.iconAnchor = new GPoint(16, 16);
            GMarkerOptions mOpts = new GMarkerOptions(icon);
            GMarker marker = new GMarker(latLng, mOpts);
            GMap1.addGMarker(marker);
            var options = new GInfoWindowOptions(0, 400);
            var lastTabs = new GInfoWindow(marker, html, false, options, GListener.Event.mouseover);
            GMap1.addInfoWindow(lastTabs);
            return marker;
        }

        /// <summary>
        /// Draw status marker in the point indicated with tabs asociated
        /// </summary>
        /// <param name="point">Point to draw marker</param>
        /// <param name="tabs">List of GInfoWindowTabs</param>
        /// <param name="GMap1">Google map</param>
        public static GMarker DrawHouseMarker(PositionDto point, string html, GMap GMap1)
        {
            var latLng = new GLatLng(point.Latitude, point.Longitude);
            GIcon icon = new GIcon(ConfigurationManager.AppSettings["HouseIcon"]);
            icon.shadowSize = new GSize(0, 0);
            icon.iconSize = new GSize(32, 32);
            icon.infoWindowAnchor = new GPoint(16, 0);
            icon.iconAnchor = new GPoint(16, 16);
            GMarkerOptions mOpts = new GMarkerOptions(icon);
            GMarker marker = new GMarker(latLng, mOpts);
            GMap1.addGMarker(marker);
            var options = new GInfoWindowOptions(0, 400);
            var lastTabs = new GInfoWindow(marker, html, false, options, GListener.Event.mouseover);
            GMap1.addInfoWindow(lastTabs);
            return marker;
        }
        
        /// <summary>
        /// Obtains the coordenate with maximun latitude and minimum longitude
        /// </summary>
        /// <param name="points">List of coordenates</param>
        /// <returns>Coordenate searched</returns>
        protected static GLatLng GetBoundSW(List<GLatLng> points)
        {
            Double lat = points.Max(a => a.lat);
            Double lng = points.Min(a => a.lng);
            GLatLng sw = new GLatLng(lat, lng);
            return sw;
        }

        /// <summary>
        /// Obtains the coordenate with minimun latitude and maximum longitude
        /// </summary>
        /// <param name="points">List of coordenates</param>
        /// <returns></returns>
        protected static GLatLng GetBoundNE(List<GLatLng> points)
        {
            Double lat = points.Min(a => a.lat);
            Double lng = points.Max(a => a.lng);
            GLatLng ne = new GLatLng(lat, lng);
            return ne;
        }

        /// <summary>
        /// Sets GMap to center and obtains the best zoom to draw routes
        /// </summary>
        /// <param name="points">List that contains all coordenates to draw</param>
        /// <param name="GMap1">The map</param>
        public static void SetMapCenterAndZoom(List<GLatLng> points, GMap GMap1)
        {
            GLatLngBounds bounds = new GLatLngBounds(GetBoundSW(points), GetBoundNE(points));
            int zoom = GMap1.getBoundsZoomLevel(bounds);
            zoom = zoom + 3;
            if (zoom > 17)
                zoom = 17;
            if (bounds.getNorthEast().Equals(bounds.getSouthWest()))
                zoom = 17;
            GMap1.setCenter(bounds.getCenter(), zoom);
        }

        /// <summary>
        /// Sets GMap to center and obtains the best zoom to draw routes
        /// </summary>
        /// <param name="points">List that contains all coordenates to draw</param>
        /// <param name="zoom">zoom to set</param>
        /// <param name="GMap1">The map</param>
        public static void SetMapCenterAndZoom(List<GLatLng> points, int zoom, GMap GMap1)
        {
            GLatLngBounds bounds = new GLatLngBounds(GetBoundSW(points), GetBoundNE(points));
            if (zoom > 17)
                zoom = 17;
            GMap1.setCenter(bounds.getCenter(), zoom);
        }

        /// <summary>
        /// Prints traveled distance
        /// </summary>
        /// <param name="mileage">mileage</param>
        /// <returns>string with traveled distance</returns>
        public static string PrintMileage(string mileage)
        {
            string distance = "";
            if (!string.IsNullOrEmpty(mileage))
            {
                //Hacemos try por si en la BD hay datos no convertibles (incorrectos) para un double, int, etc
                try
                {
                    char[] delim = { ',', '.' };
                    String[] integer = mileage.Split(delim);
                    long dist = Int64.Parse(integer[0]);

                    const long divisor = 1000;
                    long meters;
                    if (dist > 0)
                    {
                        if (dist > 1000)
                        {
                            long km = Math.DivRem(dist, divisor, out meters);
                            if (meters > 0)
                            {
                                distance = km + " km " + meters + " m";
                            }
                            else
                            {
                                distance = km + " km ";
                            }
                        }
                        else
                        {
                            distance = dist + " m";
                        }
                    }
                }
                catch(Exception ex){}
            }
            return distance;
        }

        /// <summary>
        /// Creates random colours
        /// </summary>
        /// <param name="seed">seed</param>
        /// <returns>RGB code of color generated</returns>
        public static string GenerarColores(int seed)
        {
            string colores = "";
            Random aleatorios = new Random(DateTime.Now.Millisecond / seed);
            int red = aleatorios.Next(215);
            int blue = aleatorios.Next(215);
            int green = aleatorios.Next(215);
            colores += red.ToString("X");
            colores += blue.ToString("X");
            colores += green.ToString("X");
            colores = colores.PadLeft(6, '0');
            return colores;
        }

        ///// <summary>
        ///// Last grouping after applying restrictions
        ///// </summary>
        ///// <param name="context">Session context</param>
        ///// <param name="data">Report data</param>
        ///// <param name="idActive">Active´s identifier</param>
        ///// <returns>Data to bind</returns>
        //public static ArrayList LastGrouping(HttpContext context, ArrayList data, int idActive)
        //{
        //    try
        //    {
        //        for (int i = 1; i < data.Count; i++)
        //        {
        //            bool isMobileDevice = ActiveFacade.GetInstance().IsMobileDevice(idActive);
        //            MonitoringRoutesRowDto startRow = (MonitoringRoutesRowDto)data[0];
        //            MonitoringRoutesRowDto prevRow = (MonitoringRoutesRowDto)data[i - 1];
        //            string prevStatus = prevRow.Status;
        //            MonitoringRoutesRowDto row = (MonitoringRoutesRowDto)data[i];
        //            string status = row.Status;
        //            PositionDto pos1 = PositionFacade.GetInstance().FindPositionByDate(prevRow.Date, idActive);
        //            PositionDto pos2 = PositionFacade.GetInstance().FindPositionByDate(row.Date, idActive);
        //            PositionDto startPos = PositionFacade.GetInstance().FindPositionByDate(startRow.Date,startRow.IdActive);
        //            double mileage = CalculateDistanceInMeters(idActive,startPos, pos1, pos2);
        //            prevRow.Mileage = Double.Parse(mileage.ToString().Replace('.',','),new CultureInfo("es-ES")).ToString();
        //            if (prevStatus.Equals(status) && !prevStatus.Equals("moving") && !status.Equals("cellid") && !status.Equals("stopped"))
        //            {
        //                if (!(isMobileDevice && Common.Util.Utils.DistanceTo(pos1.Latitude,pos1.Longitude, pos2.Latitude, pos2.Longitude) > 50))
        //                {
        //                    prevRow.EndTime = row.EndTime;
        //                    DateTime lclEndTime = ConvertToLclTime(context, DateTime.Parse(prevRow.Date.ToShortDateString() + " " + prevRow.EndTime));
        //                    DateTime lclStartTime = ConvertToLclTime(context, DateTime.Parse(prevRow.Date.ToShortDateString() + " " + prevRow.StartTime));
        //                    string time = DateTime.Parse(lclEndTime.ToLongTimeString()).Subtract(DateTime.Parse(lclStartTime.ToLongTimeString())).ToString();
        //                    if (time.Substring(0, 1).Equals("-"))
        //                    {
        //                        prevRow.DiffTime = time.Substring(1, time.Length - 1);
        //                    }
        //                    else
        //                    {
        //                        prevRow.DiffTime = time;
        //                    }
        //                    data.RemoveAt(i);
        //                    i--;
        //                }
        //            }
        //        }
        //        for (int j = 0; j < data.Count - 1; j++)
        //        {
        //            MonitoringRoutesRowDto row = (MonitoringRoutesRowDto)data[j];
        //            string status = row.Status;
        //            MonitoringRoutesRowDto nextRow = (MonitoringRoutesRowDto)data[j + 1];
        //            if (status.Equals("stopped")||status.Equals("parked")||status.Equals("cellid"))
        //            {
        //                double mil = Double.Parse(row.Mileage);
        //                if (!String.IsNullOrEmpty(nextRow.Mileage))
        //                {
        //                    double nextMil = Double.Parse(nextRow.Mileage);
        //                    mil = mil + nextMil;
        //                }
        //                row.Mileage = "";
        //                nextRow.Mileage = mil.ToString();
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //    }
        //    return data;
        //}

        ///// <summary>
        ///// Groups positions with the same status
        ///// </summary>
        ///// <param name="context">Session context</param>
        ///// <param name="positions">Positions list</param>
        ///// <param name="idActive">Active´s identifier</param>
        ///// <returns>Data with positions grouped</returns>
        //public static ArrayList GroupPositions(HttpContext context, ArrayList positions, int idActive)
        //{
        //    DateTime lastPosDate = DateTime.MinValue;
        //    DateTime firstPosDate;
        //    var last = (MonitoringRoutesRowDto)positions[positions.Count - 1];
        //    DateTime lastDate = last.Date;
        //    ArrayList data = positions;
        //    try
        //    {
        //        string prevStatus; 
        //        string status = "";
        //        var startRow = (MonitoringRoutesRowDto)data[0];
        //        for (int i = 0; i < data.Count; i++)
        //        {
        //            PositionDto startPos = PositionFacade.GetInstance().FindPositionByDate(startRow.Date, idActive);
        //            MonitoringRoutesRowDto row = (MonitoringRoutesRowDto)data[i];
        //            if (i > 0)
        //            {
        //                prevStatus = status;
        //                MonitoringRoutesRowDto prevRow = (MonitoringRoutesRowDto)data[i - 1];
        //                DateTime date = row.Date;
        //                DateTime prevDate = lastPosDate;
        //                PositionDto pos = PositionFacade.GetInstance().FindPositionByDate(date, idActive);
        //                status = PositionUtils.GetActiveStatus(idActive, pos.Date);
        //                row.Status = status;
        //                row.StatusPath = GetReportStatusIconPath(pos, status, false);
        //                PositionDto prevPos = PositionFacade.GetInstance().FindPositionByDate(prevDate, idActive);

        //                //Comprobamos si dos posiciones seguidas son parked
        //                if (prevStatus.Equals("parked") && status.Equals("parked"))
        //                {
        //                    //Calculamos distancia entre ellas
        //                    double dis = Common.Util.Utils.DistanceTo(prevPos.Latitude, prevPos.Longitude, pos.Latitude, pos.Longitude);
        //                    //Si es dispositivo movil y la distancia es mayor de la establecida -> no agrupamos
        //                    if (ActiveFacade.GetInstance().IsMobileDevice(idActive) && dis > Int32.Parse(ConfigurationManager.AppSettings["groupDistance"]))
        //                    {
        //                        prevRow.EndTime = pos.Date.ToLongTimeString();
        //                        prevRow.DiffTime = ConvertToLclTime(context, DateTime.Parse(prevRow.EndTime)).Subtract(ConvertToLclTime(context, DateTime.Parse(prevRow.StartTime))).ToString();
        //                        prevRow.Status = prevStatus;
        //                        //firstPosDate = pos.Date;
        //                        row.StartTime = pos.Date.ToLongTimeString();
        //                        lastPosDate = pos.Date;
        //                    }
        //                        //Si no es dispositivo movil, o lo es pero no supera distancia entre posiciones -> Agrupamos
        //                    else
        //                    {
        //                        prevRow.Latitude = row.Latitude;
        //                        prevRow.Longitude = row.Longitude;
        //                        prevRow.EndTime = pos.Date.ToLongTimeString();
        //                        lastPosDate = pos.Date;
        //                        data.RemoveAt(i);
        //                        i--;
        //                    }
        //                }
        //                //Si las posiciones son movimiento -> No hacemos nada (calculamos diferencias, etc etc)
        //                if ((prevStatus.Equals("moving") && status.Equals("moving"))||(prevStatus.Equals("crane") && status.Equals("crane")))
        //                {
        //                    prevRow.EndTime = pos.Date.ToLongTimeString();
        //                    row.Status = status;
        //                    row.StartTime = row.Date.ToLongTimeString();
        //                    lastPosDate = pos.Date;
        //                    row.StatusPath = GetReportStatusIconPath(pos, status, false);

        //                }
        //                //Si son dos posiciones CellId consecutivas
        //                if (prevStatus.Equals("cellid") && status.Equals("cellid"))
        //                {
        //                    //Si la posicion es la misma -> Agrupamos
        //                    if (pos.Latitude.Equals(prevPos.Latitude) && pos.Longitude.Equals(prevPos.Longitude))
        //                    {
        //                        prevRow.EndTime = pos.Date.ToLongTimeString();
        //                        lastPosDate = pos.Date;
        //                        data.RemoveAt(i);
        //                        i--;
        //                    }
        //                        //Si no no hacemos nada
        //                    else
        //                    {
        //                        prevRow.EndTime = pos.Date.ToLongTimeString();
        //                        prevRow.Status = prevStatus;
        //                        row.StartTime = pos.Date.ToLongTimeString();
        //                        lastPosDate = pos.Date;
        //                    }
        //                }
        //                //Si las dos posiciones son parados
        //                if (prevStatus.Equals("stopped") && status.Equals("stopped"))
        //                {
        //                    prevRow.EndTime = pos.Date.ToLongTimeString();
        //                    prevRow.Status = prevStatus;
        //                    row.StartTime = pos.Date.ToLongTimeString();
        //                    lastPosDate = pos.Date;
        //                    ////Si la distancia es = 0 -> Agrupamos las dos posiciones
        //                    //if(CalculateDistanceInMeters(idActive,startPos, prevPos, pos) == 0)
        //                    //{
        //                    //prevRow.EndTime = pos.Date.ToLongTimeString();
        //                    //lastPosDate = pos.Date;
        //                    //data.RemoveAt(i);
        //                    //i--;
        //                    //}
        //                    ////Si existe distancia entre dos posiciones paused -> CAMBIAMOS A MOVIMIENTO
        //                    //else
        //                    //{
        //                    //    prevRow.EndTime = pos.Date.ToLongTimeString();
        //                    //    row.StatusPath = row.StatusPath = GetReportStatusIconPath(pos, "moving", false);
        //                    //    row.Status = "moving";
        //                    //    row.StartTime = row.Date.ToLongTimeString();
        //                    //    lastPosDate = pos.Date;
        //                    //    status = "moving";
        //                    //}
        //                }
        //                //Si no coinciden los estados
        //                if (!prevStatus.Equals(status))
        //                {
        //                    //Si NO es un dispositivo movil Y está parado o aparcado Y la distancia es mayor que 0
        //                    //if (!ActiveFacade.GetInstance().IsMobileDevice(idActive) && (status.Equals("parked") || status.Equals("stopped")) && CalculateDistanceInMeters(idActive, startPos, prevPos, pos) > 0)
        //                    //{
        //                    //    //Si el anterior era movimiento -> no hacemos nada
        //                    //    if (!prevStatus.Equals("moving"))
        //                    //    {
        //                    //        prevRow.EndTime = pos.Date.ToLongTimeString();
        //                    //        row.StartTime = row.Date.ToLongTimeString();
        //                    //        lastPosDate = pos.Date;
        //                    //    }
        //                    //    //si el anterior era movimiento -> cambiamos actual a movimiento
        //                    //    else
        //                    //    {
        //                    //        prevRow.EndTime = pos.Date.ToLongTimeString();
        //                    //        row.StartTime = pos.Date.ToLongTimeString();
        //                    //        lastPosDate = pos.Date;
        //                    //        status = "moving";
        //                    //        row.Status = status;
        //                    //        row.StatusPath = row.StatusPath = GetReportStatusIconPath(pos, "moving", false);
        //                    //    }
        //                    //}
        //                    //else
        //                    //{
        //                    prevRow.EndTime = pos.Date.ToLongTimeString();
        //                    prevRow.Status = prevStatus;
        //                    row.StartTime = pos.Date.ToLongTimeString();
        //                    lastPosDate = pos.Date;
        //                    //}
        //                }

        //                if (i + 1 == data.Count)
        //                {
        //                    row.EndTime = lastDate.ToLongTimeString();
        //                }
        //            }
        //            else //First Position
        //            {
        //                firstPosDate = row.Date;
        //                lastPosDate = row.Date;
        //                PositionDto firstPos = PositionFacade.GetInstance().FindPositionByDate(firstPosDate, idActive);
        //                row.StartTime = firstPosDate.ToLongTimeString();
        //                status = PositionUtils.GetActiveStatus(idActive, firstPosDate);
        //                row.StatusPath = GetReportStatusIconPath(firstPos, status, false);
        //                row.Status = status;
        //                if (i + 1 == data.Count)
        //                {
        //                    row.EndTime = lastDate.ToLongTimeString();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {}
        //    return data;
        //}
        ///// <summary>
        ///// Convert a date GMT to local time
        ///// </summary>
        ///// <param name="context">Context</param>
        ///// <param name="date">Date to convert</param>
        ///// <returns>Local date time</returns>
        //public static DateTime ConvertToLclTime(HttpContext context, DateTime date)
        //{
        //    try
        //    {
        //        TimeZoneInfo localZone = TimeZoneInfo.FindSystemTimeZoneById(SessionManager.GetUserTimeZone(context));
        //        DateTime localDate = TimeZoneInfo.ConvertTime(date, TimeZoneInfo.Utc, localZone);
        //        return localDate;
        //    }
        //    catch(Exception ex)
        //    {
        //        return date;
        //    }
        //}

        ///// <summary>
        ///// Converts a local date to GMT time
        ///// </summary>
        ///// <param name="context">Context</param>
        ///// <param name="date">Date to convert</param>
        ///// <returns>GMT date time</returns>
        //public static DateTime ConvertToGMTTime(HttpContext context, DateTime date)
        //{
        //    TimeZoneInfo localZone = TimeZoneInfo.FindSystemTimeZoneById(SessionManager.GetUserTimeZone(context));
        //    DateTime dateGMT;

        //    if (date.Kind.Equals(DateTimeKind.Local))
        //        dateGMT = TimeZoneInfo.ConvertTime(date, TimeZoneInfo.Local, TimeZoneInfo.Utc);
        //    else
        //        dateGMT = TimeZoneInfo.ConvertTime(date, localZone, TimeZoneInfo.Utc);

        //    return dateGMT;
        //}
        ///// <summary>
        ///// Applies restrictions to the positions list
        ///// </summary>
        ///// <param name="pos">Positions list</param>
        ///// <returns>Data with restrictions applied</returns>
        //public static ArrayList ApplyRestrictions(ArrayList pos)
        //{
        //    try
        //    {
        //        for (int j = 0; j < pos.Count; j++)
        //        {
        //            MonitoringRoutesRowDto row = (MonitoringRoutesRowDto) pos[j];
                    
        //            if (row.Status.Equals("stopped"))
        //            {
        //                if(!String.IsNullOrEmpty(row.Mileage) && !row.Mileage.Equals("0"))
        //                {
        //                    row.StatusPath = GetReportStatusIconPath("moving");
        //                    row.Status = "moving";
        //                }
        //                //else
        //                //{
        //                //    if (j + 1 < pos.Count)
        //                //    {
        //                //        MonitoringRoutesRowDto nextRow = (MonitoringRoutesRowDto)pos[j + 1];
        //                //        if (nextRow.Status.Equals("parked"))
        //                //        {
        //                //            row.StatusPath = GetReportStatusIconPath("parked");
        //                //            row.Status = "parked";
        //                //        }
        //                //    }
        //                //}
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //    }
        //    return pos;
        //}

        ///// <summary>
        ///// Obtains active status and fills it into positions
        ///// </summary>
        ///// <param name="lastPositions">Positions arraylist</param>
        public static void FillActiveStatus(ref List<MonitoringReportDto> lastPositions)
        {
            foreach (MonitoringReportDto dto in lastPositions)
            {
                if (dto.LastPosDate.Equals(DateTime.MinValue))
                    dto.LastPosDate = null;
                if (dto.LastPosDate != null)
                {
                    DateTime date = DateTime.Parse(dto.LastPosDate.ToString());
                    PositionDto posDto = PositionFacade.GetInstance().FindPositionByDate(date, dto.IdActive);
                    dto.Status = new MapUtils().GetReportStatusIcon(posDto, true);
                }
            }
        }

        /// <summary>
        /// Obtains active status and fills it into positions
        /// </summary>
        /// <param name="lastPositions">Positions arraylist</param>
        public static void FillActiveStatus(ref ArrayList lastPositions)
        {
            foreach (MonitoringReportDto dto in lastPositions)
            {
                if (dto.LastPosDate.Equals(DateTime.MinValue))
                    dto.LastPosDate = null;
                if (dto.LastPosDate != null)
                {
                    DateTime date = DateTime.Parse(dto.LastPosDate.ToString());
                    PositionDto posDto = PositionFacade.GetInstance().FindPositionByDate(date, dto.IdActive);
                    dto.Status = new MapUtils().GetReportStatusIcon(posDto, true);
                }
            }
        }

        /// <summary>
        /// Draws cellid polygon around a cellid marker in map
        /// </summary>
        /// <param name="posDto">Position</param>
        /// <param name="marker">Marker</param>
        /// <param name="GMap1">GMap</param>
        public static void DrawCellIdPolygon(PositionDto posDto, GMarker marker, GMap GMap1)
        {
            var sb2 = new StringBuilder();
            sb2.Append("Array.prototype.removeAt = function (iIndex){");
            sb2.Append("var vItem = this[iIndex];");
            sb2.Append("if (vItem) {");
            sb2.Append("this.splice(iIndex, 1);}");
            sb2.Append("return vItem;};");

            sb2.Append("Array.prototype.indexOf = function (needle) {");
            sb2.Append("for (var x=0;x<this.length;x++) if(this[x] == needle) return x;");
            sb2.Append("return false;};");

            sb2.Append("Array.prototype.contains = function (needle) {");
            sb2.Append("for (i in this) {");
            sb2.Append("if (this[i] == needle) return true;");
            sb2.Append("}");
            sb2.Append("return false;};");

            sb2.Append("var polygons = Array();");
            sb2.Append("var markers = Array();");
            sb2.Append("function drawCircle(marker, lat, lng, radius, strokeColor, strokeWidth, strokeOpacity, fillColor, fillOpacity) {");
            sb2.Append("var d2r = Math.PI / 180;");
            sb2.Append("var r2d = 180 / Math.PI;");
            sb2.Append("var Clat = radius * 0.014483;");
            sb2.Append("var Clng = Clat / Math.cos(lat * d2r);");
            sb2.Append("var Cpoints = [];");
            sb2.Append("for (var i = 0; i < 101; i++) {");
            sb2.Append("var theta = Math.PI * (i / 50);");
            sb2.Append("var Cy = lat + (Clat * Math.sin(theta));");
            sb2.Append("var Cx = lng + (Clng * Math.cos(theta));");
            sb2.Append("var P = new google.maps.LatLng(Cy, Cx);");
            sb2.Append("Cpoints.push(P);");
            sb2.Append("}");
            sb2.Append("var polygon = new GPolygon(Cpoints, strokeColor, strokeWidth, strokeOpacity, fillColor, fillOpacity);");
            sb2.Append("if (!markers.contains(marker)){");
            sb2.Append(GMap1.GMap_Id + ".addOverlay(polygon);");
            sb2.Append("polygons.push(polygon);");
            sb2.Append("markers.push(marker);}");

            sb2.AppendFormat("google.maps.event.addListener(polygon, 'click',");
            sb2.Append("function(){");
            sb2.Append("var index = polygons.indexOf(polygon);");
            sb2.Append("polygons.removeAt(index);");
            sb2.Append("markers.removeAt(index);");
            sb2.AppendFormat(GMap1.GMap_Id + ".removeOverlay(polygon);");
            sb2.Append("});");
            sb2.Append("}");

            sb2.AppendFormat("google.maps.event.addListener(" + marker.ID + ", 'infowindowopen',");
            sb2.Append("function(){");
            sb2.AppendFormat("{0}.savePosition();", GMap1.GMap_Id);
            sb2.Append("drawCircle(" + marker.ID + ", " + posDto.Latitude.ToString().Replace(',', '.') + ", " + posDto.Longitude.ToString().Replace(',', '.') + ", " + CalculatePolygonRadius(posDto.Date, posDto.IdActive) + ", '#0000FF', 1, 0.75, '#5AB4C5', 0.5);");
            sb2.Append("});");

            sb2.AppendFormat("google.maps.event.addListener(" + marker.ID + ", 'infowindowclose',");
            sb2.Append("function(){");
            sb2.AppendFormat("{0}.returnToSavedPosition();", GMap1.GMap_Id);
            sb2.Append("});");

            GMap1.addCustomInsideJavascript(sb2.ToString());
        }

        /// <summary>
        /// Draws cellid polygon around a cellid marker in map
        /// </summary>
        /// <param name="posDto">Position</param>
        /// <param name="marker">Marker</param>
        /// <param name="GMap1">GMap</param>
        public static void DrawCellIdPolygon(MonitoringRoutesRowDto posDto, GMarker marker, GMap GMap1)
        {
            var sb2 = new StringBuilder();
            sb2.Append("Array.prototype.removeAt = function (iIndex){");
            sb2.Append("var vItem = this[iIndex];");
            sb2.Append("if (vItem) {");
            sb2.Append("this.splice(iIndex, 1);}");
            sb2.Append("return vItem;};");

            sb2.Append("Array.prototype.indexOf = function (needle) {");
            sb2.Append("for (var x=0;x<this.length;x++) if(this[x] == needle) return x;");
            sb2.Append("return false;};");

            sb2.Append("Array.prototype.contains = function (needle) {");
            sb2.Append("for (i in this) {");
            sb2.Append("if (this[i] == needle) return true;");
            sb2.Append("}");
            sb2.Append("return false;};");

            sb2.Append("var polygons = Array();");
            sb2.Append("var markers = Array();");
            sb2.Append("function drawCircle(marker, lat, lng, radius, strokeColor, strokeWidth, strokeOpacity, fillColor, fillOpacity) {");
            sb2.Append("var d2r = Math.PI / 180;");
            sb2.Append("var r2d = 180 / Math.PI;");
            sb2.Append("var Clat = radius * 0.014483;");
            sb2.Append("var Clng = Clat / Math.cos(lat * d2r);");
            sb2.Append("var Cpoints = [];");
            sb2.Append("for (var i = 0; i < 101; i++) {");
            sb2.Append("var theta = Math.PI * (i / 50);");
            sb2.Append("var Cy = lat + (Clat * Math.sin(theta));");
            sb2.Append("var Cx = lng + (Clng * Math.cos(theta));");
            sb2.Append("var P = new google.maps.LatLng(Cy, Cx);");
            sb2.Append("Cpoints.push(P);");
            sb2.Append("}");
            sb2.Append("var polygon = new GPolygon(Cpoints, strokeColor, strokeWidth, strokeOpacity, fillColor, fillOpacity);");
            sb2.Append("if (!markers.contains(marker)){");
            sb2.Append(GMap1.GMap_Id + ".addOverlay(polygon);");
            sb2.Append("polygons.push(polygon);");
            sb2.Append("markers.push(marker);}");

            sb2.AppendFormat("google.maps.event.addListener(polygon, 'click',");
            sb2.Append("function(){");
            sb2.Append("var index = polygons.indexOf(polygon);");
            sb2.Append("polygons.removeAt(index);");
            sb2.Append("markers.removeAt(index);");
            sb2.AppendFormat(GMap1.GMap_Id + ".removeOverlay(polygon);");
            sb2.Append("});");
            sb2.Append("}");

            sb2.AppendFormat("google.maps.event.addListener(" + marker.ID + ", 'infowindowopen',");
            sb2.Append("function(){");
            sb2.AppendFormat("{0}.savePosition();", GMap1.GMap_Id);
            sb2.Append("drawCircle(" + marker.ID + ", " + posDto.Latitude.ToString().Replace(',', '.') + ", " + posDto.Longitude.ToString().Replace(',', '.') + ", " + CalculatePolygonRadius(posDto.Date, posDto.IdActive) + ", '#0000FF', 1, 0.75, '#5AB4C5', 0.5);");
            sb2.Append("});");

            sb2.AppendFormat("google.maps.event.addListener(" + marker.ID + ", 'infowindowclose',");
            sb2.Append("function(){");
            sb2.AppendFormat("{0}.returnToSavedPosition();", GMap1.GMap_Id);
            sb2.Append("});");

            GMap1.addCustomInsideJavascript(sb2.ToString());
        }

        /// <summary>
        /// Groups positions with the same status
        /// </summary>
        /// <param name="context">Session context</param>
        /// <param name="positions">Positions list</param>
        /// <param name="idActive">Active´s identifier</param>
        /// <returns>Data with positions grouped</returns>
        public static ArrayList GroupPositions(HttpContext context, ArrayList positions, int idActive)
        {
            DateTime lastPosDate = DateTime.MinValue;
            DateTime firstPosDate;
            var last = (MonitoringRoutesRowDto)positions[positions.Count - 1];
            DateTime lastDate = last.Date;
            ArrayList data = positions;
            try
            {
                string prevStatus;
                string status = "";
                for (int i = 0; i < data.Count; i++)
                {
                    MonitoringRoutesRowDto row = (MonitoringRoutesRowDto)data[i];
                    if (i > 0)
                    {
                        prevStatus = status;
                        MonitoringRoutesRowDto prevRow = (MonitoringRoutesRowDto)data[i - 1];
                        DateTime date = row.Date;
                        DateTime prevDate = lastPosDate;
                        PositionDto pos = PositionFacade.GetInstance().FindPositionByDate(date, idActive);
                        status = PositionUtils.GetActiveStatus(idActive, pos.Date);
                        row.Status = status;
                        row.StatusPath = GetReportStatusIconPath(pos, status, false);
                        PositionDto prevPos = PositionFacade.GetInstance().FindPositionByDate(prevDate, idActive);

                        //Comprobamos si dos posiciones seguidas son parked
                        if (prevStatus.Equals("parked") && status.Equals("parked"))
                        {
                            //Calculamos distancia entre ellas
                            double dis = Utils.DistanceTo(prevPos.Latitude, prevPos.Longitude, pos.Latitude, pos.Longitude);
                            //Si es dispositivo movil y la distancia es mayor de la establecida -> no agrupamos
                            if (dis > Int32.Parse(ConfigurationManager.AppSettings["groupDistance"]))
                            {
                                prevRow.EndTime = pos.Date.ToString();
                                prevRow.DiffTime = Utils.ConvertToLclTime(DateTime.Parse(prevRow.EndTime)).Subtract(Utils.ConvertToLclTime(DateTime.Parse(prevRow.StartTime))).ToString();
                                prevRow.Status = prevStatus;
                                row.StartTime = pos.Date.ToString();
                                lastPosDate = pos.Date;
                            }
                            //Si no es dispositivo movil, o lo es pero no supera distancia entre posiciones -> Agrupamos
                            else
                            {
                                prevRow.Latitude = row.Latitude;
                                prevRow.Longitude = row.Longitude;
                                prevRow.EndTime = pos.Date.ToString();
                                lastPosDate = pos.Date;
                                data.RemoveAt(i);
                                i--;
                            }
                        }
                        //Si las posiciones son movimiento -> No hacemos nada (calculamos diferencias, etc etc)
                        if (prevStatus.Equals("moving") && status.Equals("moving"))
                        {
                            prevRow.EndTime = pos.Date.ToString();
                            row.Status = status;
                            row.StartTime = row.Date.ToString();
                            lastPosDate = pos.Date;
                            row.StatusPath = GetReportStatusIconPath(pos, status, false);

                        }
                        //Si son dos posiciones CellId consecutivas
                        if (prevStatus.Equals("cellid") && status.Equals("cellid"))
                        {
                            //Si la posicion es la misma -> Agrupamos
                            if (pos.Latitude.Equals(prevPos.Latitude) && pos.Longitude.Equals(prevPos.Longitude))
                            {
                                prevRow.EndTime = pos.Date.ToString();
                                lastPosDate = pos.Date;
                                data.RemoveAt(i);
                                i--;
                            }
                            //Si no no hacemos nada
                            else
                            {
                                prevRow.EndTime = pos.Date.ToString();
                                prevRow.Status = prevStatus;
                                row.StartTime = pos.Date.ToString();
                                lastPosDate = pos.Date;
                            }
                        }
                        //Si las dos posiciones son parados
                        if (prevStatus.Equals("stopped") && status.Equals("stopped"))
                        {
                            prevRow.EndTime = pos.Date.ToString();
                            prevRow.Status = prevStatus;
                            row.StartTime = pos.Date.ToString();
                            lastPosDate = pos.Date;
                        }
                        //Si no coinciden los estados
                        if (!prevStatus.Equals(status))
                        {
                            prevRow.EndTime = pos.Date.ToString();
                            prevRow.Status = prevStatus;
                            row.StartTime = pos.Date.ToString();
                            lastPosDate = pos.Date;
                        }
                        if (i + 1 == data.Count)
                        {
                            row.EndTime = lastDate.ToString();
                        }
                    }
                    else //First Position
                    {
                        firstPosDate = row.Date;
                        lastPosDate = row.Date;
                        PositionDto firstPos = PositionFacade.GetInstance().FindPositionByDate(firstPosDate, idActive);
                        row.StartTime = firstPosDate.ToString();
                        status = PositionUtils.GetActiveStatus(idActive, firstPosDate);
                        row.StatusPath = GetReportStatusIconPath(firstPos, status, false);
                        row.Status = status;
                        if (i + 1 == data.Count)
                        {
                            row.EndTime = lastDate.ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {}
            return data;
        }

        /// <summary>
        /// Gets status icon path to show into monitoring reports
        /// </summary>
        /// <param name="pos">position dto</param>
        /// <param name="status">active status</param>
        /// <param name="isLastPos">true if position is last position, false otherwise</param>
        /// <returns>status icon</returns>
        public static string GetReportStatusIconPath(PositionDto pos, string status, bool isLastPos)
        {
            string path = "";
            if (!pos.IsCellID)
            {
                if (status.Equals("parked"))
                {
                    path = "images/icons/16x16/parking.png";
                }
                if (status.Equals("stopped"))
                {
                    path = "images/icons/16x16/stopped.png";
                }
                if (status.Equals("crane"))
                {
                    path = "images/icons/16x16/alert.png";
                }
                if (status.Equals("moving"))
                {
                    OrderedDictionary addInfo = PositionFacade.GetInstance().GetAllAdditionalInformationByDate(pos.Date, pos.IdActive);

                    if (addInfo.Contains("direction"))
                    {
                        double direction = Double.Parse(addInfo["direction"].ToString().Replace('.', ','), new CultureInfo("es-ES"));
                        if (direction <= 22)
                        {
                            path = "images/icons/16x16/moving_000.png";
                        }
                        if (direction > 22 && direction <= 67)
                        {
                            path = "images/icons/16x16/moving_045.png";
                        }
                        if (direction > 67 && direction <= 112)
                        {
                            path = "images/icons/16x16/moving_090.png";
                        }
                        if (direction > 112 && direction <= 157)
                        {
                            path = "images/icons/16x16/moving_135.png";
                        }
                        if (direction > 157 && direction <= 202)
                        {
                            path = "images/icons/16x16/moving_180.png";
                        }
                        if (direction > 202 && direction <= 247)
                        {
                            path = "images/icons/16x16/moving_225.png";
                        }
                        if (direction > 247 && direction <= 292)
                        {
                            path = "images/icons/16x16/moving_270.png";
                        }
                        if (direction > 292)
                        {
                            path = "images/icons/16x16/moving_315.png";
                        }
                    }
                    else
                    {
                        path = "images/icons/16x16/moving_000.png";
                    }
                }
                if (!status.Equals("parked") & !status.Equals("stopped") & !status.Equals("moving") & !status.Equals("crane"))
                {
                    path = "images/icons/16x16/questionmark.png";
                }
            }
            else
            {
                path = "images/icons/16x16/antenna.png";
            }
            return path;
        }

        /// <summary>
        /// Applies restrictions to the positions list
        /// </summary>
        /// <param name="pos">Positions list</param>
        /// <returns>Data with restrictions applied</returns>
        public static ArrayList ApplyRestrictions(ArrayList pos)
        {
            try
            {
                for (int j = 0; j < pos.Count; j++)
                {
                    MonitoringRoutesRowDto row = (MonitoringRoutesRowDto)pos[j];

                    if (row.Status.Equals("stopped"))
                    {
                        if (!String.IsNullOrEmpty(row.Mileage) && !row.Mileage.Equals("0"))
                        {
                            row.StatusPath = GetReportStatusIconPath("moving");
                            row.Status = "moving";
                        }
                    }
                }
            }
            catch (Exception e)
            {
            }
            return pos;
        }

        /// <summary>
        /// Gets status icon path to show into monitoring reports
        /// </summary>
        /// <param name="status">the status</param>
        /// <returns>status icon</returns>
        public static string GetReportStatusIconPath(string status)
        {
            string path = "";
            if (status.Equals("parked"))
            {
                path = "images/icons/16x16/parking.png";
            }
            if (status.Equals("stopped"))
            {
                path = "images/icons/16x16/stopped.png";
            }
            if (status.Equals("moving"))
            {
                path = "images/icons/16x16/moving_000.png";
            }
            if (status.Equals("crane"))
            {
                path = "images/icons/16x16/alert.png";
            }
            if (status.Equals("cellid"))
            {
                path = "images/icons/16x16/antenna.png";
            }
            if (!status.Equals("parked") & !status.Equals("stopped") & !status.Equals("crane") & !status.Equals("moving") & !status.Equals("cellid"))
            {
                path = "images/icons/16x16/questionmark.png";
            }
            return path;
        }
    }
}
