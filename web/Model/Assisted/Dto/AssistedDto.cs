using System;

namespace Nextgal.ECare.Model.Assisted.Dto
{
    [Serializable]
    public class AssistedDto:IComparable
    {
        private int m_IdActive;

        public int IdActive
        {
            get { return m_IdActive; }
            set { m_IdActive = value; }
        }

        private int m_IdFamily;

        public int IdFamily
        {
            get { return m_IdFamily; }
            set { m_IdFamily = value; }
        }

        private string m_Name;

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        private string m_Surname;

        public string Surname
        {
            get { return m_Surname; }
            set { m_Surname = value; }
        }

        private DateTime m_BirthDate;

        public DateTime BirthDate
        {
            get { return m_BirthDate; }
            set { m_BirthDate = value; }
        }

        private string m_ImagePath;

        public string ImagePath
        {
            get { return m_ImagePath; }
            set { m_ImagePath = value; }
        }

        private string m_Identifier;

        public string Identifier
        {
            get { return m_Identifier; }
            set { m_Identifier = value; }
        }

        private string m_Phone;

        public string Phone
        {
            get { return m_Phone; }
            set { m_Phone = value; }
        }

        private string m_Sos1_Phone;

        public string Sos1_Phone
        {
            get { return m_Sos1_Phone; }
            set { m_Sos1_Phone = value; }
        }

        private string m_Sos1_Name;

        public string Sos1_Name
        {
            get { return m_Sos1_Name; }
            set { m_Sos1_Name = value; }
        }

        private int m_Sos1_Recall_Count;

        public int Sos1_Recall_Count
        {
            get { return m_Sos1_Recall_Count; }
            set { m_Sos1_Recall_Count = value; }
        }

        private bool m_Sos1_Enabled;

        public bool Sos1_Enabled
        {
            get { return m_Sos1_Enabled; }
            set { m_Sos1_Enabled = value; }
        }

        private string m_Sos2_Phone;

        public string Sos2_Phone
        {
            get { return m_Sos2_Phone; }
            set { m_Sos2_Phone = value; }
        }

        private string m_Sos2_Name;

        public string Sos2_Name
        {
            get { return m_Sos2_Name; }
            set { m_Sos2_Name = value; }
        }

        private int m_Sos2_Recall_Count;

        public int Sos2_Recall_Count
        {
            get { return m_Sos2_Recall_Count; }
            set { m_Sos2_Recall_Count = value; }
        }

        private bool m_Sos2_Enabled;

        public bool Sos2_Enabled
        {
            get { return m_Sos2_Enabled; }
            set { m_Sos2_Enabled = value; }
        }

        private string m_Sos3_Phone;

        public string Sos3_Phone
        {
            get { return m_Sos3_Phone; }
            set { m_Sos3_Phone = value; }
        }

        private string m_Sos3_Name;

        public string Sos3_Name
        {
            get { return m_Sos3_Name; }
            set { m_Sos3_Name = value; }
        }

        private int m_Sos3_Recall_Count;

        public int Sos3_Recall_Count
        {
            get { return m_Sos3_Recall_Count; }
            set { m_Sos3_Recall_Count = value; }
        }

        private bool m_Sos3_Enabled;

        public bool Sos3_Enabled
        {
            get { return m_Sos3_Enabled; }
            set { m_Sos3_Enabled = value; }
        }

        private bool m_AllowMinimize;

        public bool AllowMinimize
        {
            get { return m_AllowMinimize; }
            set { m_AllowMinimize = value; }
        }

        private string m_DeviceToken;

        public string DeviceToken
        {
            get { return m_DeviceToken; }
            set { m_DeviceToken = value; }
        }

        public AssistedDto(int idActive, int idFamily, string name, string surname, DateTime birthDate, 
            string imagePath, string identifier, string phone, string sos1phone, string sos1name,
            int sos1recall,bool sos1enable,string sos2phone, string sos2name,
            int sos2recall,bool sos2enable,string sos3phone, string sos3name,
            int sos3recall, bool sos3enable, bool allowMinimize, string deviceToken)
        {
            m_IdActive = idActive;
            m_IdFamily = idFamily;
            m_Name = name;
            m_Surname = surname;
            m_BirthDate = birthDate;
            m_ImagePath = imagePath;
            m_Identifier = identifier;
            m_Phone = phone;
            m_Sos1_Phone = sos1phone;
            m_Sos1_Name = sos1name;
            m_Sos1_Recall_Count = sos1recall;
            m_Sos1_Enabled = sos1enable;
            m_Sos2_Phone = sos2phone;
            m_Sos2_Name = sos2name;
            m_Sos2_Recall_Count = sos2recall;
            m_Sos2_Enabled = sos2enable;
            m_Sos3_Phone = sos3phone;
            m_Sos3_Name = sos3name;
            m_Sos3_Recall_Count = sos3recall;
            m_Sos3_Enabled = sos3enable;
            m_AllowMinimize = allowMinimize;
            m_DeviceToken = deviceToken;
        }

        public AssistedDto()
        {}

        public int CompareTo(object obj)
        {
            if (obj is AssistedDto)
            {
                AssistedDto temp = (AssistedDto)obj;

                string name = m_Name + " " + m_Surname;
                return name.CompareTo(temp.m_Name+" "+temp.m_Surname);
            }

            throw new ArgumentException("object is not an AssistedDto");
        }
    }
}