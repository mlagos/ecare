using System.Collections;
using System.Collections.Generic;
using Nextgal.ECare.Common.Exceptions;
using Nextgal.ECare.Model.Assisted.Facade;
using Nextgal.ECare.Model.Message.Dao;
using Nextgal.ECare.Model.Message.Dto;

namespace Nextgal.ECare.Model.Message.Facade
{
    public class MessageFacade
    {
        #region Message

        /// <summary>
        /// Creates a new message
        /// </summary>
        /// <param name="messageDto">the message</param>
        /// <returns>the message created</returns>
        public MessageDto CreateMessage(MessageDto messageDto)
        {
            try
            {
                MessageDao messageDao = MessageDao.GetInstance();
                return messageDao.Create(messageDto);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }


        public ReplyDto CreateReply(ReplyDto replyDto)
        {
            try
            {
                MessageDao messageDao = MessageDao.GetInstance();
                return messageDao.CreateReply(replyDto);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        public ReplyDto FindReplyByIdMessage(int idMessage)
        {
            try
            {
                MessageDao messageDao = MessageDao.GetInstance();
                return messageDao.FindReplyByIdMesasage(idMessage);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Searches the message with the identifier given
        /// </summary>
        /// <param name="idMessage">message´s identifier</param>
        /// <returns>the message searched</returns>
        public MessageDto FindMessageById(int idMessage)
        {
            try
            {
                MessageDao messageDao = MessageDao.GetInstance();
                return messageDao.FindByIdActive(idMessage);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Gets all message
        /// </summary>
        /// <returns>an ArrayList with all message</returns>
        public ArrayList GetAllMessage()
        {
            try
            {
                MessageDao messageDao = MessageDao.GetInstance();
                return messageDao.GetAll();
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        public MessageDto Update(MessageDto dto)
        {
            try
            {
                MessageDao messageDao = MessageDao.GetInstance();
                return messageDao.Update(dto);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        public List<CustomMessageDto> GetAllMessageByIdActive(int idActive)
        {
            return MessageDao.GetInstance().GetAllByIdActive(idActive);
        }

        public List<ReplyDto> GetMessageReplies(int idMessage)
        {
            return MessageDao.GetInstance().GetAllRepliesByIdMessage(idMessage);
        }

        public List<ReplyDto> GetMessageReplies()
        {
            return MessageDao.GetInstance().GetAllReplies();
        }

        public int CountMenssagesNotReaded(int idActive)
        {
            return MessageDao.GetInstance().CountNotReaded(idActive);
        }

        #endregion

        private static MessageFacade instance;

        public static MessageFacade GetInstance()
        {
            if (instance == null)
            {
                instance = new MessageFacade();
            }
            return instance;
        }
    }
}
