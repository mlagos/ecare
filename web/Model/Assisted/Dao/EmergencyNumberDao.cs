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
    public class EmergencyNumberDao
    {
        private const string m_CreateSp = "pa_insert_emergencynumber";
        private const string m_UpdateSp = "pa_update_emergencynumber";
        private const string m_DeleteSp = "pa_delete_emergencynumber";
        private Database db;

        private static EmergencyNumberDao instance;

        private EmergencyNumberDao()
        {
            db = DatabaseFactory.CreateDatabase();
        }

        /// <summary>
        /// Unique instance
        /// </summary>
        /// <returns>this unique instance</returns>
        public static EmergencyNumberDao GetInstance()
        {
            if (instance == null)
            {
                instance = new EmergencyNumberDao();
            }
            return instance;
        }

        /// <summary>
        /// Creates a new emergency number
        /// </summary>
        /// <param name="emergencyNumberDto">the emergency number</param>
        /// <returns>the emergency number created</returns>
        public EmergencyNumberDto Create(EmergencyNumberDto emergencyNumberDto)
        {
            try
            {
                //Creamos un comando para ejecutar el procedimiento almacenado
                DbCommand dbCommand = db.GetStoredProcCommand(m_CreateSp);
                
                //Creamos los parámetros del procedimiento almacenado
                db.AddInParameter(dbCommand, "callOrder", DbType.Int32, emergencyNumberDto.CallOrder);
                db.AddInParameter(dbCommand, "enable", DbType.Boolean, emergencyNumberDto.Enable);
                db.AddInParameter(dbCommand, "phone", DbType.String, emergencyNumberDto.Phone);
                
                int idActive = (int)db.ExecuteScalar(dbCommand);
                emergencyNumberDto.IdActive = idActive;
               
                return emergencyNumberDto;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    throw new DuplicateInstanceException(emergencyNumberDto, "EmergencyNumberDao.Create");
                }
                throw e;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Updates an emergency number
        /// </summary>
        /// <param name = "emergencyNumberDto">the emergency number to update</param>
        /// <returns>the emergency number updated</returns>
        public EmergencyNumberDto Update(EmergencyNumberDto emergencyNumberDto)
        {
            try
            {
                //Creamos un comando para ejecutar el procedimiento almacenado
                DbCommand dbCommand = db.GetStoredProcCommand(m_UpdateSp);

                //Creamos los parámetros del procedimiento almacenado
                db.AddInParameter(dbCommand, "idActive", DbType.Int32, emergencyNumberDto.IdActive);
                db.AddInParameter(dbCommand, "callOrder", DbType.Int32, emergencyNumberDto.CallOrder);
                db.AddInParameter(dbCommand, "enable", DbType.Boolean, emergencyNumberDto.Enable);
                db.AddInParameter(dbCommand, "phone", DbType.String, emergencyNumberDto.Phone);
               
                int rowsAffected = db.ExecuteNonQuery(dbCommand);

                if (rowsAffected == 0)
                {
                    throw new InstanceNotFoundException(emergencyNumberDto.IdActive, "EmergencyNumberDto");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("EmergencyNumberDto.Update", e);
            }
            return emergencyNumberDto;
        }

        /// <summary>
        /// Deletes an emergency number
        /// </summary>
        /// <param name="idActive">emergency number´s identifier</param>
        public void Delete(int idActive)
        {
            try
            {
                //Creamos un comando para ejecutar el procedimiento almacenado
                DbCommand dbCommand = db.GetStoredProcCommand(m_DeleteSp);

                //Creamos los parámetros del procedimiento almacenado
                db.AddInParameter(dbCommand, "idActive", DbType.Int32, idActive);

                int rowsAffected = db.ExecuteNonQuery(dbCommand);

                if (rowsAffected == 0)
                {
                    throw new InstanceNotFoundException(idActive, "EmergencyNumberDto");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("EmergencyNumberDao.Delete", e);
            }
        }

        /// <summary>
        /// Searches the emergency number with the identifier given
        /// </summary>
        /// <param name="idActive">emergency number´s identifier</param>
        /// <returns>the emergency number searched</returns>
        public EmergencyNumberDto FindByIdActive(int idActive)
        {
            try
            {
                //La consulta
                string sqlQuery = "SELECT callOrder, enable, phone "+
                                  "FROM EmergencyNumber " +
                                  "WHERE idActive = @idActive";


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

                        int callOrder = reader.GetInt32(i++);
                        bool enable = reader.GetBoolean(i++);
                        string phone = reader.GetString(i);
                        
                        EmergencyNumberDto emergencyNumberDto = new EmergencyNumberDto(idActive, callOrder, enable, phone);
                        return emergencyNumberDto;
                    }
                    throw new InstanceNotFoundException(idActive, "EmergencyNumberDao.FindByIdActive");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("EmergencyNumberDao.FindByIdActive", e);
            }
        }
       
        /// <summary>
        /// Gets all emergency number
        /// </summary>
        /// <returns>an ArrayList with all emergency numbers</returns>
        public ArrayList GetAll()
        {
            ArrayList assisted = new ArrayList();

            try
            {
                //La consulta
                string sqlQuery = "SELECT idActive, callOrder, enable, phone " +
                                  "FROM EmergencyNumber ";

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
                        int callOrder = reader.GetInt32(i++);
                        bool enable = reader.GetBoolean(i++);
                        string phone = reader.GetString(i);

                        EmergencyNumberDto emergencyNumberDto = new EmergencyNumberDto(idActive, callOrder, enable, phone);
                        assisted.Add(emergencyNumberDto);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("EmergencyNumberDao.GetAll", e);
            }
            return assisted;
        }
    }
}
