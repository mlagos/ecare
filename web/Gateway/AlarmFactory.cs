using System;
using System.Configuration;
using Nextgal.ECare.Gateway.Alarms;
using Nextgal.ECare.Model.Notification.Dto;

namespace Nextgal.ECare.Gateway
{
    public class AlarmFactory
    {
        public static AbstractAlarm GetInstance(NotificationDto notificationDto)
        {
            try
            {
                if (notificationDto.InZone && notificationDto.OutZone)
                {
                    return new InOutZoneAlarm(notificationDto);
                }
                if (notificationDto.InZone)
                {
                    return new InZoneAlarm(notificationDto);
                }
                if (notificationDto.OutZone)
                {
                    return new OutZoneAlarm(notificationDto);
                }
                string batteryLevel = ConfigurationManager.AppSettings["batteryLevelAlarm"];
                if (notificationDto.IdAlarmType.ToString().Equals(batteryLevel))
                {
                    return new LowBatteryLevelAlarm(notificationDto);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }
    }
}