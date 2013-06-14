using System;

namespace Nextgal.ECare.Model.Position.Dto
{
    [Serializable]
    public class MonitoringRoutesRowDto
    {
        private int m_IdActive;

        public int IdActive
        {
            get { return m_IdActive; }
            set { m_IdActive = value; }
        }

        private DateTime m_Date;

        public DateTime Date
        {
            get { return m_Date; }
            set { m_Date = value; }
        }

        private string m_StartTime;

        public string StartTime
        {
            get { return m_StartTime; }
            set { m_StartTime = value; }
        }

        private string m_EndTime;

        public string EndTime
        {
            get { return m_EndTime; }
            set { m_EndTime = value; }
        }

        private string m_DiffTime;

        public string DiffTime
        {
            get { return m_DiffTime; }
            set { m_DiffTime = value; }
        }

        private string m_Status;

        public string Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        private string m_StatusPath;

        public string StatusPath
        {
            get { return m_StatusPath; }
            set { m_StatusPath = value; }
        }

        private string m_Mileage;

        public string Mileage
        {
            get { return m_Mileage; }
            set { m_Mileage = value; }
        }

        private string m_Location;

        public string Location
        {
            get { return m_Location; }
            set { m_Location = value; }
        }

        private double m_Latitude;

        public double Latitude
        {
            get { return m_Latitude; }
            set { m_Latitude = value; }
        }

        private double m_Longitude;

        public double Longitude
        {
            get { return m_Longitude; }
            set { m_Longitude = value; }
        }

        public MonitoringRoutesRowDto(int idActive, DateTime date, string startTime, string endTime, string diffTime, string status, string statusPath, string mileage, string location, double latitude, double longitude)
        {
            m_IdActive = idActive;
            m_Date = date;
            m_StartTime = startTime;
            m_EndTime = endTime;
            m_DiffTime = diffTime;
            m_Status = status;
            m_StatusPath = statusPath;
            m_Mileage = mileage;
            m_Location = location;
            m_Latitude = latitude;
            m_Longitude = longitude;
        }

        public override string ToString()
        {
            string cadena = "";

            cadena += "idActive = " + m_IdActive + "\n";
            cadena += "date = " + m_Date + "\n";
            cadena += "startTime = " + m_StartTime + "\n";
            cadena += "endTime = " + m_EndTime + "\n";
            cadena += "diffTime = " + m_DiffTime + "\n";
            cadena += "status = " + m_Status + "\n";
            cadena += "statusPath = " + m_StatusPath + "\n";
            cadena += "mileage = " + m_Mileage + "\n";
            cadena += "location = " + m_Location + "\n";
            cadena += "latitude = " + m_Latitude + "\n";
            cadena += "longitude = " + m_Longitude + "\n";

            return cadena;
        }
    }
}