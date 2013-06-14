using System;
using System.Configuration;
using Nextgal.ECare.Common.Util;
using Nextgal.ECare.Model.Notification.Dto;
using Nextgal.ECare.Model.User.Dto;
using Nextgal.ECare.Model.User.Facade;

namespace Nextgal.ECare.Gateway
{
    public class SendEmail : INotificationStrategy
    {
        private static SendEmail instance;

        /// <summary>
        /// Unique instance
        /// </summary>
        /// <returns>this unique instance</returns>
        public static SendEmail GetInstance()
        {
            if (instance == null)
            {
                instance = new SendEmail();
            }
            return instance;
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        private SendEmail()
        {}

        /// <summary>
        /// Notify alarm occurrence to a target user
        /// </summary>
        /// <param name="notification">alarm notification</param>
        /// <param name="text">Notification text</param>
        public void NotifyAlarm(NotificationDto notification, string text)
        {
            try
            {
                UserDto userDto = UserFacade.GetInstance().FindUser(notification.IdUser);
                MailUtils mailUtils = new MailUtils(ConfigurationManager.AppSettings["email_smtp_server"], ConfigurationManager.AppSettings["email_smtp_user"], ConfigurationManager.AppSettings["email_smtp_password_encrypted"], ConfigurationManager.AppSettings["email_from"]);
                if (!String.IsNullOrEmpty(userDto.Email))
                {
                    string emailTo = userDto.Email;
                    string subject = "Alarma Detectada";
                    mailUtils.SendMail(emailTo, subject, text);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}