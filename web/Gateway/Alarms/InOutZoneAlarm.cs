using System;
using Nextgal.ECare.Model.User.Dto;
using Nextgal.ECare.Model.User.Facade;
using Nextgal.ECare.Model.Position.Dto;
using Nextgal.ECare.Model.Assisted.Dto;
using Nextgal.ECare.Model.Assisted.Facade;
using Nextgal.ECare.Model.Notification.Dto;

namespace Nextgal.ECare.Gateway.Alarms
{
    public class InOutZoneAlarm : AbstractAlarm
    {
        private NotificationDto notificationDto;
        private bool inZone;
        private bool outZone;

        public InOutZoneAlarm(NotificationDto notificationDto)
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
                InZoneAlarm inAlarm = new InZoneAlarm(notificationDto);
                OutZoneAlarm outAlarm = new OutZoneAlarm(notificationDto);
                inZone = inAlarm.isActiveAlarm(positionDto);
                outZone = outAlarm.isActiveAlarm(positionDto);
                if (inZone || outZone)
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
                string text = "Fecha evento: " + positionDto.Date + "\n";
                if (inZone)
                    text += assisted.Name + " " + assisted.Surname + " ha entrado en la zona: " + zoneDto.Name;
                if (outZone)
                    text += assisted.Name + " " + assisted.Surname + " ha salido de la zona: " + zoneDto.Name;
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
