using System;
using System.Web;
using System.Web.Security;
using Nextgal.ECare.Model.User.Dto;
using Nextgal.ECare.Model.User.Facade;
using System.Globalization;

namespace Nextgal.ECare.Controller
{
    public class SessionManager
    {
        public static readonly string IDUSER_SESSION_ATTRIBUTE = "idUser";
        public static readonly string IDFAMILY_SESSION_ATTRIBUTE = "idFamily";
        public static readonly string FIRST_NAME_SESSION_ATTRIBUTE = "name";
        public static readonly string USER_FACADE_SESSION_ATTRIBUTE = "userFacade";
        public static readonly string LOGIN_NAME_SESSION_ATTRIBUTE = "login";
        public static readonly string ENCRYPTED_PASSWORD_SESSION_ATTRIBUTE = "encryptedPassword";
        public static readonly string UICULTURE = "uiculture";

        private SessionManager() {}

        /// <summary>
        /// Login method. Authenticates an user in the current context.
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="login">User login</param>
        /// <param name="clearPassword">Password in clear text</param>
        /// <param name="isEncryptedPassword">Indicates if password is encrypted</param>
        /// <returns>data session of authenticated user</returns>
        public static LoginResultDto Login(HttpContext context, string login, string clearPassword, bool isEncryptedPassword)
        {
            /* Try to login, and if successful, update session with the necessary 
             * objects for an authenticated user. */
            LoginResultDto loginResult = DoLogin(context, login, clearPassword, isEncryptedPassword);
            return loginResult;
        }

        

        /// <summary>
        /// Tries to log in with the corresponding method of  
        /// <c>UserFacade</c>, and if successful, inserts in the
        /// session the necessary objects for an authenticated user.
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="login">User login </param>
        /// <param name="password">User Password</param>
        /// <param name="passwordIsEncrypted">Password is either encrypted or in clear text</param>
        /// <returns>data session of authenticated user</returns>
        private static LoginResultDto DoLogin(HttpContext context, string login, string password, bool passwordIsEncrypted)
        {
            // Check "loginName" and "clearPassword".
            UserFacade userFacade = GetUserFacade(context);
            LoginResultDto loginResultDto = userFacade.Login(login, password, passwordIsEncrypted);
            
            
            // Create the authetication ticket.
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, login, DateTime.Now, DateTime.Now.AddMinutes(120), false, loginResultDto.Name);

            // Now encrypt the ticket.
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            // Create a cookie and add the encrypted ticket to the cookie as data.
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            // Add the cookie to the outgoing cookies collection. 
            context.Response.Cookies.Add(authCookie);

            // Insert necessary objects in the session.
            UpdateSessionForAuthenticatedUser(context, loginResultDto.IdUser, loginResultDto.Login, loginResultDto.Password, loginResultDto.Name, loginResultDto.idFamily);
            return loginResultDto;
        }

        /// <summary>
        /// Updates the session values for an previously authenticated user
        /// </summary>
        /// <param name="context"></param>
        /// <param name="idUser"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        /// <param name="idFamily"></param>
        private static void UpdateSessionForAuthenticatedUser(HttpContext context, int idUser, string login, string password, string name, int idFamily)
        {
            /* Insert objects in session. */
            if (context.Session[IDUSER_SESSION_ATTRIBUTE] == null)
                context.Session.Add(IDUSER_SESSION_ATTRIBUTE, idUser);
            else
                context.Session[IDUSER_SESSION_ATTRIBUTE] = idUser;

            if (context.Session[LOGIN_NAME_SESSION_ATTRIBUTE] == null)
                context.Session.Add(LOGIN_NAME_SESSION_ATTRIBUTE, login);
            else
                context.Session[LOGIN_NAME_SESSION_ATTRIBUTE] = login;

            if (context.Session[ENCRYPTED_PASSWORD_SESSION_ATTRIBUTE] == null)
                context.Session.Add(ENCRYPTED_PASSWORD_SESSION_ATTRIBUTE, password);
            else
                context.Session[ENCRYPTED_PASSWORD_SESSION_ATTRIBUTE] = password;

            if (context.Session[FIRST_NAME_SESSION_ATTRIBUTE] == null)
                context.Session.Add(FIRST_NAME_SESSION_ATTRIBUTE, name);
            else
                context.Session[FIRST_NAME_SESSION_ATTRIBUTE] = name;

            if (context.Session[IDFAMILY_SESSION_ATTRIBUTE] == null)
                context.Session.Add(IDFAMILY_SESSION_ATTRIBUTE, idFamily);
            else
                context.Session[IDFAMILY_SESSION_ATTRIBUTE] = idFamily;

            //if (context.Session[UICULTURE] == null)
            //    context.Session.Add(UICULTURE, UiCult);
            //else
            //    context.Session[UICULTURE] = UiCult;

        }
        
        /// <summary>
        /// Gets an UserFacadeDelegate instance. First it tries to recover it
        /// from the session. If it is not possible a new facade is created.
        /// </summary>
        /// <param name="context">The context</param>
        /// <returns>An UserFacade instance</returns>
        public static UserFacade GetUserFacade(HttpContext context)
        {
            UserFacade userFacade = (UserFacade)context.Session[USER_FACADE_SESSION_ATTRIBUTE];

            if (userFacade == null)
            {
                userFacade = new UserFacade();
            }
            context.Session.Add(USER_FACADE_SESSION_ATTRIBUTE, userFacade);
            return userFacade;
        }

        public static int GetIdUser(HttpContext context)
        {
            if (context.Session[IDUSER_SESSION_ATTRIBUTE] != null)
            {
                return (int)context.Session[IDUSER_SESSION_ATTRIBUTE];
            }
            else
            {
                context.Response.Redirect("~/Default.aspx");
                return 0;
            }
        }

        /// <summary>
        /// Gets family´s id
        /// </summary>
        /// <param name="context">Context</param>
        /// <returns>family´s id</returns>
        public static int GetIdFamily(HttpContext context)
        {
            if (context.Session[IDFAMILY_SESSION_ATTRIBUTE] != null)
            {
                return (int)context.Session[IDFAMILY_SESSION_ATTRIBUTE];
            }
            context.Response.Redirect("~/Default.aspx");
            return 0;
        }

        /// <summary>
        /// Destroyes the session.
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        public static void Logout(HttpContext context)
        {
            /* Invalidate session. */
            context.Session.Abandon();

            /* Invalidate Authentication Ticket */
            FormsAuthentication.SignOut();
        }
    }
}
