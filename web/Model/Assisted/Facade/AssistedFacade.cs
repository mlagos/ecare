using System.Collections;
using Nextgal.ECare.Common.Exceptions;
using Nextgal.ECare.Model.Assisted.Dao;
using Nextgal.ECare.Model.Assisted.Dto;

namespace Nextgal.ECare.Model.Assisted.Facade
{
    public class AssistedFacade
    {
        #region Assisted

        /// <summary>
        /// Creates a new assisted
        /// </summary>
        /// <param name="assistedDto">the assisted</param>
        /// <returns>the assisted created</returns>
        public AssistedDto CreateAssisted(AssistedDto assistedDto)
        {
            try
            {
                AssistedDao assistedDao = AssistedDao.GetInstance();
                return assistedDao.Create(assistedDto);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Updates an assisted
        /// </summary>
        /// <param name = "assistedDto">the assisted to update</param>
        /// <returns>the assisted updated</returns>
        public AssistedDto UpdateAssisted(AssistedDto assistedDto)
        {
            try
            {
                AssistedDao assistedDao = AssistedDao.GetInstance();
                return assistedDao.Update(assistedDto);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Deletes an assisted
        /// </summary>
        /// <param name="idActive">assisted´s identifier</param>
        public void DeleteAssisted(int idActive)
        {
            try
            {
                AssistedDao assistedDao = AssistedDao.GetInstance();
                assistedDao.Delete(idActive);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Searches the assisted with the identifier given
        /// </summary>
        /// <param name="idActive">assisted´s identifier</param>
        /// <returns>the assisted searched</returns>
        public AssistedDto FindAssistedByIdActive(int idActive)
        {
            try
            {
                AssistedDao assistedDao = AssistedDao.GetInstance();
                return assistedDao.FindByIdActive(idActive);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Searches the assisted with the identifier given
        /// </summary>
        /// <param name="identifier">assisted´s identifier</param>
        /// <returns>the assisted searched</returns>
        public AssistedDto FindAssistedByIdentifier(string identifier)
        {
            try
            {
                AssistedDao assistedDao = AssistedDao.GetInstance();
                return assistedDao.FindByIdentifier(identifier);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Gets all assisted
        /// </summary>
        /// <returns>an ArrayList with all assisted</returns>
        public ArrayList GetAllAssisted()
        {
            try
            {
                AssistedDao assistedDao = AssistedDao.GetInstance();
                return assistedDao.GetAll();
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Gets all assisted by family
        /// </summary>
        /// <returns>an ArrayList with all assisted</returns>
        public ArrayList GetAllAssistedByFamily(int idFamily)
        {
            try
            {
                AssistedDao assistedDao = AssistedDao.GetInstance();
                return assistedDao.GetAllAssistedByFamily(idFamily);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }
       
        #endregion

        #region ActiveFacility

        /// <summary>
        /// Creates a new active-facility relation
        /// </summary>
        /// <param name="activeFacilityDto">the active-facility relation</param>
        /// <returns>the active-facility relation created</returns>
        public ActiveFacilityDto CreateActiveFacility(ActiveFacilityDto activeFacilityDto)
        {
            try
            {
                ActiveFacilityDao activeFacilityDao = ActiveFacilityDao.GetInstance();
                return activeFacilityDao.Create(activeFacilityDto);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Deletes an active-facility relation
        /// </summary>
        /// <param name="idActive">active´s identifier</param>
        /// <param name="idFacility">facility´s identifier</param>
        public void DeleteActiveFacility(int idActive, int idFacility)
        {
            try
            {
                ActiveFacilityDao activeFacilityDao = ActiveFacilityDao.GetInstance();
                activeFacilityDao.Delete(idActive, idFacility);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Gets all active-facility relations
        /// </summary>
        /// <returns>an ArrayList with all relations</returns>
        public ArrayList GetAllActiveFacility()
        {
            try
            {
                ActiveFacilityDao activeFacilityDao = ActiveFacilityDao.GetInstance();
                return activeFacilityDao.GetAll();
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Finds facility´s identifier by name
        /// </summary>
        /// <param name="name">facility name</param>
        /// <returns>facility identifier</returns>
        public int FindIdFacilityByName(string name)
        {
            try
            {
                ActiveFacilityDao dao = ActiveFacilityDao.GetInstance();
                return dao.FindIdFacilityByName(name);
            }
            catch (ModelException me)
            {

                throw me;
            }

        }

        /// <summary>
        /// Searches the facilities of one active
        /// </summary>
        /// <param name="identifier">actives´s identifier</param>
        /// <returns>facilities searched</returns>
        public ArrayList FindActiveFacilitiesByIdentifier(string identifier)
        {
            try
            {
                ActiveFacilityDao activeFacilityDao = ActiveFacilityDao.GetInstance();
                return activeFacilityDao.FindActiveFacilitiesByIdentifier(identifier);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        #endregion

        #region EmergencyNumber

        /// <summary>
        /// Creates a new emergency number
        /// </summary>
        /// <param name="emergencyNumberDto">the emergency number</param>
        /// <returns>the emergency number created</returns>
        public EmergencyNumberDto CreateEmergencyNumber(EmergencyNumberDto emergencyNumberDto)
        {
            try
            {
                EmergencyNumberDao emergencyNumberDao = EmergencyNumberDao.GetInstance();
                return emergencyNumberDao.Create(emergencyNumberDto);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Updates an emergency number
        /// </summary>
        /// <param name = "emergencyNumberDto">the emergency number to update</param>
        /// <returns>the emergency number updated</returns>
        public EmergencyNumberDto UpdateEmergencyNumber(EmergencyNumberDto emergencyNumberDto)
        {
            try
            {
                EmergencyNumberDao emergencyNumberDao = EmergencyNumberDao.GetInstance();
                return emergencyNumberDao.Update(emergencyNumberDto);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Deletes an emergency number
        /// </summary>
        /// <param name="idActive">emergency number´s identifier</param>
        public void DeleteEmergencyNumber(int idActive)
        {
            try
            {
                EmergencyNumberDao emergencyNumberDao = EmergencyNumberDao.GetInstance();
                emergencyNumberDao.Delete(idActive);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Searches the emergency number with the identifier given
        /// </summary>
        /// <param name="idActive">emergency number´s identifier</param>
        /// <returns>the emergency number searched</returns>
        public EmergencyNumberDto FindEmergencyNumberById(int idActive)
        {
            try
            {
                EmergencyNumberDao emergencyNumberDao = EmergencyNumberDao.GetInstance();
                return emergencyNumberDao.FindByIdActive(idActive);
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        /// <summary>
        /// Gets all emergency number
        /// </summary>
        /// <returns>an ArrayList with all emergency number</returns>
        public ArrayList GetAllEmergencyNumber()
        {
            try
            {
                AssistedDao assistedDao = AssistedDao.GetInstance();
                return assistedDao.GetAll();
            }
            catch (ModelException me)
            {
                throw me;
            }
        }

        #endregion

        private static AssistedFacade instance;

        public static AssistedFacade GetInstance()
        {
            if (instance == null)
            {
                instance = new AssistedFacade();
            }
            return instance;
        }

    }
}