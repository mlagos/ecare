using System;
using System.Collections;
using System.Collections.Generic;
using Nextgal.ECare.Model.Event.Dao;
using Nextgal.ECare.Model.Event.Dto;
using Nextgal.ECare.Common.Exceptions;

namespace Nextgal.ECare.Model.Event.Facade
{
    public class EventFacade
    {
        #region Event

        /// <summary>
        /// Creates a new event
        /// </summary>
        /// <param name="eventDto">the event</param>
        /// <returns>the event created</returns>
        public EventDto CreateEvent(EventDto eventDto)
        {
            try
            {
                EventDao eventDao = EventDao.GetInstance();
                return eventDao.Create(eventDto);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Searches the event with the identifier given
        /// </summary>
        /// <param name="idEvent">event´s identifier</param>
        /// <returns>the event searched</returns>
        public EventDto FindAssistedByIdEvent(int idEvent)
        {
            try
            {
                EventDao eventDao = EventDao.GetInstance();
                return eventDao.FindByIdEvent(idEvent);
            }
            catch (ModelException me)
            {
                throw me;
            }
        } 

        public List<EventDto> GetEventsByIdActive(int idActive)
        {
            try
            {
                return EventDao.GetInstance().GetByIdActive(idActive);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Gets all events
        /// </summary>
        /// <returns>an ArrayList with all assisted</returns>
        public ArrayList GetAllEvent()
        {
            try
            {
                EventDao eventDao = EventDao.GetInstance();
                return eventDao.GetAll();
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        public EventDto Update(EventDto dto)
        {
            EventDao dao = EventDao.GetInstance();
            return dao.Update(dto);
        }

        public ArrayList GetAllNoSentEvents()
        {
            EventDao dao = EventDao.GetInstance();
            return dao.GetAllNoSentEvents();
        }

        public ArrayList GetAllNoSentEvents(int idActive)
        {
            EventDao dao = EventDao.GetInstance();
            return dao.GetAllNoSentEvents(idActive);
        }

        public void DeleteEvent(int idEvent)
        {
            EventDao dao = EventDao.GetInstance();
            dao.Delete(idEvent);
        }

        #endregion

        private static EventFacade instance;

        public static EventFacade GetInstance()
        {
            if (instance == null)
            {
                instance = new EventFacade();
            }
            return instance;
        }
    }
}
