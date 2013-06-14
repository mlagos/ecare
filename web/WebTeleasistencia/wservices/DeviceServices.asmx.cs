using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.MobileControls;
using Nextgal.ECare.Common.Exceptions;
using Nextgal.ECare.Gateway;
using Nextgal.ECare.Model.Assisted.Dto;
using Nextgal.ECare.Model.Assisted.Facade;
using Nextgal.ECare.Model.Event.Dto;
using Nextgal.ECare.Model.Event.Facade;
using Nextgal.ECare.Model.Message.Dto;
using Nextgal.ECare.Model.Message.Facade;
using Nextgal.ECare.Model.Notification.Dto;
using Nextgal.ECare.Model.Notification.Facade;
using Nextgal.ECare.Model.Position.Dto;
using Nextgal.ECare.Model.Position.Facade;
using Nextgal.PocketLocator.Model.Position.Dto;
using Nextgal.ECare.Common.Util;



namespace Nextgal.ECare.WebTeleasistencia.wservices
{
    /// <summary>
    /// Servivio Web que proporciona acceso a todas las funcionalidades de la plataforma para los dispositivos móviles.
    /// </summary>
    [WebService(Namespace = "http://familyecare.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DeviceServices : System.Web.Services.WebService
    {
        private const int FACILITY_BATTERY_LEVEL = 1;
        private const int FACILITY_DIRECTION = 3;
        private const int FACILITY_SPEED = 5;
        private const int FACILITY_HDOP = 6;

        /// <summary>
        /// Devuelve la versión del API de los Servicios Web
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetVersion()
        {
            return "1.0";
        }

        /// <summary>
        /// Registra el Token del iPhone para poder recibir mensajes Push.
        /// </summary>
        /// <param name="deviceId">ID del Dispositivo</param>
        /// <param name="deviceToken">El DeviceToken del iPhone/iPad</param>
        /// <returns></returns>
        [WebMethod]
        public int RegisterDeviceToken(string deviceId, string deviceToken)
        {
            try
            {
                AssistedDto assistedDto = AssistedFacade.GetInstance().FindAssistedByIdentifier(deviceId);
                if (assistedDto.DeviceToken != deviceToken)
                {
                    assistedDto.DeviceToken = deviceToken;
                    AssistedFacade.GetInstance().UpdateAssisted(assistedDto);
                }
            }
            catch (Exception)
            {
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// Devuelve los números de emergencia para un activo
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetEmergencyNumbers(string identifier)
        {
            string secuencia = "";
            try
            {
                AssistedDto assistedDto = AssistedFacade.GetInstance().FindAssistedByIdentifier(identifier);
                if (assistedDto.Sos1_Enabled)
                {
                    secuencia += assistedDto.Sos1_Name + "##" + assistedDto.Sos1_Phone + "##" +
                                 assistedDto.Sos1_Recall_Count + "##";
                }
                if (assistedDto.Sos2_Enabled)
                {
                    secuencia += assistedDto.Sos2_Name + "##" + assistedDto.Sos2_Phone + "##" +
                                 assistedDto.Sos2_Recall_Count + "##";
                }
                if (assistedDto.Sos3_Enabled)
                {
                    secuencia += assistedDto.Sos3_Name + "##" + assistedDto.Sos3_Phone + "##" +
                                 assistedDto.Sos3_Recall_Count + "##";
                }
            }
            catch (Exception)
            {
            }
            return secuencia;
        }
        
        /// <summary>
        /// Devuelve los mensajes para un activo
        /// </summary>
        /// <param name="identifier">Identificador del activo</param>
        /// <returns></returns>
        [WebMethod]
        public List<CustomMessageDto> GetMessages(string identifier)
        {
            try
            {
                AssistedDto assistedDto = AssistedFacade.GetInstance().FindAssistedByIdentifier(identifier);
                return MessageFacade.GetInstance().GetAllMessageByIdActive(assistedDto.IdActive);
            }
            catch (Exception)
            {
                
                return new List<CustomMessageDto>();
            }
        }

        /// <summary>
        /// Crea una respuesta a un mensaje
        /// </summary>
        /// <param name="replyDto"></param>
        /// <returns></returns>
        [WebMethod]
        public ReplyDto ReplyMessage(ReplyDto replyDto)
        {
            return MessageFacade.GetInstance().CreateReply(replyDto);
        }

        /// <summary>
        /// Actualiza el mensaje
        /// </summary>
        /// <param name="messageDto"></param>
        /// <returns></returns>
        [WebMethod]
        public MessageDto UpdateMessage(MessageDto messageDto)
        {
            return MessageFacade.GetInstance().Update(messageDto);
        }

        /// <summary>
        /// Devuelve los eventos para el activo
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        [WebMethod]
        public List<EventDto> GetEventsByActive(string identifier)
        {
            try
            {
                AssistedDto assistedDto = AssistedFacade.GetInstance().FindAssistedByIdentifier(identifier);
                return EventFacade.GetInstance().GetEventsByIdActive(assistedDto.IdActive);
            }
            catch (Exception)
            {
                return new List<EventDto>();
            }
        }

        /// <summary>
        /// Obtiene los datos de un evento
        /// </summary>
        /// <param name="idEvent"></param>
        /// <returns></returns>
        [WebMethod]
        public EventDto GetEvent(int idEvent)
        {
            return EventFacade.GetInstance().FindAssistedByIdEvent(idEvent);
        }

        /// <summary>
        /// Guarda una posición GPS del dispositivo.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public int AddLocation(string identifier, double lat, double lon, DateTime date, bool isCellId, int bateryLevel, string CID, double direction, double mileage, double speed, double HDOP, int CRC)
        {
            //Por seguridad calculamos un sumatorio del imei, y si ese parámetro no es correcto ignoramos la llamada al servicio web.
            //int imeicrc = 0;
            //for (int i = 0; i < imei.Length; i++)
            //    imeicrc += Int32.Parse(imei[i].ToString());

            //if (CRC==imeicrc)
            if (CRC == (date.Year + date.Month) * date.Day )
            {
                return AddLocation(identifier, lat, lon, date, isCellId, bateryLevel, CID, direction, mileage, speed, HDOP);
            }
            else
            {
                return 4;
            }
        }

        private int AddLocation(string imei, double lat, double lon, DateTime date, bool isCellId, int bateryLevel, string CID, double direction, double mileage, double speed, double HDOP)
        {
            try
            {
                //Buscamos el activo por su IMEI
                AssistedDto assistedDto = AssistedFacade.GetInstance().FindAssistedByIdentifier(imei);
                try
                {
                    var _date = Utils.ConvertToGMTTime(date);
                    var positionDto = new PositionDto(_date, assistedDto.IdActive, lat, lon, isCellId);
                    PositionFacade.GetInstance().CreatePosition(positionDto);
                    PositionFacade.GetInstance().CreateAdditionalInformation(new AdditionalInformationDto(_date, assistedDto.IdActive, FACILITY_BATTERY_LEVEL, bateryLevel.ToString()));
                    PositionFacade.GetInstance().CreateAdditionalInformation(new AdditionalInformationDto(_date, assistedDto.IdActive, FACILITY_DIRECTION, direction.ToString()));
                    PositionFacade.GetInstance().CreateAdditionalInformation(new AdditionalInformationDto(_date, assistedDto.IdActive, FACILITY_SPEED, speed.ToString()));
                    PositionFacade.GetInstance().CreateAdditionalInformation(new AdditionalInformationDto(_date, assistedDto.IdActive, FACILITY_HDOP, HDOP.ToString()));
                    CheckActiveAlarms(positionDto);
                }
                catch (Exception ex)
                {
                    return 3;
                }
            }
            catch (InstanceNotFoundException)
            {
                return 2;
            }
            catch (Exception)
            {
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// Guarda una posición GPS.
        /// </summary>
        /// <param name="encryptedLocation">Todos los parámetros de la posición encriptados</param>
        /// <returns></returns>
        [WebMethod]
        public int AddEncryptedLocation(string encryptedLocation)
        {
            try
            {
                //Desencriptamos la cadena de parámetros
                string location = EncryptionUtils.Decrypt(encryptedLocation);
                //Separamos los parámetros
                string[] parametros = location.Split(';');
                string imei = parametros[0];
                double lat = Double.Parse(parametros[1]);
                double lon = Double.Parse(parametros[2]);
                DateTime date = DateTime.Parse(parametros[3]);
                bool isCellId = (parametros[4]=="1");
                int bateryLevel = Int32.Parse(parametros[5]);
                string CID = parametros[6];
                double direction = Double.Parse(parametros[7]);
                double mileage = Double.Parse(parametros[8]);
                double speed = Double.Parse(parametros[9]);
                double HDOP = Double.Parse(parametros[10]);
                //Insertamos la información
                return AddLocation(imei, lat, lon, date, isCellId, bateryLevel, CID, direction, mileage, speed, HDOP);
            }
            catch (Exception)
            {
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// Checks if active has alarms predefined 
        /// to be managed and handles them
        /// </summary>
        private void CheckActiveAlarms(PositionDto positionDto)
        {
            try
            {
                ArrayList notifications = NotificationFacade.GetInstance().GetAllByIdActive(positionDto.IdActive);
                foreach (NotificationDto notificationDto in notifications)
                {
                    try
                    {
                        AlarmFactory.GetInstance(notificationDto).CheckAlarm(positionDto);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Confirma un evento del usuario.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public int Ckeckin(string deviceId, double lat, double lon, DateTime date, bool isCellId, int bateryLevel, string CID, double direction, double mileage, double speed, double HDOP, int CRC, int checkinId)
        {
            return 0;
        }

    }


}
