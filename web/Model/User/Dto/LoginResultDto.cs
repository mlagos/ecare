namespace Nextgal.ECare.Model.User.Dto
{
    public class LoginResultDto
    {
        private int m_IdUser;

        public int IdUser
        {
            get { return m_IdUser; }
            set { m_IdUser = value; }
        }

        private string m_Login;

        public string Login
        {
            get { return m_Login; }
            set { m_Login = value; }
        }

        private string m_Password;

        public string Password
        {
            get { return m_Password; }
            set { m_Password = value; }
        }

        private int m_idFamily;

        public int idFamily
        {
            get { return m_idFamily; }
            set { m_idFamily = value; }
        }

        private string m_Name;

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public LoginResultDto(int idUser, string login, string password,  int idFamily, string name)
        {
            m_IdUser = idUser;
            m_Login = login;
            m_Password = password;
            m_idFamily = idFamily;
            m_Name = name;
        }
    }
}