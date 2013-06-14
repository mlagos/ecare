namespace Nextgal.ECare.Model.Notification.Dto
{
    public class NotificationDto
    {
        private int m_IdNotification;

        public int IdNotification
        {
            get { return m_IdNotification; }
            set { m_IdNotification = value; }
        }

        private int m_IdActive;

        public int IdActive
        {
            get { return m_IdActive; }
            set { m_IdActive = value; }
        }

        private int? m_IdZone;

        public int? IdZone
        {
            get { return m_IdZone; }
            set { m_IdZone = value; }
        }

        private int m_IdUser;

        public int IdUser
        {
            get { return m_IdUser; }
            set { m_IdUser = value; }
        }

        private int m_IdAlarmType;

        public int IdAlarmType
        {
            get { return m_IdAlarmType; }
            set { m_IdAlarmType = value; }
        }

        private bool m_InZone;

        public bool InZone
        {
            get { return m_InZone; }
            set { m_InZone = value; }
        }

        private bool m_OutZone;

        public bool OutZone
        {
            get { return m_OutZone; }
            set { m_OutZone = value; }
        }

        private bool m_Sms;

        public bool Sms
        {
            get { return m_Sms; }
            set { m_Sms = value; }
        }

        private bool m_Email;

        public bool Email
        {
            get { return m_Email; }
            set { m_Email = value; }
        }

        private string m_ZoneName;

        public string ZoneName
        {
            get { return m_ZoneName; }
            set { m_ZoneName = value; }
        }

        private string m_AlarmType;

        public string AlarmType
        {
            get { return m_AlarmType; }
            set { m_AlarmType = value; }
        }

        public NotificationDto(int idNotification, int idActive,int? idZone, int idAlarmType, int idUser, bool inZone, bool outZone, bool sms, bool email)
        {
            m_IdNotification = idNotification;
            m_IdActive = idActive;
            m_IdZone = idZone;
            m_IdAlarmType = idAlarmType;
            m_IdUser = idUser;
            m_InZone = inZone;
            m_OutZone = outZone;
            m_Sms = sms;
            m_Email = email;
        }

        public NotificationDto(int idNotification, int idActive, int? idZone, int idUser, bool inZone, bool outZone, bool sms, bool email, string zoneName, string alarmType)
        {
            m_IdNotification = idNotification;
            m_IdActive = idActive;
            m_IdZone = idZone;
            m_IdUser = idUser;
            m_InZone = inZone;
            m_OutZone = outZone;
            m_Sms = sms;
            m_Email = email;
            m_ZoneName = zoneName;
            m_AlarmType = alarmType;
        }

        public override string ToString()
        {
            string cadena = "";
            cadena += "IdNotification = " + m_IdNotification + "\n";
            cadena += "IdActive = " + m_IdActive + "\n";
            cadena += "IdZone = " + m_IdZone + "\n";
            cadena += "IdAlarmType = " + m_IdAlarmType + "\n";
            cadena += "IdUser = " + m_IdUser + "\n";
            cadena += "InZone  = " + m_InZone + "\n";
            cadena += "OutZone  = " + m_OutZone + "\n";
            cadena += "sms  = " + m_Sms + "\n";
            cadena += "email  = " + m_Email + "\n";

            return cadena;
        }
    }
}