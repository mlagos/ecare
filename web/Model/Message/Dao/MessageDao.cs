using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Nextgal.ECare.Common.Exceptions;
using Nextgal.ECare.Model.Message.Dto;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Nextgal.ECare.Model.Message.Dao
{
    public class MessageDao
    {
        private const string m_CreateSp = "pa_insert_message";
        private const string m_CreateSpR = "pa_insert_reply";
        private const string m_UpdateSp = "pa_update_message";
        
        private Database db;

        private static MessageDao instance;

        private MessageDao()
        {
            db = DatabaseFactory.CreateDatabase();
        }

        /// <summary>
        /// Unique instance
        /// </summary>
        /// <returns>this unique instance</returns>
        public static MessageDao GetInstance()
        {
            if (instance == null)
            {
                instance = new MessageDao();
            }
            return instance;
        }

        /// <summary>
        /// Creates a new message
        /// </summary>
        /// <param name="messageDto">the message</param>
        /// <returns>the message created</returns>
        public MessageDto Create(MessageDto messageDto)
        {
            try
            {
                //Creamos un comando para ejecutar el procedimiento almacenado
                DbCommand dbCommand = db.GetStoredProcCommand(m_CreateSp);
                
                //Creamos los parámetros del procedimiento almacenado
                db.AddInParameter(dbCommand, "idUser", DbType.Int32, messageDto.IdUser);
                db.AddInParameter(dbCommand, "idActive", DbType.Int32, messageDto.IdActive);
                db.AddInParameter(dbCommand, "dateSent", DbType.DateTime, messageDto.DateSent);
                if (messageDto.DateReceipt == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "dateReceipt", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "dateReceipt", DbType.DateTime, messageDto.DateReceipt);
                db.AddInParameter(dbCommand, "msgText", DbType.String, messageDto.MsgText);
                
                int idMessage = (int)db.ExecuteScalar(dbCommand);
                messageDto.IdMessage = idMessage;
               
                return messageDto;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    throw new DuplicateInstanceException(messageDto, "MessageDao.Create");
                }
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ReplyDto CreateReply(ReplyDto replyDto)
        {
            try
            {
                //Creamos un comando para ejecutar el procedimiento almacenado
                DbCommand dbCommand = db.GetStoredProcCommand(m_CreateSpR);

                //Creamos los parámetros del procedimiento almacenado
                db.AddInParameter(dbCommand, "idMessage", DbType.Int32, replyDto.IdMessage);
                db.AddInParameter(dbCommand, "date", DbType.DateTime, replyDto.Date);
                db.AddInParameter(dbCommand, "text", DbType.String, replyDto.Text);

                int idReply = (int)db.ExecuteScalar(dbCommand);
                replyDto.IdReply = idReply;

                return replyDto;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    throw new DuplicateInstanceException(replyDto, "MessageDao.CreateReply");
                }
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ReplyDto FindReplyByIdMesasage(int idMessage)
        {
            try
            {
                //La consulta
                string sqlQuery = "SELECT idReply, date, text " +
                                  "FROM Reply " +
                                  "WHERE idMessage = @idMessage";


                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "idMessage", DbType.Int32, idMessage);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        int i = 0;

                        int idReply = reader.GetInt32(i++);
                        DateTime date = reader.GetDateTime(i++);
                        string text = reader.GetString(i);


                        ReplyDto replyDto = new ReplyDto(idReply, idMessage, date, text);
                        return replyDto;
                    }
                    throw new InstanceNotFoundException(idMessage, "MessageDao.FindReplyByIdMesasage");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("MessageDao.FindReplyByIdMesasage", e);
            }
        }

        public MessageDto Update(MessageDto dto)
        {
            try
            {
                //Creamos un comando para ejecutar el procedimiento almacenado
                DbCommand dbCommand = db.GetStoredProcCommand(m_UpdateSp);
                db.AddInParameter(dbCommand, "idMessage", DbType.Int32, dto.IdMessage);
                db.AddInParameter(dbCommand, "idActive", DbType.Int32, dto.IdActive);
                db.AddInParameter(dbCommand, "idUser", DbType.Int32, dto.IdUser);
                db.AddInParameter(dbCommand, "dateSent", DbType.DateTime, dto.DateSent);
                if (dto.DateReceipt == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "dateReceipt", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "dateReceipt", DbType.DateTime, dto.DateReceipt);
                db.AddInParameter(dbCommand, "msgText", DbType.String, dto.MsgText);

                int rowsAffected = db.ExecuteNonQuery(dbCommand);

                if (rowsAffected == 0)
                {
                    throw new InstanceNotFoundException(dto.IdMessage, "MessageDto");
                }
                
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("MessageDao.Update", e);
            }
            return dto;
        }

        /// <summary>
        /// Searches the message with the identifier given
        /// </summary>
        /// <param name="idMessage">message´s identifier</param>
        /// <returns>the message searched</returns>
        public MessageDto FindByIdActive(int idMessage)
        {
            try
            {
                //La consulta
                string sqlQuery = "SELECT idUser, idActive, dateSent, dateReceipt, msgText "+
                                  "FROM Message " +
                                  "WHERE idMessage = @idMessage";


                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "idMessage", DbType.Int32, idMessage);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        int i = 0;

                        int idUser = reader.GetInt32(i++);
                        int idActive = reader.GetInt32(i++);
                        DateTime dateSent = reader.GetDateTime(i++);
                        DateTime dateReceipt = DateTime.MinValue;
                        if(!reader.IsDBNull(i++))
                        {
                             dateReceipt = reader.GetDateTime(i-1);
                        }
                        string msgText = reader.GetString(i);    


                        MessageDto messageDto = new MessageDto(idMessage, idUser, idActive, dateSent, dateReceipt, msgText);
                        return messageDto;
                    }
                    throw new InstanceNotFoundException(idMessage, "MessageDao.FindByIdMessage");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("MessageDao.FindByIdMessage", e);
            }
        }
       
        /// <summary>
        /// Gets all message
        /// </summary>
        /// <returns>an ArrayList with all message</returns>
        public ArrayList GetAll()
        {
            ArrayList message = new ArrayList();

            try
            {
                //La consulta
                string sqlQuery = "SELECT idUser, idActive, dateSent, dateReceipt, msgText " +
                                  "FROM Message " +
                                  "ORDER BY date ";

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                
                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        int i = 0;

                        int idMessage = reader.GetInt32(i++);
                        int idUser = reader.GetInt32(i++);
                        int idActive = reader.GetInt32(i++);
                        DateTime dateSent = reader.GetDateTime(i++);
                        DateTime dateReceipt = DateTime.MinValue;
                        if(!reader.IsDBNull(i++))
                        {
                             dateReceipt = reader.GetDateTime(i-1);
                        }


                        string msgText = reader.GetString(i);


                        MessageDto messageDto = new MessageDto(idMessage, idUser, idActive, dateSent, dateReceipt, msgText);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("MessageDao.GetAll", e);
            }
            return message;
        }

        public List<CustomMessageDto> GetAllByIdActive(int idActive)
        {
            List<CustomMessageDto> message = new List<CustomMessageDto>();

            try
            {
                //La consulta
                string sqlQuery = "SELECT phone,assistedName,userName,dateSent,dateReceipt,msgText, " +
                                  "idMessage,idUser " +
                                  "FROM View_CustomMessage " +
                                  "WHERE idActive = @idActive " +
                                  "ORDER BY dateSent DESC";

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
                        string phone = reader.GetString(i++);
                        string assistedName = reader.GetString(i++);
                        string userName = reader.GetString(i++);
                        DateTime dateSent = reader.GetDateTime(i++);
                        DateTime? dateReceipt = null;
                        string confirmed = "No";
                        if (!reader.IsDBNull(i++))
                        {
                            confirmed = "Si";
                            dateReceipt = reader.GetDateTime(i - 1);
                        }
                        string msgText = "";
                        if (!reader.IsDBNull(i++))
                            msgText = reader.GetString(i - 1);
                        int idMessage = reader.GetInt32(i++);
                        int idUser = reader.GetInt32(i);

                        List<ReplyDto> replies = GetAllRepliesByIdMessage(idMessage);
                        
                        CustomMessageDto messageDto = new CustomMessageDto(idMessage,idUser,idActive,dateSent,
                            dateReceipt,msgText,phone,userName,assistedName,replies,confirmed); 
                        message.Add(messageDto);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("MessageDao.GetAll", e);
            }
            return message;
        }

        public List<ReplyDto> GetAllRepliesByIdMessage(int idMessage)
        {
            List<ReplyDto> replies = new List<ReplyDto>();

            try
            {
                //La consulta
                string sqlQuery = "SELECT idReply,date,text " +
                                  "FROM Reply " +
                                  "WHERE idMessage= @idMessage " +
                                  "ORDER BY date DESC";

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "idMessage", DbType.Int32, idMessage);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        int i = 0;
                        int idReply = reader.GetInt32(i++);
                        DateTime dateReceipt = DateTime.MinValue;
                        if (!reader.IsDBNull(i++))
                        {
                            dateReceipt = reader.GetDateTime(i - 1);
                        }
                        string msgText = "";
                        if (!reader.IsDBNull(i++))
                            msgText = reader.GetString(i - 1);
                        
                        ReplyDto replyDto = new ReplyDto(idReply,idMessage,dateReceipt,msgText);
                        replies.Add(replyDto);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("MessageDao.GetAllRepliesByIdMessage", e);
            }
            return replies;
        }

        public List<ReplyDto> GetAllReplies()
        {
            List<ReplyDto> replies = new List<ReplyDto>();

            try
            {
                //La consulta
                string sqlQuery = "SELECT idMessage,idReply,date,text " +
                                  "FROM Reply " +
                                  "ORDER BY date DESC";

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                
                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        int i = 0;
                        int idMessage = reader.GetInt32(i++);
                        int idReply = reader.GetInt32(i++);
                        DateTime dateReceipt = DateTime.MinValue;
                        if (!reader.IsDBNull(i++))
                        {
                            dateReceipt = reader.GetDateTime(i - 1);
                        }
                        string msgText = "";
                        if (!reader.IsDBNull(i++))
                            msgText = reader.GetString(i - 1);

                        ReplyDto replyDto = new ReplyDto(idReply, idMessage, dateReceipt, msgText);
                        replies.Add(replyDto);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("MessageDao.GetAllReplies", e);
            }
            return replies;
        }

        /// <summary>
        /// Cuenta los mensajes no leídos para un activo
        /// </summary>
        /// <param name="idActive"></param>
        /// <returns></returns>
        public int CountNotReaded(int idActive)
        {
            int count = 0;

            try
            {
                //La consulta
                string sqlQuery = "SELECT count(idMessage) FROM Message " +
                                  "WHERE idActive = @idActive ";

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "idActive", DbType.Int32, idActive);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        count = reader.GetInt32(0);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("MessageDao.CountNotReaded", e);
            }
            return count;
        }
    }
}
