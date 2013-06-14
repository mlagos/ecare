using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Nextgal.ECare.Common.Exceptions;
using Nextgal.ECare.Model.Position.Dao;
using Nextgal.ECare.Model.Position.Dto;
using Nextgal.PocketLocator.Model.Position.Dto;

namespace Nextgal.ECare.Model.Position.Facade
{
    public class PositionFacade
    {
        #region Position

        /// <summary>
        /// Creates a new position
        /// </summary>
        /// <param name="positionDto">the position</param>
        /// <returns>the position created</returns>
        public PositionDto CreatePosition(PositionDto positionDto)
        {
            try
            {
                PositionDao positionDao = PositionDao.GetInstance();
                return positionDao.Create(positionDto);
            }
            catch (DuplicateInstanceException e)
            {
                throw e;
            }
            catch(ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Gets all positions
        /// </summary>
        /// <returns>an ArrayList with all positions</returns>
        public ArrayList GetAllPosition()
        {
            try
            {
                PositionDao positionDao = PositionDao.GetInstance();
                return positionDao.GetAll();
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Searches the last known position of the active with the identifier and date given </summary>
        /// <param name="idActive">active´s identifier</param>
        /// <param name="date">min date and time of the position</param>
        /// <param name="allowCellIdPos">include cell id pos</param>
        /// <returns>the position searched</returns>
        public PositionDto GetPreviousPosition(int idActive, DateTime date, bool allowCellIdPos)
        {
            try
            {
                PositionDao positionDao = PositionDao.GetInstance();
                return positionDao.GetPreviousPosition(idActive, date, allowCellIdPos);
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
        /// Obtains all positions between two dates
        /// </summary>
        /// <param name="idActive">Assisted´s identifier</param>
        /// <param name="date">Date from</param>
        /// <param name="date2">Date to</param>
        /// <returns>Positions searched</returns>
        public ArrayList GetAllPositionsByDate(int idActive, DateTime date, DateTime date2)
        {
            try
            {
                PositionDao positionDao = PositionDao.GetInstance();
                return positionDao.GetAllByDate(idActive, date, date2);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Searches positions of the active with the identifier and date given </summary>
        /// <param name="idActive">Active´s identifier</param>
        /// <param name="date">Start dateTime</param>
        /// <param name="date2">End dateTime</param>
        /// <param name="allowCellIdPos">Include cell id pos</param>
        /// <returns>Positions searched</returns>
        public ArrayList GetPositionsByDate(int idActive, DateTime date, DateTime date2, bool allowCellIdPos)
        {
            try
            {
                PositionDao positionDao = PositionDao.GetInstance();
                return positionDao.GetPositionsByDate(idActive, date, date2, allowCellIdPos);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Finds all assisted last known position
        /// </summary>
        /// <param name="allowCellIdPos">include cell id pos</param>
        /// <returns>ArrayList with the positions</returns>
        public ArrayList GetAllActiveLastKnownPosition(bool allowCellIdPos)
        {
            try
            {
                PositionDao positionDao = PositionDao.GetInstance();
                ArrayList positions = positionDao.GetAllActiveLastKnownPosition(allowCellIdPos);
                return positions;
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Searches the first known position of the active
        /// </summary>
        /// <param name="idActive">active´s identifier</param>
        /// <param name="allowCellIdPos">include cell id pos</param>
        /// <returns>the position searched</returns>
        public PositionDto FindFirstPositionActive(int idActive, bool allowCellIdPos)
        {
            try
            {
                PositionDao positionDao = PositionDao.GetInstance();
                PositionDto positions = positionDao.FindFirstPositionActive(idActive, allowCellIdPos);
                return positions;
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Searches the positions between two dates and a idActive given
        /// </summary>
        /// <param name="firstDate">first date</param>
        /// <param name="idActive">active identifier</param>
        /// <returns>all active´s position</returns>
        public PositionDto FindPositionByDate(DateTime firstDate, int idActive)
        {
            try
            {
                PositionDao positionDao = PositionDao.GetInstance();
                PositionDto positions = positionDao.FindByDate(firstDate, idActive);
                return positions;
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Searches the last known position of the active
        /// </summary>
        /// <param name="idActive">Active´s identifier</param>
        /// <param name="allowCellIdPos">True to include cellid pos</param>
        /// <param name="dateTime">Date and time</param>
        /// <returns>the position searched</returns>
        public PositionDto FindLastPositionActiveByDate(int idActive, DateTime dateTime, bool allowCellIdPos)
        {
            try
            {
                PositionDao positionDao = PositionDao.GetInstance();
                PositionDto positions = positionDao.FindLastPositionActiveByDate(idActive, dateTime, allowCellIdPos);
                return positions;
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Searches the last known position of the active
        /// </summary>
        /// <param name="idActive">Active´s identifier</param>
        /// <param name="allowCellIdPos">True to include cellid pos</param>
        /// <returns>the position searched</returns>
        public PositionDto FindLastPositionActive(int idActive, bool allowCellIdPos)
        {
            try
            {
                PositionDao positionDao = PositionDao.GetInstance();
                PositionDto position = positionDao.FindLastPositionActive(idActive, allowCellIdPos);
                return position;
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
        /// Checks if one active has positions stored
        /// </summary>
        /// <param name="idActive">active´s identifier</param>
        /// <returns>true if it has positions</returns>
        public bool HasPositions(int idActive)
        {
            try
            {
                PositionDao positionDao = PositionDao.GetInstance();
                return positionDao.HasPositions(idActive);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Gets user route data by family identifier
        /// </summary>
        /// <param name="idFamily">identifier</param>
        /// <returns>User route date</returns>
        public List<MonitoringReportDto> GetMonitoringReportDataByIdUser(int idFamily)
        {
            try
            {
                return PositionDao.GetInstance().GetMonitoriongReportDataByIdUser(idFamily);
            }
            catch(ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Indicates if a position is CellId position
        /// </summary>
        /// <param name="date">date and time</param>
        /// <param name="idActive">active´s identifier</param>
        /// <returns>true if valid, false otherwise</returns>
        public bool IsCellIdPosition(DateTime date, int idActive)
        {
            try
            {
                PositionDao positionDao = PositionDao.GetInstance();
                return positionDao.IsCellIdPosition(date, idActive);
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
        /// Searches the last known position of the active with the identifier and date given </summary>
        /// <param name="idActive">active´s identifier</param>
        /// <param name="dateTime">min date and time of the position</param>
        /// <param name="allowCellIdPos">include cellid pos</param>
        /// <returns>the position searched</returns>
        public PositionDto GetNextPosition(int idActive, DateTime dateTime, bool allowCellIdPos)
        {
            try
            {
                PositionDao positionDao = PositionDao.GetInstance();
                return positionDao.GetNextPosition(idActive, dateTime, allowCellIdPos);
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

        #region AdditionalInformation

        /// <summary>
        /// Creates a new instance of additional information about one active
        /// </summary>
        /// <param name="additionalInformationDto">the additional information</param>
        /// <returns>the additional information created</returns>
        public AdditionalInformationDto CreateAdditionalInformation(AdditionalInformationDto additionalInformationDto)
        {
            try
            {
                AdditionalInformationDao additionalInformationDao = AdditionalInformationDao.GetInstance();
                return additionalInformationDao.Create(additionalInformationDto);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Searches all the additional information of an active
        /// </summary>
        /// <param name="date">first date</param>
        /// <param name="idActive">active identifier</param>
        /// <returns>active´s positions at a date</returns>
        public OrderedDictionary GetAllAdditionalInformationByDate(DateTime date, int idActive)
        {
            try
            {
                AdditionalInformationDao additionalInformationDao = AdditionalInformationDao.GetInstance();
                return additionalInformationDao.GetAllAdditionalInfoByDate(date, idActive);
            }
            catch (ModelException me)
            {

                throw me;
            }
        }

        /// <summary>
        /// Obtains mileage information
        /// </summary>
        /// <param name="date">position date</param>
        /// <param name="idActive">active´s identifier</param>
        /// <returns>mileage information</returns>
        public string GetMileageByDate(DateTime date, int idActive)
        {
            try
            {
                AdditionalInformationDao additionalInformationDao = AdditionalInformationDao.GetInstance();
                return additionalInformationDao.GetMileageByDate(date, idActive);
            }
            catch (ModelException me)
            {

                throw me;
            }
        }

        /// <summary>
        /// Get total mileage of non-acumulative devices
        /// </summary>
        /// <param name="idActive">active´s id</param>
        /// <returns>Average speed in km/h</returns>
        public double GetTotalMileage(int idActive)
        {
            try
            {
                AdditionalInformationDao additionalInformationDao = AdditionalInformationDao.GetInstance();
                return additionalInformationDao.GetTotalMileage(idActive);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Obtains data to fill monitoring routes report
        /// </summary>
        /// <param name="idActive">active´s identifier</param>
        /// <param name="date1">date from</param>
        /// <param name="date2">date to</param>
        /// <param name="allowCellId">true to allow cellId positions</param>
        /// <returns>ArrayList with all data</returns>
        public ArrayList GetMonitoringRoutesReportData(int idActive, DateTime date1, DateTime date2, bool allowCellId)
        {
            try
            {
                AdditionalInformationDao additionalInformationDao = AdditionalInformationDao.GetInstance();
                return additionalInformationDao.GetMonitoringRoutesReportData(idActive, date1, date2, allowCellId);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }
        
        #endregion

        private static PositionFacade instance;

        public static PositionFacade GetInstance()
        {
            if (instance == null)
            {
                instance = new PositionFacade();
            }
            return instance;
        }
    }
}
