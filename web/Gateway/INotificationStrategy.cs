using Nextgal.ECare.Model.Notification.Dto;

namespace Nextgal.ECare.Gateway
{
    public interface INotificationStrategy
    {
        void NotifyAlarm(NotificationDto notification, string text);
    }
}