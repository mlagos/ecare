using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Data.SqlClient;
using Nextgal.ECare.Common.Exceptions;
using Nextgal.ECare.Model.Position.Dto;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Nextgal.ECare.Model.Position.Dao
{
    public class PositionDao
    {
        private const string m_CreateSp = "pa_insert_position";
        private Database db;

        private static PositionDao instance;

        private PositionDao()
        {
            db = DatabaseFactory.CreateDatabase();
        }

        /// <summary>
        /// Unique instance
        /// </summary>
        /// <returns>this unique instance</returns>
        public static PositionDao GetInstance()
        {
            if (instance == null)
            {
                instance = new PositionDao();
            }
            return instance;
        }

        /// <summary>
        /// Creates a new position
        /// </summary>
        /// <param name="positionDto">the position</param>
        /// <returns>the position created</returns>
        public PositionDto Create(PositionDto positionDto)
        {
            try
            {

                //Creamos un comando para ejecutar el procedimiento almacenado
                DbCommand dbCommand = db.GetStoredProcCommand(m_CreateSp);


                //Creamos los parámetros del procedimiento almacenado
                db.AddInParameter(dbCommand, "date", DbType.DateTime, positionDto.Date);
                db.AddInParameter(dbCommand, "idActive", DbType.Int32, positionDto.IdActive);
                db.AddInParameter(dbCommand, "latitude", DbType.Double, positionDto.Latitude);
                db.AddInParameter(dbCommand, "longitude", DbType.Double, positionDto.Longitude);
                db.AddInParameter(dbCommand, "isCellId", DbType.String, positionDto.IsCellID.ToString());

                db.ExecuteNonQuery(dbCommand);

                return positionDto;

            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    throw new DuplicateInstanceException(positionDto, "PositionDao.Create"); 
                }
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Searches the positions between two dates and a idActive given
        /// </summary>
        /// <param name="firstDate">first date</param>
        /// <param name="idActive">active identifier</param>
        /// <returns>all active´s position</returns>
        public PositionDto FindByDate(DateTime firstDate, int idActive)
        {
            try
            {
                //La consulta
                string sqlQuery = "SELECT date, latitude, longitude, isCellId " +
                                  "FROM Position " +
                                  "WHERE (date = @firstDate) AND (idActive = @idActive)";

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "firstDate", DbType.DateTime, firstDate);
                db.AddInParameter(cmd, "idActive", DbType.Int32, idActive);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        int i = 0;

                        DateTime date = reader.GetDateTime(i++);
                        double latitude = reader.GetDouble(i++);
                        double longitude = reader.GetDouble(i++);
                        bool cellId = Boolean.Parse(reader.GetString(i));

                        PositionDto positionDto = new PositionDto(date, idActive, latitude, longitude, cellId);
                        return positionDto;
                    }
                    throw new InstanceNotFoundException(idActive, "PositionDao.FindByDate");
                }

            }
            catch (SqlException e)
            {
                throw new InternalErrorException("PositionDao.FindByDate", e);
            }
        }
        
        /// <summary>
        /// Gets all positions
        /// </summary>
        /// <returns>an ArrayList with all positions</returns>
        public ArrayList GetAll()
        {
            ArrayList positions = new ArrayList();

            try
            {
                //La consulta
                string sqlQuery = "SELECT date, idActive, latitude, longitude, isCellId " +
                                  "FROM Position ";


                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        int i = 0;

                        DateTime date = reader.GetDateTime(i++);
                        int idActive = reader.GetInt32(i++);
                        double latitude = reader.GetDouble(i++);
                        double longitude = reader.GetDouble(i++);
                        bool isCellId = Boolean.Parse(reader.GetString(i));

                        PositionDto positionDto = new PositionDto(date, idActive, latitude, longitude, isCellId);

                        positions.Add(positionDto);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("PositionDao.GetAll", e);
            }
            return positions;
        }

        /// <summary>
        /// Obtains all positions between two dates
        /// </summary>
        /// <param name="idActive">Assisted´s identifier</param>
        /// <param name="date1">Date from</param>
        /// <param name="date2">Date to</param>
        /// <returns>Positions searched</returns>
        public ArrayList GetAllByDate(int idActive, DateTime date1, DateTime date2)
        {
            ArrayList positions = new ArrayList();
            try
            {
                //La consulta
                string sqlQuery = "SELECT date, latitude, longitude, isCellId " +
                                  "FROM Position " +
                                  "WHERE idActive = @idActive AND date>@dateTime AND date<@dateTime2 AND isCellId='False'";

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "idActive", DbType.Int32, idActive);
                db.AddInParameter(cmd, "dateTime", DbType.DateTime, date1);
                db.AddInParameter(cmd, "dateTime2", DbType.DateTime, date2);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        int i = 0;

                        DateTime date = reader.GetDateTime(i++);
                        double latitude = reader.GetDouble(i++);
                        double longitude = reader.GetDouble(i++);
                        bool cellId = Boolean.Parse(reader.GetString(i));

                        PositionDto positionDto = new PositionDto(date, idActive, latitude, longitude, cellId);
                        positions.Add(positionDto);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("PositionDao.GetAllPositionsByDate", e);
            }
            return positions;
        }

        /// <summary>
        /// Searches positions of the active with the identifier and date given </summary>
        /// <param name="idActive">active´s identifier</param>
        /// <param name="dateTime">start dateTime</param>
        /// <param name="dateTime2">end dateTime</param>
        /// <param name="allowCellIdPos">include cell id pos</param>
        /// <returns>Positions searched</returns>
        public ArrayList GetPositionsByDate(int idActive, DateTime dateTime, DateTime dateTime2, bool allowCellIdPos)
        {
            ArrayList positions = new ArrayList();
            try
            {
                string sqlQuery;

                //La consulta
                if (allowCellIdPos)
                {
                    sqlQuery = "SELECT date, latitude, longitude, isCellId " +
                                  "FROM Position " +
                                  "WHERE idActive = @idActive AND date>=@dateTime AND date<@dateTime2";
                }
                else
                {
                    sqlQuery = "SELECT date, latitude, longitude, isCellId " +
                                  "FROM Position " +
                                  "WHERE idActive = @idActive AND date>=@dateTime AND date<@dateTime2 AND isCellId='False'";
                }

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "idActive", DbType.Int32, idActive);
                db.AddInParameter(cmd, "dateTime", DbType.DateTime, dateTime);
                db.AddInParameter(cmd, "dateTime2", DbType.DateTime, dateTime2);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        int i = 0;

                        DateTime date = reader.GetDateTime(i++);
                        double latitude = reader.GetDouble(i++);
                        double longitude = reader.GetDouble(i++);
                        bool cellId = Boolean.Parse(reader.GetString(i));

                        PositionDto positionDto = new PositionDto(date, idActive, latitude, longitude, cellId);
                        positions.Add(positionDto);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("PositionDao.GetPositionsByDate", e);
            }
            return positions;
        }

        /// <summary>
        /// Finds all assisted last known position
        /// </summary>
        /// <param name="allowCellIdPos">include cell id pos</param>
        /// <returns>ArrayList with the positions</returns>
        public ArrayList GetAllActiveLastKnownPosition(bool allowCellIdPos)
        {
            ArrayList positions = new ArrayList();

            try
            {
                string sqlQuery;
                //La consulta
                if (allowCellIdPos)
                {
                    sqlQuery = "SELECT Position.date, Position.idActive, Position.latitude, Position.longitude, Position.isCellId " +
                               "FROM Position INNER JOIN Assisted ON Position.idActive = Assisted.idActive " +
                               "INNER JOIN (SELECT MAX(date) as fecha, idActive as activo " +
                               "FROM Position GROUP BY idActive) q " +
                               "ON Position.date = fecha AND Position.idActive = activo ";
                }
                else
                {
                    sqlQuery = "SELECT Position.date, Position.idActive, Position.latitude, Position.longitude, Position.isCellId " +
                               "FROM Position INNER JOIN Assisted ON Position.idActive = Assisted.idActive " +
                               "INNER JOIN (SELECT MAX(date) as fecha, idActive as activo " +
                               "FROM Position WHERE isCellId = 'False' GROUP BY idActive) q " +
                               "ON Position.date = fecha AND Position.idActive = activo " +
                               "WHERE isCellId = 'False'";
                }


                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        int i = 0;

                        DateTime fecha = reader.GetDateTime(i++);
                        int idActive = reader.GetInt32(i++);
                        double latitude = reader.GetDouble(i++);
                        double longitude = reader.GetDouble(i++);
                        bool isCellId = Boolean.Parse(reader.GetString(i));

                        PositionDto positionDto = new PositionDto(fecha, idActive, latitude, longitude, isCellId);

                        positions.Add(positionDto);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("PositionDao.GetAllActiveLastKnownPosition", e);
            }
            return positions;
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
                string sqlQuery;

                //La consulta
                if (allowCellIdPos)
                {
                    sqlQuery = "SELECT date, latitude, longitude, isCellId " +
                                  "FROM Position INNER JOIN (SELECT MAX(date) as fecha, idActive as activo " +
                                  "FROM Position WHERE date<@dateTime GROUP BY idActive) q ON date = fecha AND idActive = activo " +
                                  "WHERE Position.idActive = @idActive";
                }
                else
                {
                    sqlQuery = "SELECT date, latitude, longitude, isCellId " +
                                  "FROM Position INNER JOIN (SELECT MAX(date) as fecha, idActive as activo " +
                                  "FROM Position WHERE date<@dateTime AND isCellId = 'False' GROUP BY idActive) q ON date = fecha AND idActive = activo " +
                                  "WHERE Position.idActive = @idActive AND isCellId = 'False'";
                }

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "idActive", DbType.Int32, idActive);
                db.AddInParameter(cmd, "dateTime", DbType.DateTime, dateTime);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        int i = 0;

                        DateTime date = reader.GetDateTime(i++);
                        double latitude = reader.GetDouble(i++);
                        double longitude = reader.GetDouble(i++);
                        bool cellId = Boolean.Parse(reader.GetString(i));

                        PositionDto positionDto = new PositionDto(date, idActive, latitude, longitude, cellId);

                        return positionDto;
                    }
                    throw new InstanceNotFoundException(idActive, "PositionDao.FindLastPositionActiveByDate");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("PositionDao.FindLastPositionActiveByDate", e);
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
                string sqlQuery;

                //La consulta
                if (allowCellIdPos)
                {
                    sqlQuery = "SELECT date, latitude, longitude, isCellId " +
                                  "FROM Position INNER JOIN (SELECT MAX(date) as fecha, idActive as activo " +
                                  "FROM Position GROUP BY idActive) q ON date = fecha AND idActive = activo " +
                                  "WHERE Position.idActive = @idActive";
                }
                else
                {
                    sqlQuery = "SELECT date, latitude, longitude, isCellId " +
                                  "FROM Position INNER JOIN (SELECT MAX(date) as fecha, idActive as activo " +
                                  "FROM Position WHERE isCellId = 'False' GROUP BY idActive) q ON date = fecha AND idActive = activo " +
                                  "WHERE Position.idActive = @idActive AND isCellId = 'False'";
                }

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "idActive", DbType.Int32, idActive);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        int i = 0;

                        DateTime date = reader.GetDateTime(i++);
                        double latitude = reader.GetDouble(i++);
                        double longitude = reader.GetDouble(i++);
                        bool cellId = Boolean.Parse(reader.GetString(i));

                        PositionDto positionDto = new PositionDto(date, idActive, latitude, longitude, cellId);

                        return positionDto;
                    }
                    throw new InstanceNotFoundException(idActive, "PositionDao.FindLastPositionActive");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("PositionDao.FindLastPositionActive", e);
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
                string sqlQuery;

                //La consulta
                if (allowCellIdPos)
                {
                    sqlQuery = "SELECT date, latitude, longitude, isCellId " +
                                  "FROM Position INNER JOIN (SELECT MIN(date) as fecha, idActive as activo " +
                                  "FROM Position GROUP BY idActive) q ON date = fecha AND idActive = activo " +
                                  "WHERE Position.idActive = @idActive";
                }
                else
                {
                    sqlQuery = "SELECT date, latitude, longitude, isCellId " +
                               "FROM Position INNER JOIN (SELECT MIN(date) as fecha, idActive as activo " +
                               "FROM Position WHERE isCellId = 'False' GROUP BY idActive) q ON date = fecha AND idActive = activo " +
                               "WHERE Position.idActive = @idActive AND isCellId = 'False'";
                }

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "idActive", DbType.Int32, idActive);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        int i = 0;

                        DateTime date = reader.GetDateTime(i++);
                        double latitude = reader.GetDouble(i++);
                        double longitude = reader.GetDouble(i++);
                        bool cellId = Boolean.Parse(reader.GetString(i));

                        PositionDto positionDto = new PositionDto(date, idActive, latitude, longitude, cellId);

                        return positionDto;
                    }
                    throw new InstanceNotFoundException(idActive, "PositionDao.FindFirstPositionActive");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("PositionDao.FindFirstPositionActive", e);
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
                string sqlQuery = "SELECT COUNT(*) " +
                                  "FROM Position " +
                                  "WHERE idActive = @idActive";

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "idActive", DbType.Int32, idActive);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                int count = (int)db.ExecuteScalar(cmd);
                if (count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("PositionDao.HasPositions", e);
            }
        }

        /// <summary>
        /// Gets user route data by family identifier
        /// </summary>
        /// <param name="idFamily">identifier</param>
        /// <returns>User route date</returns>
        public List<MonitoringReportDto> GetMonitoriongReportDataByIdUser(int idFamily)
        {
            List<MonitoringReportDto> data = new List<MonitoringReportDto>();
            try
            {
                string sqlQuery;

                sqlQuery = "SELECT idActive,name,date " +
                           "FROM View_MonitoringReport " +
                           "WHERE idFamily=@idFamily";


                if (!String.IsNullOrEmpty(sqlQuery))
                {
                    //Creamos un comando para ejecutar la consulta sql
                    var cmd = db.GetSqlStringCommand(sqlQuery);
                    db.AddInParameter(cmd, "idFamily", DbType.Int32, idFamily);

                    //The using statement ensures that the DbDataReader object is disposed, which 
                    //closes the DbDataReader object.
                    using (IDataReader reader = db.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            int i = 0;
                            int idActive = reader.GetInt32(i++);
                            string activeName = "";
                            if (!reader.IsDBNull(i++))
                                activeName = reader.GetString(i - 1);
                            DateTime lastPos = DateTime.MinValue;
                            if (!reader.IsDBNull(i++))
                                lastPos = reader.GetDateTime(i - 1);

                            MonitoringReportDto dataRow = new MonitoringReportDto(idActive, activeName, lastPos, "", "");
                            data.Add(dataRow);
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("AdditionalInformation.GetMonitoringReportDataByIdUser", e);
            }
            return data;
        }

        /// <summary>
        /// Indicates if a position is CellId position
        /// </summary>
        /// <param name="dateTime">date an time</param>
        /// <param name="idActive">active´s identifier</param>
        /// <returns>true if valid, false otherwise</returns>
        public bool IsCellIdPosition(DateTime dateTime, int idActive)
        {
            bool isCellId = false;

            try
            {
                //La consulta
                string sqlQuery = "SELECT isCellId FROM Position " +
                                  "WHERE date = @dateTime AND idActive = @idActive";

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "idActive", DbType.Int32, idActive);
                db.AddInParameter(cmd, "dateTime", DbType.DateTime, dateTime);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        isCellId = Boolean.Parse(reader.GetString(0));
                    }
                }
            }

            catch (SqlException e)
            {
                throw new InternalErrorException("PositionDao.IsCellIdPosition", e);
            }
            return isCellId;
        }

        /// <summary>
        /// Searches the last known position of the active with the identifier and date given </summary>
        /// <param name="idActive">active´s identifier</param>
        /// <param name="dateTime">min date and time of the position</param>
        /// <param name="allowCellIdPos">include cell id pos</param>
        /// <returns>the position searched</returns>
        public PositionDto GetPreviousPosition(int idActive, DateTime dateTime, bool allowCellIdPos)
        {
            try
            {
                PositionDto positionDto = FindByDate(dateTime, idActive);
                ArrayList positions = GetPositionsByDate(idActive, dateTime.Date.Date, dateTime.Date.AddDays(1), allowCellIdPos);
                int index = 0;
                foreach (PositionDto pos in positions)
                {
                    if (pos.Date.Equals(positionDto.Date))
                    {
                        index = positions.IndexOf(pos);
                        break;
                    }
                }

                PositionDto previousPosition;
                if (index == 0)
                    previousPosition = positionDto;
                else
                    previousPosition = (PositionDto)positions[index - 1];
                return previousPosition;
            }
            catch (Exception e)
            {
                throw new InternalErrorException("PositionDao.GetPreviousPosition", e);
            }
        }

        /// <summary>
        /// Searches the last known position of the active with the identifier and date given </summary>
        /// <param name="idActive">active´s identifier</param>
        /// <param name="dateTime">min date and time of the position</param>
        /// <param name="allowCellIdPos">include cell id pos</param>
        /// <returns>the position searched</returns>
        public PositionDto GetNextPosition(int idActive, DateTime dateTime, bool allowCellIdPos)
        {
            try
            {
                PositionDto positionDto = FindByDate(dateTime, idActive);
                ArrayList positions = GetPositionsByDate(idActive, dateTime.Date, dateTime.Date.AddDays(1), allowCellIdPos);
                int index = 0;
                foreach (PositionDto pos in positions)
                {
                    if (pos.Date.Equals(positionDto.Date))
                    {
                        index = positions.IndexOf(pos);
                        break;
                    }
                }

                PositionDto nextPosition;
                if (index == positions.Count - 1)
                    nextPosition = positionDto;
                else
                    nextPosition = (PositionDto)positions[index + 1];
                return nextPosition;
            }
            catch (Exception e)
            {
                throw new InternalErrorException("PositionDao.GetNextPosition", e);
            }
        }
    }
}
