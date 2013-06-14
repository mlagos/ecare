using System;

namespace Nextgal.ECare.Model.User.Dto
{
    public class UserDto:IComparable
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

        private string m_Telephone;

        public string Telephone
        {
            get { return m_Telephone; }
            set { m_Telephone = value; }
        }

        private string m_Email;

        public string Email
        {
            get { return m_Email; }
            set { m_Email = value; }
        }



        public UserDto(){}

        public UserDto(int idUser, string login, string password, int idFamily, string name, string surname, string telephone, string email)
        {
            m_IdUser = idUser;
            m_Login = login;
            m_Password = password;
            m_IdFamily = idFamily;
            m_Name = name;
            m_Surname = surname;
            m_Telephone = telephone;
            m_Email = email;
            
        }

        public int CompareTo(object obj)
        {
            if (obj is UserDto)
            {
                UserDto temp = (UserDto)obj;

                return m_Login.CompareTo(temp.m_Login);
            }
            throw new ArgumentException("object is not an UserDto");
        }
    }
}