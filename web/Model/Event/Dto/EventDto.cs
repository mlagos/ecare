using System;

namespace Nextgal.ECare.Model.Event.Dto
{
    [Serializable]
    public class EventDto
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

        public EventDto(int idEvent, int idActive, DateTime date, string text, DateTime sentDate)
        {
            m_IdEvent = idEvent;
            m_IdActive = idActive;
            m_Date = date;
            m_Text = text;
            m_SentDate = sentDate;
        }

        public EventDto()
        {}

        public override string ToString()
        {
            string cadena = "";

            cadena += "idEvent = " + m_IdEvent + "\n";
            cadena += "idActive = " + m_IdActive + "\n";
            cadena += "date = " + m_Date + "\n";
            cadena += "text  = " + m_Text + "\n";
            cadena += "sentDate  = " + m_SentDate + "\n";

            return cadena;
        }
    }
}
