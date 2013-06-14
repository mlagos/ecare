using System;

namespace Nextgal.ECare.Model.Position.Dto
{
    [Serializable]
    public class PositionDto
    {
        private DateTime m_Date;

        public DateTime Date
        {
            get { return m_Date; }
            set { m_Date = value; }
        }

        private int m_IdActive;

        public int IdActive
        {
            get { return m_IdActive; }
            set { m_IdActive = value; }
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

        private bool m_IsCellID;

        public bool IsCellID
        {
            get { return m_IsCellID; }
            set { m_IsCellID = value; }
        }

        public PositionDto(DateTime date, int idActive, double latitude, double longitude,bool isCellId)
        {
            m_Date = date;
            m_IdActive = idActive;
            m_Latitude = latitude;
            m_Longitude = longitude;
            m_IsCellID = isCellId;
        }

        public override string ToString()
        {
            string cadena = "";

            cadena += "date = " + m_Date + "\n";
            cadena += "idActive = " + m_IdActive + "\n";
            cadena += "latitude = " + m_Latitude + "\n";
            cadena += "longitude = " + m_Longitude + "\n";
            cadena += "isCellId = " + m_IsCellID;

            return cadena;
        }


    }
}