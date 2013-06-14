using System;
using Nextgal.ECare.Model.Notification.Dto;

namespace Nextgal.ECare.Gateway
{
    public class NotificationFactory
    {
        private static NotificationFactory instance;

        private NotificationFactory() { }

        /// <summary>
        /// Unique instance
        /// </summary>
        /// <returns>this unique instance</returns>
        public static NotificationFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new NotificationFactory();
            }
            return instance;
        }

        public void NotifyAlarm(NotificationDto notification, string text)
        {
            try
            {
                if (notification.Sms)
                {
                    throw new NotImplementedException("SMS notification not implemented.");
                }
                if (notification.Email)
                {
                    SendEmail.GetInstance().NotifyAlarm(notification, text);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}