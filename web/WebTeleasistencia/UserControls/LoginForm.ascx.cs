using System;
using Nextgal.ECare.Common.Exceptions;
using Nextgal.ECare.Controller;

namespace Nextgal.ECare.WebTeleasistencia.UserControls
{
    public partial class LoginForm : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBoxLogin.Focus();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                SessionManager.Login(Context, TextBoxLogin.Text.Trim(),TextBoxPassword.Text.Trim(), false);
                Response.Redirect("~/State.aspx");
            }
            catch (InstanceNotFoundException)
            {
                LabelError.Text = (string) GetGlobalResourceObject("Resources", "lblLoginError");              
            }
            catch (IncorrectPasswordException)
            {
                LabelError.Text = (string)GetGlobalResourceObject("Resources", "lblPasswordError");
            }
            catch (Exception exc)
            {
                LabelError.Text = (string)GetGlobalResourceObject("Resources", "lblUnknownError") + exc.ToString();
            }
            
        }
    }
}
