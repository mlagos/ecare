using System.Collections.Specialized;
using Nextgal.ECare.Model.Position.Dto;

namespace Nextgal.ECare.Gateway
{
    public abstract class AbstractDevice
    {
        /// <summary>
        /// Gets an imei of PT35
        /// </summary>
        /// <returns>imei of the device</returns>
        public abstract string GetIdentifier();
        /// <summary>
        /// Obtains device model
        /// </summary>
        /// <returns>device model string</returns>
        public abstract string GetDeviceModel();
        /// <summary>
        /// stores data received in the data base
        /// </summary>
        /// <param name="msg">message</param>
        public abstract void StoreData(string msg);
        /// <summary>
        /// Indicates if a message has to be stored 
        /// </summary>
        /// <param name="msg">message</param>
        /// <returns>true to store, false otherwise</returns>
        public abstract bool IsMessageToStore(string msg);
        /// <summary>
        /// stores data received in the data base
        /// </summary>
        /// <param name="data">message</param>
        public abstract void StoreData(OrderedDictionary data);
        /// <summary>
        /// Stores message data into the database
        /// </summary>
        /// <param name="msg">message receive</param>
        /// <param name="data">data bytes</param>
        public abstract void CheckMessage(string msg, byte[] data);
        /// <summary>
        /// Filters invalid positions
        /// </summary>
        /// <param name="data">position data</param>
        /// <returns>true if valid position, false otherwise</returns>
        public abstract bool FilterPosition(OrderedDictionary data);
        /// <summary>
        /// Checks if active has alarms predefined 
        /// to be managed and handles them
        /// </summary>
        public abstract void CheckActiveAlarms(PositionDto positionDto);
        /// <summary>
        /// stores additional information into a database
        /// </summary>
        /// <param name="data">active data</param>
        public abstract void StoreAdditionalInfo(OrderedDictionary data);
        /// <summary>
        /// stores position data into a database
        /// </summary>
        /// <param name="data">position data</param>
        public abstract PositionDto StorePosition(OrderedDictionary data);
        /// <summary>
        /// Converts active data without compression
        /// </summary>
        /// <param name="msg">active data</param>
        /// <returns>NameValueCollection with data converted and their values</returns>
        public abstract OrderedDictionary ParseDataWithoutCompression(string msg);
        /// <summary>
        /// Calcutates speed between two coordenates
        /// </summary>
        /// <param name="point1">first coordenate</param>
        /// <param name="point2">second coordenate</param>
        /// <returns>speed calculated</returns>
        public abstract double CalculateSpeedBetweenTwoPoints(PositionDto point1, PositionDto point2);
    }
}