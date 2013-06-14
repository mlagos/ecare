using System;

namespace Nextgal.ECare.Model.Assisted.Dto
{
    [Serializable]
    public class ActiveFacilityDto
    {
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
        public ActiveFacilityDto(int idActive, int idFacility)
        {
            m_IdActive = idActive;
            m_IdFacility = idFacility;
        }

        public override string ToString()
        {
            string cadena = "";

            cadena += "idActive = " + m_IdActive + "\n";
            cadena += "idFacility = " + m_IdFacility + "\n";

            return cadena;
        }
    }
}