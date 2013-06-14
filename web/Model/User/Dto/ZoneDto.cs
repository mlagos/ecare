using System;

namespace Nextgal.ECare.Model.User.Dto
{
    public class ZoneDto : IComparable
    {
        private int m_IdZone;

        public int IdZone
        {
            get { return m_IdZone; }
            set { m_IdZone = value; }
        }

        private string m_Name;

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        private string m_Position;

        public string Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }

        private int m_IdFamily;

        public int IdFamily
        {
            get { return m_IdFamily; }
            set { m_IdFamily = value; }
        }

        private DateTime m_RegisterDate;

        public DateTime RegisterDate
        {
            get { return m_RegisterDate; }
            set { m_RegisterDate = value; }
        }

        public ZoneDto(int idZone, string name, string position, int idFamily, DateTime registerDate)
        {
            m_IdZone = idZone;
            m_Name = name;
            m_Position = position;
            m_IdFamily = idFamily;
            m_RegisterDate = registerDate;
        }

        public override string ToString()
        {
            string cadena = "";
            cadena += "IdZone = " + m_IdZone + "\n";
            cadena += "Name = " + m_Name + "\n";
            cadena += "Position = " + m_Position + "\n";
            cadena += "IdFamily = " + m_IdFamily + "\n";
            cadena += "RegisterDate = " + m_RegisterDate + "\n";
            
            return cadena;
        }

        public int CompareTo(object obj)
        {
            if (obj is ZoneDto)
            {
                ZoneDto temp = (ZoneDto)obj;

                return m_Name.CompareTo(temp.m_Name);
            }

            throw new ArgumentException("object is not an ZoneDto");
        }
    }
}