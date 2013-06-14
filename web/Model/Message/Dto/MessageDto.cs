using System;

namespace Nextgal.ECare.Model.Message.Dto
{
    [Serializable]
    public class MessageDto
    {
        private int m_IdMessage;

        public int IdMessage
        {
            get { return m_IdMessage; }
            set { m_IdMessage = value; }
        }

        private int m_IdUser;

        public int IdUser
        {
            get { return m_IdUser; }
            set { m_IdUser = value; }
        }

        private int m_IdActive;

        public int IdActive
        {
            get { return m_IdActive; }
            set { m_IdActive = value; }
        }

        private DateTime m_DateSent;

        public DateTime DateSent
        {
            get { return m_DateSent; }
            set { m_DateSent = value; }
        }

        private DateTime m_DateReceipt;

        public DateTime DateReceipt
        {
            get { return m_DateReceipt; }
            set { m_DateReceipt = value; }
        }

        private string m_msgText;

        public string MsgText
        {
            get { return m_msgText; }
            set { m_msgText = value; }
        }


        public MessageDto(int m_IdMessage, int m_IdUser, int m_IdActive, DateTime m_DateSent, DateTime m_DateReceipt, string msgText)
        {
            this.m_IdMessage = m_IdMessage;
            this.m_IdUser = m_IdUser;
            this.m_IdActive = m_IdActive;
            this.m_DateSent = m_DateSent;
            this.m_DateReceipt = m_DateReceipt;
            this.m_msgText = msgText;
        }

        public MessageDto()
        {}

        public override string ToString()
        {
            string cadena = "";

            cadena += "idMessage = " + m_IdMessage + "\n";
            cadena += "idUser = " + m_IdUser + "\n";
            cadena += "idActive = " + m_IdActive + "\n";
            cadena += "dateSent = " + m_DateSent + "\n";
            cadena += "dateReceipt = " + m_DateReceipt + "\n";
            cadena += "msgText = " + m_msgText + "\n";


            return cadena;
        }
    }
}
