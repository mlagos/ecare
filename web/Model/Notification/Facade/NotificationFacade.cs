using System.Collections;
using Nextgal.ECare.Common.Exceptions;
using Nextgal.ECare.Model.Notification.Dao;
using Nextgal.ECare.Model.Notification.Dto;

namespace Nextgal.ECare.Model.Notification.Facade
{
    public class NotificationFacade
    {
        #region Notification

        /// <summary>
        /// Creates a new notification
        /// </summary>
        /// <param name="notificationDto">notification</param>
        /// <returns>the notification created</returns>
        public NotificationDto CreateNotification(NotificationDto notificationDto)
        {
            try
            {
                NotificationDao notificationDao = NotificationDao.GetInstance();
                return notificationDao.Create(notificationDto);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Updates a notification
        /// </summary>
        /// <param name = "notificationDto">notification to update</param>
        /// <returns>notification updated</returns>
        public NotificationDto UpdateNotification(NotificationDto notificationDto)
        {
            try
            {
                NotificationDao notificationDao = NotificationDao.GetInstance();
                return notificationDao.Update(notificationDto);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Deletes a notification
        /// </summary>
        ///<param name="idNotification">Notification´s identifier</param>
        public void DeleteNotification(int idNotification)
        {
            try
            {
                NotificationDao notificationDao = NotificationDao.GetInstance();
                notificationDao.Delete(idNotification);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Searches the assisted with the identifier given
        /// </summary>
       /// <param name="idNotification"></param>
        /// <returns>the assisted searched</returns>
        public NotificationDto FindNotificationByIdentifier(int idNotification)
        {
            try
            {
                NotificationDao notificationDao = NotificationDao.GetInstance();
                return notificationDao.FindByIdentifier(idNotification);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Gets all notifications
        /// </summary>
        /// <returns>an ArrayList with all notifications</returns>
        public ArrayList GetAllNotifications()
        {
            try
            {
                NotificationDao notificationDao = NotificationDao.GetInstance();
                return notificationDao.GetAll();
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Gets all notifications by idActive
        /// </summary>
        /// <returns>an ArrayList with all assisted</returns>
        public ArrayList GetAllByIdActive(int idActive)
        {
            try
            {
                NotificationDao notificationDao = NotificationDao.GetInstance();
                return notificationDao.GetAllByIdActive(idActive);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Gets all active notifications with user´s identifier given
        /// </summary>
        /// <param name="idActive">Active´s identifier</param>
        /// <param name="idUser">User´s identifier</param>
        /// <returns>List with notifications searched</returns>
        public ArrayList GetAllByIdActiveAndIdUser(int idActive, int idUser)
        {
            try
            {
                NotificationDao notificationDao = NotificationDao.GetInstance();
                return notificationDao.GetAllByIdActiveAndIdUser(idActive,idUser);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Gets all active notifications with user´s identifier given
        /// </summary>
        /// <param name="idActive">Active´s identifier</param>
        /// <param name="idUser">User´s identifier</param>
        /// <param name="idAlarmType">Alarm type´s identifier</param>
        /// <returns>List with notifications searched</returns>
        public ArrayList GetAllByIdActiveAndIdUser(int idActive, int idUser, int idAlarmType)
        {
            try
            {
                NotificationDao notificationDao = NotificationDao.GetInstance();
                return notificationDao.GetAllByIdActiveAndIdUser(idActive, idUser, idAlarmType);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Checks if it already exists a battery level alarm assigned to active
        /// </summary>
        /// <param name="idActive">Active´s identifier</param>
        /// <param name="idUser">User´s identifier</param>
        /// <returns>True if exists battery level alarm assigned</returns>
        public bool ExistsBatteryNotification(int idActive, int idUser)
        {
            try
            {
                NotificationDao notificationDao = NotificationDao.GetInstance();
                return notificationDao.ExistsBatteryNotification(idActive, idUser);
            }
            catch (InstanceNotFoundException me)
            {
                throw me;
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Checks if it already exists a zone alarm assigned to active
        /// </summary>
        /// <param name="idActive">Active´s identifier</param>
        /// <param name="idZone">Zone´s identifier</param>
        /// <returns>True if it already exists zone alarm</returns>
        public bool ExistsZoneNotification(int idActive, int idZone)
        {
            try
            {
                NotificationDao notificationDao = NotificationDao.GetInstance();
                return notificationDao.ExistsZoneNotification(idActive, idZone);
            }
            catch (InstanceNotFoundException me)
            {
                throw me;
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Checks if it already exists a zone alarm
        /// </summary>
        /// <param name="idZone">Zone´s identifier</param>
        /// <returns>True if it already exists zone alarm</returns>
        public bool ExistsZoneNotification(int idZone)
        {
            try
            {
                NotificationDao notificationDao = NotificationDao.GetInstance();
                return notificationDao.ExistsZoneNotification(idZone);
            }
            catch (InstanceNotFoundException me)
            {
                throw me;
            }
            catch (ModelException me)
            {
                throw me;
            }
        }


        #endregion

        private static NotificationFacade instance;

        public static NotificationFacade GetInstance()
        {
            if (instance == null)
            {
                instance = new NotificationFacade();
            }
            return instance;
        }
     }
}