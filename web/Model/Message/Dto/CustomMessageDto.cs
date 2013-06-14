using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nextgal.ECare.Model.Message.Dto
{
    [Serializable]
    public class CustomMessageDto
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

        private DateTime m_Date;

        public DateTime Date
        {
            get { return m_Date; }
            set { m_Date = value; }
        }

        private DateTime? m_DateReceived;

        public DateTime? DateReceived
        {
            get { return m_DateReceived; }
            set { m_DateReceived = value; }
        }

        private string m_Text;

        public string Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }

        private string m_Phone;

        public string Phone
        {
            get { return m_Phone; }
            set { m_Phone = value; }
        }

        private string m_SendBy;

        public string SendBy
        {
            get { return m_SendBy; }
            set { m_SendBy = value; }
        }

        private string m_AssistedName;

        public string AssistedName
        {
            get { return m_AssistedName; }
            set { m_AssistedName = value; }
        }

        private List<ReplyDto> m_Replies;

        public List<ReplyDto> Replies
        {
            get { return m_Replies; }
            set { m_Replies = value; }
        }

        private string m_Confirmed;

        public string Confirmed
        {
            get { return m_Confirmed; }
            set { m_Confirmed = value; }
        }

        public CustomMessageDto(int m_IdMessage, int m_IdUser, int m_IdActive, DateTime m_Date, DateTime? m_DateReceived, string m_Text, string m_Phone)
        {
            this.m_IdMessage = m_IdMessage;
            this.m_IdUser = m_IdUser;
            this.m_IdActive = m_IdActive;
            this.m_Date = m_Date;
            this.m_DateReceived = m_DateReceived;
            this.m_Text = m_Text;
            this.m_Phone = m_Phone;
        }

        public CustomMessageDto(int m_IdMessage, int m_IdUser, int m_IdActive, DateTime m_Date, 
            DateTime? m_DateReceived, string m_Text, string m_Phone,string sendby, string assname,
            List<ReplyDto> replies,string confirmed)
        {
            this.m_IdMessage = m_IdMessage;
            this.m_IdUser = m_IdUser;
            this.m_IdActive = m_IdActive;
            this.m_Date = m_Date;
            this.m_DateReceived = m_DateReceived;
            this.m_Text = m_Text;
            this.m_Phone = m_Phone;
            this.m_SendBy = sendby;
            this.m_AssistedName = assname;
            this.m_Replies = replies;
            this.Confirmed = confirmed;
        }

        public CustomMessageDto()
        {}

        public override string ToString()
        {
            string cadena = "";

            cadena += "idMessage = " + m_IdMessage + "\n";
            cadena += "idUser = " + m_IdUser + "\n";
            cadena += "idActive = " + m_IdActive + "\n";
            cadena += "date = " + m_Date + "\n";
            cadena += "dateReceived = " + m_DateReceived + "\n";
            cadena += "text = " + m_Text + "\n";
            cadena += "phone = " + m_Phone + "\n";

            return cadena;
        }
    }
    
}
