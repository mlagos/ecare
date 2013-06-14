using System;

namespace Nextgal.ECare.Model.Message.Dto
{
    [Serializable]
    public class ReplyDto
    {

        private int m_IdReply;

        public int IdReply
        {
            get { return m_IdReply; }
            set { m_IdReply = value; }
        }

        private int m_IdMessage;

        public int IdMessage
        {
            get { return m_IdMessage; }
            set { m_IdMessage = value; }
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

        public ReplyDto(int m_IdReply, int m_IdMessage, DateTime m_Date, string m_Text)
        {
            this.m_IdMessage = m_IdMessage;
            this.m_IdReply = m_IdReply;
            this.m_Date = m_Date;
            this.m_Text = m_Text;
        }

        public ReplyDto()
        {}

        public override string ToString()
        {
            string cadena = "";


            cadena += "idReply = " + m_IdReply + "\n";
            cadena += "idMessage = " + m_IdMessage + "\n";
            cadena += "date = " + m_Date + "\n";
            cadena += "text = " + m_Text + "\n";

            return cadena;
        }
    }
}
