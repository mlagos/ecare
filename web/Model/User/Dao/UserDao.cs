using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Nextgal.ECare.Model.User.Dto;
using Nextgal.ECare.Common.Exceptions;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Nextgal.ECare.Model.User.Dao
{
    public class UserDao
    {
        private Database db;
        private const string m_UpdateSp = "pa_update_user";
        private static UserDao instance;

        private UserDao()
        {
            db = DatabaseFactory.CreateDatabase();
        }

        /// <summary>
        /// Unique instance
        /// </summary>
        /// <returns>The unique instance</returns>
        public static UserDao GetInstance()
        {
            if (instance == null)
            {
                instance = new UserDao();
            }
            return instance;
        }

        /// <summary>
        /// Find a user´s complete profile
        /// </summary>
        /// <param name="login">The user´s login name</param>
        public UserDto FindByLogin(string login)
        {
            try
            {
                string sqlQuery = "SELECT idUser,idFamily, pass, name, surname, phone, email " +
                                  "FROM Users " +
                                  "WHERE login = @login";

                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "login", DbType.String, login);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        int i = 0;
                        int idUser = reader.GetInt32(i++);
                        int idFamily = reader.GetInt32(i++);
                        string password = reader.GetString(i++);
                        string name = reader.GetString(i++);
                        string surname = reader.GetString(i++);
                        string telephone = reader.GetString(i++);
                        string email = reader.GetString(i);
                        
                        UserDto userDto = new UserDto(idUser, login, password, idFamily, name, surname, telephone, email);
                        return userDto;
                    }
                    throw new InstanceNotFoundException(login, "UserDao.FindByLogin");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("UserDao.FindByLogin", e);
            }
        }

        public UserDto FindByID(int idUser)
        {
            try
            {
                string sqlQuery = "SELECT idFamily, login, pass, name, surname, phone, email " +
                                  "FROM Users " +
                                  "WHERE idUser = @idUser";

                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                db.AddInParameter(cmd, "idUser", DbType.String, idUser);

                //The using statement ensures that the DbDataReader object is disposed, which 
                //closes the DbDataReader object.
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        int i = 0;
                        int idFamily = reader.GetInt32(i++);
                        string login = reader.GetString(i++);
                        string password = reader.GetString(i++);
                        string name = reader.GetString(i++);
                        string surname = reader.GetString(i++);
                        string telephone = reader.GetString(i++);
                        string email = reader.GetString(i);

                        UserDto userDto = new UserDto(idUser, login, password, idFamily, name, surname, telephone, email);
                        return userDto;
                    }
                    throw new InstanceNotFoundException(idUser, "UserDao.FindById");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("UserDao.FindById", e);
            }
        }

        public UserDto Update(UserDto userDto)
        {
            try
            {
                //Creamos un comando para ejecutar el procedimiento almacenado
                DbCommand dbCommand = db.GetStoredProcCommand(m_UpdateSp);

                //Creamos los parámetros del procedimiento almacenado
                db.AddInParameter(dbCommand, "idUser", DbType.Int32, userDto.IdUser);
                db.AddInParameter(dbCommand, "idFamily", DbType.Int32, userDto.IdFamily);
                db.AddInParameter(dbCommand, "login", DbType.String, userDto.Login);
                db.AddInParameter(dbCommand, "pass", DbType.String, userDto.Password);
                db.AddInParameter(dbCommand, "name", DbType.String, userDto.Name);
                db.AddInParameter(dbCommand, "surname", DbType.String, userDto.Surname);
                db.AddInParameter(dbCommand, "phone", DbType.String, userDto.Telephone);
                db.AddInParameter(dbCommand, "email", DbType.String, userDto.Email);
               

                int rowsAffected = db.ExecuteNonQuery(dbCommand);

                if (rowsAffected == 0)
                {
                    throw new InstanceNotFoundException(userDto.IdUser, "UserDto");
                }
            }
            catch (SqlException e)
            {
                throw new InternalErrorException("UserDto.Update", e);
            }
            return userDto;
        }
    }
}