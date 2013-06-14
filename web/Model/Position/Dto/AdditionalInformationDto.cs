using System;

namespace Nextgal.PocketLocator.Model.Position.Dto
{
    public class AdditionalInformationDto
    {
        private DateTime m_Date;

        public DateTime Date
        {
            get { return m_Date; }
            set { m_Date = value; }
        }

        private int m_IdActive;

        public int IdActive
        {
            get { return m_IdActive; }
            set { m_IdActive = value; }
        }

        private int m_IdFacility;

        public int IdFacility
        {
            get { return m_IdFacility; }
            set { m_IdFacility = value; }
        }

        private string m_Value;

        public string Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }


        public AdditionalInformationDto(DateTime date, int idActive, int idFacility, string value)
        {
            m_Date = date;
            m_IdActive = idActive;
            m_IdFacility = idFacility;
            m_Value = value;
        }

    }
}
