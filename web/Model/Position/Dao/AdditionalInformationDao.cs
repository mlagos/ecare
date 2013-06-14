using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Specialized;
using Nextgal.ECare.Common.Exceptions;
using Nextgal.ECare.Model.Position.Dto;
using Nextgal.PocketLocator.Model.Position.Dto;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Nextgal.ECare.Model.Position.Dao
{
    public class AdditionalInformationDao
    {
        private const string m_CreateSp = "pa_insert_additionalinformation";
        
        private Database db;
        private static AdditionalInformationDao instance;

        private AdditionalInformationDao()
        {
            db = DatabaseFactory.CreateDatabase();
        }

        /// <summary>
        /// Unique instance
        /// </summary>
        /// <returns>this unique instance</returns>
        public static AdditionalInformationDao GetInstance()
        {
            if (instance == null)
            {
                instance = new AdditionalInformationDao();
            }
            return instance;
        }

        /// <summary>
        /// Creates a new instance of additional information about one active
        /// </summary>
        /// <param name="additionalInformationDto">the additional information</param>
        /// <returns>the additional information created</returns>
        public AdditionalInformationDto Create(AdditionalInformationDto additionalInformationDto)
        {
            try
            {
                //Creamos un comando para ejecutar el procedimiento almacenado
                DbCommand dbCommand = db.GetStoredProcCommand(m_CreateSp);

                //Creamos los parámetros del procedimiento almacenado
                db.AddInParameter(dbCommand, "date", DbType.DateTime, additionalInformationDto.Date);
                db.AddInParameter(dbCommand, "idActive", DbType.Int32, additionalInformationDto.IdActive);
                db.AddInParameter(dbCommand, "idFacility", DbType.Int32, additionalInformationDto.IdFacility);
                db.AddInParameter(dbCommand, "value", DbType.String, additionalInformationDto.Value);

                db.ExecuteNonQuery(dbCommand);

                return additionalInformationDto;

            }
            catch (SqlException e)
            {
                //if (e.ErrorCode == -2146232060)
                //{
                //    throw new DuplicateInstanceException(additionalInformationDto, "AdditionalInformationDao.Create");
                //}
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Searches all the additional information of an active
        /// </summary>
        /// <param name="date">first date</param>
        /// <param name="idActive">active identifier</param>
        /// <returns>active´s positions at a date</returns>
        public OrderedDictionary GetAllAdditionalInfoByDate(DateTime date, int idActive)
        {
            OrderedDictionary addInfo = new OrderedDictionary();
            try
            {
                string sqlQuery = "SELECT facilityName, value " +
                                  "FROM View_PositionAddInfo " +
                                  "WHERE (idActive = @idActive) AND (date = @date)";

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);

                db.AddInParameter(cmd, "idActive", DbType.Int32, idActive);
                db.AddInParameter(cmd, "date", DbType.DateTime, date);

                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        int i = 0;
                        string facilityName = reader.GetString(i++);
                        string value = reader.GetString(i);
                        addInfo.Add(facilityName, value);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("PositionDao.GetAllAdditionalInfoByDate", e);
            }
            return addInfo;
        }

        /// <summary>
        /// Obtains mileage information
        /// </summary>
        /// <param name="date">position date</param>
        /// <param name="idActive">active´s identifier</param>
        /// <returns>mileage information</returns>
        public string GetMileageByDate(DateTime date, int idActive)
        {
            string mileage = "";
            try
            {
                string sqlQuery = "SELECT value " +
                                  "FROM View_PositionAddInfo " +
                                  "WHERE facilityName = 'mileage' AND date=@date AND idActive=@idActive";

                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "idActive", DbType.Int32, idActive);
                db.AddInParameter(cmd, "date", DbType.DateTime, date);

                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        mileage = reader.GetString(0);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("AdditionalInformationDao.GetMileageByDate", e);
            }
            return mileage;
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
                string sqlQuery = "SELECT ROUND(SUM(CAST(REPLACE(value, ',','.')AS FLOAT)), 2) " +
                                  "FROM View_PositionAddInfo " +
                                  "WHERE idActive=@idActive " +
                                  "AND facilityName='mileage'";

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);

                db.AddInParameter(cmd, "idActive", DbType.Int32, idActive);
                try
                {
                    double sum = (double)db.ExecuteScalar(cmd);
                    return sum;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("PositionDao.GetAverageSpeed", e);
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
            ArrayList data = new ArrayList();
            try
            {
                string sqlQuery;
                if (allowCellId)
                {
                    //La consulta
                    sqlQuery = "SELECT f.date, posTimeFrom, posTimeTo, posDiff, status, statusPath, mileage, location, f.latitude, f.longitude " +
                                      "FROM Func_GetRoutePositions(@idActive, @date1, @date2) f INNER JOIN Position " +
                                      "ON Position.date = f.date WHERE Position.idActive=@idActive";
                }
                else
                {
                    //La consulta
                    sqlQuery = "SELECT f.date, posTimeFrom, posTimeTo, posDiff, status, statusPath, mileage, location, f.latitude, f.longitude " +
                                      "FROM Func_GetRoutePositions(@idActive, @date1, @date2) f INNER JOIN Position " +
                                      "ON Position.date = f.date where f.isCellId='False' AND Position.idActive=@idActive";
                }

                if (!String.IsNullOrEmpty(sqlQuery))
                {
                    //Creamos un comando para ejecutar la consulta sql
                    var cmd = db.GetSqlStringCommand(sqlQuery);
                    db.AddInParameter(cmd, "idActive", DbType.Int32, idActive);
                    db.AddInParameter(cmd, "date1", DbType.DateTime, date1);
                    db.AddInParameter(cmd, "date2", DbType.DateTime, date2);

                    //The using statement ensures that the DbDataReader object is disposed, which 
                    //closes the DbDataReader object.
                    using (IDataReader reader = db.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            int i = 0;
                            DateTime date = reader.GetDateTime(i++);
                            string startTime = reader.GetString(i++);
                            string endTime = reader.GetString(i++);
                            string diffTime = reader.GetString(i++);
                            string status = reader.GetString(i++);
                            string statusPath = reader.GetString(i++);
                            string mileage = reader.GetString(i++);
                            string location = reader.GetString(i++);
                            double latitude = reader.GetDouble(i++);
                            double longitude = reader.GetDouble(i);

                            MonitoringRoutesRowDto dataRow = new MonitoringRoutesRowDto(idActive, date, startTime, endTime, diffTime, status, statusPath, mileage, location, latitude, longitude);
                            data.Add(dataRow);
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("AdditionalInformation.GetMonitoringRoutesReportData", e);
            }
            return data;
        }
    }
}