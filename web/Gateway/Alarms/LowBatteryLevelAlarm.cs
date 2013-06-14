using System;
using System.Configuration;
using System.Collections.Specialized;
using Nextgal.ECare.Model.Position.Dto;
using Nextgal.ECare.Model.Assisted.Dto;
using Nextgal.ECare.Model.Assisted.Facade;
using Nextgal.ECare.Model.Position.Facade;
using Nextgal.ECare.Model.Notification.Dto;

namespace Nextgal.ECare.Gateway.Alarms
{
    public class LowBatteryLevelAlarm:AbstractAlarm
    {
        private NotificationDto notificationDto;

        public LowBatteryLevelAlarm(NotificationDto notificationDto)
        {
            this.notificationDto = notificationDto;
        }

        /// <summary>
        /// Checks if alarm is active
        /// </summary>
        /// <returns>true if active, false otherwise</returns>
        public override void CheckAlarm(PositionDto positionDto)
        {
            try
            {
                if (isActiveAlarm(positionDto))
                {
                    HandleAlarm(positionDto);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Checks if alarm is active
        /// </summary>
        /// <returns>true if active, false otherwise</returns>
        public override bool isActiveAlarm(PositionDto positionDto)
        {
            try
            {
                PositionDto prevPos = PositionFacade.GetInstance().GetPreviousPosition(positionDto.IdActive, positionDto.Date, true);
                OrderedDictionary prevInfo = PositionFacade.GetInstance().GetAllAdditionalInformationByDate(prevPos.Date, positionDto.IdActive);
                OrderedDictionary addInfo = PositionFacade.GetInstance().GetAllAdditionalInformationByDate(positionDto.Date, positionDto.IdActive);
                if (addInfo.Contains("batteryLevel"))
                {
                    if (Double.Parse(addInfo["batteryLevel"].ToString()) <= Double.Parse(ConfigurationManager.AppSettings["lowBatteryLevel"]))
                    {
                        if (prevInfo.Contains("batteryLevel"))
                        {
                            if (Double.Parse(prevInfo["batteryLevel"].ToString()) > Double.Parse(ConfigurationManager.AppSettings["lowBatteryLevel"]))
                            {
                                return true;
                            }
                            return false;
                        }
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// Handles alarm with actions predefined
        /// </summary>
        public override void HandleAlarm(PositionDto positionDto)
        {
            try
            {
                AssistedDto assisted = AssistedFacade.GetInstance().FindAssistedByIdActive(notificationDto.IdActive);
                string text = "Fecha alarma: "+ positionDto.Date+"\nEl dispositivo móvil de " + assisted.Name + " " + assisted.Surname + " tiene el nivel de batería bajo.";
                NotificationFactory.GetInstance().NotifyAlarm(notificationDto, text);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Saves notification of alarm into database
        /// </summary>
        public override void SaveAlarm(PositionDto positionDto)
        {
            throw new NotImplementedException();
        }
    }
}