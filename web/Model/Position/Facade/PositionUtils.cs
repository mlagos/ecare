using System;
using System.Collections;
using System.Data.SqlClient;
using System.Collections.Specialized;
using Nextgal.ECare.Model.Assisted.Dto;
using Nextgal.ECare.Model.Assisted.Facade;
using Nextgal.ECare.Model.Position.Dto;
using Nextgal.PocketLocator.Model.Position.Dto;

namespace Nextgal.ECare.Model.Position.Facade
{
    public class PositionUtils
    {
        /// <summary>
        /// Obtains the active status of one position
        /// </summary>
        /// <param name="idActive">active´s identifier</param>
        /// <param name="date">date an time of position</param>
        /// <returns>active status</returns>
        public static string GetActiveStatus(int idActive, DateTime date)
        {
            PositionDto pos = PositionFacade.GetInstance().FindPositionByDate(date, idActive);
            if (pos.IsCellID)
            {
                return "cellid";
            }
            string status = "";
            OrderedDictionary addInfo = PositionFacade.GetInstance().GetAllAdditionalInformationByDate(date, idActive);
            
                //Nuevo calculo de estados en base a las posiciones anteriores y siguientes (22/12/2009)
                #region mobile device

            if (addInfo.Contains("speed"))
            {
                if (Double.Parse(addInfo["speed"].ToString().Replace('.', ',')) < 15)
                {
                    if (ExistMovementPositionInTime(-10, idActive, date, 15))
                        status = "stopped";
                    else
                        status = "parked";
                }
                else
                {
                    status = "moving";
                }

                //Comprobamos si existe alguna posicion en los 10 minutos siguientes
                //si no existe -> pasamos cualquier estado a aparcado
                if (DateTime.Now.ToUniversalTime().Subtract(date).TotalMinutes > 10)
                {
                    if (!ExistPositionsInTime(10, idActive, date))
                        status = "parked";
                }
            }

            #endregion
            
            
            if (String.IsNullOrEmpty(status))
            {
                status = "unknown";
            }

            return status;
        }

        /// <summary>
        /// Check if exist an assisted's movement position in "minutes"
        /// </summary>
        /// <param name="minutes">Minutes back from position to check</param>
        /// <param name="idActive">Assisted identifier</param>
        /// <param name="posTime">Position date and time</param>
        /// <param name="minSpeed">Minimum sp</param>
        /// <returns></returns>
        public static bool ExistMovementPositionInTime(int minutes, int idActive, DateTime posTime, int minSpeed)
        {
            DateTime startTime = posTime.AddMinutes(minutes);

            //Obtenemos posiciones en los minutos deseados
            ArrayList positions = PositionFacade.GetInstance().GetAllPositionsByDate(idActive, startTime, posTime);

            //Si no hay posiciones en los ultimos "minutos" se devuelve false
            if (positions.Count == 0)
                return false;
            foreach (PositionDto posDto in positions)
            {
                //Obtenemos facilities para cada posicion -> Si velocidad > X es movimiento -> True
                OrderedDictionary facilities = PositionFacade.GetInstance().GetAllAdditionalInformationByDate(posDto.Date, idActive);
                if (!String.IsNullOrEmpty(facilities["speed"].ToString()))
                {
                    if (Double.Parse(facilities["speed"].ToString().Replace('.', ',')) > minSpeed)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check if exist positions in "minutes" minutes
        /// </summary>
        /// <param name="minutes">Minutes from position to check</param>
        /// <param name="idActive">Assisted identifier</param>
        /// <param name="posTime">Position date and time</param>
        /// <returns></returns>
        public static bool ExistPositionsInTime(int minutes, int idActive, DateTime posTime)
        {
            DateTime startTime = posTime.AddMinutes(minutes);

            //Obtenemos posiciones en los minutos deseados
            ArrayList positions = PositionFacade.GetInstance().GetAllPositionsByDate(idActive, posTime.AddMilliseconds(2000), startTime);

            //Si no hay posiciones en los ultimos "minutos" se devuelve false
            if (positions.Count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Gets battery level icon
        /// </summary>
        /// <param name="batteryLevel">battery Level</param>
        /// <returns>battery level icon</returns>
        public static string GetBatteryLevelIcon(string batteryLevel)
        {
            string path = "";
            int battery = Int32.Parse(batteryLevel);

            if (battery < 5)
            {
                path = "images/IconosBateria32x32/battery_empty.png";
            }
            if (battery >= 5 && battery < 15)
            {
                path = "images/IconosBateria32x32/battery_1.png";
            }
            if (battery >= 15 && battery < 30)
            {
                path = "images/IconosBateria32x32/battery_2.png";
            }
            if (battery >= 30 && battery < 45)
            {
                path = "images/IconosBateria32x32/battery_3.png";
            }
            if (battery >= 45 && battery < 60)
            {
                path = "images/IconosBateria32x32/battery_4.png";
            }
            if (battery >= 60 && battery < 75)
            {
                path = "images/IconosBateria32x32/battery_5.png";
            }
            if (battery >= 75 && battery < 90)
            {
                path = "images/IconosBateria32x32/battery_6.png";
            }
            if (battery >= 90 && battery < 100)
            {
                path = "images/IconosBateria32x32/battery_7.png";
            }
            if (battery == 100)
            {
                path = "images/IconosBateria32x32/battery_full.png";
            }
            return path;
        }

        /// <summary>
        /// Gets battery level of mobile devices
        /// </summary>
        /// <param name="idActive">active identifiers</param>
        /// <param name="date">date and time</param>
        /// <returns>battery level</returns>
        public static string GetBatteryLevel(int idActive, DateTime date)
        {
            string batteryLevel = "";
            OrderedDictionary od = PositionFacade.GetInstance().GetAllAdditionalInformationByDate(date, idActive);

            if (od.Contains("batteryLevel"))
            {
                batteryLevel = od["batteryLevel"].ToString();
            }
            return batteryLevel;
        }

        ///// <summary>
        ///// Searches the last known position of the active with the identifier and date given </summary>
        ///// <param name="idActive">active´s identifier</param>
        ///// <returns>the position searched</returns>
        //public bool HasCellIdPositions(int idActive)
        //{
        //    try
        //    {
        //        ArrayList positions = PositionFacade.GetInstance().GetAllCellIdPositions(idActive);
        //        if (positions.Count > 0)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (SqlException e)
        //    {
        //        throw new InternalErrorException("PositionDao.HasCellIdPositions ", e);
        //    }
        //}

        ///// <summary>
        ///// Checks if a position has no data
        ///// </summary>
        ///// <param name="pos">position dto</param>
        ///// <param name="lastPos">last position of a route</param>
        ///// <returns>true if alarm status, false otherwise</returns>
        //public static bool NoDataPosition(PositionDto pos, PositionDto lastPos)
        //{
        //    try
        //    {
        //        PositionDto maxPosition = PositionFacade.GetInstance().FindLastPositionActive(pos.IdActive, true);
        //        ActiveDto activeDto = ActiveFacade.GetInstance().FindActiveById(pos.IdActive);
        //        GetActiveStatus(activeDto.IdActive, pos.Date);
        //        if (!activeDto.EngineConnected)
        //        {
        //            //if (maxPosition.Date <= lastPos.Date)
        //            //{
        //            //    if ((pos.Date.Equals(lastPos.Date) && ConvertToLclTime(pos.Date).AddHours(1) < DateTime.Now) ||
        //            //        (pos.Date.Equals(lastPos.Date) && !GetActiveStatus(pos.IdActive, pos.Date).Equals("parked") &&
        //            //         ConvertToLclTime(pos.Date).AddMinutes(10) < DateTime.Now))
        //            //    {
        //            //        return true;
        //            //    }
        //            //}
        //            return false;
        //        }
        //        else
        //        {
        //            if (!GetActiveStatus(pos.IdActive, pos.Date).Equals("parked"))
        //            {
        //                if (maxPosition.Date <= lastPos.Date)
        //                {
        //                    if ((pos.Date.Equals(lastPos.Date) && ConvertToLclTime(pos.Date).AddHours(1) < DateTime.Now) ||
        //                        (pos.Date.Equals(lastPos.Date) &&
        //                         !GetActiveStatus(pos.IdActive, pos.Date).Equals("parked") &&
        //                         ConvertToLclTime(pos.Date).AddMinutes(10) < DateTime.Now))
        //                    {
        //                        return true;
        //                    }
        //                }
        //            }
        //        }
        //        return false;
        //    }
        //    catch (InstanceNotFoundException ex)
        //    {
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// Convert a date GMT to local time
        ///// </summary>
        ///// <param name="date">Date to convert</param>
        ///// <returns>Local date time</returns>
        //public static DateTime ConvertToLclTime(DateTime date)
        //{
        //    DateTime dateGMT = DateTime.SpecifyKind(date, DateTimeKind.Utc);
        //    TimeZoneInfo localZone = TimeZoneInfo.Local;

        //    DateTime localDate = TimeZoneInfo.ConvertTime(dateGMT, localZone);

        //    return localDate;
        //}

        ///// <summary>
        ///// Show into InfoWindows device alarms 
        ///// </summary>
        ///// <param name="idActive">active´s identifier</param>
        ///// <param name="date">position date ant time</param>
        ///// <returns>ArrayList with alarm information</returns>
        //public static ArrayList ShowInfoWindowAlarm(int idActive, DateTime date)
        //{
        //    ArrayList alarms = new ArrayList();
        //    string model = ActiveFacade.GetInstance().FindDeviceModelByIdActive(idActive);
        //    OrderedDictionary addInfo = PositionFacade.GetInstance().GetAllAdditionalInformationByDate(date, idActive);
        //    if (model.Equals("roverV9"))
        //    {
        //        #region roverV9

        //        if (addInfo.Contains("statusCode"))
        //        {
        //            OrderedDictionary od = TranslateStatusCode(idActive, date);
        //            if (od.Contains("PanicButton") && od["PanicButton"].Equals("Pressed"))
        //            {
        //                alarms.Add("panicButtonPressed");
        //            }
        //            if (od.Contains("ExternalPowerFailure"))
        //            {
        //                alarms.Add("externalPowerFailure");
        //            }
        //            if (od.Contains("InputPowerAboveOrBelowSetValue"))
        //            {
        //                alarms.Add("inputPowerAboveOrBelowSetValue");
        //            }
        //        }

        //        #endregion
        //    }
        //    else
        //    {
        //        if (model.Equals("globalTrackG200X"))
        //        {
        //            if (addInfo.Contains("deviceSystemFlags"))
        //            {
        //                ArrayList powerLow = new ArrayList { "2", "3", "6", "7", "A", "B", "E", "F" };
        //                if (powerLow.Contains(addInfo["deviceSystemFlags"].ToString().Substring(3, 1)))
        //                {
        //                    alarms.Add("powerLow");
        //                }
        //            }
        //        }

        //        else
        //        {
        //            if (model.Equals("blackBerry"))
        //            {
        //                #region blackBerry

        //                #endregion
        //            }
        //            else
        //            {
        //                if (model.Equals("PDA"))
        //                {
        //                    #region PDA

        //                    #endregion
        //                }
        //            }
        //        }
        //    }

        //    return alarms;
        //}

        ///// <summary>
        ///// Checks if an active has alarm status
        ///// </summary>
        ///// <param name="idActive">active´s identifier</param>
        ///// <param name="date">position dateTime</param>
        ///// <returns>true if alarm status, false otherwise</returns>
        //public static bool isAlarmPosition(int idActive, DateTime date)
        //{
        //    bool isAlarm = false;
        //    string model = ActiveFacade.GetInstance().FindDeviceModelByIdActive(idActive);
        //    OrderedDictionary addInfo = PositionFacade.GetInstance().GetAllAdditionalInformationByDate(date, idActive);
        //    if (model.Equals("roverV9"))
        //    {
        //        #region roverV9

        //        if (addInfo.Contains("statusCode"))
        //        {
        //            OrderedDictionary od = PositionUtils.TranslateStatusCode(idActive, date);
        //            if (od.Contains("PanicButton") && od["PanicButton"].Equals("Pressed"))
        //            {
        //                isAlarm = true;
        //            }
        //            if (od.Contains("ExternalPowerFailure"))
        //            {
        //                isAlarm = true;
        //            }
        //        }

        //        #endregion
        //    }
        //    else
        //    {
        //        if (model.Equals("globalTrackG200X"))
        //        {
        //            if (addInfo.Contains("deviceSystemFlags"))
        //            {
        //                ArrayList powerLow = new ArrayList { "2", "3", "6", "7", "A", "B", "E", "F" };
        //                string ab = addInfo["deviceSystemFlags"].ToString().Substring(3, 1);
        //                if (powerLow.Contains(addInfo["deviceSystemFlags"].ToString().Substring(3, 1)))
        //                {
        //                    isAlarm = true;
        //                }
        //            }
        //        }

        //        else
        //        {
        //            if (model.Equals("blackBerry"))
        //            {
        //                #region blackBerry



        //                #endregion
        //            }
        //            else
        //            {
        //                if (model.Equals("PDA"))
        //                {
        //                    #region PDA

        //                    #endregion
        //                }
        //            }
        //        }
        //    }

        //    return isAlarm;
        //}

        ///// <summary>
        ///// Gets additional information to introduce at infoWindows in the map
        ///// </summary>
        ///// <param name="idActive">active´s id</param>
        ///// <param name="date">date</param>
        ///// <returns>Additional information to the info windows</returns>
        //public static OrderedDictionary GetInfoWindowAddInfo(int idActive, DateTime date)
        //{
        //    OrderedDictionary od = PositionFacade.GetInstance().GetAllAdditionalInformationByDate(date, idActive);
        //    od.Remove("idCode");
        //    od.Remove("status");
        //    od.Remove("versionNumber");
        //    od.Remove("dataSentReasonCode");
        //    od.Remove("resendData");
        //    od.Remove("ACK");
        //    od.Remove("speed");
        //    od.Remove("unitName");
        //    od.Remove("unitVersionNumber");
        //    od.Remove("statusCode");
        //    od.Remove("ADCValue0");
        //    od.Remove("mileage");


        //    if (od.Contains("online_sleepMode"))
        //        od["online_sleepMode"] = od["online_sleepMode"].Equals("1") ? "sleep" : "online";
        //    if (od.Contains("mileage"))
        //        od["mileage"] = od["mileage"] + " km";
        //    if (od.Contains("height"))
        //        od["height"] = od["height"] + " m";
        //    if (od.Contains("direction"))
        //        od["direction"] = od["direction"] + "º";
        //    if (od.Contains("externalAndBatteryPowerVoltage"))
        //    {
        //        char[] delim = { ',' };
        //        string[] voltages = od["externalAndBatteryPowerVoltage"].ToString().Split(delim);
        //        od.Add("externalPowerVoltage", voltages[0] + "v");
        //        od.Add("batteryPowerVoltage", voltages[1] + "v");
        //    }
        //    if (od.Contains("GPSDistanceCounter"))
        //        od["GPSDistanceCounter"] = od["GPSDistanceCounter"] + " km";

        //    if (od.Contains("inputPowerLevel"))
        //        od["inputPowerLevel"] = od["inputPowerLevel"] + "v";
        //    if (od.Contains("ADCValue0"))
        //        od["ADCValue0"] = od["ADCValue0"] + "v";

        //    od.Remove("externalAndBatteryPowerVoltage");

        //    return od;
        //}
    }
}