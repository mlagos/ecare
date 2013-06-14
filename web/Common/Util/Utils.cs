using System;
using System.Data;
using System.Text;
using Subgurim.Controles;
using System.Collections;
using System.Collections.Generic;

namespace Nextgal.ECare.Common.Util
{
    public class Utils
    {
        /// <summary>
        /// Tranfers the data of an ArrayList to a DataTable
        /// </summary>
        /// <param name="alist">original arrayList</param>
        /// <returns>DataTable with arrayList data</returns>
        public static DataTable ToDataTable(ArrayList alist)
        {
            DataTable dt = new DataTable();
            if (alist[0] == null)
                throw new FormatException("Parameter ArrayList empty");
            dt.TableName = alist[0].GetType().Name;
            DataRow dr;
            System.Reflection.PropertyInfo[] propInfo = alist[0].GetType().GetProperties();
            for (int i = 0; i < propInfo.Length; i++)
            {
                dt.Columns.Add(propInfo[i].Name, propInfo[i].PropertyType);
            }
            for (int row = 0; row < alist.Count; row++)
            {
                dr = dt.NewRow();
                for (int i = 0; i < propInfo.Length; i++)
                {
                    object tempObject = alist[row];
                    object t = propInfo[i].GetValue(tempObject, null);
                    if (t != null)
                        dr[i] = t.ToString();
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// Creates a random password
        /// </summary>
        /// <param name="PasswordLength">longitud del password</param>
        /// <returns></returns>
        public static string CreateRandomPassword(int PasswordLength)
        {
            //string _allowedChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789!@$?";
            string _allowedChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ123456789";
            Byte[] randomBytes = new Byte[PasswordLength];
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < PasswordLength; i++)
            {
                Random randomObj = new Random();
                randomObj.NextBytes(randomBytes);
                chars[i] = _allowedChars[(int)randomBytes[i] % allowedCharCount];
            }
            return new string(chars);
        }

        /// <summary>
        /// Convert a date GMT to local time
        /// </summary>
        /// <param name="date">Date to convert</param>
        /// <returns>Local date time</returns>
        public static DateTime ConvertToLclTime(DateTime date)
        {
            DateTime dateGMT = DateTime.SpecifyKind(date, DateTimeKind.Utc);
            TimeZoneInfo localZone = TimeZoneInfo.Local;
            DateTime localDate = TimeZoneInfo.ConvertTime(dateGMT, localZone);

            return localDate;
        }

        /// <summary>
        /// Converts a local date to GMT time
        /// </summary>
        /// <param name="date">Date to convert</param>
        /// <returns>GMT date time</returns>
        public static DateTime ConvertToGMTTime(DateTime date)
        {
            DateTime localDate = DateTime.SpecifyKind(date, DateTimeKind.Local);
            TimeZoneInfo zonaGMT = TimeZoneInfo.Utc;
            DateTime dateGMT = TimeZoneInfo.ConvertTime(localDate, zonaGMT);

            return dateGMT;
        }

        /// <summary>
        /// Converts degrees to radians
        /// </summary>
        /// <param name="degrees">degrees value</param>
        /// <returns>radians value</returns>
        public static double DegToRad(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        /// <summary>
        /// Converts degrees to radians
        /// </summary>
        /// <param name="radians">radians value</param>
        /// <returns>radians value</returns>
        public static double RadToDeg(double radians)
        {
            return radians * 180 / Math.PI;
        }

        /// <summary>
        /// Obtains course direction between two points
        /// </summary>
        /// <param name="c1">first coordenate</param>
        /// <param name="c2">second coordenate</param>
        /// <returns>course direction</returns>
        public static double GetAzimuth(GLatLng c1, GLatLng c2)
        {
            var lat1 = DegToRad(c1.lat);
            var lon1 = DegToRad(c1.lng);
            var lat2 = DegToRad(c2.lat);
            var lon2 = DegToRad(c2.lng);

            double radians = Math.Asin((Math.Sin(lon1 - lon2) * Math.Cos(lat2)) / (Math.Sin(Math.Acos(Math.Sin(lat2) * Math.Sin(lat1) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(lon2 - lon1)))));
            return RadToDeg(radians);
        }

        /// <summary>
        /// Converts hexadecimal value into double
        /// </summary>
        /// <returns></returns>
        public static double GetDoubleByHexValue(string hex)
        {
            double d = 0;

            for (int n = hex.Length - 1; n >= 0; n--)
            {
                d += Uri.FromHex(hex[n]) * Math.Pow(16, hex.Length - 1 - n);
            }
            return d;
        }

        /// <summary>
        /// Calculates the distance between two GPS points, in meters.
        /// </summary>
        /// <param name="lat1">latitude of the first point</param>
        /// <param name="lon1">longitude of the first point</param>
        /// <param name="lat2">latitude of the second point</param>
        /// <param name="lon2">longitude of the second point</param>
        /// <returns>double</returns>
        public static double DistanceTo(double lat1, double lon1, double lat2, double lon2)
        {
            const double degrad = Math.PI / 180;
            const double R = 6367000; //Earth radius in meters.

            //Convert the degree values to radians before calculation
            lat1 = lat1 * degrad;
            lon1 = lon1 * degrad;
            lat2 = lat2 * degrad;
            lon2 = lon2 * degrad;

            double dlon = lon2 - lon1;
            double dlat = lat2 - lat1;

            double a = Math.Pow(Math.Sin(dlat / 2), 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(dlon / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return (R * c);
        }

        /// <summary>
        /// Convierte datos decimales a hexadecimales
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Asc2Hex(string input)
        {
            string hexOutput = "";
            char[] values = input.ToCharArray();
            foreach (char letter in values)
            {
                // Get the integral value of the character.
                int value = Convert.ToInt32(letter);
                // Convert the decimal value to a hexadecimal value in string form.
                hexOutput += String.Format("{0:X}", value);
            }
            return hexOutput;
        }

        /// <summary>
        /// Convierte datos hexadicimales en ascii (decimales)
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static string Hex2Asc(string Data)
        {
            string sData = "";
            while (Data.Length > 0)
                //first take two hex value using substring.
                //then convert Hex value into ascii.
                //then convert ascii value into character.
            {
                string Data1 = Convert.ToChar(Convert.ToUInt32(Data.Substring(0, 2), 16)).ToString();
                sData = sData + Data1;
                Data = Data.Substring(2, Data.Length - 2);
            }
            return sData;
        }

        public static string TurnHexadecimalByPairs(string hex)
        {
            string result = "";
            char[] chars = hex.ToCharArray();
            for (int i = 0; i < hex.Trim().Length; i++)
            {
                //OJO: Necesatio poner el ToString -> Si no no concatena
                if ((i != 0) && (i % 2 == 0))
                    result = chars[i - 2].ToString() + chars[i - 1].ToString() + result;
            }
            //Añadimos dos ultimos que perdemos por iteraciones
            return hex.Substring(hex.Length - 2, 2) + result;
        }

        public static double ConvertRadiansToDegrees(double radians)
        {
            double degrees = (180 / Math.PI) * radians;
            return (degrees);
        }

        public static string GetC2ByPairs(string hexValue)
        {
            string result = "";
            char[] chars = hexValue.ToCharArray();
            for (int i = 0; i < hexValue.Trim().Length; i++)
            {
                //OJO: Necesario poner el ToString -> Si no no concatena
                if ((i != 0) && (i % 2 == 0))
                    result += GetC2(chars[i - 2].ToString() + chars[i - 1].ToString());
            }
            //Añadimos dos ultimos que perdemos por iteraciones
            return result + GetC2(hexValue.Substring(hexValue.Length - 2, 2));
        }

        public static string GetC2(string hexValue)
        {
            return Convert.ToString((Convert.ToInt32(hexValue, 16) ^ Convert.ToInt32("FF", 16)), 16).PadLeft(2,'0');
        }

        public static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2); 
            foreach (byte b in ba)    
                hex.AppendFormat("{0:x2}", b); 
            return hex.ToString();
        }

        //  Globals which should be set before calling this function:
        //
        //  int    polySides  =  how many corners the polygon has
        //  float  polyX[]    =  horizontal coordinates of corners
        //  float  polyY[]    =  vertical coordinates of corners
        //  float  x, y       =  point to be tested
        //
        //  (Globals are used in this example for purposes of speed.  Change as
        //  desired.)
        //
        //  The function will return YES if the point x,y is inside the polygon, or
        //  NO if it is not.  If the point is exactly on the edge of the polygon,
        //  then the function may return YES or NO.
        //
        //  Note that division by zero is avoided because the division is protected
        //  by the "if" clause which surrounds it.
        public static bool pointInPolygon(GLatLng point, List<GLatLng>points)
        {
            int j = points.Count - 1;
            bool oddNodes = false;

            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].lng < point.lng && points[j].lng >= point.lng || 
                    points[j].lng < point.lng && points[i].lng >= point.lng)
                {
                    if (points[i].lat + (point.lng - points[i].lng) / (points[j].lng - points[i].lng) * (points[j].lat - points[i].lat) < point.lat)
                    {
                        oddNodes = !oddNodes;
                    }
                }
                j = i;
            }

            return oddNodes;
        }

        /// <summary>
        /// Converts hexadecimal string into decimal value
        /// </summary>
        /// <param name="hexString">Hex string</param>
        /// <returns>Decimal value</returns>
        public static string ConvertHex2Dec(string hexString)
        {
            try
            {
                return Int64.Parse(hexString, System.Globalization.NumberStyles.HexNumber).ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Converts Hexadecimal string to Binary string
        /// </summary>
        /// <param name="hexString">Hexadecimal value</param>
        /// <returns>Binary value</returns>
        public static string ConvertHex2Bin(string hexString)
        {
            int addBits = hexString.Length*4; 
            string binary = Convert.ToString(Convert.ToInt64(hexString, 16), 2);
            for (int i = binary.Length; i < addBits; i++)
            {
                binary = "0" + binary;
            }
            return binary;
        }

        /// <summary>
        /// Converts a string into 'numbase' format number
        /// </summary>
        /// <param name="sBase">Input string</param>
        /// <param name="numbase">Base number</param>
        /// <returns>Number into 'numbase' format</returns>
        public static int BaseToDecimal(string sBase, int numbase)
        {
            const int base10 = 10;
            var cHexa = new char[] { 'A', 'B', 'C', 'D', 'E', 'F' };
            var iHexaNumeric = new int[] { 10, 11, 12, 13, 14, 15 };
            
            const int asciiDiff = 48;
            int dec = 0;
            int b;
            int iProduct = 1;
            string sHexa = "";
            if (numbase > base10)
                for (int i = 0; i < cHexa.Length; i++)
                    sHexa += cHexa.GetValue(i).ToString();
            for (int i = sBase.Length - 1; i >= 0; i--, iProduct *= numbase)
            {
                string sValue = sBase[i].ToString();
                if (sValue.IndexOfAny(cHexa) >= 0)
                    b = iHexaNumeric[sHexa.IndexOf(sBase[i])];
                else
                    b = (int)sBase[i] - asciiDiff;
                dec += (b * iProduct);
            }
            return dec;
        }

        /// <summary>
        /// Converts hex string into IEEE 754 protocol value
        /// </summary>
        /// <param name="hexVal">Hexadecimal value</param>
        /// <returns>Single precision value</returns>
        public static string ConvertHex2Single(string hexVal)
        {
            try
            {
                int i;
                int j = 0;
                var bArray = new byte[4];

                for (i = 0; i <= hexVal.Length - 1; i += 2)
                {
                    bArray[j] = Byte.Parse(hexVal[i] + hexVal[i + 1].ToString(), System.Globalization.NumberStyles.HexNumber);
                    j += 1;
                }
                Array.Reverse(bArray);
                string s = BitConverter.ToSingle(bArray, 0).ToString();
                return(s);
            }
            catch (Exception ex)
            {
                throw new FormatException("The supplied hex value is either empty or in an incorrect format.  Use the following format: 00000000", ex);
            }
        }

        /// <summary>
        /// Calculates person age
        /// </summary>
        /// <param name="birthDate"></param>
        /// <returns>Person age</returns>
        public static int CalculateAge(DateTime birthDate)
        {
            DateTime fechaActual = DateTime.Now;
            int edad = fechaActual.Year - birthDate.Year;
            // Calcular si no ha cumplido aun 
            if (new DateTime(fechaActual.Year, birthDate.Month, birthDate.Day) > fechaActual)
            {
                edad--;
            }
            return edad;
        } 
    }
}
