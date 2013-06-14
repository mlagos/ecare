using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nextgal.ECare.Model.Position.Dto
{
    [Serializable]
    public class MonitoringReportDto
    {
        private int m_IdActive;

        public int IdActive
        {
            get { return m_IdActive; }
            set { m_IdActive = value; }
        }

        private string m_ActiveName;

        public string ActiveName
        {
            get { return m_ActiveName; }
            set { m_ActiveName = value; }
        }

        private DateTime? m_LastPosDate;

        public DateTime? LastPosDate
        {
            get { return m_LastPosDate; }
            set { m_LastPosDate = value; }
        }

        private string m_Status;

        public string Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        private string m_Location;

        public string Location
        {
            get { return m_Location; }
            set { m_Location = value; }
        }

        public MonitoringReportDto(int idActive, string name, DateTime lastPosDate, string status, string location)
        {
            m_IdActive = idActive;
            m_ActiveName = name;
            m_LastPosDate = lastPosDate;
            m_Status = status;
            m_Location = location;
        }
    }
}
