using System;
using Subgurim.Controles;
using Nextgal.ECare.Common.Util;
using System.Collections.Generic;
using Nextgal.ECare.Model.User.Dto;
using Nextgal.ECare.Model.User.Facade;
using Nextgal.ECare.Model.Position.Dto;
using Nextgal.ECare.Model.Assisted.Dto;
using Nextgal.ECare.Model.Assisted.Facade;
using Nextgal.ECare.Model.Position.Facade;
using Nextgal.ECare.Model.Notification.Dto;

namespace Nextgal.ECare.Gateway.Alarms
{
    public class InZoneAlarm:AbstractAlarm
    {
        private NotificationDto notificationDto;

        public InZoneAlarm(NotificationDto notificationDto)
        {
            this.notificationDto = notificationDto;
        }

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

        public override bool isActiveAlarm(PositionDto positionDto)
        {
            try
            {
                GLatLng point = new GLatLng(positionDto.Latitude, positionDto.Longitude);
                ZoneDto zoneDto = UserFacade.GetInstance().FindZoneById((int) notificationDto.IdZone);
                List<GLatLng> points = new List<GLatLng>();
                char[] delim = {'#'};
                char[] delim2 = {';'};
                String[] coordinates = zoneDto.Position.Split(delim);
                for (int i = 0; i < coordinates.Length - 1; i++)
                {
                    String[] latLng = coordinates[i].Split(delim2);
                    GLatLng gLatLng = new GLatLng(Double.Parse(latLng[0].Replace('.', ',')), Double.Parse(latLng[1].Replace('.', ',')));
                    points.Add(gLatLng);
                }
                PositionDto prevPos = PositionFacade.GetInstance().GetPreviousPosition(positionDto.IdActive, positionDto.Date, true);
                GLatLng prevPoint = new GLatLng(prevPos.Latitude, prevPos.Longitude);

                if (Utils.pointInPolygon(point, points) && !Utils.pointInPolygon(prevPoint, points))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        public override void HandleAlarm(PositionDto positionDto)
        {
            try
            {
                AssistedDto assisted = AssistedFacade.GetInstance().FindAssistedByIdActive(notificationDto.IdActive);
                ZoneDto zoneDto = UserFacade.GetInstance().FindZoneById((int) notificationDto.IdZone);
                string text = assisted.Name + " " + assisted.Surname + " ha entrado en la zona: " + zoneDto.Name;
                NotificationFactory.GetInstance().NotifyAlarm(notificationDto, text);
            }
            catch (Exception ex)
            {
            }
        }

        public override void SaveAlarm(PositionDto positionDto)
        {
            throw new NotImplementedException();
        }
    }
}