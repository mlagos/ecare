using System;
using System.Linq;
using System.Web.UI;
using Nextgal.ECare.Common.Util;
using Nextgal.ECare.Controller;
using Nextgal.ECare.Model.User.Dto;
using Nextgal.ECare.Model.User.Facade;

namespace Nextgal.PocketLocator.Web.UserControls
{
    public partial class HeaderMenu : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            passTB.Attributes.Add("value", passTB.Text);
            if (!IsPostBack)
            {
                UserDto dto = UserFacade.GetInstance().FindUser(SessionManager.GetIdUser(Context));
                loginTB.Text = dto.Login;
                nameTB.Text = dto.Name;
                surnameTB.Text = dto.Surname;
                passTB.Text = EncryptionUtils.Decrypt(dto.Password);
                phoneTB.Text = dto.Telephone;
                emailTB.Text = dto.Email;
                passTB.Attributes.Add("value", passTB.Text);
                Label2.CssClass = "userData";
                Label3.CssClass = "userData";
                Label4.CssClass = "userData";
                Label5.CssClass = "userData";
                Label6.CssClass = "userData";
                Label7.CssClass = "userData";
                msg.Visible = false;
            }
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            try
            {
                SessionManager.Logout(Context);
                Response.Redirect("~/");
            }
            catch
            {
            }
        }

         protected void guardar_Click(object sender, EventArgs e)
         {
             try
             {
                 string password = "";
                 if (passTB.Text.Trim().Count() == 0)
                 {
                     password = UserFacade.GetInstance().FindUser(SessionManager.GetIdUser(Context)).Password;
                 }
                 else
                 {
                     password = EncryptionUtils.Encrypt(passTB.Text.Trim());
                 }
                 UserDto dto = new UserDto(SessionManager.GetIdUser(Context), loginTB.Text.Trim(),
                                           password, SessionManager.GetIdFamily(Context), nameTB.Text.Trim(),
                                           surnameTB.Text.Trim(), phoneTB.Text.Trim(), emailTB.Text.Trim());
                 UserFacade.GetInstance().UpdateUser(dto);
                 msg.CssClass = "labelOk";
                 msg.Visible = true;
                 msg.Text = Resources.Resources.saveDataOk;
                 
             }catch(Exception ex)
             {
                 msg.CssClass = "labelError";
                 msg.Visible = true;
                 msg.Text = Resources.Resources.errorData;
                 
             }
         }
    }
}