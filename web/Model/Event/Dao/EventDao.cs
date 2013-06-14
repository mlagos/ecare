using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Data.Common;
using System.Data.SqlClient;
using Nextgal.ECare.Model.Event.Dto;
using Nextgal.ECare.Common.Exceptions;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Nextgal.ECare.Model.Event.Dao
{
    public class EventDao
    {
        private const string m_CreateSp = "pa_insert_event";
        private const string m_UpdateSp = "pa_update_event";
        private const string m_DeleteSp = "pa_delete_event";
        private Database db;

        private static EventDao instance;

        private EventDao()
        {
            db = DatabaseFactory.CreateDatabase();
        }

        /// <summary>
        /// Unique instance
        /// </summary>
        /// <returns>this unique instance</returns>
        public static EventDao GetInstance()
        {
            if (instance == null)
            {
                instance = new EventDao();
            }
            return instance;
        }

        /// <summary>
        /// Creates a new event
        /// </summary>
        /// <param name="eventDto">the event</param>
        /// <returns>the event created</returns>
        public EventDto Create(EventDto eventDto)
        {
            try
            {
                //Creamos un comando para ejecutar el procedimiento almacenado
                DbCommand dbCommand = db.GetStoredProcCommand(m_CreateSp);
                
                //Creamos los parámetros del procedimiento almacenado
                db.AddInParameter(dbCommand, "idActive", DbType.Int32, eventDto.IdActive);
                db.AddInParameter(dbCommand, "date", DbType.DateTime, eventDto.Date);
                db.AddInParameter(dbCommand, "text", DbType.String, eventDto.Text);
                if (eventDto.SentDate==DateTime.MinValue)
                    db.AddInParameter(dbCommand, "sentDate", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "sentDate", DbType.DateTime, eventDto.SentDate);
                
                int idActive = (int)db.ExecuteScalar(dbCommand);
                eventDto.IdActive = idActive;
               
                return eventDto;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    throw new DuplicateInstanceException(eventDto, "EventDao.Create");
                }
                throw e;

            }
            catch (Exception e)
            {
                throw e;
            }
           

        }

        public void Delete(int idEvent)
        {
            try
            {
                //Creamos un comando para ejecutar el procedimiento almacenado
                DbCommand dbCommand = db.GetStoredProcCommand(m_DeleteSp);

                //Creamos los parámetros del procedimiento almacenado
                db.AddInParameter(dbCommand, "idEvent", DbType.Int32, idEvent);

                int rowsAffected = db.ExecuteNonQuery(dbCommand);

                if (rowsAffected == 0)
                {
                    throw new InstanceNotFoundException(idEvent, "EventDto");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("EventDao.Delete", e);
            }
        }


        public ArrayList GetAllNoSentEvents()
        {
            ArrayList _event = new ArrayList();

            string sqlQuery = "SELECT idEvent, idActive, date, text, sentDate, phone " +
                  "FROM View_CustomEvent "+
                  "WHERE (sentDate IS NULL) AND (date < @date) "+
                  "ORDER BY date";

            //Creamos un comando para ejecutar la consulta sql 
            DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
            db.AddInParameter(cmd, "date", DbType.DateTime, DateTime.Now);
            //db.AddInParameter(cmd, "sentDate", DbType.DateTime, DBNull.Value);

            //The using statement ensures that the DbDataReader object is disposed, which 
            //closes the DbDataReader object.
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    int i = 0;

                    int idEvent = reader.GetInt32(i++);
                    int idActive = reader.GetInt32(i++);
                    DateTime date = reader.GetDateTime(i++);
                    string text = reader.GetString(i++);
                    DateTime sentDate = DateTime.MinValue;
                    if (!reader.IsDBNull(i++))
                        sentDate = reader.GetDateTime(i-1);
                    string phone = reader.GetString(i);

                    CustomEventDto eventDto = new CustomEventDto(idEvent, idActive, date, text, sentDate, phone);
                    _event.Add(eventDto);
                }
            }
            return _event;
        }

        public ArrayList GetAllNoSentEvents(int idActive)
        {
            ArrayList _event = new ArrayList();

            string sqlQuery = "SELECT idEvent, date, text, sentDate " +
                  "FROM Event " +
                  "WHERE (sentDate IS NULL) AND (idActive = @idActive) " +
                  "ORDER BY date";

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

                    int idEvent = reader.GetInt32(i++);
                    DateTime date = reader.GetDateTime(i++);
                    string text = reader.GetString(i++);
                    DateTime sentDate = DateTime.MinValue;
                    if (!reader.IsDBNull(i++))
                        sentDate = reader.GetDateTime(i - 1);

                    EventDto eventDto = new EventDto(idEvent, idActive, date, text, sentDate);
                    _event.Add(eventDto);
                }
            }
            return _event;
        }

        public EventDto Update(EventDto dto)
        {
            try
            {
                //Creamos un comando para ejecutar el procedimiento almacenado
                DbCommand dbCommand = db.GetStoredProcCommand(m_UpdateSp);
                db.AddInParameter(dbCommand, "idEvent", DbType.Int32, dto.IdEvent);
                db.AddInParameter(dbCommand, "idActive", DbType.Int32, dto.IdActive);
                db.AddInParameter(dbCommand, "date", DbType.DateTime, dto.Date);
                db.AddInParameter(dbCommand, "text", DbType.String, dto.Text);
                db.AddInParameter(dbCommand, "sentDate", DbType.DateTime, dto.SentDate);

                int rowsAffected = db.ExecuteNonQuery(dbCommand);

                if (rowsAffected == 0)
                {
                    throw new InstanceNotFoundException(dto.IdEvent, "EventDto");
                }

            }
            catch (SqlException e)
            {
                throw new InternalErrorException("EventDao.Update", e);
            }
            return dto;
        }

        /// <summary>
        /// Searches the event with the identifier given
        /// </summary>
        /// <param name="idEvent">event´s identifier</param>
        /// <returns>the event searched</returns>
        public EventDto FindByIdEvent(int idEvent)
        {
            try
            {
                //La consulta
                string sqlQuery = "SELECT idActive, date, text, sent "+
                                  "FROM Event " +
                                  "WHERE idEvent = @idEvent";


                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "idEvent", DbType.Int32, idEvent);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        int i = 0;

                        int idFamily = reader.GetInt32(i++);
                        DateTime date = reader.GetDateTime(i++);
                        string text = reader.GetString(i++);
                        DateTime sentDate = DateTime.MinValue;
                        if (!reader.IsDBNull(i++))
                        {
                            sentDate = reader.GetDateTime(i - 1);
                        }

                        EventDto eventDto = new EventDto(idEvent, idFamily, date, text, sentDate);
                        return eventDto;
                    }
                    throw new InstanceNotFoundException(idEvent, "EventDao.FindByIdEvent");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("EventDao.FindByIdEvent", e);
            }
        }

        /// <summary>
        /// Gets all event
        /// </summary>
        /// <returns>an ArrayList with all event</returns>
        public ArrayList GetAll()
        {
            ArrayList _event = new ArrayList();

            try
            {
                //La consulta
                string sqlQuery = "SELECT idEvent, idActive, date, text, sentDate " +
                                  "FROM Event ";
                                  
                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                
                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        int i = 0;

                        int idEvent = reader.GetInt32(i++);
                        int idActive = reader.GetInt32(i++);
                        DateTime date = reader.GetDateTime(i++);
                        string text = reader.GetString(i++);
                        DateTime sentDate = DateTime.MinValue;
                        if (!reader.IsDBNull(i++))
                        {
                            sentDate = reader.GetDateTime(i - 1);
                        }

                        EventDto eventDto = new EventDto(idEvent, idActive, date, text, sentDate);
                        _event.Add(eventDto);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("EventDao.GetAll", e);
            }
            return _event;
        }


        public List<EventDto> GetByIdActive(int idActive)
        {
            List<EventDto> _event = new List<EventDto>();

            try
            {
                //La consulta
                string sqlQuery = "SELECT idEvent, date, text, sentDate " +
                                  "FROM Event where idActive = @idActive";

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

                        int idEvent = reader.GetInt32(i++);
                        DateTime date = reader.GetDateTime(i++);
                        string text = reader.GetString(i++);
                        DateTime sentDate = DateTime.MinValue;
                        if (!reader.IsDBNull(i++))
                        {
                            sentDate = reader.GetDateTime(i - 1);
                        }

                        EventDto eventDto = new EventDto(idEvent, idActive, date, text, sentDate);
                        _event.Add(eventDto);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("EventDao.GetByIdActive", e);
            }
            return _event;
        }
    }
}
