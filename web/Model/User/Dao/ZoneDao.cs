using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Data.SqlClient;
using Nextgal.ECare.Model.User.Dto;
using Nextgal.ECare.Common.Exceptions;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Nextgal.ECare.Model.User.Dao
{
    /// <summary>
    /// Short description of ZoneDao
    /// </summary>
    public class ZoneDao
    {
        private const string m_CreateSp = "pa_insert_zone";
        private const string m_UpdateSp = "pa_update_zone";
        private const string m_DeleteSp = "pa_delete_zone";
        private Database db;

        private static ZoneDao instance;

        private ZoneDao()
        {
            //Creamos la conexion con la BD por defecto
            db = DatabaseFactory.CreateDatabase();
        }

        /// <summary>
        /// Unique instance
        /// </summary>
        /// <returns>this unique instance</returns>
        public static ZoneDao GetInstance()
        {
            if (instance == null)
            {
                instance = new ZoneDao();
            }
            return instance;
        }

        /// <summary>
        /// Creates a new zone
        /// </summary>
        /// <param name="zoneDto">the zone</param>
        /// <returns>the zone created</returns>
        public ZoneDto Create(ZoneDto zoneDto)
        {
            try
            {
                //Creamos un comando para ejecutar el procedimiento almacenado
                DbCommand dbCommand = db.GetStoredProcCommand(m_CreateSp);

                //Creamos los parámetros del procedimiento almacenado
                db.AddInParameter(dbCommand, "name", DbType.String, zoneDto.Name);
                db.AddInParameter(dbCommand, "position", DbType.String, zoneDto.Position);
                db.AddInParameter(dbCommand, "idFamily", DbType.Int32, zoneDto.IdFamily);
                db.AddInParameter(dbCommand, "registerDate", DbType.DateTime, zoneDto.RegisterDate);
                
                int idZone = (int)db.ExecuteScalar(dbCommand);
                zoneDto.IdZone = idZone;

                return zoneDto;

            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    throw new DuplicateInstanceException(zoneDto, "ZoneDao.Create");
                }
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Updates a zone
        /// </summary>
        /// <param name = "zoneDto">the zone to update</param>
        /// <returns>the zone updated</returns>
        public ZoneDto Update(ZoneDto zoneDto)
        {
            try
            {
                //Creamos un comando para ejecutar el procedimiento almacenado
                DbCommand dbCommand = db.GetStoredProcCommand(m_UpdateSp);

                db.AddInParameter(dbCommand, "idZone", DbType.Int32, zoneDto.IdZone);
                db.AddInParameter(dbCommand, "name", DbType.String, zoneDto.Name);
                db.AddInParameter(dbCommand, "position", DbType.String, zoneDto.Position);
                db.AddInParameter(dbCommand, "idFamily", DbType.Int32, zoneDto.IdFamily);
                db.AddInParameter(dbCommand, "registerDate", DbType.DateTime, zoneDto.RegisterDate);
               
                int rowsAffected = db.ExecuteNonQuery(dbCommand);

                if (rowsAffected == 0)
                {
                    throw new InstanceNotFoundException(zoneDto.IdZone, "ZoneDto");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("ZoneDao.Update", e);
            }

            return zoneDto;
        }

        /// <summary>
        /// Deletes zones
        /// </summary>
        /// <param name="idZone">zone´s identifier</param>
        public void Delete(int idZone)
        {
            try
            {

                //Creamos un comando para ejecutar el procedimiento almacenado
                DbCommand dbCommand = db.GetStoredProcCommand(m_DeleteSp);

                //Creamos los parámetros del procedimiento almacenado
                db.AddInParameter(dbCommand, "idZone", DbType.Int32, idZone);

                int rowsAffected = db.ExecuteNonQuery(dbCommand);

                if (rowsAffected == 0)
                {
                    throw new InstanceNotFoundException(idZone, "ZoneDto");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("ZoneDao.Delete", e);
            }
        }

        /// <summary>
        /// Searches the zone with the identifier given
        /// </summary>
        /// <param name="idZone">zone´s identifier</param>
        /// <returns>the zone searched</returns>
        public ZoneDto FindByIdentifier(int idZone)
        {
            try
            {
                //La consulta
                string sqlQuery = "SELECT name, position, idFamily, registerDate " +
                                  "FROM Zone " +
                                  "WHERE idZone = @idZone";


                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "idZone", DbType.Int32, idZone);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        int i = 0;
                        string name = reader.GetString(i++);
                        string position = reader.GetString(i++);
                        int idFamily = reader.GetInt32(i++);
                        DateTime registerDate = reader.GetDateTime(i);

                        ZoneDto zoneDto = new ZoneDto(idZone, name, position, idFamily, registerDate);
                        return zoneDto;
                    }
                    throw new InstanceNotFoundException(idZone, "ZoneDao.FindByIdentifier");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("ZoneDao.FindByIdentifier", e);
            }
        }

        /// <summary>
        /// Gets all zones
        /// </summary>
        /// <returns>an ArrayList with all zones</returns>
        public ArrayList GetAll()
        {
            ArrayList zones = new ArrayList();

            try
            {
                //La consulta
                string sqlQuery = "SELECT idZone, name, position, idFamily, registerDate " +
                                  "FROM Zone " +
                                  "ORDER BY name ";

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        int i = 0;

                        int idZone = reader.GetInt32(i++);
                        string name = reader.GetString(i++);
                        string position = reader.GetString(i++);
                        int idFamily = reader.GetInt32(i++);
                        DateTime registerDate = reader.GetDateTime(i);
                        
                        ZoneDto zoneDto = new ZoneDto(idZone, name, position, idFamily, registerDate);
                        zones.Add(zoneDto);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("AlarmDao.GetAll", e);
            }
            return zones;
        }

        /// <summary>
        /// Gets all zones by family identifier
        /// </summary>
        /// <returns>an ArrayList with all zones</returns>
        public ArrayList GetAllByIdFamily(int idFamily)
        {
            ArrayList zones = new ArrayList();

            try
            {
                //La consulta
                string sqlQuery = "SELECT idZone, name, position, registerDate " +
                                  "FROM Zone " +
                                  "WHERE idFamily = @idFamily " +
                                  "ORDER BY name ";

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "idFamily", DbType.Int32, idFamily);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        int i = 0;

                        int idZone = reader.GetInt32(i++);
                        string name = reader.GetString(i++);
                        string position = reader.GetString(i++);
                        DateTime registerDate = reader.GetDateTime(i);
                       
                        ZoneDto zoneDto = new ZoneDto(idZone, name, position, idFamily, registerDate);
                        zones.Add(zoneDto);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("AlarmDao.GetAllByIdFamily", e);
            }
            return zones;
        }
    }
}
