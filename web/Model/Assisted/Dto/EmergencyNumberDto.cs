using System;

namespace Nextgal.ECare.Model.Assisted.Dto
{
    [Serializable]
    public class EmergencyNumberDto : IComparable
    {
        private int m_IdActive;

        public int IdActive
        {
            get { return m_IdActive; }
            set { m_IdActive = value; }
        }

        private int m_CallOrder;

        public int CallOrder
        {
            get { return m_CallOrder; }
            set { m_CallOrder = value; }
        }

        private bool m_Enable;

        public bool Enable
        {
            get { return m_Enable; }
            set { m_Enable = value; }
        }

        private string m_Phone;

        public string Phone
        {
            get { return m_Phone; }
            set { m_Phone = value; }
        }

        public EmergencyNumberDto(int idActive, int callOrder, bool enable, string phone)
        {
            m_IdActive = idActive;
            m_CallOrder = callOrder;
            m_Enable = enable;
            m_Phone = phone;
        }

        public override string ToString()
        {
            string cadena = "";

            cadena += "idActive = " + m_IdActive + "\n";
            cadena += "callOrder = " + m_CallOrder + "\n";
            cadena += "enable = " + m_Enable + "\n";
            cadena += "phone = " + m_Phone + "\n";

            return cadena;
        }

        public int CompareTo(object obj)
        {
            if (obj is EmergencyNumberDto)
            {
                EmergencyNumberDto temp = (EmergencyNumberDto)obj;

                return m_Phone.CompareTo(temp.m_Phone);
            }

            throw new ArgumentException("object is not an EmergencyNumberDto");
        }
    }
}
