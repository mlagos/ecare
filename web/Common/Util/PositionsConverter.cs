using System;
using System.Text.RegularExpressions;
using Nextgal.ECare.Common.Exceptions;

namespace Nextgal.ECare.Common.Util
{
    public static class PositionsConverter
    {
        /// <summary>
        /// Converts coordinates from NMEA to Decimal
        /// </summary>
        /// <param name="lat">latitude</param>
        /// <param name="lon">longitude</param>
        /// <param name="N_S">north/south</param>
        /// <param name="E_W">east/west</param>
        /// <returns>coordinates converted</returns>
        public static String[] NMEAtoDecimal(string lat, string lon, string N_S, string E_W)
        {
            if (!IsValidLatitudeNMEACoordinate(lat))
            {
                throw new Exception("Invalid value of latitude");
            }
            if (!IsValidLongitudeNMEACoordinate(lon))
            {
                throw new Exception("Invalid value of longitude");
            }
            lat = lat.Replace('.', ',');
            double lat_d = Double.Parse(lat);
            lon = lon.Replace('.', ',');
            double lon_d = Double.Parse(lon);
            double deg_lat = Math.Floor(lat_d/100);
            double deg_lon = Math.Floor(lon_d/100);
            double lat_pos = deg_lat + (lat_d - (deg_lat*100))/60;
            double lon_pos = deg_lon + (lon_d - (deg_lon*100))/60;

            if ((N_S != "S" && N_S != "N") || (E_W != "E" && E_W != "W"))
            {
                throw new Exception("Wrong indicators.Can not convert coordenates");
            }
            if (N_S == "S")
            {
                lat_pos = -lat_pos;
            }
            if (E_W == "W")
            {
                lon_pos = -lon_pos;
            }
            String[] coordenates = {lat_pos.ToString(), lon_pos.ToString()};
            return coordenates; 
        }

        /// <summary>
        /// Converts coordinates from Decimal to NMEA format
        /// </summary>
        /// <param name="lat">latitude</param>
        /// <param name="lon">longitude</param>
        /// <returns>coordinates converted</returns>
        public static String[] DecimaltoNMEA(string lat, string lon)
        {
            if (!IsValidLatitudeDecimalCoordinate(lat))
            {
                throw new Exception("Invalid value of latitude");
            }
            if (!IsValidLongitudeDecimalCoordinate(lon))
            {
                throw new Exception("Invalid value of longitude");
            }

            char[] delim = {','};
            string[] lat_s = lat.Split(delim);
            string[] lon_s = lon.Split(delim);
            double lat_min = Double.Parse((Math.Abs(Math.Round((Double.Parse(lat)-Double.Parse(lat_s[0]))*60, 4))).ToString());
            double lon_min = Double.Parse((Math.Abs(Math.Round((Double.Parse(lon)-Double.Parse(lon_s[0]))*60, 4))).ToString());
            string lat_min_s = lat_min.ToString();
            string lon_min_s = lon_min.ToString();
            if(lat_min < 10.0)
            {
                lat_min_s = "0" + lat_min;  
            }
            if(lon_min < 10.0)
            {
                lon_min_s = "0" + lon_min;
            }

            string latitude = Math.Abs(Double.Parse(lat_s[0])) + lat_min_s ;
            string longitude = Math.Abs(Double.Parse(lon_s[0])) + lon_min_s;

            char[] c = {'.',','};
            if(!latitude.Contains("."))
            {
                latitude = latitude + ".0";
            }
            if(!longitude.Contains("."))
            {
                longitude = longitude + ".0";
            }
            string[] latit = latitude.Split(c);
            string[] longit = longitude.Split(c);

            for (int i = latit[1].Length; i < 4; i++)
            {
                latit[1] = latit[1] + "0";
            }
            for (int i = longit[1].Length; i < 4; i++)
            {
                longit[1] = longit[1] + "0";
            }
            for (int i = latit[0].Length; i < 4; i++)
            {
                latit[0] = "0" + latit[0];
            }
            for (int i = longit[0].Length; i < 5; i++)
            {
                longit[0] = "0" + longit[0];
            }
            latitude = latit[0] + "." + latit[1];
            longitude = longit[0] + "." + longit[1];

            if (Double.Parse(lat) < 0)
            {
                latitude = latitude + "S";
            }
            else
            {
                latitude = latitude + "N";
            }
            if (Double.Parse(lon) <0)
            {
                longitude = longitude + "W";
            }
            else
            {
                longitude = longitude + "E";
            }
            String[] coordenates = { latitude, longitude };
            return coordenates;
        }
       
        /// <summary>
        /// Converts coordinates from NMEA to hexadecimal to GEO fencedarea Setup
        /// </summary>
        /// <param name="lat">latitude</param>
        /// <param name="lon">longitude</param>
        /// <param name="N_S">north/south</param>
        /// <param name="E_W">east/west</param>
        /// <returns>coordinates converted</returns>
        public static String[] NMEAtoHexadecimal(string lat, string lon, string N_S, string E_W)
        {
            if (!IsValidLatitudeNMEACoordinate(lat))
            {
                throw new WrongParameterValueException("YouMustUseNMEAFormat_ex_1234_5678", "lat");
            }
            if (!IsValidLongitudeNMEACoordinate(lon))
            {
                throw new WrongParameterValueException("YouMustUseNMEAFormat_ex_12345_6789", "lon");
            }
            if (N_S != "S" && N_S != "N") 
            {
                throw new WrongParameterValueException("Type_N_ToNorthOr_S_ToSouth", "N_S");
            }
            if (E_W != "E" && E_W != "W")
            {
                throw new WrongParameterValueException("Type_E_ToEastOr_W_ToWest", "E_W");
            }
            try
            {

                // To hold our converted unsigned integer32 value
                uint lat_deg = UInt32.Parse(lat.Substring(0, 2));
                uint lat_min = UInt32.Parse(lat.Substring(2, 2));
                uint lat_sec = UInt32.Parse(lat.Substring(5, 2));
                uint lon_deg = UInt32.Parse(lon.Substring(0, 3));
                uint lon_min = UInt32.Parse(lon.Substring(3, 2));
                uint lon_sec = UInt32.Parse(lon.Substring(6, 2));

                // Format unsigned integer value to hex 
                string lat_deg_hex = String.Format("{0:x2}", lat_deg).ToUpper();
                string lat_min_hex = String.Format("{0:x2}", lat_min).ToUpper();
                string lat_sec_hex = String.Format("{0:x2}", lat_sec).ToUpper();
                string lon_deg_hex = String.Format("{0:x2}", lon_deg).ToUpper();
                string lon_min_hex = String.Format("{0:x2}", lon_min).ToUpper();
                string lon_sec_hex = String.Format("{0:x2}", lon_sec).ToUpper();

                string latitude = lat_deg_hex + lat_min_hex + lat_sec_hex + N_S;
                string longitude = lon_deg_hex + lon_min_hex + lon_sec_hex + E_W;
                String[] coordenates = { latitude, longitude };
                return coordenates;
            }
            catch (WrongParameterValueException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Converts a latitude or longitude in degrees, minutes and seconds to decimal coordenate
        /// </summary>
        /// <returns>decimal coordenate</returns>
        public static string DMStoDecimal(string DMS)
        {
            char[] delim = {'+','?', '"', '\'', '-'};
            String[] dms = DMS.Split(delim);
            double deg = Double.Parse(dms[1]);
            double min = Double.Parse(dms[2]);
            double sec = Double.Parse(dms[3]+dms[4].Replace('.',','));
            string coord = (deg + (min/60) + (sec/3600)).ToString();
            if (DMS.Substring(0,1).Equals("-"))
            {
                coord = "-" + coord;
            }
            return coord;
        }

        /// <summary>
        /// Converts a latitude or longitude in degrees, minutes and seconds to decimal coordenate
        /// </summary>
        /// <param name="degrees">degrees value</param>
        /// <param name="minutes">minutes value</param>
        /// <param name="seconds">seconds value</param>
        /// <returns>decimal coordenate without sign</returns>
        public static string DMStoDecimal(string degrees, string minutes, string seconds)
        {
            double deg = Double.Parse(degrees);
            double min = Double.Parse(minutes);
            double sec = Double.Parse(seconds);
            string coord = (deg + (min / 60) + (sec / 3600)).ToString();
            return coord;
        }

        /// <summary>
        /// Checks if a latitude with NMEA format is a valid coordenate
        /// </summary>
        /// <param name="latitude">latitude value</param>
        /// <returns>true if valid, false otherwise</returns>
        public static bool IsValidLatitudeNMEACoordinate (string latitude)
        {
            bool valid = true;
            const string latPattern = "^\\d{4}.\\d{4}";
            if (!Regex.IsMatch(latitude, latPattern))
            {
                valid = false;
            }
            else
            {
                latitude = latitude.Replace('.', ',');
                double lat_d = double.Parse(latitude);
                double deg_lat = Math.Floor(lat_d/100);
                double lat_pos = deg_lat + (lat_d - (deg_lat*100))/60;
                if (lat_pos > 90.0)
                {
                    throw new WrongParameterValueException("InvalidValueOfLatitude", "lat");
                }
            }
            return valid;
        }

        /// <summary>
        /// Checks if a latitude with NMEA format is a valid coordenate
        /// </summary>
        /// <param name="longitude">longitude value</param>
        /// <returns>true if valid, false otherwise</returns>
        public static bool IsValidLongitudeNMEACoordinate(string longitude)
        {
            bool valid = true;
            const string lonPattern = "^\\d{5}.\\d{4}";
            if (!Regex.IsMatch(longitude, lonPattern))
            {
                valid = false;
            }
            else
            {
                longitude = longitude.Replace('.', ',');
                double lon_d = double.Parse(longitude);
                double deg_lon = Math.Floor(lon_d/100);
                double lon_pos = deg_lon + (lon_d - (deg_lon*100))/60;
                if (lon_pos > 180.0)
                {
                    throw new WrongParameterValueException("InvalidValueOfLongitude", "lon");
                }
            }
            return valid;
        }

        /// <summary>
        /// Checks if a latitude in decimal format is a valid coordenate
        /// </summary>
        /// <param name="latitude">latitude value</param>
        /// <returns>true if valid, false otherwise</returns>
        public static bool IsValidLatitudeDecimalCoordinate(string latitude)
        {
            bool valid = true;
            double lat_d = Double.Parse(latitude);

            if ((lat_d > 90.0) || (lat_d < -90.0))
            {
                valid = false;
            }
            return valid;
        }

        /// <summary>
        /// Checks if a longitude with Decimal format is a valid coordenate
        /// </summary>
        /// <param name="longitude">longitude value</param>
        /// <returns>true if valid, false otherwise</returns>
        public static bool IsValidLongitudeDecimalCoordinate(string longitude)
        {
            bool valid = true;
            double lon_d = Double.Parse(longitude);

            if ((lon_d > 180.0) || (lon_d < -180.0))
            {
                valid = false;
            }
            
            return valid;
        }

        /// <summary>
        /// Checks if time value has a coorect format
        /// </summary>
        /// <param name="time">time value</param>
        /// <returns>true if correct, false otherwise</returns>
        public static bool IsValidTimeValue(string time)
        {
            bool valid = true;

            const string latPattern = "^(0[0-9]|1\\d|2[0-3]):([0-5]\\d):([0-5]\\d)$";
            if (!Regex.IsMatch(time, latPattern))
            {
                valid = false;
            }
            return valid;
        }

        /// <summary>
        /// Converts an ASCII string into hexadecimal value
        /// </summary>
        /// <param name="asciiString">ASCII string</param>
        /// <returns>string converted into hexadecimal value</returns>
        public static string ConvertToHex(string asciiString)
        {
            string hex = "";
            hex += String.Format("{0:x2}", Convert.ToUInt32(asciiString));
            hex = hex.ToUpper();
            return hex;
        }
    }
}