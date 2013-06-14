using System;

namespace Nextgal.ECare.Model.Event.Dto
{
    [Serializable]
    public class CustomEventDto
    {
        private int m_IdEvent;

        public int IdEvent
        {
            get { return m_IdEvent; }
            set { m_IdEvent = value; }
        }

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

        private string m_Text;

        public string Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }

        private DateTime m_SentDate;

        public DateTime SentDate
        {
            get { return m_SentDate; }
            set { m_SentDate = value; }
        }

        private string m_Phone;

        public string Phone
        {
            get { return m_Phone; }
            set { m_Phone = value; }
        }

        public CustomEventDto(int idEvent, int idActive, DateTime date, string text, DateTime sentDate, string phone)
        {
            m_IdEvent = idEvent;
            m_IdActive = idActive;
            m_Date = date;
            m_Text = text;
            m_SentDate = sentDate;
            m_Phone = phone;
        }

        public override string ToString()
        {
            string cadena = "";

            cadena += "idEvent = " + m_IdEvent + "\n";
            cadena += "idActive = " + m_IdActive + "\n";
            cadena += "date = " + m_Date + "\n";
            cadena += "text  = " + m_Text + "\n";
            cadena += "sentDate  = " + m_SentDate + "\n";
            cadena += "phone  = " + m_Phone + "\n";

            return cadena;
        }
    }
}
