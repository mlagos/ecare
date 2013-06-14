using System;
using System.Data;
using System.Collections;
using System.Data.Common;
using System.Data.SqlClient;
using Nextgal.ECare.Common.Exceptions;
using Nextgal.ECare.Model.Assisted.Dto;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Nextgal.ECare.Model.Assisted.Dao
{
    public class ActiveFacilityDao
    {
        private const string m_CreateSp = "pa_insert_activefacility";
        private const string m_DeleteSp = "pa_delete_activefacility";
        private Database db;

        private static ActiveFacilityDao instance;

        private ActiveFacilityDao()
        {
            db = DatabaseFactory.CreateDatabase();
        }

        /// <summary>
        /// Unique instance
        /// </summary>
        /// <returns>this unique instance</returns>
        public static ActiveFacilityDao GetInstance()
        {
            if (instance == null)
            {
                instance = new ActiveFacilityDao();
            }
            return instance;
        }

        /// <summary>
        /// Creates a new active-facility relation
        /// </summary>
        /// <param name="activeFacilityDto">the active-facility relation</param>
        /// <returns>the active-facility relation created</returns>
        public ActiveFacilityDto Create(ActiveFacilityDto activeFacilityDto)
        {
            try
            {
                //Creamos un comando para ejecutar el procedimiento almacenado
                DbCommand dbCommand = db.GetStoredProcCommand(m_CreateSp);


                //Creamos los parámetros del procedimiento almacenado
                db.AddInParameter(dbCommand, "idActive", DbType.Int32, activeFacilityDto.IdActive);
                db.AddInParameter(dbCommand, "idFacility", DbType.Int32, activeFacilityDto.IdFacility);

                db.ExecuteScalar(dbCommand);

                return activeFacilityDto;

            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    throw new DuplicateInstanceException(activeFacilityDto, "ActiveFacilityDao.Create"); 
                }
                throw e;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
      
        /// <summary>
        /// Deletes an active-facility relation
        /// </summary>
        /// <param name="idActive">active´s identifier</param>
        /// <param name="idFacility">facility´s identifier</param>
        public void Delete(int idActive, int idFacility)
        {
            try
            {
                //Creamos un comando para ejecutar el procedimiento almacenado
                DbCommand dbCommand = db.GetStoredProcCommand(m_DeleteSp);

                //Creamos los parámetros del procedimiento almacenado
                db.AddInParameter(dbCommand, "idActive", DbType.Int32, idActive);
                db.AddInParameter(dbCommand, "idFacility", DbType.Int32, idFacility);

                int rowsAffected = db.ExecuteNonQuery(dbCommand);

                if (rowsAffected == 0)
                {
                    throw new InstanceNotFoundException(idFacility, "ActiveFacilityDto");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("ActiveFacilityDao.Delete", e);
            }
        }

        /// <summary>
        /// Searches the facilities of one active
        /// </summary>
        /// <param name="identifier">actives´s identifier</param>
        /// <returns>facilities searched</returns>
        public ArrayList FindActiveFacilitiesByIdentifier(string identifier)
        {
            ArrayList facilities = new ArrayList();

            try
            {
                //La consulta
                string sqlQuery = "SELECT facilityName " +
                                  "FROM View_AssistedFacilities " +
                                  "WHERE identifier = @identifier";

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "identifier", DbType.String, identifier);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        int i = 0;
                        string name = reader.GetString(i);
                        facilities.Add(name);
                    }
                }
                return facilities;
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("ActiveFacilityDao.FindActiveFacilitiesByIdentifier", e);
            }
        }

        /// <summary>
        /// Searches the facility  identifier with the name given
        /// </summary>
        /// <param name="name">facility´s identifier</param>
        /// <returns>the facility searched</returns>
        public int FindIdFacilityByName(string name)
        {
            try
            {
                //La consulta
                string sqlQuery = "SELECT idFacility " +
                                  "FROM Facility " +
                                  "WHERE name = @name";

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "name", DbType.String, name);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        int i = 0;

                        int idFacility = reader.GetInt32(i++);

                        return idFacility;
                    }
                    else
                    {
                        throw new InstanceNotFoundException(name, "FacilityDao.FindIdFacilityByName");
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("FacilityDao.FindIdFacilityByName", e);
            }
        }

        /// <summary>
        /// Gets all active-facility relations
        /// </summary>
        /// <returns>an ArrayList with all relations</returns>
        public ArrayList GetAll()
        {
            ArrayList act_fac = new ArrayList();

            try
            {
                //La consulta
                string sqlQuery = "SELECT idActive, idFacility " +
                                  "FROM ActiveFacility ";

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        int i = 0;

                        int idActive = reader.GetInt32(i++);
                        int idFacility = reader.GetInt32(i);

                        ActiveFacilityDto activeFacilityDto = new ActiveFacilityDto(idActive, idFacility);

                        act_fac.Add(activeFacilityDto);
                    }
                    reader.Close();
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("ActiveFacilityDao.GetAll", e);
            }
            return act_fac;
        }
    }
}