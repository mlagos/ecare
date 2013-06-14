using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Nextgal.ECare.Common.Exceptions;
using Nextgal.ECare.Model.Assisted.Dto;
using Nextgal.ECare.Model.Assisted.Facade;

namespace Nextgal.ECare.Model.Assisted.Dao
{
    public class AssistedDao
    {
        private const string m_CreateSp = "pa_insert_assisted";
        private const string m_UpdateSp = "pa_update_assisted";
        private const string m_DeleteSp = "pa_delete_assisted";
        private Database db;

        private static AssistedDao instance;

        private AssistedDao()
        {
            db = DatabaseFactory.CreateDatabase();
        }

        /// <summary>
        /// Unique instance
        /// </summary>
        /// <returns>this unique instance</returns>
        public static AssistedDao GetInstance()
        {
            if (instance == null)
            {
                instance = new AssistedDao();
            }
            return instance;
        }

        /// <summary>
        /// Creates a new assisted
        /// </summary>
        /// <param name="assistedDto">the assisted</param>
        /// <returns>the assisted created</returns>
        public AssistedDto Create(AssistedDto assistedDto)
        {
            try
            {
                //Creamos un comando para ejecutar el procedimiento almacenado
                DbCommand dbCommand = db.GetStoredProcCommand(m_CreateSp);
                
                //Creamos los parámetros del procedimiento almacenado
                db.AddInParameter(dbCommand, "idFamily", DbType.Int32, assistedDto.IdFamily);
                db.AddInParameter(dbCommand, "name", DbType.String, assistedDto.Name);
                db.AddInParameter(dbCommand, "surname", DbType.String, assistedDto.Surname);
                db.AddInParameter(dbCommand, "birthDate", DbType.DateTime, assistedDto.BirthDate);
                if(!String.IsNullOrEmpty(assistedDto.ImagePath))
                    db.AddInParameter(dbCommand, "imagePath", DbType.String, assistedDto.ImagePath);
                else
                    db.AddInParameter(dbCommand, "imagePath", DbType.String, DBNull.Value);
                db.AddInParameter(dbCommand, "identifier", DbType.String, assistedDto.Identifier);
                db.AddInParameter(dbCommand, "phone", DbType.String, assistedDto.Phone);
                //SOS 1
                if (!String.IsNullOrEmpty(assistedDto.Sos1_Phone))
                    db.AddInParameter(dbCommand, "sos1_phone", DbType.String, assistedDto.Sos1_Phone);
                else
                    db.AddInParameter(dbCommand, "sos1_phone", DbType.String, DBNull.Value);
                if (!String.IsNullOrEmpty(assistedDto.Sos1_Name))
                    db.AddInParameter(dbCommand, "sos1_name", DbType.String, assistedDto.Sos1_Name);
                else
                    db.AddInParameter(dbCommand, "sos1_name", DbType.String, DBNull.Value);
                if (assistedDto.Sos1_Recall_Count != 0)
                    db.AddInParameter(dbCommand, "sos1_recall_count", DbType.Int16, assistedDto.Sos1_Recall_Count);
                else
                    db.AddInParameter(dbCommand, "sos1_recall_count", DbType.Int16, DBNull.Value);
                db.AddInParameter(dbCommand, "sos1_enabled", DbType.Boolean, assistedDto.Sos1_Enabled);
                //SOS 2
                if (!String.IsNullOrEmpty(assistedDto.Sos2_Phone))
                    db.AddInParameter(dbCommand, "sos2_phone", DbType.String, assistedDto.Sos2_Phone);
                else
                    db.AddInParameter(dbCommand, "sos2_phone", DbType.String, DBNull.Value);
                if (!String.IsNullOrEmpty(assistedDto.Sos2_Name))
                    db.AddInParameter(dbCommand, "sos2_name", DbType.String, assistedDto.Sos2_Name);
                else
                    db.AddInParameter(dbCommand, "sos2_name", DbType.String, DBNull.Value);
                if (assistedDto.Sos2_Recall_Count != 0)
                    db.AddInParameter(dbCommand, "sos2_recall_count", DbType.Int16, assistedDto.Sos2_Recall_Count);
                else
                    db.AddInParameter(dbCommand, "sos2_recall_count", DbType.Int16, DBNull.Value);
                db.AddInParameter(dbCommand, "sos2_enabled", DbType.Boolean, assistedDto.Sos2_Enabled);
                //SOS 3
                if (!String.IsNullOrEmpty(assistedDto.Sos3_Phone))
                    db.AddInParameter(dbCommand, "sos3_phone", DbType.String, assistedDto.Sos3_Phone);
                else
                    db.AddInParameter(dbCommand, "sos3_phone", DbType.String, DBNull.Value);
                if (!String.IsNullOrEmpty(assistedDto.Sos3_Name))
                    db.AddInParameter(dbCommand, "sos3_name", DbType.String, assistedDto.Sos3_Name);
                else
                    db.AddInParameter(dbCommand, "sos3_name", DbType.String, DBNull.Value);
                if (assistedDto.Sos3_Recall_Count != 0)
                    db.AddInParameter(dbCommand, "sos3_recall_count", DbType.Int16, assistedDto.Sos3_Recall_Count);
                else
                    db.AddInParameter(dbCommand, "sos3_recall_count", DbType.Int16, DBNull.Value);
                db.AddInParameter(dbCommand, "sos3_enabled", DbType.Boolean, assistedDto.Sos3_Enabled);

                db.AddInParameter(dbCommand, "allowMinimize", DbType.Boolean, assistedDto.AllowMinimize);

                int idActive = (int)db.ExecuteScalar(dbCommand);
                assistedDto.IdActive = idActive;
               
                return assistedDto;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    throw new DuplicateInstanceException(assistedDto, "AssistedDao.Create");
                }
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
           

        }

        /// <summary>
        /// Updates an assisted
        /// </summary>
        /// <param name = "assistedDto">the assisted to update</param>
        /// <returns>the assisted updated</returns>
        public AssistedDto Update(AssistedDto assistedDto)
        {
            try
            {
                //Creamos un comando para ejecutar el procedimiento almacenado
                DbCommand dbCommand = db.GetStoredProcCommand(m_UpdateSp);

                //Creamos los parámetros del procedimiento almacenado
                db.AddInParameter(dbCommand, "idActive", DbType.Int32, assistedDto.IdActive);
                db.AddInParameter(dbCommand, "idFamily", DbType.Int32, assistedDto.IdFamily);
                db.AddInParameter(dbCommand, "name", DbType.String, assistedDto.Name);
                db.AddInParameter(dbCommand, "surname", DbType.String, assistedDto.Surname);
                db.AddInParameter(dbCommand, "birthDate", DbType.DateTime, assistedDto.BirthDate);
                if (!String.IsNullOrEmpty(assistedDto.ImagePath))
                    db.AddInParameter(dbCommand, "imagePath", DbType.String, assistedDto.ImagePath);
                else
                    db.AddInParameter(dbCommand, "imagePath", DbType.String, DBNull.Value);
                db.AddInParameter(dbCommand, "identifier", DbType.String, assistedDto.Identifier);
                db.AddInParameter(dbCommand, "phone", DbType.String, assistedDto.Phone);
                //SOS 1
                if (!String.IsNullOrEmpty(assistedDto.Sos1_Phone))
                    db.AddInParameter(dbCommand, "sos1_phone", DbType.String, assistedDto.Sos1_Phone);
                else
                    db.AddInParameter(dbCommand, "sos1_phone", DbType.String, DBNull.Value);
                if (!String.IsNullOrEmpty(assistedDto.Sos1_Name))
                    db.AddInParameter(dbCommand, "sos1_name", DbType.String, assistedDto.Sos1_Name);
                else
                    db.AddInParameter(dbCommand, "sos1_name", DbType.String, DBNull.Value);
                if (assistedDto.Sos1_Recall_Count != 0)
                    db.AddInParameter(dbCommand, "sos1_recall_count", DbType.Int16, assistedDto.Sos1_Recall_Count);
                else
                    db.AddInParameter(dbCommand, "sos1_recall_count", DbType.Int16, DBNull.Value);
                db.AddInParameter(dbCommand, "sos1_enabled", DbType.Boolean, assistedDto.Sos1_Enabled);
                //SOS 2
                if (!String.IsNullOrEmpty(assistedDto.Sos2_Phone))
                    db.AddInParameter(dbCommand, "sos2_phone", DbType.String, assistedDto.Sos2_Phone);
                else
                    db.AddInParameter(dbCommand, "sos2_phone", DbType.String, DBNull.Value);
                if (!String.IsNullOrEmpty(assistedDto.Sos2_Name))
                    db.AddInParameter(dbCommand, "sos2_name", DbType.String, assistedDto.Sos2_Name);
                else
                    db.AddInParameter(dbCommand, "sos2_name", DbType.String, DBNull.Value);
                if (assistedDto.Sos2_Recall_Count != 0)
                    db.AddInParameter(dbCommand, "sos2_recall_count", DbType.Int16, assistedDto.Sos2_Recall_Count);
                else
                    db.AddInParameter(dbCommand, "sos2_recall_count", DbType.Int16, DBNull.Value);
                db.AddInParameter(dbCommand, "sos2_enabled", DbType.Boolean, assistedDto.Sos2_Enabled);
                //SOS 3
                if (!String.IsNullOrEmpty(assistedDto.Sos3_Phone))
                    db.AddInParameter(dbCommand, "sos3_phone", DbType.String, assistedDto.Sos3_Phone);
                else
                    db.AddInParameter(dbCommand, "sos3_phone", DbType.String, DBNull.Value);
                if (!String.IsNullOrEmpty(assistedDto.Sos3_Name))
                    db.AddInParameter(dbCommand, "sos3_name", DbType.String, assistedDto.Sos3_Name);
                else
                    db.AddInParameter(dbCommand, "sos3_name", DbType.String, DBNull.Value);
                if (assistedDto.Sos3_Recall_Count != 0)
                    db.AddInParameter(dbCommand, "sos3_recall_count", DbType.Int16, assistedDto.Sos3_Recall_Count);
                else
                    db.AddInParameter(dbCommand, "sos3_recall_count", DbType.Int16, DBNull.Value);
                db.AddInParameter(dbCommand, "sos3_enabled", DbType.Boolean, assistedDto.Sos3_Enabled);

                db.AddInParameter(dbCommand, "allowMinimize", DbType.Boolean, assistedDto.AllowMinimize);

                if (!String.IsNullOrEmpty(assistedDto.DeviceToken))
                    db.AddInParameter(dbCommand, "deviceToken", DbType.String, assistedDto.DeviceToken);
                else
                    db.AddInParameter(dbCommand, "deviceToken", DbType.String, DBNull.Value);

                int rowsAffected = db.ExecuteNonQuery(dbCommand);

                if (rowsAffected == 0)
                {
                    throw new InstanceNotFoundException(assistedDto.IdActive, "AssistedDto");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("AssistedDto.Update", e);
            }
            return assistedDto;
        }

        /// <summary>
        /// Deletes an assisted
        /// </summary>
        /// <param name="idActive">assisted´s identifier</param>
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
                    throw new InstanceNotFoundException(idActive, "AssistedDto");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("AssistedDao.Delete", e);
            }
        }

        /// <summary>
        /// Searches the assisted with the identifier given
        /// </summary>
        /// <param name="idActive">assisted´s identifier</param>
        /// <returns>the assisted searched</returns>
        public AssistedDto FindByIdActive(int idActive)
        {
            try
            {
                //La consulta
                string sqlQuery = "SELECT idFamily, name, surname, birthDate, imagePath, identifier, phone, "+
                                  "sos1_phone,sos1_name,sos1_recall_count,sos1_enabled,"+
                                  "sos2_phone,sos2_name,sos2_recall_count,sos2_enabled," +
                                  "sos3_phone,sos3_name,sos3_recall_count,sos3_enabled, allowMinimize, deviceToken " +
                                  "FROM Assisted " +
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

                        int idFamily = reader.GetInt32(i++);
                        string name = reader.GetString(i++);
                        string surname = reader.GetString(i++);
                        DateTime birthDate = reader.GetDateTime(i++);
                        string imagePath = "";
                        if(!reader.IsDBNull(i++))
                            imagePath = reader.GetString(i-1);
                        string identifier = reader.GetString(i++);
                        string phone = reader.GetString(i++);
                        //SOS1
                        string sos1phone = "";
                        if (!reader.IsDBNull(i++))
                            sos1phone = reader.GetString(i - 1);
                        string sos1name = "";
                        if (!reader.IsDBNull(i++))
                            sos1name = reader.GetString(i - 1);
                        int sos1recall = 0;
                        if (!reader.IsDBNull(i++))
                            sos1recall = reader.GetInt16(i - 1);
                        bool sos1enabled = false;
                        if (!reader.IsDBNull(i++))
                            sos1enabled = reader.GetBoolean(i - 1);
                        //SOS2
                        string sos2phone = "";
                        if (!reader.IsDBNull(i++))
                            sos2phone = reader.GetString(i - 1);
                        string sos2name = "";
                        if (!reader.IsDBNull(i++))
                            sos2name = reader.GetString(i - 1);
                        int sos2recall = 0;
                        if (!reader.IsDBNull(i++))
                            sos2recall = reader.GetInt16(i - 1);
                        bool sos2enabled = false;
                        if (!reader.IsDBNull(i++))
                            sos2enabled = reader.GetBoolean(i - 1);
                        //SOS3
                        string sos3phone = "";
                        if (!reader.IsDBNull(i++))
                            sos3phone = reader.GetString(i - 1);
                        string sos3name = "";
                        if (!reader.IsDBNull(i++))
                            sos3name = reader.GetString(i - 1);
                        int sos3recall = 0;
                        if (!reader.IsDBNull(i++))
                            sos3recall = reader.GetInt16(i - 1);
                        bool sos3enabled = false;
                        if (!reader.IsDBNull(i++))
                            sos3enabled = reader.GetBoolean(i - 1);
                        bool allowMinimize = reader.GetBoolean(i++);

                        string deviceToken = "";
                        if (!reader.IsDBNull(i++))
                            deviceToken = reader.GetString(i - 1);

                        AssistedDto assistedDto = new 
                            AssistedDto(idActive, idFamily, name, surname, birthDate, imagePath, 
                                        identifier,phone,sos1phone,sos1name,sos1recall,sos1enabled,
                                        sos2phone,sos2name,sos2recall,sos2enabled,
                                        sos3phone,sos3name,sos3recall,sos3enabled,allowMinimize, deviceToken);
                        return assistedDto;
                    }
                    throw new InstanceNotFoundException(idActive, "AssistedDao.FindByIdActive");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("AssistedDao.FindByIdActive", e);
            }
        }

        /// <summary>
        /// Searches the assisted with the identifier given
        /// </summary>
        /// <param name="identifier">assisted´s identifier</param>
        /// <returns>the assisted searched</returns>
        public AssistedDto FindByIdentifier(string identifier)
        {
            try
            {
                //La consulta
                string sqlQuery = "SELECT idActive,idFamily, name, surname, birthDate, imagePath, phone, " +
                                  "sos1_phone,sos1_name,sos1_recall_count,sos1_enabled," +
                                  "sos2_phone,sos2_name,sos2_recall_count,sos2_enabled," +
                                  "sos3_phone,sos3_name,sos3_recall_count,sos3_enabled, allowMinimize, deviceToken " +
                                  "FROM Assisted " +
                                  "WHERE identifier = @identifier";

                //Creamos un comando para ejecutar la consulta sql
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "identifier", DbType.String, identifier);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        int i = 0;

                        int idActive = reader.GetInt32(i++);
                        int idFamily = reader.GetInt32(i++);
                        string name = reader.GetString(i++);
                        string surname = reader.GetString(i++);
                        DateTime birthDate = reader.GetDateTime(i++);
                        string imagePath = "";
                        if (!reader.IsDBNull(i++))
                            imagePath = reader.GetString(i - 1);
                        string phone = reader.GetString(i++);
                        //SOS1
                        string sos1phone = "";
                        if (!reader.IsDBNull(i++))
                            sos1phone = reader.GetString(i - 1);
                        string sos1name = "";
                        if (!reader.IsDBNull(i++))
                            sos1name = reader.GetString(i - 1);
                        int sos1recall = 0;
                        if (!reader.IsDBNull(i++))
                            sos1recall = reader.GetInt16(i - 1);
                        bool sos1enabled = false;
                        if (!reader.IsDBNull(i++))
                            sos1enabled = reader.GetBoolean(i - 1);
                        //SOS2
                        string sos2phone = "";
                        if (!reader.IsDBNull(i++))
                            sos2phone = reader.GetString(i - 1);
                        string sos2name = "";
                        if (!reader.IsDBNull(i++))
                            sos2name = reader.GetString(i - 1);
                        int sos2recall = 0;
                        if (!reader.IsDBNull(i++))
                            sos2recall = reader.GetInt16(i - 1);
                        bool sos2enabled = false;
                        if (!reader.IsDBNull(i++))
                            sos2enabled = reader.GetBoolean(i - 1);
                        //SOS3
                        string sos3phone = "";
                        if (!reader.IsDBNull(i++))
                            sos3phone = reader.GetString(i - 1);
                        string sos3name = "";
                        if (!reader.IsDBNull(i++))
                            sos3name = reader.GetString(i - 1);
                        int sos3recall = 0;
                        if (!reader.IsDBNull(i++))
                            sos3recall = reader.GetInt16(i - 1);
                        bool sos3enabled = false;
                        if(!reader.IsDBNull(i++))
                            sos3enabled = reader.GetBoolean(i - 1);
                        bool allowMinimize = reader.GetBoolean(i++);

                        string deviceToken = "";
                        if (!reader.IsDBNull(i++))
                            deviceToken = reader.GetString(i - 1);

                        AssistedDto assistedDto = new
                            AssistedDto(idActive, idFamily, name, surname, birthDate, imagePath,
                                        identifier, phone, sos1phone, sos1name, sos1recall, sos1enabled,
                                        sos2phone, sos2name, sos2recall, sos2enabled,
                                        sos3phone, sos3name, sos3recall, sos3enabled, allowMinimize, deviceToken);
                        return assistedDto;
                    }
                    throw new InstanceNotFoundException(identifier, "AssistedDao.FindByIdentifier");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("AssistedDao.FindByIdentifier", e);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
       
        /// <summary>
        /// Gets all assisted
        /// </summary>
        /// <returns>an ArrayList with all assisted</returns>
        public ArrayList GetAll()
        {
            ArrayList assisted = new ArrayList();

            try
            {
                //La consulta
                string sqlQuery = "SELECT idActive, idFamily, name, surname, birthDate, imagePath, identifier, phone, " +
                                  "sos1_phone,sos1_name,sos1_recall_count,sos1_enabled," +
                                  "sos2_phone,sos2_name,sos2_recall_count,sos2_enabled," +
                                  "sos3_phone,sos3_name,sos3_recall_count,sos3_enabled, allowMinimize, deviceToken " +
                                  "FROM Assisted";

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
                        int idFamily = reader.GetInt32(i++);
                        string name = reader.GetString(i++);
                        string surname = reader.GetString(i++);
                        DateTime birthDate = reader.GetDateTime(i++);
                        string imagePath = "";
                        if (!reader.IsDBNull(i++))
                            imagePath = reader.GetString(i - 1);
                        string identifier = reader.GetString(i++);
                        string phone = reader.GetString(i++);
                        //SOS1
                        string sos1phone = "";
                        if (!reader.IsDBNull(i++))
                            sos1phone = reader.GetString(i - 1);
                        string sos1name = "";
                        if (!reader.IsDBNull(i++))
                            sos1name = reader.GetString(i - 1);
                        int sos1recall = 0;
                        if (!reader.IsDBNull(i++))
                            sos1recall = reader.GetInt16(i - 1);
                        bool sos1enabled = false;
                        if (!reader.IsDBNull(i++))
                            sos1enabled = reader.GetBoolean(i - 1);
                        //SOS2
                        string sos2phone = "";
                        if (!reader.IsDBNull(i++))
                            sos2phone = reader.GetString(i - 1);
                        string sos2name = "";
                        if (!reader.IsDBNull(i++))
                            sos2name = reader.GetString(i - 1);
                        int sos2recall = 0;
                        if (!reader.IsDBNull(i++))
                            sos2recall = reader.GetInt16(i - 1);
                        bool sos2enabled = false;
                        if (!reader.IsDBNull(i++))
                            sos2enabled = reader.GetBoolean(i - 1);
                        //SOS3
                        string sos3phone = "";
                        if (!reader.IsDBNull(i++))
                            sos3phone = reader.GetString(i - 1);
                        string sos3name = "";
                        if (!reader.IsDBNull(i++))
                            sos3name = reader.GetString(i - 1);
                        int sos3recall = 0;
                        if (!reader.IsDBNull(i++))
                            sos3recall = reader.GetInt16(i - 1);
                        bool sos3enabled = false;
                        if (!reader.IsDBNull(i++))
                            sos3enabled = reader.GetBoolean(i - 1);
                        bool allowMinimize = reader.GetBoolean(i++);
                        string deviceToken = "";
                        if (!reader.IsDBNull(i++))
                            deviceToken = reader.GetString(i - 1);

                        AssistedDto assistedDto = new
                            AssistedDto(idActive, idFamily, name, surname, birthDate, imagePath,
                                        identifier, phone, sos1phone, sos1name, sos1recall, sos1enabled,
                                        sos2phone, sos2name, sos2recall, sos2enabled,
                                        sos3phone, sos3name, sos3recall, sos3enabled, allowMinimize, deviceToken);
                        assisted.Add(assistedDto);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("AssistedDao.GetAll", e);
            }
            return assisted;
        }

        /// <summary>
        /// Gets all assisted
        /// </summary>
        /// <returns>an ArrayList with all assisted</returns>
        public ArrayList GetAllAssistedByFamily(int idFamily)
        {
            ArrayList assisted = new ArrayList();

            try
            {
                //La consulta
                string sqlQuery = "SELECT idActive, name, surname, birthDate, imagePath, identifier, phone, " +
                                  "sos1_phone,sos1_name,sos1_recall_count,sos1_enabled," +
                                  "sos2_phone,sos2_name,sos2_recall_count,sos2_enabled," +
                                  "sos3_phone,sos3_name,sos3_recall_count,sos3_enabled, allowMinimize, deviceToken " +            
                                  "FROM Assisted " +
                                  "WHERE idFamily=@idFamily";

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
                        int idActive = reader.GetInt32(i++);
                        string name = reader.GetString(i++);
                        string surname = reader.GetString(i++);
                        DateTime birthDate = reader.GetDateTime(i++);
                        string imagePath = "";
                        if (!reader.IsDBNull(i++))
                            imagePath = reader.GetString(i - 1);
                        string identifier = reader.GetString(i++);
                        string phone = reader.GetString(i++);
                        //SOS1
                        string sos1phone = "";
                        if (!reader.IsDBNull(i++))
                            sos1phone = reader.GetString(i - 1);
                        string sos1name = "";
                        if (!reader.IsDBNull(i++))
                            sos1name = reader.GetString(i - 1);
                        int sos1recall = 0;
                        if (!reader.IsDBNull(i++))
                            sos1recall = reader.GetInt16(i - 1);
                        bool sos1enabled = false;
                        if (!reader.IsDBNull(i++))
                            sos1enabled = reader.GetBoolean(i - 1);
                        //SOS2
                        string sos2phone = "";
                        if (!reader.IsDBNull(i++))
                            sos2phone = reader.GetString(i - 1);
                        string sos2name = "";
                        if (!reader.IsDBNull(i++))
                            sos2name = reader.GetString(i - 1);
                        int sos2recall = 0;
                        if (!reader.IsDBNull(i++))
                            sos2recall = reader.GetInt16(i - 1);
                        bool sos2enabled = false;
                        if (!reader.IsDBNull(i++))
                            sos2enabled = reader.GetBoolean(i - 1);
                        //SOS3
                        string sos3phone = "";
                        if (!reader.IsDBNull(i++))
                            sos3phone = reader.GetString(i - 1);
                        string sos3name = "";
                        if (!reader.IsDBNull(i++))
                            sos3name = reader.GetString(i - 1);
                        int sos3recall = 0;
                        if (!reader.IsDBNull(i++))
                            sos3recall = reader.GetInt16(i - 1);
                        bool sos3enabled = false;
                        if (!reader.IsDBNull(i++))
                            sos3enabled = reader.GetBoolean(i - 1);
                        bool allowMinimize = reader.GetBoolean(i++);
                        string deviceToken = "";
                        if (!reader.IsDBNull(i++))
                            deviceToken = reader.GetString(i - 1);

                        AssistedDto assistedDto = new
                            AssistedDto(idActive, idFamily, name, surname, birthDate, imagePath,
                                        identifier, phone, sos1phone, sos1name, sos1recall, sos1enabled,
                                        sos2phone, sos2name, sos2recall, sos2enabled,
                                        sos3phone, sos3name, sos3recall, sos3enabled, allowMinimize, deviceToken);
                        assisted.Add(assistedDto);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("AssistedDao.GetAllAssistedByFamily", e);
            }
            return assisted;
        }
    }
}
