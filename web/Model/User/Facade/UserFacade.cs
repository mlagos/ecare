using System;
using System.Collections;
using Nextgal.ECare.Common.Util;
using Nextgal.ECare.Model.User.Dao;
using Nextgal.ECare.Model.User.Dto;
using Nextgal.ECare.Common.Exceptions;

namespace Nextgal.ECare.Model.User.Facade
{
    public class UserFacade
    {
        #region User

        /// <summary>
        /// Tries user login 
        /// </summary>
        /// <param name="login">User name</param>
        /// <param name="password">Password</param>
        /// <param name="passwordIsEncrypted">true if encrypted</param>
        /// <returns>User login result, with user data</returns>
        public LoginResultDto Login(string login, string password, bool passwordIsEncrypted)
        {
            UserDao dao = UserDao.GetInstance();
            UserDto user = dao.FindByLogin(login);

            String encryptedPassword;

            if (passwordIsEncrypted)
            {
                encryptedPassword = password;
            }
            else
            {
                //encrypts password
                encryptedPassword = EncryptionUtils.Encrypt(password);
            }

            if (!user.Password.Equals(encryptedPassword))
            {
                throw new IncorrectPasswordException(login);
            }

            return new LoginResultDto(user.IdUser, login, password, user.IdFamily, user.Name);
        }


        public UserDto FindUser(int idUser)
        {
            UserDao dao = UserDao.GetInstance();
            return dao.FindByID(idUser);
        }

        public UserDto UpdateUser(UserDto dto)
        {
            UserDao dao = UserDao.GetInstance();
            return dao.Update(dto);
        }
        #endregion

        #region Zone

        /// <summary>
        /// Creates a new zone
        /// </summary>
        /// <param name="zoneDto">the zone</param>
        /// <returns>the zone created</returns>
        public ZoneDto CreateZone(ZoneDto zoneDto)
        {
            try
            {
                ZoneDao zoneDao = ZoneDao.GetInstance();
                return zoneDao.Create(zoneDto);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Updates a zone
        /// </summary>
        /// <param name = "zoneDto">the zone to update</param>
        /// <returns>the zone updated</returns>
        public ZoneDto UpdateZone(ZoneDto zoneDto)
        {
            try
            {
                ZoneDao zoneDao = ZoneDao.GetInstance();
                return zoneDao.Update(zoneDto);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Deletes zones
        /// </summary>
        /// <param name="idZone">zone´s identifier</param>
        public void DeleteZone(int idZone)
        {
            try
            {
                ZoneDao zoneDao = ZoneDao.GetInstance();
                zoneDao.Delete(idZone);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Searches the zone with the identifier given
        /// </summary>
        /// <param name="idZone">zone´s identifier</param>
        /// <returns>the zone searched</returns>
        public ZoneDto FindZoneById(int idZone)
        {
            try
            {
                ZoneDao zoneDao = ZoneDao.GetInstance();
                return zoneDao.FindByIdentifier(idZone);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Gets all zones
        /// </summary>
        /// <returns>an ArrayList with all zones</returns>
        public ArrayList GetAllZones()
        {
            try
            {
                ZoneDao zoneDao = ZoneDao.GetInstance();
                return zoneDao.GetAll();
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Gets all zones by family identifier
        /// </summary>
        /// <returns>an ArrayList with all zones</returns>
        public ArrayList GetAllZoneByFamily(int idFamily)
        {
            try
            {
                ZoneDao zoneDao = ZoneDao.GetInstance();
                return zoneDao.GetAllByIdFamily(idFamily);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        #endregion

        private static UserFacade instance;

        public static UserFacade GetInstance()
        {
            if (instance == null)
            {
                instance = new UserFacade();
            }
            return instance;
        }
    }
}
