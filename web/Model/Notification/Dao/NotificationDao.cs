using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Data.SqlClient;
using Nextgal.ECare.Common.Exceptions;
using Nextgal.ECare.Model.Notification.Dto;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Nextgal.ECare.Model.Notification.Dao
{
    public class NotificationDao
    {
        private const string m_CreateSp = "pa_insert_notification";
        private const string m_UpdateSp = "pa_update_notification";
        private const string m_DeleteSp = "pa_delete_notification";
        private Database db;

        private static NotificationDao instance;

        private NotificationDao()
        {
            db = DatabaseFactory.CreateDatabase();
        }

        public static NotificationDao GetInstance()
        {
            if (instance == null)
            {
                instance = new NotificationDao();
            }
            return instance;
        }

        /// <summary>
        /// Creates new Notification 
        /// </summary>
        /// <param name="NotificationDto">New notification</param>
        /// <returns>Notification created</returns>
        public NotificationDto Create(NotificationDto NotificationDto)
        {
            try
            {
                //Creamos un comando para ejecutar el procedimiento almacenado
                DbCommand dbCommand = db.GetStoredProcCommand(m_CreateSp);

                //Creamos los parámetros del procedimiento almacenado
                db.AddInParameter(dbCommand, "idActive", DbType.Int32, NotificationDto.IdActive);
                if (NotificationDto.IdZone == null)
                    db.AddInParameter(dbCommand, "idZone", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "idZone", DbType.Int32, NotificationDto.IdZone);
                db.AddInParameter(dbCommand, "idAlarmType", DbType.Int32, NotificationDto.IdAlarmType);
                db.AddInParameter(dbCommand, "idUser", DbType.Int32, NotificationDto.IdUser);
                db.AddInParameter(dbCommand, "inZone", DbType.Boolean, NotificationDto.InZone);
                db.AddInParameter(dbCommand, "outZone", DbType.Boolean, NotificationDto.OutZone);
                db.AddInParameter(dbCommand, "sms", DbType.Boolean, NotificationDto.Sms);
                db.AddInParameter(dbCommand, "mail", DbType.Boolean, NotificationDto.Email);

                db.ExecuteScalar(dbCommand);

                return NotificationDto;

            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    throw new DuplicateInstanceException(NotificationDto, "NotificationDao.Create");
                }
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Updates an existing notification
        /// </summary>
        /// <param name="NotificationDto">Notification</param>
        /// <returns>Notification updated</returns>
        public NotificationDto Update(NotificationDto NotificationDto)
        {
            try
            {
                //Creamos un comando para ejecutar el procedimiento almacenado
                DbCommand dbCommand = db.GetStoredProcCommand(m_UpdateSp);
                db.AddInParameter(dbCommand, "idNotification", DbType.Int32, NotificationDto.IdNotification);
                db.AddInParameter(dbCommand, "idActive", DbType.Int32, NotificationDto.IdActive);
                if (NotificationDto.IdZone == null)
                    db.AddInParameter(dbCommand, "idZone", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "idZone", DbType.Int32, NotificationDto.IdZone);
                db.AddInParameter(dbCommand, "idAlarmType", DbType.Int32, NotificationDto.IdAlarmType);
                db.AddInParameter(dbCommand, "idUser", DbType.Int32, NotificationDto.IdUser);
                db.AddInParameter(dbCommand, "inZone", DbType.Boolean, NotificationDto.InZone);
                db.AddInParameter(dbCommand, "outZone", DbType.Boolean, NotificationDto.OutZone);
                db.AddInParameter(dbCommand, "sms", DbType.Boolean, NotificationDto.Sms);
                db.AddInParameter(dbCommand, "mail", DbType.Boolean, NotificationDto.Email);

                int rowsAffected = db.ExecuteNonQuery(dbCommand);

                if (rowsAffected == 0)
                {
                    throw new InstanceNotFoundException(NotificationDto.IdZone, "NotificationDto");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("NotificationDao.Update", e);
            }

            return NotificationDto;
        }

        /// <summary>
        /// Deletes notification
        /// </summary>
        /// <param name="idNotification">Notification´s identifier</param>
        public void Delete(int idNotification)
        {
            try
            {
                //Creamos un comando para ejecutar el procedimiento almacenado
                DbCommand dbCommand = db.GetStoredProcCommand(m_DeleteSp);

                //Creamos los parámetros del procedimiento almacenado
                db.AddInParameter(dbCommand, "idNotification", DbType.Int32, idNotification);

                int rowsAffected = db.ExecuteNonQuery(dbCommand);

                if (rowsAffected == 0)
                {
                    throw new InstanceNotFoundException(idNotification, "NotificationDto");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("NotificationDao.Delete", e);
            }
        }

        /// <summary>
        /// Searches the notification with the identifier given
        /// </summary>
        /// <param name="idNotification">Notification´s identifier</param>
        /// <returns>Notification searched</returns>
        public NotificationDto FindByIdentifier(int idNotification)
        {
            try
            {
                //La consulta
                string sqlQuery = "SELECT idActive, idZone, idAlarmType, idUser, inZone, outZone, sms, mail " +
                                  "FROM Notification " +
                                  "WHERE idNotification = @idNotification";


                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "idNotification", DbType.Int32, idNotification);
                
                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        int i = 0;
                        int idActive = reader.GetInt32(i++);
                        int idZone = 0;
                        if (!reader.IsDBNull(i++))
                            idZone = reader.GetInt32(i - 1);
                        int idAlarmType = reader.GetInt32(i++);
                        int idUser = reader.GetInt32(i++);
                        bool inZone = reader.GetBoolean(i++);
                        bool outZone = reader.GetBoolean(i++);
                        bool sms = reader.GetBoolean(i++);
                        bool mail = reader.GetBoolean(i);
                        NotificationDto NotificationDto = new NotificationDto(idNotification, idActive, idZone, idAlarmType, idUser, inZone, outZone, sms, mail);
                        return NotificationDto;
                    }
                    throw new InstanceNotFoundException(idNotification, "NotificationDao.FindByIdentifier");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("NotificationDao.FindByIdentifier", e);
            }
        }

        /// <summary>
        /// Gets all notifications
        /// </summary>
        /// <returns>List with all notifications</returns>
        public ArrayList GetAll()
        {
            ArrayList notifications = new ArrayList();

            try
            {
                //La consulta
                string sqlQuery = "SELECT idNotification, idActive, idZone, idAlarmType, idUser, inZone, outZone, sms, mail " +
                                  "FROM Notification";

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        int i = 0;
                        int idNotification = reader.GetInt32(i++);
                        int idActive = reader.GetInt32(i++);
                        int idZone = 0;
                        if (!reader.IsDBNull(i++))
                            idZone = reader.GetInt32(i - 1);
                        int idAlarmType = reader.GetInt32(i++);
                        int idUser = reader.GetInt32(i++);
                        bool inZone = reader.GetBoolean(i++);
                        bool outZone = reader.GetBoolean(i++);
                        bool sms = reader.GetBoolean(i++);
                        bool mail = reader.GetBoolean(i);

                        NotificationDto NotificationDto = new NotificationDto(idNotification, idActive, idZone, idAlarmType, idUser, inZone, outZone, sms, mail);
                        notifications.Add(NotificationDto);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("NotificationsDao.GetAll", e);
            }
            return notifications;
        }

        /// <summary>
        /// Gets all active notifications
        /// </summary>
        /// <param name="idActive">Active´s identifier</param>
        /// <returns>List with notifications searched</returns>
        public ArrayList GetAllByIdActive(int idActive)
        {
            ArrayList notifications = new ArrayList();

            try
            {
                //La consulta
                string sqlQuery = "SELECT idNotification, idUser, idAlarmType, idZone, inZone, outZone, sms, mail " +
                                  "FROM View_Notifications " +
                                  "WHERE idActive = @idActive";

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "idActive", DbType.Int32, idActive);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        int i = 0;
                        int idNotification = reader.GetInt32(i++);
                        int idUser = reader.GetInt32(i++);
                        int idAlarmType = reader.GetInt32(i++);
                        int idZone = 0;
                        if (!reader.IsDBNull(i++))
                            idZone = reader.GetInt32(i - 1);
                        bool inZone = reader.GetBoolean(i++);
                        bool outZone = reader.GetBoolean(i++);
                        bool sms = reader.GetBoolean(i++);
                        bool mail = reader.GetBoolean(i);

                        NotificationDto NotificationDto = new NotificationDto(idNotification, idActive, idZone, idAlarmType, idUser, inZone, outZone, sms, mail);
                        notifications.Add(NotificationDto);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("NotificationsDao.GetAllByIdActiveAndIdUser", e);
            }
            return notifications;
        }

        /// <summary>
        /// Gets all active notifications with user´s identifier given
        /// </summary>
        /// <param name="idActive">Active´s identifier</param>
        /// <param name="idUser">User´s identifier</param>
        /// <returns>List with notifications searched</returns>
        public ArrayList GetAllByIdActiveAndIdUser(int idActive, int idUser)
        {
            ArrayList notifications = new ArrayList();

            try
            {
                //La consulta
                string sqlQuery = "SELECT idNotification, idZone, inZone, outZone, sms, mail, zoneName, alarmType " +
                                  "FROM View_Notifications " +
                                  "WHERE idActive = @idActive AND idUser = @idUser";

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "idActive", DbType.Int32, idActive);
                db.AddInParameter(cmd, "idUser", DbType.Int32, idUser);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        int i = 0;

                        int idNotification = reader.GetInt32(i++);
                        int idZone = 0;
                        if (!reader.IsDBNull(i++))
                            idZone = reader.GetInt32(i - 1);
                        bool inZone = reader.GetBoolean(i++);
                        bool outZone = reader.GetBoolean(i++);
                        bool sms = reader.GetBoolean(i++);
                        bool mail = reader.GetBoolean(i++);
                        string zoneName = "";
                        if (!reader.IsDBNull(i++))
                            zoneName = reader.GetString(i - 1);
                        string alarmType = reader.GetString(i);

                        NotificationDto NotificationDto = new NotificationDto(idNotification, idActive, idZone, idUser, inZone, outZone, sms, mail, zoneName, alarmType);
                        notifications.Add(NotificationDto);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("NotificationsDao.GetAllByIdActiveAndIdUser", e);
            }
            return notifications;
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
            ArrayList notifications = new ArrayList();

            try
            {
                //La consulta
                string sqlQuery = "SELECT idNotification, idZone, inZone, outZone, sms, mail, zoneName, alarmType " +
                                  "FROM View_Notifications " +
                                  "WHERE idActive = @idActive AND idUser = @idUser AND idAlarmType=@idAlarmType";

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "idActive", DbType.Int32, idActive);
                db.AddInParameter(cmd, "idUser", DbType.Int32, idUser);
                db.AddInParameter(cmd, "idAlarmType", DbType.Int32, idAlarmType);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        int i = 0;

                        int idNotification = reader.GetInt32(i++);
                        int idZone = 0;
                        if (!reader.IsDBNull(i++))
                            idZone = reader.GetInt32(i - 1);
                        bool inZone = reader.GetBoolean(i++);
                        bool outZone = reader.GetBoolean(i++);
                        bool sms = reader.GetBoolean(i++);
                        bool mail = reader.GetBoolean(i++);
                        string zoneName = "";
                        if (!reader.IsDBNull(i++))
                            zoneName = reader.GetString(i - 1);
                        string alarmType = reader.GetString(i);

                        NotificationDto NotificationDto = new NotificationDto(idNotification, idActive, idZone, idUser, inZone, outZone, sms, mail, zoneName, alarmType);
                        notifications.Add(NotificationDto);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("NotificationsDao.GetAllByIdActiveAndIdUser", e);
            }
            return notifications;
        }

        /// <summary>
        /// Checks if it already exists a battery level alarm asogned to active
        /// </summary>
        /// <param name="idActive">Active´s identifier</param>
        /// <param name="idUser">User´s identifier</param>
        /// <returns></returns>
        public bool ExistsBatteryNotification(int idActive, int idUser)
        {
            try
            {
                //La consulta
                string sqlQuery = "SELECT * FROM View_Notifications " +
                                  "WHERE idActive = @idActive AND alarmType='batteryLevelAlarm' AND idUser=@idUser";

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "idActive", DbType.Int32, idActive);
                db.AddInParameter(cmd, "idUser", DbType.Int32, idUser);
                
                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        return true;
                    }
                }
            }

            catch (SqlException e)
            {
                throw new InternalErrorException("PositionDao.ExistsBatteryNotification", e);
            }
            return false;
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
                //La consulta
                string sqlQuery = "SELECT * FROM View_Notifications " +
                                  "WHERE alarmType='zoneAlarm' and idZone=@idZone";

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "idZone", DbType.Int32, idZone);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        return true;
                    }
                }
            }

            catch (SqlException e)
            {
                throw new InternalErrorException("PositionDao.ExistsZoneNotification", e);
            }
            return false;
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
                //La consulta
                string sqlQuery = "SELECT * FROM View_Notifications " +
                                  "WHERE idActive = @idActive AND alarmType='zoneAlarm' and idZone=@idZone";

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "idActive", DbType.Int32, idActive);
                db.AddInParameter(cmd, "idZone", DbType.Int32, idZone);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        return true;
                    }
                }
            }

            catch (SqlException e)
            {
                throw new InternalErrorException("PositionDao.ExistsZoneNotification", e);
            }
            return false;
        }
    }
}